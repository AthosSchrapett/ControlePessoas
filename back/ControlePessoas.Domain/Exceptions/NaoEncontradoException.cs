namespace ControlePessoas.Domain.Exceptions;

public class NaoEncontradoException(string mensagem = "Registro não encontrado") : 
    Exception(mensagem)
{
}
