using PacmanGame.ViewModels;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// An interface that provide support for changing views in the application.
    /// </summary>
    public interface IViewModelChanger
    {
        /// <summary>
        /// Gets the view model associated with the currently displaying view in the application.
        /// </summary>
        ViewModelBase CurrentViewModel { get; }
        /// <summary>
        /// Changes currently displaying view.
        /// </summary>
        /// <param name="name">The name of the view to be displayed.</param>
        void ChangeCurrentViewModel(string name);
        /// <summary>
        /// Gets the ViewModelBase object specified by the given name.
        /// </summary>
        /// <param name="name">The name f the view model.</param>
        /// <returns>A view model associated with the given name.</returns>
        ViewModelBase GetViewModelByName(string name);
    }
}
