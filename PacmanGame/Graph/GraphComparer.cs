using System;
using System.Collections.Generic;

namespace PacmanGame.Graph
{
    internal class GraphComparer : IComparer<int>
    {
        private readonly int[] _dist;

        public GraphComparer(int [] dist)
        {
            if(dist==null) throw new ArgumentNullException(nameof(dist));
            _dist = dist;
        }

        public int Compare(int x, int y)
        {
            return _dist[x] - _dist[y];
        }
    }
}