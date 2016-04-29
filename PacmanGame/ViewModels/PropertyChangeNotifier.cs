using System.ComponentModel;
using System.Runtime.CompilerServices;
using PacmanGame.Annotations;

namespace PacmanGame.ViewModels
{
    public class PropertyChangeNotifier : INotifyPropertyChanged
    {
        /// <summary>
        /// An event raised when the property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The method which raises the PropertyChanged event for the propertyName property.
        /// </summary>
        /// <param name="propertyName">The name of property which has changed.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
