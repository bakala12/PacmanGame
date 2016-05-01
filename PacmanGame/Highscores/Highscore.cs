using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Highscores
{
    public class Highscore
    {
        public Highscore() { }

        public string PlayerName { get; set; }
        public uint Points { get; set; }
        public TimeSpan GameTime { get; set; }
    }
}
