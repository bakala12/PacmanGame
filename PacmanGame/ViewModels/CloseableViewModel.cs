using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        /// <param name="name">Name of the view model.</param>
        protected CloseableViewModel(string name = null) : base(name)
        {
            ReturnCommand = new DelegateCommand(x=>Close());
            ViewAppeared += (sender, args) => OnViewAppeared();
        }

        /// <summary>
        /// Occures when the view associated with the current view model appeared on the screen.
        /// </summary>
        public EventHandler ViewAppeared;

        /// <summary>
        /// Raises the ViewAppeared event. This is for internal use.
        /// </summary>
        internal virtual void RaiseViewAppearedEvent()
        {
            ViewAppeared?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Reaction for ViewAppeared event. This is empty by default. Subclasses may override this method.
        /// </summary>
        protected virtual void OnViewAppeared()
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

        /// <summary>
        /// A command to invoked to close the view.
        /// </summary>
        public ICommand ReturnCommand { get; protected set; }
    }
}
