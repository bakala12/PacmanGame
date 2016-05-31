using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// Provides support for checking collisions and movement possibility between elements on gameboard.
    /// </summary>
    public interface IGameMovementChecker
    {
        /// <summary>
        /// Checks collision between two elements.
        /// </summary>
        /// <param name="element1">The first element to check.</param>
        /// <param name="element2">The secund element to check.</param>
        /// <returns>True if the elements have collision, otherwise false.</returns>
        bool CheckCollision(IGameElement element1, IGameElement element2);
        /// <summary>
        /// Checks whether the specified element can be moved in the specified direction.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <param name="direction">Movement direction</param>
        /// <returns>True if the element can be moved in a specified direction, otherwise false.</returns>
        bool CheckMovement(MovableElement element, Direction direction);
        /// <summary>
        /// Checks whether there is the element of the specified type next to the given element in the specified direction.
        /// </summary>
        /// <typeparam name="T">Type of elements to check.</typeparam>
        /// <param name="movable">The element to be check.</param>
        /// <param name="direction">The direction in which elements would be tested.</param>
        /// <returns>True if there is the element of the specified type next to the given element in the specified direction, otherwise false.</returns>
        bool IsElementNextTo<T>(MovableElement movable, Direction direction) where T : ICanCollide;
    }
}