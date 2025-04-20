using ControlePessoas.Domain.Entities;

namespace ControlePessoas.Domain.Interfaces.Persistence.Repositories;
public interface IPessoaRepository
{
    Task<Pessoa?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pessoa>> GetAllAsync();
    Task AddAsync(Pessoa pessoa);
    void Update(Pessoa pessoa);
}
