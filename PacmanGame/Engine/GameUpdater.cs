using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Graph;

namespace PacmanGame.Engine
{
    internal class GameUpdateUpdateChecker : IGameUpdateChecker
    {
        private readonly GameBoard _gameBoard;
        private readonly IGraph _graph;

        public GameUpdateUpdateChecker(GameBoard board)
        {
            _gameBoard = board;
            _graph = new Graph.Graph(board);
        }

        public bool CheckCollision(IGameElement element1, IGameElement element2)
        {
            Rect rect1 = new Rect(new Point(element1.X, element1.Y), new Size(1,1));
            Rect rect2 = new Rect(new Point(element2.X, element2.Y), new Size(1,1));
            return rect1.IntersectsWith(rect2);
        }

        public bool CheckMovement(MovableElement movable, Direction direction)
        {
            if (direction == Direction.None) return false;
            Rect rect = TryMove(movable, direction);
            return CheckBoardMovementPossibility(rect, _gameBoard) && CheckMovement(movable, direction, _gameBoard);
        }

        private static Rect TryMove(MovableElement movable, Direction direction)
        {
            Rect rect1 = new Rect(new Point(movable.X, movable.Y), new Size(1,1));
            double speed = movable.Speed;
            switch (direction)
            {
                case Direction.Up:
                    rect1.Offset(-speed, 0);
                    break;
                case Direction.Down:
                    rect1.Offset(speed, 0);
                    break;                   
                case Direction.Left:
                    rect1.Offset(0, -speed);
                    break;
                case Direction.Right:
                    rect1.Offset(0, speed);
                    break;
            }
            return rect1;
        }

        private static bool CheckBoardMovementPossibility(Rect rect, GameBoard gameBoard)
        {
            Rect boardRect = new Rect(new Point(0,0), new Size(gameBoard.Rows, gameBoard.Columns));
            return boardRect.Contains(rect);
        }

        private bool CheckMovement(MovableElement movable, Direction direction, GameBoard board)
        {
            int ind = (int)(((int)movable.X) * board.Rows + (int)movable.Y);
            var neighbours = _graph.Neighbours[ind];
            switch (direction)
            {
                case Direction.Left:
                    return neighbours.Contains(ind - 1);
                case Direction.Right:
                    return neighbours.Contains(ind + 1);
                case Direction.Up:
                    return neighbours.Contains((int)(ind - board.Rows));
                case Direction.Down:
                    return neighbours.Contains((int) (ind + board.Rows));
                default:
                    return false;
            }
        }
    }
}
