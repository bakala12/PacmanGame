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
        private List<Highscore> _highsocres;
        private readonly IComparer<Highscore> _comparer = new HighscoreComparer(); 

        public HighscoreList() : this(Settings.Default.Highscores ?? new List<Highscore>())
        {
        }

        public HighscoreList(IList<Highscore> highscores)
        {
            Highscores = new List<Highscore>(highscores);   
        }

        public List<Highscore> Highscores
        {
            get { return _highsocres; }
            protected set { _highsocres = value; OnPropertyChanged(); }
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
            Highscores.Sort(_comparer);
            OnPropertyChanged(nameof(Highscores));
            Settings.Default.Highscores = Highscores.ToList();
            Settings.Default.Save();
        }
    }
}
