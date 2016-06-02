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
    /// <summary>
    /// An implementation of ISettingsProvider used in the game. This stores the settings in Visual Studio settings.
    /// </summary>
    internal class SettingsProvider : ISettingsProvider
    {
        /// <summary>
        /// Initializes a new instance of SettingsProvider.
        /// </summary>
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
            BoardWidth = Settings.Default.BoardWidth;
            BoardHeight = Settings.Default.BoardHeight;
            BoardFilePath = Settings.Default.BoardFilePath;
            AdditionalLifeGenerationInterval = Settings.Default.AdditionalLifeGenerationInterval;
            EnemyDifficultyIncreaseSpeed = Settings.Default.EnemyDifficultyIncreaseSpeed;
        }
        /// <summary>
        /// Gets or sets the Key that move player in left direction.
        /// </summary>
        public Key LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in right direction.
        /// </summary>
        public Key RightKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in up direction.
        /// </summary>
        public Key UpKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in down direction.
        /// </summary>
        public Key DownKey { get; set; }
        /// <summary>
        /// Gets the key that turns the pause mode.
        /// </summary>
        public Key PauseKey { get; }
        /// <summary>
        /// Gets or sets the list of game highscores.
        /// </summary>
        public List<Highscore> Highscores { get; set; }
        /// <summary>
        /// Gets the number of stored highscores.
        /// </summary>
        public uint RememberedHighscoresCount { get; }
        /// <summary>
        /// Gets or sets the time interval between two enemies' moves.
        /// </summary>
        public uint EnemyMovementInterval { get; set; }
        /// <summary>
        /// Gets the width of the board.
        /// </summary>
        public uint BoardWidth { get; }
        /// <summary>
        /// Gets the height of the board.
        /// </summary>
        public uint BoardHeight { get; }
        /// <summary>
        /// Gets the path to the example gameboard file.
        /// </summary>
        public string BoardFilePath { get; }
        /// <summary>
        /// Gets the time interval between two bonuses' generations.
        /// </summary>
        public TimeSpan AdditionalLifeGenerationInterval { get; }
        /// <summary>
        /// Gets the time interval which is substracted from the enemy movement time interval on each 
        /// increasement of game difficulty.
        /// </summary>
        public uint EnemyDifficultyIncreaseSpeed { get; }
        /// <summary>
        /// Saves all the games settings.
        /// </summary>
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
