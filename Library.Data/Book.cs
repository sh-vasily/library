namespace Library.Data;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    
    public virtual List<Author> Authors { get; set; }
}

public class Author
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}