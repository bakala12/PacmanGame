using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Graph
{
    public interface IPriorityQueue<T>
    {
        bool IsEmpty { get; }
        void Insert(T item);
        T DeleteFirst();
        bool Contains(T item);
    }

    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly IComparer<T> _comparer;

        public PriorityQueue(IComparer<T> comparer)
        {
            if(comparer == null) throw new ArgumentNullException(nameof(comparer));
            _comparer = comparer;
        }

        public bool IsEmpty => _list.Count == 0;

        public void Insert(T item)
        {
            int i = 0;
            while (i < _list.Count && _comparer.Compare(item, _list[i]) < 0) i++;
            _list.Insert(i, item);
        }

        public T DeleteFirst()
        {
            if (IsEmpty) return default(T);
            var item = _list[0];
            _list.RemoveAt(0);
            return item;
        }

        public bool Contains(T item)
        {
            return _list.Contains(item);
        }
    }

    public class GraphComparer : IComparer<int>
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
