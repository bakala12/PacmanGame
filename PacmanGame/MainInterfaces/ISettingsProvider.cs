using System;
using System.Collections.Generic;
using System.Windows.Input;
using PacmanGame.Highscores;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// An interface that provides access to settings values used in the game. 
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Gets or sets the Key that move player in left direction.
        /// </summary>
        Key LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in right direction.
        /// </summary>
        Key RightKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in up direction.
        /// </summary>
        Key UpKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in down direction.
        /// </summary>
        Key DownKey { get; set; }
        /// <summary>
        /// Gets the key that turns the pause mode.
        /// </summary>
        Key PauseKey { get; }
        /// <summary>
        /// Gets or sets the list of game highscores.
        /// </summary>
        List<Highscore> Highscores { get; set; }
        /// <summary>
        /// Gets the number of stored highscores.
        /// </summary>
        uint RememberedHighscoresCount { get; }
        /// <summary>
        /// Gets or sets the time interval between two enemies' moves.
        /// </summary>
        uint EnemyMovementInterval { get; set; }
        /// <summary>
        /// Gets the width of the board.
        /// </summary>
        uint BoardWidth { get; }
        /// <summary>
        /// Gets the height of the board.
        /// </summary>
        uint BoardHeight { get; }
        /// <summary>
        /// Gets the path to the example gameboard file.
        /// </summary>
        string BoardFilePath { get; }
        /// <summary>
        /// Gets the time interval between two bonuses' generations.
        /// </summary>
        TimeSpan AdditionalLifeGenerationInterval { get; }
        /// <summary>
        /// Saves all the games settings.
        /// </summary>
        void Save();
    }
}
