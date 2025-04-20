namespace ControlePessoas.Domain.DTOs.Create;
public record PessoaCreateDTO
(
    string Nome,
    int Idade,
    char Sexo,
    double Peso,
    double? Altura
);
