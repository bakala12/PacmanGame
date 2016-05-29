using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Highscores
{
    /// <summary>
    /// An implementation of ICompares to compare Highscore objects.
    /// </summary>
    public class HighscoreComparer : IComparer<Highscore>
    {
        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than,
        /// equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of x and y, as shown in the
        /// following table.Value Meaning Less than zerox is less than y.Zerox equals y.Greater
        /// than zerox is greater than y.</returns>
        public int Compare(Highscore x, Highscore y)
        {
            int r = (int)x.Points - (int)y.Points;
            if (r != 0) return -r;
            return x.GameTime > y.GameTime ? 1 : -1;
        }
    }
}
