using ControlePessoas.Domain.DTOs.Create;
using ControlePessoas.Domain.DTOs.Get;
using ControlePessoas.Domain.DTOs.Update;
using ControlePessoas.Domain.Entities;

namespace ControlePessoas.Application.Mapeamentos;
public static class PessoaMap
{
    public static Pessoa DTOParaPessoa(this PessoaCreateDTO dto)
    {
        return new Pessoa
        (
            dto.Nome,
            dto.Idade,
            dto.Sexo,
            dto.Peso,
            dto.Altura
        );
    }

    public static void AtualizarPessoaPorDTO(this Pessoa pessoa, PessoaUpdateDTO dto)
    {
        pessoa.AtualizarDados
        (
            dto.Nome,
            dto.Idade,
            dto.Sexo,
            dto.Peso,
            dto.Altura
        );
    }

    public static PessoaGetDTO PessoaParaDTO(this Pessoa pessoa)
    {
        return new PessoaGetDTO
        (
            pessoa.Id,
            pessoa.Nome,
            pessoa.Idade,
            pessoa.Sexo,
            pessoa.Peso,
            pessoa.Altura,
            pessoa.Idoso
        );
    }

    public static IEnumerable<PessoaGetAllDTO> PessoasParaDTO(this IEnumerable<Pessoa> pessoas)
    {
        return pessoas.Select
        (p => 
            new PessoaGetAllDTO
            (
                p.Id,
                p.Nome,
                p.Idade,
                p.Idoso
            )
        );
    }
}
