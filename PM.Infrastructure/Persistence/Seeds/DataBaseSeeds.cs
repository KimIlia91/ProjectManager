using Microsoft.Extensions.DependencyInjection;

namespace PM.Infrastructure.Persistence.Seeds;

public static class DataBaseSeeds
{
    public static async Task AddSeeds(IServiceProvider services)
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        
    }
}
