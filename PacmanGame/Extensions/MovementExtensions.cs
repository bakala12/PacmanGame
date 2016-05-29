using System;
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
    /// <summary>
    /// Usefull set of extension methods for MovableElement and IGameMovementChecker.
    /// </summary>
    public static class MovementExtensions
    {
        /// <summary>
        /// Moves the element via given portal from one endpoint to another.
        /// </summary>
        /// <param name="movable">The element which should be moved.</param>
        /// <param name="portal">A portal object which gives information about where to move.</param>
        public static void MoveViaPortal(this MovableElement movable, Portal portal)
        {
            if(movable == null) return;
            if (portal?.ConnectedPortal != null)
            {
                movable.X = portal.ConnectedPortal.X;
                movable.Y = portal.ConnectedPortal.Y;
            }
        }

        /// <summary>
        /// Looks for collisions between the given element and the set of other elements.
        /// </summary>
        /// <param name="checker">The IGameMovementChecker instance.</param>
        /// <param name="element">The element for testing collisions.</param>
        /// <param name="obstacles">Another elements to test collision with the given element.</param>
        /// <returns>True if the element has collision with at least one from the given obstacles, otherwise false.</returns>
        public static bool CheckCollision(this IGameMovementChecker checker, IGameElement element,
            IEnumerable<IGameElement> obstacles)
        {
            return checker.CheckCollision(element, obstacles, false);
        }

        /// <summary>
        /// Looks for collisions between the given element and the set of other elements.
        /// </summary>
        /// <param name="checker">The IGameMovementChecker instance.</param>
        /// <param name="element">The element for testing collisions.</param>
        /// <param name="obstacles">Another elements to test collision with the given element.</param>
        /// <param name="checkElementInObstacles">Specifies whether the testing element is in the given collection of obstacles.
        /// If true it wouldn't be testet for collision.</param>
        /// <returns>True if the element has collision with at least one from the given obstacles, otherwise false.</returns>
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
