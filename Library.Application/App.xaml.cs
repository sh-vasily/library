using System.Windows;
using Library.Data;
using Library.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Library.Application;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
                services.AddSingleton<IBookRepository, BooksRepository>();
            })
            .Build();

        ServiceProviderSource.Resolver = type => host.Services.GetRequiredService(type);
    }
}