using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// A class that stores information about portal's connections in the hierarchical way.
    /// </summary>
    [Serializable]
    public class PortalConnetcionList : IEnumerable<Tuple<int,int>>
    {
        [NonSerialized]
        private readonly IEqualityComparer<Tuple<int,int>> _comparer = new TupleComparer();
        private List<Tuple<int, int>> _list;

        /// <summary>
        /// Initializes a new instance of PortalConnectionList object.
        /// </summary>
        public PortalConnetcionList()
        {
            _list=new List<Tuple<int, int>>();
        }
         
        /// <summary>
        /// Adds the specified item to the collection.
        /// </summary>
        /// <param name="item"></param>
        public void Add(Tuple<int, int> item)
        {
            if(!Contains(item))
                _list.Add(item);
        }

        /// <summary>
        /// Checks whether the collection contians specified item.
        /// </summary>
        /// <param name="item">Item to be checked.</param>
        /// <returns>True if collection contains element, otherwise false.</returns>
        public bool Contains(Tuple<int, int> item)
        {
            return _list.Any(x => _comparer.Equals(x, item));
        }

        #region IEnumerable implementation
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator object that can be used to iterate through
        /// the collection.</returns>
        public IEnumerator<Tuple<int, int>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An System.Collections.IEnumerator object that can be used to iterate through
        /// the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        #endregion
    }
}