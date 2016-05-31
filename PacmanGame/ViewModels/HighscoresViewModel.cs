using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;
using System.Windows;
using Commander;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a view model associated with the Highscores view.
    /// </summary>
    public class HighscoresViewModel : CloseableViewModel
    {
        protected override void OnViewAppeared()
        {
            HighscoreList.RefreshList();
        }

        /// <summary>
        /// Initializes a new instance of HighscoreViewModel.
        /// </summary>
        /// <param name="highscoreList">The list of highscores.</param>
        public HighscoresViewModel(HighscoreList highscoreList) : base("Highscores")
        {
            HighscoreList = highscoreList;
        }

        /// <summary>
        /// Gets or sets the list of highscores.
        /// </summary>
        public HighscoreList HighscoreList { get; set; }

        [OnCommand("ClearHighscoresCommand")]
        protected virtual void ClearAllHighscores()
        {
            HighscoreList.ClearAllHighscores();
        }
    }
}
