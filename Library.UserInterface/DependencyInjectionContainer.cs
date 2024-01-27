using Library.Persistance;
using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Library.UserInterface;

internal static class DependencyInjectionContainer
{
    private static readonly IServiceCollection Services = new ServiceCollection();
    
    public static void Register<TService, TImplementation>() where TService: class
                                                             where TImplementation : class, TService 
        => Services.AddSingleton<TService, TImplementation>();

    public static TService ResolveService<TService>()
    {
        using var sp = Services.BuildServiceProvider();
        return sp.GetRequiredService<TService>();
    }

    public static void AddDbContext(IConfiguration configuration)
        => Services.AddDbContext<LibraryContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("Library");
            var dataSource = new NpgsqlDataSourceBuilder(connectionString);
            dataSource.MapEnum<Gender>();
            options.UseNpgsql(dataSource.Build());
        });
}