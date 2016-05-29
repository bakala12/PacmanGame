using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;
using PacmanGame.ViewModels;
using PropertyChanged;

namespace PacmanGame.Highscores
{
    /// <summary>
    /// Represents the list of highscores in the game.
    /// </summary>
    [ImplementPropertyChanged]
    public class HighscoreList
    {
        private readonly IComparer<Highscore> _comparer = new HighscoreComparer();
        private uint _rememberedHighscoresCount;
        private readonly ISettingsProvider _provider;

        /// <summary>
        /// Initializes an new instance of HighscoreList.
        /// </summary>
        /// <param name="provider">An object which can provide some information about the list.</param>
        public HighscoreList(ISettingsProvider provider)
        {
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
            RefreshList();
        }

        /// <summary>
        /// Gets the list of highscores.
        /// </summary>
        public List<Highscore> Highscores { get; protected set; }

        /// <summary>
        /// Adds new highscore to the list.
        /// </summary>
        /// <param name="highscore">Highscore object to be added.</param>
        public void AddHighscore(Highscore highscore)
        {
            if(highscore==null)
                throw new ArgumentNullException(nameof(highscore));
            Highscores.Add(highscore);
            RemoveRendundantElements();
            SaveChanges();
        }

        /// <summary>
        /// Clears all highscores in the list.
        /// </summary>
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

        /// <summary>
        /// Refreshes the list of highscores.
        /// </summary>
        public void RefreshList()
        {
            Highscores = _provider.Highscores ?? new List<Highscore>();
            _rememberedHighscoresCount = _provider.RememberedHighscoresCount;
            RemoveRendundantElements();
        }

        /// <summary>
        /// Returns the information whether the given element can be a new highscore.
        /// </summary>
        /// <param name="highscore">The highscore to be tested.</param>
        /// <returns>True if the given element can be a new highscore, otherwise false.</returns>
        public bool IsNewHighscore(Highscore highscore)
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
