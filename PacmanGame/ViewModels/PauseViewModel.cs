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
    /// <summary>
    /// Represents a view model associated with pause view in the application.
    /// </summary>
    public class PauseViewModel : CloseableViewModel
    {
        private bool _savedGame;
        private readonly IViewModelChanger _viewModelChanger;
        private readonly IGameSerializer _gameSerializer;

        protected override void OnViewAppeared()
        {
            base.OnViewAppeared();
            _savedGame = false;
        }

        /// <summary>
        /// Initializes a new instance of PauseViewModel object.
        /// </summary>
        /// <param name="viewModelChanger">An object that changes the views in the application.</param>
        /// <param name="gameSerializer">An object that serializes the game state.</param>
        public PauseViewModel(IViewModelChanger viewModelChanger, IGameSerializer gameSerializer) : base("Pause")
        {
            if(viewModelChanger == null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
            if(gameSerializer == null) throw new ArgumentNullException(nameof(gameSerializer));
            _gameSerializer = gameSerializer;
        }

        /// <summary>
        /// Saves the game.
        /// </summary>
        [OnCommand("SaveGameCommand")]
        public virtual void SaveGame()
        {
            var gvm = _viewModelChanger?.GetViewModelByName("Game");
            var state = (gvm as GameViewModel)?.GameEngine?.SaveState();
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Stan gry pacman (*.pacsv) | *.pacsv";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    string path = Path.GetFullPath(dialog.FileName);
                    _gameSerializer.SaveGame(state, path);
                    _savedGame = true;
                    MessageBox.Show("Zapisano", string.Empty, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Zapis stanu gry nie powiódł się.", "Błąd", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Navigate back to the main menu.
        /// </summary>
        [OnCommand("BackToMenuCommand")]
        public virtual void BackToMenu()
        {
            if (_savedGame || (MessageBoxResult.OK == MessageBox.Show("Jeśli przejdziesz do menu stracisz niezapisany postęp gry. Czy na pewno chcesz powrócić do menu?" +
                                                       "", "Uwaga", MessageBoxButton.YesNoCancel, MessageBoxImage.Question)))
            {
                _viewModelChanger?.ChangeCurrentViewModel("StartMenu");
            }
        }
    }
}
