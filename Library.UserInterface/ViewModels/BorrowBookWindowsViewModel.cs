using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DbDemo;
using Library.MVVM.Command;
using Library.Persistance.Repository;

namespace Library.UserInterface.ViewModels;

internal sealed class BorrowBookWindowsViewModel : UsersControlViewModel
{
    private readonly IBorrowedBooksRepository _borrowedBooksRepository = DependencyInjectionContainer.ResolveService<IBorrowedBooksRepository>();
    private ICommand _borrowBookCommand;
    public int? BookId { get; set; }
    
    public ICommand BorrowBookCommand =>
        _borrowBookCommand ??= new RelayCommand(BorrowBook);

    private void BorrowBook(object o)
    {
        if (o is User user && BookId is not null)
        {
            Task.Run(async () =>
            {
                await _borrowedBooksRepository.BorrowBook(BookId.Value, user.Id);
                MessageBox.Show($"Книга выдана абоненту {user.FirstName} {user.LastName}");
            });
        }
    }
}