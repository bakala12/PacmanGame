using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PacmanGame.Extensions
{
    public class InvokeCommandOnEventAction : TriggerAction<DependencyObject>
    {
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

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand),
            typeof(InvokeCommandOnEventAction), new UIPropertyMetadata(null));
    }
}