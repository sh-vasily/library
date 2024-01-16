using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DbDemo;
using Library.MVVM.Command;
using Library.MVVM.ViewModel;
using Library.Persistence.Repository;

namespace Library.UserInterface.ViewModels;

internal sealed class UsersControlViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository;
    private ObservableCollection<User> _users;
    private ICommand _insertCommand;
    private ICommand _selectCommand;

    public UsersControlViewModel()
    {
        _userRepository = DependencyInjectionContainer.ResolveService<IUserRepository>();
        Users = new ObservableCollection<User>();
        Task.Run(() => SelectUsersAsync());
    }

    public ICommand SelectCommand =>
        _selectCommand ??= new RelayCommand(async _ => await SelectUsersAsync());

    public ICommand InsertCommand =>
        _insertCommand ??= new RelayCommand(async _ => await InsertUserAsync());

    public ObservableCollection<User> Users
    {
        get => _users;
        private set
        {
            _users = value;
            RaisePropertyChanged(nameof(Users));
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

    private Task InsertUserAsync(CancellationToken token = default)
    {

        return Task.CompletedTask;
        /*var addBookWindow = new AddBookWindow();
        var dialogResult = addBookWindow.ShowDialog();

        if (dialogResult.HasValue && dialogResult.Value)
        {
            await SelectBooksAsync(token);
        }*/

    }
}