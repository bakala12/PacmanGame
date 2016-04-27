using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PacmanGame.ViewModels
{
    public class PauseViewModel : CloseableViewModel
    {
        public PauseViewModel() : base("Pause")
        {
            BackToMenuCommand = new DelegateCommand(x=>BackToMenu());
            SaveGameCommand = new DelegateCommand(x=> SaveGame());
        }

        public ICommand SaveGameCommand { get; }
        public ICommand BackToMenuCommand { get; }

        public virtual void SaveGame()
        {
            IViewModelChanger changer = (Application.Current as App)?.ViewModelChanger;
            var gvm = changer?.GetViewModelByName("Game");
            //var state = (gvm as GameViewModel)?.GameState;
            //Saving with dedicating class
            MessageBox.Show("Zapisano");
        }

        public virtual void BackToMenu()
        {
            if (MessageBoxResult.OK == MessageBox.Show("Jeśli przejdziesz do menu stracisz niezapisany postęp gry", "Uwaga", MessageBoxButton.OKCancel))
            {
                IViewModelChanger changer = (Application.Current as App)?.ViewModelChanger;
                changer?.ChangeCurrentViewModel("StartMenu");
            }
        }
    }
}
