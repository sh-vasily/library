using System.IO;
using System.Windows;
using Library.Persistance.Repository;
using Microsoft.Extensions.Configuration;

namespace Library.UserInterface;

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
        
        DependencyInjectionContainer.AddDbContext(configuration);
        DependencyInjectionContainer.Register<IBookRepository, BookRepository>();
        DependencyInjectionContainer.Register<IUserRepository, UserRepository>();
        DependencyInjectionContainer.Register<IBorrowedBooksRepository, BorrowedBooksRepository>();
    }
}