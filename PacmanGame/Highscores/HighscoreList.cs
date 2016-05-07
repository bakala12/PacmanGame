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
        private uint _rememberedHighscoresCount;

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
            RemoveRendundantElements();
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
            _rememberedHighscoresCount = Settings.Default.RememberedHighscoresCount;
            RemoveRendundantElements();
            Highscores.Sort(_comparer);
            OnPropertyChanged(nameof(Highscores));
        }

        public bool IsHighscore(Highscore highscore)
        {
            return Highscores.Count < _rememberedHighscoresCount || _comparer.Compare(highscore, Highscores.Last()) <0;
        }

        private void RemoveRendundantElements()
        {
            if (Highscores.Count <= _rememberedHighscoresCount) return;
            for(int i=(int)_rememberedHighscoresCount; i<Highscores.Count; i++)
                Highscores.RemoveAt(i);
            OnPropertyChanged(nameof(Highscores));
        }
    }
}
