using ControlePessoas.Domain.Interfaces.Persistence.Repositories;

namespace ControlePessoas.Domain.Interfaces.Persistence;
public interface IUnitOfWork
{
    IPessoaRepository PessoaRepository { get; }
    Task<bool> CommitAsync();
    bool Commit();
}
