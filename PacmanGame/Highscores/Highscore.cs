using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Highscores
{
    /// <summary>
    /// An object representing a highscore in the game.
    /// </summary>
    public class Highscore
    {
        private TimeSpan _gameTime;
        private uint _time;

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string PlayerName { get; set; }
        /// <summary>
        /// Gets or sets the amount of points.
        /// </summary>
        public uint Points { get; set; }
        /// <summary>
        /// Gets or sets the time of the game.
        /// </summary>
        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set
            {
                _gameTime = value;
                Time = (uint)_gameTime.TotalSeconds;
            }
        }
        /// <summary>
        /// Gets or sets the total game time in secunds.
        /// </summary>
        public uint Time
        {
            get { return _time; }
            set { _time = value; _gameTime = TimeSpan.FromSeconds(_time); }
        }
    }
}
