using ControlePessoas.Application.Mapeamentos;
using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Entities;
using ControlePessoas.Domain.Exceptions;
using ControlePessoas.Domain.Interfaces.Persistence;
using ControlePessoas.Domain.Interfaces.Services;

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

    private async Task<Pessoa> GetPessoaByIdAsync(Guid id)
    {
        return await _unitOfWork.PessoaRepository.GetByIdAsync(id) ?? 
            throw new NaoEncontradoException();
    }
}
