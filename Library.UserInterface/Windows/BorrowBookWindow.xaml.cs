using System.Windows;
using Library.UserInterface.ViewModels;

namespace Library.UserInterface.Windows;

public partial class BorrowBookWindow : Window
{
    public BorrowBookWindow(int bookId)
    {
        InitializeComponent();
        ((BorrowBookWindowsViewModel)DataContext).BookId = bookId;
    }
}