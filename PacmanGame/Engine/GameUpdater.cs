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
            return false;
        }
    }
}
