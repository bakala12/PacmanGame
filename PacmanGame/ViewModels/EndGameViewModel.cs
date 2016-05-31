using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Commander;
using PacmanGame.Highscores;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a view model associated with the EndGameView.
    /// </summary>
    public class EndGameViewModel : CloseableViewModel
    {
        private readonly HighscoreList _highscores;

        /// <summary>
        /// Overrides OnViewAppeared method and checking information about the highscore before the view appears.
        /// </summary>
        protected override void OnViewAppeared()
        {
            _highscores.RefreshList();
            IsHighscore = _highscores.IsNewHighscore(CreateHighscore());
            PlayerName = null;
        }

        /// <summary>
        /// Initializes a new instance of EndGameViewModel.
        /// </summary>
        /// <param name="highscores">The list of game highscores.</param>
        public EndGameViewModel(HighscoreList highscores) : base("EndGame")
        {
            _highscores = highscores;
            PlayerName = "";
            IsHighscore = false;
        }

        /// <summary>
        /// Gets or sets the number of points gained by the player.
        /// </summary>
        public uint Points { get; set; }

        /// <summary>
        /// Gets or sets the time of the game.
        /// </summary>
        public TimeSpan GameTime { get; set; }

        /// <summary>
        /// Gets or sets selected player name.
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// Gets the value indicating whether the current score is a new highscore.
        /// </summary>
        public bool IsHighscore { get; protected set; }

        /// <summary>
        /// Saves the current highscore.
        /// </summary>
        [OnCommand("SaveCommand")]
        public virtual void SaveHighscore()
        {
            if(!IsHighscore) return;
            _highscores.AddHighscore(CreateHighscore());
            Close();
        }

        [OnCommandCanExecute("SaveCommand")]
        private bool OnSaveCanExecute()
        {
            return !string.IsNullOrEmpty(PlayerName);
        }

        private Highscore CreateHighscore()
        {
            return new Highscore()
            {
                Points = Points,
                GameTime = GameTime,
                PlayerName = PlayerName
            };
        }
    }
}
