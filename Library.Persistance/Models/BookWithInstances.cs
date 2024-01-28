namespace Library.Persistance.Models;

public sealed class BookWithInstances
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Author { get; set; }
    public int Isbn { get; set; }
    public int AllCount { get; set; }
    public int AvailableCount { get; set; }
}