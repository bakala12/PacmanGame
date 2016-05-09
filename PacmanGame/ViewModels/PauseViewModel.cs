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
        public PauseViewModel() : base("Pause")
        {
        }

        [OnCommand("SaveGameCommand")]
        public virtual void SaveGame()
        {
            IViewModelChanger changer = (Application.Current as App)?.ViewModelChanger;
            var gvm = changer?.GetViewModelByName("Game");
            var state = (gvm as GameViewModel)?.GameEngine?.SaveState();
            GameSerializer serializer = new GameSerializer();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Stan gry pacman (*.pacsv) | *.pacsv";
            if (dialog.ShowDialog() == true)
            {
                string path = Path.GetFullPath(dialog.FileName);
                serializer.SaveGame(state, path);
                MessageBox.Show("Zapisano");
            }
        }

        [OnCommand("BackToMenuCommand")]
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
