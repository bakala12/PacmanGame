using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;

namespace PacmanGame.ViewModels
{
    public class HighscoresViewModel : CloseableViewModel
    {
        private HighscoreList _highscoreList;

        public HighscoresViewModel() : base("Highscores")
        {
            ClearHighscoresCommand = new DelegateCommand(x => HighscoreList.ClearAllHighscores());
        }

        public HighscoreList HighscoreList
        {
            get { return _highscoreList; }
            set { _highscoreList = value; OnPropertyChanged(); }
        }

        public ICommand ClearHighscoresCommand { get; }
    }
}
