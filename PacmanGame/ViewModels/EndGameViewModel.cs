using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;

namespace PacmanGame.ViewModels
{
    public class EndGameViewModel : CloseableViewModel
    {
        private string _playerName;
        private bool _isHighscore;
        private uint _points;
        private TimeSpan _gameTime;
        private readonly HighscoreList _highscores;

        protected override void OnViewAppeared()
        {
            _highscores.RefreshList();
            IsHighscore = _highscores.IsHighscore(CreateHighscore());
        }

        public EndGameViewModel(HighscoreList highscores) : base("EndGame")
        {
            _highscores = highscores;
            PlayerName = "";
            IsHighscore = false;
            SaveCommand = new DelegateCommand(x=>SaveHighscore());
        }

        public uint Points
        {
            get { return _points; }
            set { _points = value; OnPropertyChanged(); }
        }

        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set { _gameTime = value; OnPropertyChanged(); }
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; OnPropertyChanged(); }
        }

        public bool IsHighscore
        {
            get { return _isHighscore; }
            protected set { _isHighscore = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public virtual void SaveHighscore()
        {
            if(!IsHighscore) return;
            _highscores.AddHighscore(CreateHighscore());
            Close();
        }

        private Highscore CreateHighscore()
        {
            return new Highscore()
            {
                Points = Points,
                GameTime = GameTime,
                PlayerName = PlayerName
            };
        }
    }
}
