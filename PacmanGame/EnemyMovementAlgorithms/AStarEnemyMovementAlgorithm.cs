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
    public class AStarEnemyMovementAlgorithm : IMovementAlgorithm
    {
        private readonly IGraph _graph;
        private readonly IGameElement _player;
        private readonly int _width;
        private readonly int _height;

        public AStarEnemyMovementAlgorithm(IGraph graph, IGameElement player, int width, int height)
        {
            if(graph==null) throw new ArgumentNullException(nameof(graph));
            _graph = graph;
            if(player == null) throw new ArgumentNullException(nameof(player));
            _player = player;
            _width = width;
            _height = height;
        }

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
