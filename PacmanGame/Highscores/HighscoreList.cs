using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Properties;
using PacmanGame.ViewModels;

namespace PacmanGame.Highscores
{
    public class HighscoreList : PropertyChangedNotifier
    {
        private List<Highscore> _highscores;
        private readonly IComparer<Highscore> _comparer = new HighscoreComparer();

        public HighscoreList()
        {
            RefreshList();
        }

        public List<Highscore> Highscores
        {
            get { return _highscores; }
            protected set { _highscores = value; OnPropertyChanged(); }
        }

        public void AddHighscore(Highscore highscore)
        {
            if(highscore==null)
                throw new ArgumentNullException(nameof(highscore));
            Highscores.Add(highscore);
            SaveChanges();
        }

        public void ClearAllHighscores()
        {
            Highscores.Clear();
            SaveChanges();
        }

        protected virtual void SaveChanges()
        {
            Settings.Default.Highscores = Highscores.ToList();
            Settings.Default.Save();
            RefreshList();
        }

        public void RefreshList()
        {
            Highscores = Settings.Default.Highscores ?? new List<Highscore>();
            Highscores.Sort(_comparer);
            OnPropertyChanged(nameof(Highscores));
        }

        public int GetPosition(Highscore highscore)
        {
            RefreshList();
            var highscoresCopy = new List<Highscore>(Highscores);
            highscoresCopy.Add(highscore);
            highscoresCopy.Sort();
            return highscoresCopy.IndexOf(highscore);
        }
    }
}
