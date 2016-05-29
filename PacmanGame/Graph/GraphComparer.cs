using System;
using System.Collections.Generic;

namespace PacmanGame.Graph
{
    /// <summary>
    /// Simple implementation of IComparer for integer elements used in A* proiority queue.
    /// </summary>
    internal class GraphComparer : IComparer<int>
    {
        private readonly int[] _dist;

        /// <summary>
        /// Initializes a new instance of GraphComparer with the given distance's array used in A* algorithm.
        /// </summary>
        /// <param name="dist">An array of vertices' distances used in A* algorithm</param>
        public GraphComparer(int[] dist)
        {
            if (dist == null) throw new ArgumentNullException(nameof(dist));
            _dist = dist;
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than,
        /// equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of x and y, as shown in the
        /// following table.Value Meaning Less than zerox is less than y.Zerox equals y.Greater
        /// than zerox is greater than y.</returns>
        public int Compare(int x, int y)
        {
            return _dist[x] - _dist[y];
        }
    }
}