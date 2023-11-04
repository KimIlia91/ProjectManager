using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PM.Application.Common.Interfaces.IRepositories;
using PM.Infrastructure.Persistence.Repositories;

namespace PM.Infrastructure.Persistence;

/// <summary>
/// Represents a class for configuring and adding persistence-related services to the application.
/// </summary>
public static class PesistenceDi
{
    /// <summary>
    /// Configures and adds persistence-related services to the specified service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which the services should be added.</param>
    /// <param name="configuration">The application configuration, which includes database connection settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddPesistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
