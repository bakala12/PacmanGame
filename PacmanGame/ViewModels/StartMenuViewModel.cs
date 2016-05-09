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
        private readonly IViewModelChanger _viewModelChanger;
        private readonly IGameSerializer _gameSerializer;

        public StartMenuViewModel(IViewModelChanger viewModelChanger, IGameSerializer gameSerializer) : base("StartMenu")
        {
            if (viewModelChanger == null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
            if (gameSerializer == null) throw new ArgumentNullException(nameof(gameSerializer));
            _gameSerializer = gameSerializer;
        }

        [OnCommand("NewGameCommand")]
        protected virtual void NewGame(GameState state = null)
        {
            GameViewModel gvm = _viewModelChanger?.GetViewModelByName("Game") as GameViewModel;
            gvm?.StartGame(state);
            _viewModelChanger?.ChangeCurrentViewModel("Game");
        }

        [OnCommand("LoadGameCommand")]
        protected virtual void LoadGame()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pacman game state (*.pacsv)|*.pacsv";
            if (dialog.ShowDialog() == true)
            {
                var state = _gameSerializer.LoadGame(Path.GetFullPath(dialog.FileName));
                NewGame(state);
            }
        }

        [OnCommand("ShowHighscoresCommand")]
        protected virtual void ShowHighscores()
        {
            _viewModelChanger.ChangeCurrentViewModel("Highscores");
        }

        [OnCommand("ShowOptionsCommand")]
        protected virtual void ShowOptions()
        {
            _viewModelChanger.ChangeCurrentViewModel("Options");
        }

        [OnCommand("ShowHelpCommand")]
        protected virtual void ShowHelp()
        {
            _viewModelChanger.ChangeCurrentViewModel("Help");
        }

        [OnCommand("ExitCommand")]
        protected virtual void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
