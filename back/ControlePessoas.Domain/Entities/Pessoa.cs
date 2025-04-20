using ControlePessoas.Domain.Entities.Base;

namespace ControlePessoas.Domain.Entities;
public class Pessoa : BaseEntity
{
    public string Nome { get; private set; }
    public int Idade { get; private set; }
    public char Sexo { get; private set; }
    public double Peso { get; private set; }
    public double? Altura { get; private set; }
    public bool Idoso { get; private set; }

    public Pessoa
    (
        string nome, 
        int idade, 
        char sexo, 
        double peso, 
        double? altura
    )
    {
        Nome = nome;
        Idade = idade;
        Sexo = sexo;
        Peso = peso;
        Altura = altura;
        Idoso = VerificarIdoso();
    }

    private bool VerificarIdoso()
    {
        return Idade >= 60;
    }

    public void AtualizarDados
    (
        string nome,
        int idade,
        char sexo,
        double peso,
        double? altura
    )
    {
        Nome = nome;
        Idade = idade;
        Sexo = sexo;
        Peso = peso;
        Altura = altura;
        Idoso = VerificarIdoso();
        base.AtualizarDataEdicao();
    }
}
