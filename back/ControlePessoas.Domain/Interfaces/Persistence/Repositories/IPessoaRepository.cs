using ControlePessoas.Domain.Entities;

namespace ControlePessoas.Domain.Interfaces.Persistence.Repositories;
public interface IPessoaRepository
{
    Task<Pessoa?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pessoa>> GetAllAsync();
    Task AddAsync(Pessoa pessoa);
    IQueryable<Pessoa> GetAll();
    void Update(Pessoa pessoa);
}
