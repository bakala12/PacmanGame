using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Highscores
{
    public class HighscoreComparer : IComparer<Highscore>
    {
        public int Compare(Highscore x, Highscore y)
        {
            int r = (int)x.Points - (int)y.Points;
            if (r != 0) return -r;
            return x.GameTime > y.GameTime ? 1 : -1;
        }
    }
}
