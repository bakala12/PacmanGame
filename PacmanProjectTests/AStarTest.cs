using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacmanGame.Graph;
using Xunit;

namespace PacmanProjectTests
{
    public class AStarTest
    {
        private IGraph _graph;

        public AStarTest()
        {
            bool[,] blocks = new bool[4, 4]
            {
                {false, false, false, true },
                {false,  true, false, false },
                {false,false, true, false },
                { true, false,false,false}
            };

            _graph = new Graph(blocks);
        }

        [Theory]
        [InlineData(0, 9, 3)]
        [InlineData(1,2, 1)]
        [InlineData(5,5,0)]
        [InlineData(2,14,5)]
        private void AStarAlghorithTest(int s, int e, int solution)
        {
            int sol;
            _graph.AStar(s, e, out sol);
            Assert.Equal(solution,sol);
        }
    }
}
