using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PacmanGame.Extensions
{
    /// <summary>
    /// Represents an action for Prism TriggerAction.
    /// The action invokes 
    /// </summary>
    public class InvokeCommandOnEventAction : TriggerAction<DependencyObject>
    {
        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the 
        /// parameter may be set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject != null)
            {
                if (Command != null && Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
        }

        /// <summary>
        /// Gets or sets the command that would be invoked as the action.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// A dependency property for storing Command property value.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand),
            typeof(InvokeCommandOnEventAction), new UIPropertyMetadata(null));
    }
}