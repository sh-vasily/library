using System;

namespace DbDemo;

public sealed class Book
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Author { get; set; }
    public int Isbn { get; set; }
}