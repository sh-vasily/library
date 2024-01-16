using Microsoft.Extensions.DependencyInjection;

namespace Library.UserInterface;

internal static class DependencyInjectionContainer
{
    private static readonly IServiceCollection Services;
    
    static DependencyInjectionContainer()
    {
        Services = new ServiceCollection();
    }
    
    public static void Register<TService, TImplementation>() where TService: class
                                                             where TImplementation : class, TService 
        => Services.AddSingleton<TService, TImplementation>();

    public static TService ResolveService<TService>()
    {
        using var sp = Services.BuildServiceProvider();
        return sp.GetRequiredService<TService>();
    }
}