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
        public HighscoreList() : this(Settings.Default.Highscores ?? new List<Highscore>())
        {
        }

        public HighscoreList(IList<Highscore> highscores)
        {
            Highscores = new ObservableCollection<Highscore>(highscores);   
        }

        public ObservableCollection<Highscore> Highscores { get; }

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
        }
    }
}
