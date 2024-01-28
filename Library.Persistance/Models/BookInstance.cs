namespace Library.Persistance.Models;

public sealed class BookInstance
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; }
    public ICollection<BorrowedBook> BorrowedBooks { get; set; }
}