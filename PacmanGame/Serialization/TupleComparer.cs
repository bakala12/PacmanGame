using System;
using System.Collections.Generic;

namespace PacmanGame.Serialization
{
    public class TupleComparer : IEqualityComparer<Tuple<int, int>>
    {
        public bool Equals(Tuple<int, int> x, Tuple<int, int> y)
        {
            return (x.Item1 == y.Item1 && x.Item2 == y.Item2) ||
                   (x.Item1 == y.Item2 && x.Item2 == y.Item1);
        }

        public int GetHashCode(Tuple<int, int> obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}