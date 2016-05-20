﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.MainInterfaces;

namespace PacmanGame.Extensions
{
    public static class MovementExtensions
    {
        public static void MoveViaPortal(this MovableElement movable, Portal portal)
        {
            if(movable == null) return;
            if (portal?.ConnectedPortal != null)
            {
                movable.X = portal.ConnectedPortal.X;
                movable.Y = portal.ConnectedPortal.Y;
            }
        }

        public static bool CheckCollision(this IGameMovementChecker checker, IGameElement element,
            IEnumerable<IGameElement> obstacles)
        {
            return checker.CheckCollision(element, obstacles, false);
        }

        public static bool CheckCollision(this IGameMovementChecker checker, IGameElement element,
            IEnumerable<IGameElement> obstacles, bool checkElementInObstacles)
        {
            if (checker == null) throw new NullReferenceException(nameof(checker));
            if (element == null || obstacles == null) return false;
            IList<IGameElement> obst = obstacles.ToList();
            if (obst.Contains(element)) obst.Remove(element);
            return obst.Aggregate(true, (current, gameElement) => current && checker.CheckCollision(gameElement, element));
        }
    }
}
