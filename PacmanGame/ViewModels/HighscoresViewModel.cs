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
    public class HighscoresViewModel : CloseableViewModel
    {
        protected override void OnViewAppeared()
        {
            HighscoreList.RefreshList();
        }

        public HighscoresViewModel(HighscoreList highscoreList) : base("Highscores")
        {
            HighscoreList = highscoreList;
        }

        public HighscoreList HighscoreList { get; set; }

        [OnCommand("ClearHighscoresCommand")]
        protected virtual void ClearAllHighscores()
        {
            HighscoreList.ClearAllHighscores();
        }
    }
}
