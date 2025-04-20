using ControlePessoas.Domain.DTOs.Update;
using FluentValidation;

namespace ControlePessoas.Application.Validators.Pessoa;
public class PessoaUpdateDTOValidator : AbstractValidator<PessoaUpdateDTO>
{
    public PessoaUpdateDTOValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("O ID é obrigatório.");

        RuleFor(p => p.Nome)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.")
            .MaximumLength(60);

        RuleFor(p => p.Idade)
            .GreaterThan(0)
            .WithMessage("A idade deve ser maior que zero.");

        RuleFor(p => p.Sexo)
            .Must(s => s == 'M' || s == 'F')
            .WithMessage("Sexo deve ser 'M' ou 'F'.");

        RuleFor(p => p.Peso)
            .GreaterThan(0)
            .WithMessage("O peso deve ser maior que zero.");
    }
}
