using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Library.MVVM.ViewModel;
using Library.Persistance.Models;
using Library.Persistance.Repository;

namespace Library.UserInterface.ViewModels;

internal class UsersControlViewModel : ViewModelBase
{
    private readonly IUserRepository _userRepository = DependencyInjectionContainer.ResolveService<IUserRepository>();
    private ObservableCollection<User> _users = [];

    public UsersControlViewModel() => Task.Run(SelectUsersAsync);

    public ObservableCollection<User> Users
    {
        get => _users;
        private set
        {
            _users = value;
            RaisePropertyChanged(nameof(Users));
        }
    }

    private async Task SelectUsersAsync()
    {
        try
        {
            var users = await _userRepository.GetAll();
            Users = new ObservableCollection<User>(users);
        }
        catch (AggregateException ex)
        {
            MessageBox.Show($"Failed to execute query: \"{ex.Message}\"");
        }
    }
}