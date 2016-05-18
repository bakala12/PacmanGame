using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Graph;
using Xunit;

namespace PacmanProjectTests
{
    public class PriorityQueueTests
    {
        class Comp : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return y - x;
            }
        }

        [Fact]
        private void TestInsertToEmptyQueue()
        {
            IPriorityQueue<int> queue = new PriorityQueue<int>(new Comp());
            queue.Insert(2);
            queue.Insert(3);
            queue.Insert(5);
            Assert.Equal(2, queue.DeleteFirst());
            Assert.Equal(3, queue.DeleteFirst());
            Assert.Equal(5, queue.DeleteFirst());
        }

        [Theory]
        [InlineData(new int[] {3, 4, 5, 1})]
        [InlineData(new int[] {2, 1, 5, 6, 0, 18})]
        [InlineData(new int[] {4, 3, 2, 1})]
        private void TestInsertingIntoQueue(int[] tab)
        {
            IPriorityQueue<int> queue = new PriorityQueue<int>(new Comp());
            foreach (var i in tab)
            {
                queue.Insert(i);
            }
            var sorted = tab.OrderBy(x=>x);
            foreach (var i in sorted)
            {
                Assert.Equal(i, queue.DeleteFirst());
            }
        }

        [Theory]
        [InlineData(new int[] {})]
        [InlineData(new int[] {3, 4, 5, 1})]
        [InlineData(new int[] {2, 1, 5, 6, 1, 18})]
        [InlineData(new int[] {4, 3, 2, 1})]
        private void TestContainsAndIsEmpty(int[] tab)
        {
            IPriorityQueue<int> queue = new PriorityQueue<int>(new Comp());
            Assert.Equal(true, queue.IsEmpty);
            foreach (var i in tab)
            {
                queue.Insert(i);
            }
            foreach (var s in tab.OrderBy(x=>x))
            {
                Assert.Equal(true, queue.Contains(s));
                Assert.Equal(s,queue.DeleteFirst());
            }
            Assert.Equal(true, queue.IsEmpty);
        }
    }
}
