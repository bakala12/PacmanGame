using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Commander;
using PacmanGame.Highscores;

namespace PacmanGame.ViewModels
{
    public class EndGameViewModel : CloseableViewModel
    {
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
        }

        public uint Points { get; set; }

        public TimeSpan GameTime { get; set; }

        public string PlayerName { get; set; }

        public bool IsHighscore { get; protected set; }

        [OnCommand("SaveCommand")]
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
