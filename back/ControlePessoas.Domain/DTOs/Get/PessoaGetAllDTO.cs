namespace ControlePessoas.Domain.DTOs.Get;
public record PessoaGetAllDTO
(
    Guid Id,
    string Nome,
    int Idade,
    bool Idoso
);
