using ControlePessoas.Domain.Interfaces.Persistence;
using ControlePessoas.Domain.Interfaces.Persistence.Repositories;
using ControlePessoas.Infra.Data.Persistence.Repositories;

namespace ControlePessoas.Infra.Data.Persistence;
public class UnitOfWork(PessoaContext context) : IUnitOfWork
{
    private readonly PessoaContext _context = context;

    private IPessoaRepository? _pessoaRepository;

    public IPessoaRepository PessoaRepository =>
        _pessoaRepository ??= new PessoaRepository(_context);

    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool Commit()
    {
        return _context.SaveChanges() > 0;
    }
}
