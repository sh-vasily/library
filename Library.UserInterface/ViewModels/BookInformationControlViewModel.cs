using System.Threading.Tasks;
using Library.MVVM.ViewModel;
using Library.Persistance.Models;
using Library.Persistance.Repository;

namespace Library.UserInterface.ViewModels;

public sealed class BookInformationControlViewModel : ViewModelBase
{
    private readonly IBookRepository _bookRepository = DependencyInjectionContainer.ResolveService<IBookRepository>();
    private readonly IBorrowedBooksRepository _borrowedBooksRepository = DependencyInjectionContainer.ResolveService<IBorrowedBooksRepository>();
    private Book _book;
    private int _availableCount;

    public BookInformationControlViewModel() => BookContext.BookIdChanged +=id => Task.Run(async () => await LoadBook(id));
    
    public Book Book
    {
        get => _book;
        private set
        {
            _book = value;
            RaisePropertyChanged(nameof(this.Book));
        }
    }
    
    public int AvailableCount
    {
        get => _availableCount;
        private set
        {
            _availableCount = value;
            RaisePropertyChanged(nameof(AvailableCount));
        }
    }
    
    public int AllCount
    {
        get => _availableCount;
        private set
        {
            _availableCount = value;
            RaisePropertyChanged(nameof(AllCount));
        }
    }

    private async Task LoadBook(int bookId)
    {
        Book = await _bookRepository.GetById(bookId);
        var borrowedCount = await _borrowedBooksRepository.GetBorrowedBooksCount(bookId);
        AvailableCount = AllCount - borrowedCount;
    }
}