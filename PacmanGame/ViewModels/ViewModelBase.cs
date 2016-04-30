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
    public abstract class ViewModelBase : PropertyChangedNotifier
    {
        /// <summary>
        /// Gets the name of current ViewModel.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Initializies a new instance of ViewModelBase class with the specified name.
        /// </summary>
        /// <param name="name">The name of the view model.</param>
        protected ViewModelBase(string name = null)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be neighter null nor empty");
            Name = name;
        }
    }
}
