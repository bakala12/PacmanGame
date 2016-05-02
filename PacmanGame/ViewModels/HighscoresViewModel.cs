using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;
using System.Windows;

namespace PacmanGame.ViewModels
{
    public class HighscoresViewModel : CloseableViewModel
    {
        private HighscoreList _highscoreList;

        protected override void OnViewAppeared()
        {
            HighscoreList.RefreshList();
            OnPropertyChanged(nameof(HighscoreList));
        }

        public HighscoresViewModel(HighscoreList highscoreList) : base("Highscores")
        {
            HighscoreList = highscoreList;
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
