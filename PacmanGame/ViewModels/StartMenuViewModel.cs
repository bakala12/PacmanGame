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
    /// <summary>
    /// Represents a view model associated with the start menu view.
    /// </summary>
    public class StartMenuViewModel : ViewModelBase
    {
        private readonly IViewModelChanger _viewModelChanger;
        private readonly IGameSerializer _gameSerializer;

        /// <summary>
        /// Initializes a new instance of StartMenuViewModel object.
        /// </summary>
        /// <param name="viewModelChanger">An object that changes the views in the application.</param>
        /// <param name="gameSerializer">An object that serializes the game state.</param>
        public StartMenuViewModel(IViewModelChanger viewModelChanger, IGameSerializer gameSerializer) : base("StartMenu")
        {
            if (viewModelChanger == null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
            if (gameSerializer == null) throw new ArgumentNullException(nameof(gameSerializer));
            _gameSerializer = gameSerializer;
        }

        /// <summary>
        /// Starts new game.
        /// </summary>
        /// <param name="state">Game state. If the value is null then new game is started, otherwise the game is loaded from the state.</param>
        [OnCommand("NewGameCommand")]
        protected virtual void NewGame(GameState state = null)
        {
            GameViewModel gvm = _viewModelChanger?.GetViewModelByName("Game") as GameViewModel;
            gvm?.StartGame(state);
            _viewModelChanger?.ChangeCurrentViewModel("Game");
        }

        /// <summary>
        /// Loads the game.
        /// </summary>
        [OnCommand("LoadGameCommand")]
        protected virtual void LoadGame()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Pacman game state (*.pacsv)|*.pacsv";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var state = _gameSerializer.LoadGame(Path.GetFullPath(dialog.FileName));
                    NewGame(state);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Wystąpił błąd podczas ładowania stanu gry z pliku. Prawdopodobnie plik jest nieprawidłowy",
                        "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Navigate to the highscores view.
        /// </summary>
        [OnCommand("ShowHighscoresCommand")]
        protected virtual void ShowHighscores()
        {
            _viewModelChanger.ChangeCurrentViewModel("Highscores");
        }

        /// <summary>
        /// Navigate to the option view.
        /// </summary>
        [OnCommand("ShowOptionsCommand")]
        protected virtual void ShowOptions()
        {
            _viewModelChanger.ChangeCurrentViewModel("Options");
        }
        
        /// <summary>
        /// Exits the application.
        /// </summary>
        [OnCommand("ExitCommand")]
        protected virtual void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
