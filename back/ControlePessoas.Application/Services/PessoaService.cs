﻿using ControlePessoas.Application.Mapeamentos;
using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Entities;
using ControlePessoas.Domain.Enums;
using ControlePessoas.Domain.Exceptions;
using ControlePessoas.Domain.Interfaces.Persistence;
using ControlePessoas.Domain.Interfaces.Services;
using ControlePessoas.Domain.Models;

namespace ControlePessoas.Application.Services;
public class PessoaService : IPessoaService
{
    private readonly IUnitOfWork _unitOfWork;

    public PessoaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PessoaGetDTO> AddAsync(PessoaCreateDTO dto)
    {
        Pessoa pessoa = dto.DTOParaPessoa();

        await _unitOfWork.PessoaRepository.AddAsync(pessoa);
        await _unitOfWork.CommitAsync();

        return pessoa.PessoaParaDTO();
    }

    public async Task<IEnumerable<PessoaGetAllDTO>> GetAllAsync()
    {
        IEnumerable<Pessoa> pessoas = await _unitOfWork.PessoaRepository.GetAllAsync();
        return pessoas.PessoasParaDTO();
    }

    public ResultadoPaginacao<PessoaGetAllDTO> GetAllPaginacaoFiltro(FiltroPaginacao filtroPaginacao)
    {
        var pessoasQuery = _unitOfWork.PessoaRepository.GetAll();
        var pessoasFiltro = AplicarFiltroPessoas(filtroPaginacao, pessoasQuery);
        List<Pessoa> pessoas = [.. AplicarPaginacaoPessoas(filtroPaginacao, pessoasFiltro)];

        return new ResultadoPaginacao<PessoaGetAllDTO>
            (
                pessoas.PessoasParaDTO(),
                pessoasFiltro.Count(),
                filtroPaginacao.Pagina,
                filtroPaginacao.ItensPorPagina
            );
    }

    public async Task<PessoaGetDTO> GetByIdAsync(Guid id)
    {
        Pessoa pessoa = await GetPessoaByIdAsync(id);
        return pessoa.PessoaParaDTO();
    }

    public async Task<PessoaGetDTO> UpdateAsync(PessoaUpdateDTO dto)
    {
        Pessoa pessoa = await GetPessoaByIdAsync(dto.Id);

        pessoa.AtualizarPessoaPorDTO(dto);

        _unitOfWork.PessoaRepository.Update(pessoa);
        await _unitOfWork.CommitAsync();

        return pessoa.PessoaParaDTO();
    }

    public async Task DeleteAsync(Guid id)
    {
        Pessoa pessoa = await GetPessoaByIdAsync(id);
        pessoa.MarcarComoExcluida();

        _unitOfWork.PessoaRepository.Update(pessoa);
        await _unitOfWork.CommitAsync();
    }

    private async Task<Pessoa> GetPessoaByIdAsync(Guid id)
    {
        return await _unitOfWork.PessoaRepository.GetByIdAsync(id) ??
            throw new NaoEncontradoException();
    }

    private IQueryable<Pessoa> AplicarFiltroPessoas(FiltroPaginacao filtro, IQueryable<Pessoa> pessoas)
    {
        return filtro.FiltroPessoas switch
        {
            FiltroPessoasEnum.TODOS => pessoas,
            FiltroPessoasEnum.IDOSOS => pessoas.Where(p => p.Idoso),
            FiltroPessoasEnum.NAO_IDOSOS => pessoas.Where(p => !p.Idoso),
            _ => throw new FiltroInvalidoException(nameof(filtro.FiltroPessoas), filtro.FiltroPessoas),
        };
    }

    private IEnumerable<Pessoa> AplicarPaginacaoPessoas(FiltroPaginacao filtro, IQueryable<Pessoa> pessoas) => 
        pessoas.Skip((filtro.Pagina - 1) * filtro.ItensPorPagina).Take(filtro.ItensPorPagina);
}
