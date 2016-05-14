using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;

namespace PacmanGame
{
    internal class SettingsProvider : ISettingsProvider
    {
        public SettingsProvider()
        {
            LeftKey = Settings.Default.LeftKey;
            RightKey = Settings.Default.RightKey;
            UpKey = Settings.Default.UpKey;
            DownKey = Settings.Default.DownKey;
            PauseKey = Settings.Default.PauseKey;
            Highscores = Settings.Default.Highscores;
            RememberedHighscoresCount = Settings.Default.RememberedHighscoresCount;
            EnemyMovementInterval = Settings.Default.EnemyMovementInterval;
        }

        public Key LeftKey { get; set; }
        public Key RightKey { get; set; }
        public Key UpKey { get; set; }
        public Key DownKey { get; set; }
        public Key PauseKey { get; }
        public List<Highscore> Highscores { get; set; }
        public uint RememberedHighscoresCount { get; }
        public uint EnemyMovementInterval { get; set; }

        public void Save()
        {
            Settings.Default.LeftKey = LeftKey;
            Settings.Default.RightKey = RightKey;
            Settings.Default.UpKey = UpKey;
            Settings.Default.DownKey = DownKey;
            Settings.Default.Highscores = Highscores;
            Settings.Default.EnemyMovementInterval = EnemyMovementInterval;
            Settings.Default.Save();
        }
    }
}
