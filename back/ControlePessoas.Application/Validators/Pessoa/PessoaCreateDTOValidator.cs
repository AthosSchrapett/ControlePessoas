using ControlePessoas.Domain.DTOs.Create;
using FluentValidation;

namespace ControlePessoas.Application.Validators.Pessoa;
public class PessoaCreateDTOValidator : AbstractValidator<PessoaCreateDTO>
{
    public PessoaCreateDTOValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty()
                .WithMessage("O nome é obrigatório.")
            .MaximumLength(60);

        RuleFor(p => p.Idade)
            .GreaterThan(0)
                .WithMessage("A idade deve ser maior que zero.")
            .LessThanOrEqualTo(130)
                .WithMessage("A idade deve ser no máximo 130 anos.");

        RuleFor(p => p.Sexo)
            .Must(s => s == 'M' || s == 'F')
                .WithMessage("Sexo deve ser 'M' ou 'F'.");

        RuleFor(p => p.Peso)
            .GreaterThan(0)
                .WithMessage("O peso deve ser maior que zero.");

        RuleFor(p => p.Altura)
            .LessThanOrEqualTo(2.30)
                .WithMessage("A altura deve ser no máximo 2.30 metros.")
            .GreaterThan(0.30)
                .WithMessage("A altura deve ser maior que 0.30 metros.");
    }
}
