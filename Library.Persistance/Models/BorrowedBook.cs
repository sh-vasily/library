namespace Library.Persistance.Models;

public sealed class BorrowedBook
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public BookInstance BookInstance { get; set; }
}