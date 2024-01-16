using System;
using System.Windows.Input;

namespace DbDemo.WPF.MVVM.Command;

public sealed class RelayCommand : ICommand
{
    private readonly Predicate<object> _canExecute;

    private readonly Action<object> _execute;

    public RelayCommand(
        Action<object> execute,
        Predicate<object> canExecute = null)
    {
        _canExecute = canExecute;
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }

    public void Execute(object parameter)
    {
        _execute(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add =>
            CommandManager.RequerySuggested += value;

        remove =>
            CommandManager.RequerySuggested -= value;
    }
}