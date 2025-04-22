namespace ControlePessoas.Domain.Models;
public class ResultadoPaginacao<T> where T : class
{
    public IEnumerable<T> Itens { get; private set; }
    public int TotalRegistros { get; private set; }
    public int PaginaAtual { get; private set; }
    public int TotalPaginas { get; private set; }

    public ResultadoPaginacao(IEnumerable<T> itens, int totalRegistros, int paginaAtual, int itensPorPagina)
    {
        Itens = itens;
        TotalRegistros = totalRegistros;
        PaginaAtual = paginaAtual;
        TotalPaginas = (int)Math.Ceiling(totalRegistros / (double)itensPorPagina);
    }
}