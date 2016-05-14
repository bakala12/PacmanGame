using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;

namespace PacmanGame
{
    public interface ISettingsProvider
    {
        Key LeftKey { get; set; }
        Key RightKey { get; set; }
        Key UpKey { get; set; }
        Key DownKey { get; set; }
        Key PauseKey { get; }
        List<Highscore> Highscores { get; set; }
        uint RememberedHighscoresCount { get; }
        uint EnemyMovementInterval { get; set; }
        void Save();
    }
}
