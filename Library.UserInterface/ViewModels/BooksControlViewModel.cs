using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Library.MVVM.Command;
using Library.MVVM.ViewModel;
using Library.Persistance.Models;
using Library.Persistance.Repository;
using Library.UserInterface.Windows;

namespace Library.UserInterface.ViewModels;

internal sealed class BooksControlViewModel : ViewModelBase
{
    private readonly IBookRepository _bookRepository = DependencyInjectionContainer.ResolveService<IBookRepository>();
    private ObservableCollection<Book> _books = [];
    private ICommand _findCommand;
    private ICommand _borrowBookCommand;
    private ICommand _selectBookCommand;
    private string _searchText;
    private int?  _selectedBookId;

    public BooksControlViewModel()
    {
        Task.Run(SelectBooksAsync);
        BookContext.BookIdChanged+= bookId => _selectedBookId = bookId;
    } 

    public ICommand FindCommand =>
        _findCommand ??= new RelayCommand(async _ => await GetBooksByNameAsync());

    public ICommand SelectBookCommand =>
        _selectBookCommand ??= new RelayCommand(SelectBook);
    
    public ICommand BorrowBookCommand =>
        _borrowBookCommand ??= new RelayCommand(BorrowBook);
    

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

    private async Task SelectBooksAsync()
    {
        try
        {
            var books = await _bookRepository.GetAll();
            Books = new ObservableCollection<Book>(books);
            SelectBook(books.First());
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
            var books = await _bookRepository.GetByNameOrAuthor(SearchText, 0,100, token);
            Books = new ObservableCollection<Book>(books);
        }
        catch (AggregateException ex)
        {
            MessageBox.Show($"Failed to execute query: \"{ex.Message}\"");
        }
    }

    private void SelectBook(object o)
    {
        if (o is not Book book)
        {
            return;
        }

        BookContext.SelectBook(book.Id);
    }

    private void BorrowBook(object o)
    {
        if (_selectedBookId is null)
        {
            return;
        }

        var window = new BorrowBookWindow(_selectedBookId.Value);
        window.ShowDialog();
    }
}