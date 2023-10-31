using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Application.Common.Interfaces.ISercices;
using PM.Infrastructure.Persistence.Repositories;
using PM.Infrastructure.Services;

namespace PM.Infrastructure.Persistence;

public static class DependepcyInjection
{
    public static IServiceCollection AddPesistence(
        this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseInMemoryDatabase("DataBase"));

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        return services;
    }
}
