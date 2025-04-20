using ControlePessoas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControlePessoas.Infra.Data.Persistence;
public class PessoaContext(DbContextOptions<PessoaContext> options) : DbContext(options)
{
    DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PessoaContext).Assembly);
    }
}
