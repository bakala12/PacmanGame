using PacmanGame.Graph;
using Xunit;

namespace PacmanProjectTests.GraphTests
{
    /// <summary>
    /// Tests for A* algorithm.
    /// </summary>
    public class AStarTest
    {
        private readonly IGraph _graph1;

        public AStarTest()
        {
            bool[,] blocks = new bool[4, 4]
            {
                {false, false, false, true },
                {false,  true, false, false },
                {false,false, true, false },
                { true, false,false,false}
            };
            _graph1 = new Graph(blocks);
        }

        [Theory]
        [InlineData(0, 9, 3)]
        [InlineData(1,2, 1)]
        [InlineData(5,5,0)]
        [InlineData(2,14,5)]
        private void AStarAlghorithTest(int s, int e, int solution)
        {
            int sol;
            _graph1.AStar(s, e, out sol);
            Assert.Equal(solution,sol);
        }
    }
}
