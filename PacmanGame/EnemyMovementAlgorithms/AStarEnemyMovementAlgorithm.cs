using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.Graph;

namespace PacmanGame.EnemyMovementAlgorithms
{
    public class AStarEnemyMovementAlgorithm : IMovementAlgorithm
    {
        private readonly IGraph _graph;

        public AStarEnemyMovementAlgorithm(IGraph graph)
        {

        }

        public Direction ProvideDirection(Enemy enemy)
        {
            return Direction.Down;
        }
    }
}
