using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Interfaces.Services;
using ControlePessoas.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControlePessoas.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoaController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaGetAllDTO>>> GetAll()
    {
        IEnumerable<PessoaGetAllDTO> pessoas = await _pessoaService.GetAllAsync();
        return Ok(pessoas);
    }

    [HttpGet("paginacao")]
    public ActionResult<IEnumerable<PessoaGetAllDTO>> GetAll([FromQuery] FiltroPaginacao filtroPaginacao)
    {
        ResultadoPaginacao<PessoaGetAllDTO> pessoas = _pessoaService.GetAllPaginacaoFiltro(filtroPaginacao);
        return Ok(pessoas);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PessoaGetDTO>> GetById(Guid id)
    {
        PessoaGetDTO pessoa = await _pessoaService.GetByIdAsync(id);
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<ActionResult<PessoaGetDTO>> Add(PessoaCreateDTO dto)
    {
        PessoaGetDTO novaPessoa = await _pessoaService.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = novaPessoa.Id }, novaPessoa);
    }

    [HttpPut]
    public async Task<ActionResult<PessoaGetDTO>> Update(PessoaUpdateDTO dto)
    {
        PessoaGetDTO pessoaAtualizada = await _pessoaService.UpdateAsync(dto);
        return Ok(pessoaAtualizada);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _pessoaService.DeleteAsync(id);
        return NoContent();
    }
}
