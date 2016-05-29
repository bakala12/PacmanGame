using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Graph
{
    /// <summary>
    /// A generic implementation of IPriorityQueue interface.
    /// </summary>
    /// <typeparam name="T">A type of element in the queue.</typeparam>
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private readonly List<T> _list = new List<T>();
        private readonly IComparer<T> _comparer;

        /// <summary>
        /// Initializes a new instance of the PriorityQueue with the given comparer.
        /// </summary>
        /// <param name="comparer">A comparer used to compare element's priorities in the queue.</param>
        public PriorityQueue(IComparer<T> comparer)
        {
            if(comparer == null) throw new ArgumentNullException(nameof(comparer));
            _comparer = comparer;
        }

        /// <summary>
        /// Gets the information whether the queue is empty or not.
        /// </summary>
        public bool IsEmpty => _list.Count == 0;

        /// <summary>
        /// Inserts the item to the queue.
        /// </summary>
        /// <param name="item">An element which should be inserted into the queue.</param>
        public void Insert(T item)
        {
            int i = 0;
            while (i < _list.Count && _comparer.Compare(item, _list[i]) < 0) i++;
            _list.Insert(i, item);
        }

        /// <summary>
        /// Deletes first item in the queue and returns it.
        /// </summary>
        /// <returns>Returns the deleted element.</returns>
        public T DeleteFirst()
        {
            if (IsEmpty) return default(T);
            var item = _list[0];
            _list.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Informs whether the specified item is in the queue.
        /// </summary>
        /// <param name="item">The item which should be tested.</param>
        /// <returns>True if the queue contains the specified item, otherwise false.</returns>
        public bool Contains(T item)
        {
            return _list.Contains(item);
        }
    }
}
