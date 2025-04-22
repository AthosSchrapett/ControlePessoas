using ControlePessoas.Domain.Enums;

namespace ControlePessoas.Domain.Models;
public record FiltroPaginacao
(
    int Pagina,
    int ItensPorPagina,
    FiltroPessoasEnum FiltroPessoas
);
