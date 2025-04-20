namespace ControlePessoas.Domain.DTOs.Get;
public record PessoaGetAll
(
    Guid Id,
    string Nome,
    int Idade,
    bool Idoso
);
