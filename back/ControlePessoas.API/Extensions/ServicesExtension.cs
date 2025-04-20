using ControlePessoas.Domain.Interfaces.Persistence;
using ControlePessoas.Infra.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ControlePessoas.API.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PessoaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
