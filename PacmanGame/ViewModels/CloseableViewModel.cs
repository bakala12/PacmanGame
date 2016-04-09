using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a ViewModel that supports close opertation. 
    /// </summary>
    public abstract class CloseableViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of CloseableViewModel object.
        /// </summary>
        /// <param name="name"></param>
        protected CloseableViewModel(string name = null) : base(name)
        {
        }

        /// <summary>
        /// An event which is raised when the associated view should be closed.
        /// </summary>
        public EventHandler RequestClose;

        /// <summary>
        /// A method which raises the RequestClose event. 
        /// </summary>
        protected virtual void Close()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}
