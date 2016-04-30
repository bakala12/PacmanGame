using System.ComponentModel;
using System.Runtime.CompilerServices;
using PacmanGame.Annotations;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Provides a basic way to inform WPF binding system that a property has changed.
    /// This class provides an implementation of INotifyPropertyChanged interface. 
    /// </summary>
    public abstract class PropertyChangedNotifier : INotifyPropertyChanged
    {
        /// <summary>
        /// An event raised when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The method which raises the PropertyChanged event for the propertyName property.
        /// </summary>
        /// <param name="propertyName">
        /// The name of property which has changed.
        /// The name can be taken from setter automatically by the CallerMemberName attribute.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
