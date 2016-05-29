using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.Graph;
using PacmanGame.MainInterfaces;

namespace PacmanGame.EnemyMovementAlgorithms
{
    /// <summary>
    /// An implementation of enemy movement using A* algorithm.
    /// </summary>
    public class AStarEnemyMovementAlgorithm : IMovementAlgorithm
    {
        private readonly IGraph _graph;
        private readonly IGameElement _player;
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Initializes a new instance of AStarEnemyMovementAlgorithm object.
        /// </summary>
        /// <param name="graph">Graph representing the gameboard.</param>
        /// <param name="player">An element representing player.</param>
        /// <param name="width">Width of the board (number of columns).</param>
        /// <param name="height">Hight of the board (number of rows).</param>
        public AStarEnemyMovementAlgorithm(IGraph graph, IGameElement player, int width, int height)
        {
            if(graph==null) throw new ArgumentNullException(nameof(graph));
            _graph = graph;
            if(player == null) throw new ArgumentNullException(nameof(player));
            _player = player;
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Implementation of IMovementAlgorithm interface. 
        /// Provides a direction of movement for the given enemy.
        /// </summary>
        /// <param name="enemy">The enemy that should be moved.</param>
        /// <returns>The suggested direction of movement for the given enemy.</returns>
        public Direction ProvideDirection(Enemy enemy)
        {
            int start = _width*(int)enemy.X + (int)enemy.Y;
            int end = _width*(int)_player.X + (int)_player.Y;
            int solution;
            int m = _graph.AStar(start, end, out solution);
            if (m - start == -1) return Direction.Left;
            if (m - start == 1) return Direction.Right;
            if (m - start < 0) return Direction.Up;
            if (m - start > 0) return Direction.Down;
            return Direction.None;
        }
    }
}
