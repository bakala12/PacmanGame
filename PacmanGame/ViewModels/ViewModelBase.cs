using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Annotations;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a base class for all ViewModels. It supports property changing notification
    /// by implementing INotifyPropertyChanged interface 
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the name of current ViewModel.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializies a new instance of ViewModelBase class with the specified name.
        /// </summary>
        /// <param name="name">The name of the view model.</param>
        protected ViewModelBase(string name=null)
        {
            Name = name;
        }

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
