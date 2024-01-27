namespace Library.Persistance.Models;

public sealed class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
}