using PacmanGame.ViewModels;

namespace PacmanGame.MainInterfaces
{
    public interface IViewModelChanger
    {
        ViewModelBase CurrentViewModel { get; }
        void ChangeCurrentViewModel(string name);
        ViewModelBase GetViewModelByName(string name);
    }
}
