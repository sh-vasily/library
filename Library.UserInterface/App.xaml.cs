using System.IO;
using System.Windows;
using Library.Persistance.Repository;
using Library.Persistence.Repository;
using Microsoft.Extensions.Configuration;

namespace DbDemo;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("Library");
        DependencyInjectionContainer.Register<IBookRepository, BookRepository>();
        DependencyInjectionContainer.Register<IUserRepository, UserRepository>();
    }
}