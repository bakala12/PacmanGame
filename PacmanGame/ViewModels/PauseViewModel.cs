using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Commander;
using Microsoft.Win32;
using PacmanGame.MainInterfaces;
using PacmanGame.Serialization;

namespace PacmanGame.ViewModels
{
    public class PauseViewModel : CloseableViewModel
    {
        private readonly IViewModelChanger _viewModelChanger;
        private readonly IGameSerializer _gameSerializer;

        public PauseViewModel(IViewModelChanger viewModelChanger, IGameSerializer gameSerializer) : base("Pause")
        {
            if(viewModelChanger == null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
            if(gameSerializer == null) throw new ArgumentNullException(nameof(gameSerializer));
            _gameSerializer = gameSerializer;
        }

        [OnCommand("SaveGameCommand")]
        public virtual void SaveGame()
        {
            var gvm = _viewModelChanger?.GetViewModelByName("Game");
            var state = (gvm as GameViewModel)?.GameEngine?.SaveState();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Stan gry pacman (*.pacsv) | *.pacsv";
            if (dialog.ShowDialog() == true)
            {
                string path = Path.GetFullPath(dialog.FileName);
                _gameSerializer.SaveGame(state, path);
                MessageBox.Show("Zapisano");
            }
        }

        [OnCommand("BackToMenuCommand")]
        public virtual void BackToMenu()
        {
            if (MessageBoxResult.OK == MessageBox.Show("Jeśli przejdziesz do menu stracisz niezapisany postęp gry", "Uwaga", MessageBoxButton.OKCancel))
            {
                _viewModelChanger?.ChangeCurrentViewModel("StartMenu");
            }
        }
    }
}
