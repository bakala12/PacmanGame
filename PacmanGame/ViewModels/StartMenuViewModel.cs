using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Commander;
using Microsoft.Win32;
using PacmanGame.MainInterfaces;
using PacmanGame.Serialization;

namespace PacmanGame.ViewModels
{
    public class StartMenuViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of StartMenuViewModel class
        /// </summary>
        public StartMenuViewModel() : base("StartMenu")
        {
        }

        [OnCommand("NewGameCommand")]
        protected virtual void NewGame(GameState state = null)
        {
            IViewModelChanger changer =(Application.Current as App)?.ViewModelChanger;
            GameViewModel gvm = changer?.GetViewModelByName("Game") as GameViewModel;
            gvm?.StartGame(state);
            changer?.ChangeCurrentViewModel("Game");
        }

        [OnCommand("LoadGameCommand")]
        protected virtual void LoadGame()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pacman game state (*.pacsv)|*.pacsv";
            if (dialog.ShowDialog() == true)
            {
                GameSerializer serializer = new GameSerializer();
                var state = serializer.LoadGame(Path.GetFullPath(dialog.FileName));
                NewGame(state);
            }
        }

        [OnCommand("ShowHighscoresCommand")]
        protected virtual void ShowHighscores()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Highscores");
        }

        [OnCommand("ShowOptionsCommand")]
        protected virtual void ShowOptions()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Options");
        }

        [OnCommand("ShowHelpCommand")]
        protected virtual void ShowHelp()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Help");
        }

        [OnCommand("ExitCommand")]
        protected virtual void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
