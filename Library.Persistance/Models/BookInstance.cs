namespace Library.Persistance.Models;

public class BookInstance
{
    public int Is { get; set; }
    public int BookId { get; set; }
    public List<BorrowedBook> BorrowedBooks { get; set; }
}