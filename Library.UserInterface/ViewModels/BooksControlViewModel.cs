using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DbDemo.WPF.MVVM.Command;
using Library.Persistance.Repository;
using Lvm.ViewModel;

namespace DbDemo.ViewModels;

internal sealed class BooksControlViewModel : ViewModelBase
{
    private readonly IBookRepository _bookRepository;
    private ObservableCollection<Book> _books;
    private ICommand _insertCommand;
    private ICommand _selectCommand;

    public BooksControlViewModel()
    {
        _bookRepository = DependencyInjectionContainer.ResolveService<IBookRepository>();
        Books = new ObservableCollection<Book>();
        Task.Run(() => SelectBooksAsync());
    }

    public ICommand SelectCommand =>
        _selectCommand ??= new RelayCommand(async _ => await SelectBooksAsync());

    public ICommand InsertCommand =>
        _insertCommand ??= new RelayCommand(async _ => await InsertEmployeeAsync());

    public ObservableCollection<Book> Books
    {
        get => _books;
        private set
        {
            _books = value;
            RaisePropertyChanged(nameof(Books));
        }
    }

    private async Task SelectBooksAsync(CancellationToken token = default)
    {
        try
        {
            var books = await _bookRepository.GetAll(0,100, token);
            Books = new ObservableCollection<Book>(books);
        }
        catch (AggregateException ex)
        {
            MessageBox.Show($"Failed to execute query: \"{ex.Message}\"");
        }
    }

    private async Task InsertEmployeeAsync(CancellationToken token = default)
    {
        //await _bookRepository.Add("test", "test", token);
        await SelectBooksAsync(token);

        /*var addBookWindow = new AddBookWindow();
        var dialogResult = addBookWindow.ShowDialog();

        if (dialogResult.HasValue && dialogResult.Value)
        {
            await SelectBooksAsync(token);
        }*/
        
    }
}