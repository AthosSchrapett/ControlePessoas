using ControlePessoas.API.Controllers;
using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ControlePessoas.Tests;

public class PessoaControllerTests
{
    private readonly Mock<IPessoaService> _mockPessoaService;
    private readonly PessoaController _controller;

    public PessoaControllerTests()
    {
        _mockPessoaService = new Mock<IPessoaService>();
        _controller = new PessoaController(_mockPessoaService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResultWithPessoas()
    {
        var pessoas = new List<PessoaGetAllDTO>
        {
            new(Guid.NewGuid(), "Teste 1", 25, false),
            new(Guid.NewGuid(), "Teste 2", 30, false)
        };

        _mockPessoaService.Setup(s => s.GetAllAsync())
            .ReturnsAsync(pessoas);

        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<PessoaGetAllDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
        Assert.Equal("Teste 1", returnValue[0].Nome);
        Assert.Equal(25, returnValue[0].Idade);
    }

    [Fact]
    public async Task GetById_WithValidId_ReturnsOkResultWithPessoa()
    {
        var id = Guid.NewGuid();
        var pessoa = new PessoaGetDTO(id, "Teste", 25, 'M', 70.5, 1.75, false);

        _mockPessoaService.Setup(s => s.GetByIdAsync(id))
            .ReturnsAsync(pessoa);

        var result = await _controller.GetById(id);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PessoaGetDTO>(okResult.Value);
        Assert.Equal(id, returnValue.Id);
        Assert.Equal("Teste", returnValue.Nome);
        Assert.Equal(25, returnValue.Idade);
        Assert.Equal('M', returnValue.Sexo);
        Assert.Equal(70.5, returnValue.Peso);
        Assert.Equal(1.75, returnValue.Altura);
    }

    [Fact]
    public async Task Add_ValidPessoa_ReturnsCreatedAtAction()
    {
        var pessoaCreate = new PessoaCreateDTO("Teste", 25, 'M', 70.5, 1.75);
        var pessoaGet = new PessoaGetDTO(Guid.NewGuid(), "Teste", 25, 'M', 70.5, 1.75, false);

        _mockPessoaService.Setup(s => s.AddAsync(pessoaCreate))
            .ReturnsAsync(pessoaGet);

        var result = await _controller.Add(pessoaCreate);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<PessoaGetDTO>(createdAtActionResult.Value);
        Assert.Equal(pessoaGet.Id, returnValue.Id);
        Assert.Equal("Teste", returnValue.Nome);
        Assert.Equal(25, returnValue.Idade);
    }

    [Fact]
    public async Task Update_ValidPessoa_ReturnsOkResult()
    {
        var id = Guid.NewGuid();
        var pessoaUpdate = new PessoaUpdateDTO(id, "Teste Atualizado", 30, 'M', 75.0, 1.75);
        var pessoaGet = new PessoaGetDTO(id, "Teste Atualizado", 30, 'M', 75.0, 1.75, false);

        _mockPessoaService.Setup(s => s.UpdateAsync(pessoaUpdate))
            .ReturnsAsync(pessoaGet);

        var result = await _controller.Update(pessoaUpdate);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<PessoaGetDTO>(okResult.Value);
        Assert.Equal(id, returnValue.Id);
        Assert.Equal("Teste Atualizado", returnValue.Nome);
        Assert.Equal(30, returnValue.Idade);
        Assert.Equal(75.0, returnValue.Peso);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsNoContent()
    {
        var id = Guid.NewGuid();

        _mockPessoaService.Setup(s => s.DeleteAsync(id))
            .Returns(Task.CompletedTask);

        var result = await _controller.Delete(id);

        Assert.IsType<NoContentResult>(result);
    }
} 