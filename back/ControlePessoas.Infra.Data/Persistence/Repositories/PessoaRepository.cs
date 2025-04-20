using ControlePessoas.Domain.Entities;
using ControlePessoas.Domain.Interfaces.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ControlePessoas.Infra.Data.Persistence.Repositories;
public class PessoaRepository : IPessoaRepository
{
    private readonly PessoaContext _context;
    protected readonly DbSet<Pessoa> _dbSet;

    public PessoaRepository(PessoaContext context)
    {
        _context = context;
        _dbSet = context.Set<Pessoa>();
    }

    public async Task AddAsync(Pessoa pessoa)
    {
        await _dbSet.AddAsync(pessoa);
    }

    public async Task<IEnumerable<Pessoa>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public Task<Pessoa?> GetByIdAsync(Guid id)
    {
        return _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(Pessoa pessoa)
    {
        _dbSet.Update(pessoa);
    }
}
