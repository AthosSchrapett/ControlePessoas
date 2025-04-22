namespace ControlePessoas.Domain.Exceptions;
public class FiltroInvalidoException : Exception
{
    public FiltroInvalidoException(string filtro, object? valor)
            : base($"O valor '{valor}' é inválido para o filtro '{filtro}'.")
    {
    }
}
