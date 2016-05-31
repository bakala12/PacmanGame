using System;
using System.Collections.Generic;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// The comparer used in comparing tuples.
    /// </summary>
    internal class TupleComparer : IEqualityComparer<Tuple<int, int>>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type T to compare.</param>
        /// <param name="y">The second object of type T to compare.</param>
        /// <returns>True if the specified objects are equal; otherwise, false.</returns>
        public bool Equals(Tuple<int, int> x, Tuple<int, int> y)
        {
            return (x.Item1 == y.Item1 && x.Item2 == y.Item2) ||
                   (x.Item1 == y.Item2 && x.Item2 == y.Item1);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The System.Object for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        public int GetHashCode(Tuple<int, int> obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}