using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DbDemo;
using Library.MVVM.Command;
using Library.MVVM.ViewModel;
using Library.Persistance.Repository;
using Library.Persistence.Repository;

namespace Library.UserInterface.ViewModels;

public class BorrowBookWindowsViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository = DependencyInjectionContainer.ResolveService<IUserRepository>();
    private readonly IBorrowedBooksRepository _borrowedBooksRepository = DependencyInjectionContainer.ResolveService<IBorrowedBooksRepository>();
    private ObservableCollection<User> _users = [];
    private ICommand _borrowBookCommand;
    
    public BorrowBookWindowsViewModel() => Task.Run(() => SelectUsersAsync());

    public int? BookId { get; set; } = null;
    
    public ObservableCollection<User> Users
    {
        get => _users;
        private set
        {
            _users = value;
            RaisePropertyChanged(nameof(Users));
        }
    }
    
    public ICommand BorrowBookCommand =>
        _borrowBookCommand ??= new RelayCommand(BorrowBook);

    private void BorrowBook(object o)
    {
        if (o is User user)
        {
            Task.Run(() => _borrowedBooksRepository.BorrowBook(1, user.Id));
        }
    }

    private async Task SelectUsersAsync(CancellationToken token = default)
    {
        try
        {
            var users = await _userRepository.GetAll(0,100, token);
            Users = new ObservableCollection<User>(users);
        }
        catch (AggregateException ex)
        {
            MessageBox.Show($"Failed to execute query: \"{ex.Message}\"");
        }
    }
    
}