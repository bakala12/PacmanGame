using System;
using System.Windows.Input;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Simple ICommand inteface implementation based on delegates.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Gets the predicate called as ICommand CanExecute method. 
        /// </summary>
        public Predicate<object> CanExecutePredicate { get; }

        /// <summary>
        /// Gets the action called as ICommand Execute method.
        /// </summary>
        public Action<object> ExecuteAction { get; }

        /// <summary>
        /// Initializes new DelegateCommand object with given execute action and can execute predicate.
        /// </summary>
        /// <param name="executeAction">Action which will be executed in ICommand Execute method.</param>
        /// <param name="canExecutePredicate">Predicate which will be called as ICommand CanExecute method.</param>
        /// <exception cref="ArgumentNullException">Thrown when executeAction or canExecutePrediacte are null.</exception>
        public DelegateCommand(Action<object> executeAction, Predicate<object> canExecutePredicate)
        {
            if(executeAction==null) throw new ArgumentNullException(nameof(executeAction));
            if(canExecutePredicate==null) throw new ArgumentNullException(nameof(canExecutePredicate));
            ExecuteAction = executeAction;
            CanExecutePredicate = canExecutePredicate;
        }

        /// <summary>
        /// Initializes new DelegateCommand object with given execute action.
        ///  </summary>
        /// <param name="executeAction">Action which will be executed in ICommand Execute method.</param>
        /// <exception cref="ArgumentNullException">Thrown when executeAction is null.</exception>
        public DelegateCommand(Action<object> executeAction):this(executeAction, x => true) { }

        /// <summary>
        /// Informs WPF environment whether the command can execute.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>True if command can execute, otherwise false.</returns>
        public bool CanExecute(object parameter)
        {
            return CanExecutePredicate(parameter);
        }

        /// <summary>
        /// Method executing as command action.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public void Execute(object parameter)
        {
            ExecuteAction(parameter);
        }

        /// <summary>
        /// Event raised when the command CanExecute status changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Raises the CanExecuteChanged event.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
