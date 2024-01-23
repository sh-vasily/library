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
using Library.UserInterface.Windows;

namespace Library.UserInterface.ViewModels;

internal sealed class BooksControlViewModel : ViewModelBase
{
    private readonly IBookRepository _bookRepository = DependencyInjectionContainer.ResolveService<IBookRepository>();
    private ObservableCollection<Book> _books = [];
    private ICommand _insertCommand;
    private ICommand _selectCommand;
    private ICommand _findCommand;
    private ICommand _openBorrowBookDialogCommand;
    private string _searchText;

    public BooksControlViewModel() => Task.Run(() => SelectBooksAsync());

    public ICommand FindCommand =>
        _findCommand ??= new RelayCommand(async _ => await GetBooksByNameAsync());

    public ICommand OpenBorrowBookDialogCommand =>
        _openBorrowBookDialogCommand ??= new RelayCommand(OpenBorrowBookDialog);

    public ICommand InsertCommand =>
        _insertCommand ??= new RelayCommand(async _ => await InsertEmployeeAsync());

    public string SearchText
    {
        private get => _searchText;

        set
        {
            _searchText = value;
            RaisePropertyChanged(nameof(SearchText));
        }
    }
    
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
    
    private async Task GetBooksByNameAsync(CancellationToken token = default)
    {
        try
        {
            var books = await _bookRepository.GetByName(SearchText, 0,100, token);
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
    
    private void OpenBorrowBookDialog(object o)
    {
        if (o is not Book book)
        {
            return;
        }
        var borrowBookWindow = new BorrowBookWindow(book.Id);
        var dialogResult = borrowBookWindow.ShowDialog();

        if (dialogResult.HasValue && dialogResult.Value)
        {
            //await SelectBooksAsync(token);
        }

    }
}