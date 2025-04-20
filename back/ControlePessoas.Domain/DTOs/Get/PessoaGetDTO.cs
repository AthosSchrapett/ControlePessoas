namespace ControlePessoas.Domain.DTOs.Get;
public record PessoaGetDTO
(
    Guid Id,
    string Nome,
    int Idade,
    char Sexo,
    double Peso,
    double? Altura,
    bool Idoso
);
