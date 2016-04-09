using System.Text;
using System.Threading.Tasks;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    public interface IViewModelChanger
    {
        ViewModelBase CurrentViewModel { get; }
        void ChangeCurrentViewModel(string name);
    }
}
