using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Highscores
{
    public class Highscore
    {
        private TimeSpan _gameTime;
        private uint _time;

        public string PlayerName { get; set; }
        public uint Points { get; set; }

        public TimeSpan GameTime
        {
            get { return _gameTime; }
            set
            {
                _gameTime = value;
                Time = (uint)_gameTime.Seconds;
            }
        }

        public uint Time
        {
            get { return _time; }
            set { _time = value; _gameTime = TimeSpan.FromSeconds(_time); }
        }
    }
}
