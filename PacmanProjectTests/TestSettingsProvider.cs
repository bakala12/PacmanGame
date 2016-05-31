using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Highscores;
using PacmanGame.MainInterfaces;

namespace PacmanProjectTests
{
    public class TestSettingsProvider : ISettingsProvider
    {
        public TestSettingsProvider()
        {
            LeftKey = Key.Left;
            RightKey = Key.Right;
            UpKey = Key.Up;
            DownKey = Key.Down;
            PauseKey = Key.Escape;
            Highscores = new List<Highscore>();
            RememberedHighscoresCount = 10;
            BoardWidth = BoardHeight = 4;
            BoardFilePath = null;
            AdditionalLifeGenerationInterval = TimeSpan.FromSeconds(60);
            EnemyMovementInterval = 500;
        }

        public Key LeftKey { get; set; }
        public Key RightKey { get; set; }
        public Key UpKey { get; set; }
        public Key DownKey { get; set; }
        public Key PauseKey { get; }
        public List<Highscore> Highscores { get; set; }
        public uint RememberedHighscoresCount { get; }
        public uint EnemyMovementInterval { get; set; }
        public uint BoardWidth { get; }
        public uint BoardHeight { get; }
        public string BoardFilePath { get; }
        public TimeSpan AdditionalLifeGenerationInterval { get; }

        public void Save()
        {
        }
    }
}
