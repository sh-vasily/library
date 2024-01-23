using System;

namespace Library.UserInterface;

internal static class BookContext
{
    public static event Action<int> BookIdChanged;

    public static void SelectBook(int bookId)
    {
        BookIdChanged?.Invoke(bookId);
    }
}