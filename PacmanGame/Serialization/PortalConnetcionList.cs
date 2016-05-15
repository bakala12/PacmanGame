using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PacmanGame.Serialization
{
    [Serializable]
    public class PortalConnetcionList : IEnumerable<Tuple<int,int>>
    {
        [NonSerialized]
        private readonly IEqualityComparer<Tuple<int,int>> _comparer = new TupleComparer();
        private List<Tuple<int, int>> _list;

        public PortalConnetcionList()
        {
            _list=new List<Tuple<int, int>>();
        }
         
        public void Add(Tuple<int, int> item)
        {
            if(!Contains(item))
                _list.Add(item);
        }

        public bool Contains(Tuple<int, int> item)
        {
            return _list.Any(x => _comparer.Equals(x, item));
        }

        public IEnumerator<Tuple<int, int>> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }
    }
}