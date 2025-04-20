namespace ControlePessoas.Domain.DTOs.Update;
public record PessoaUpdateDTO
(
    Guid Id,
    string Nome,
    int Idade,
    char Sexo,
    double Peso,
    double? Altura
);
