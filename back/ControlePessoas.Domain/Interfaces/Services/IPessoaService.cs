﻿using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Models;

namespace ControlePessoas.Domain.Interfaces.Services;
public interface IPessoaService
{
    Task<PessoaGetDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<PessoaGetAllDTO>> GetAllAsync();
    Task<PessoaGetDTO> AddAsync(PessoaCreateDTO dto);
    Task<PessoaGetDTO> UpdateAsync(PessoaUpdateDTO dto);
    Task DeleteAsync(Guid id);
    ResultadoPaginacao<PessoaGetAllDTO> GetAllPaginacaoFiltro(FiltroPaginacao filtroPaginacao);
}
