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

namespace PacmanGame.Engine
{
    internal class GameUpdateUpdateChecker : IGameUpdateChecker
    {
        public bool CheckCollision(IGameElement element1, IGameElement element2)
        {
            Rect rect1 = new Rect(new Point(element1.X, element1.Y), new Size(element1.ElementWidth, element1.ElementHeight));
            Rect rect2 = new Rect(new Point(element2.X, element2.Y), new Size(element2.ElementWidth, element2.ElementHeight));
            return rect1.IntersectsWith(rect2);
        }

        public bool CheckMovement(MovableElement movable, GameBoard gameBoard, Direction direction)
        {
            if (direction == Direction.None) return false;
            Rect rect = TryMove(movable, direction);
            return CheckBoardMovementPossibility(rect, gameBoard) &&
                gameBoard.Elements.OfType<Block>().Any(b => CheckCollision(b, rect));
        }

        private static Rect TryMove(MovableElement movable, Direction direction)
        {
            Rect rect1 = new Rect(new Point(movable.X, movable.Y), new Size(movable.ElementWidth, movable.ElementHeight));
            double speed = movable.Speed;
            switch (direction)
            {
                case Direction.Left:
                    rect1.Offset(-speed, 0);
                    break;
                case Direction.Right:
                    rect1.Offset(speed, 0);
                    break;                   
                case Direction.Up:
                    rect1.Offset(0, -speed);
                    break;
                case Direction.Down:
                    rect1.Offset(0, speed);
                    break;
            }
            return rect1;
        }

        private static bool CheckCollision(IGameElement element, Rect rect)
        {
            Rect rect1 = new Rect(new Point(element.X, element.Y), new Size(element.ElementWidth, element.ElementHeight));
            return rect1.IntersectsWith(rect);
        }

        private static bool CheckBoardMovementPossibility(Rect rect, GameBoard gameBoard)
        {
            Rect boardRect = new Rect(new Point(0,0), new Size(gameBoard.Columns, gameBoard.Rows));
            return boardRect.Contains(rect);
        }
    }
}
