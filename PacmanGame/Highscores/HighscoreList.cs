using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Properties;
using PacmanGame.ViewModels;
using PropertyChanged;

namespace PacmanGame.Highscores
{
    [ImplementPropertyChanged]
    public class HighscoreList
    {
        private readonly IComparer<Highscore> _comparer = new HighscoreComparer();
        private uint _rememberedHighscoresCount;
        private ISettingsProvider _provider;

        public HighscoreList(ISettingsProvider provider)
        {
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
            RefreshList();
        }

        public List<Highscore> Highscores { get; protected set; }

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
            _provider.Highscores = Highscores.ToList();
            _provider.Save();
            RefreshList();
        }

        public void RefreshList()
        {
            Highscores = _provider.Highscores ?? new List<Highscore>();
            _rememberedHighscoresCount = _provider.RememberedHighscoresCount;
            RemoveRendundantElements();
        }

        public bool IsHighscore(Highscore highscore)
        {
            return Highscores.Count < _rememberedHighscoresCount || _comparer.Compare(highscore, Highscores.Last()) <0;
        }

        private void RemoveRendundantElements()
        {
            Highscores.Sort(_comparer);
            if (Highscores.Count <= _rememberedHighscoresCount) return;
            for(int i=(int)_rememberedHighscoresCount; i<Highscores.Count; i++)
                Highscores.RemoveAt(i);
        }
    }
}
