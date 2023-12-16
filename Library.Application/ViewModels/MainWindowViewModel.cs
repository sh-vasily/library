using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Library.Data;
using Library.Mvvm.Command;
using Library.Mvvm.ViewModel;

namespace Library.Application.ViewModels;

public class MainWindowViewModel(IBookRepository bookRepository) : ViewModelBase
{
        
        private ObservableCollection<Book> _books;

        public ObservableCollection<Book> Books
        {
            get => _books;
            private set
            {
                _books = value;
                RaisePropertyChanged(nameof(Books));
            }
        }

        private void SelectEmployeesAsync()
        {
            try
            {
                var books = bookRepository.GetAll();
                Books = new ObservableCollection<Book>(books);
            }
            catch (AggregateException ex)
            {
                MessageBox.Show($"Failed to execute query: \"{ex.Message}\"");
            }
        }
        
        public ICommand SelectCommand =>
            _selectCommand ??= new RelayCommand(_ => SelectEmployeesAsync());
        
        private ICommand? _selectCommand;

    }