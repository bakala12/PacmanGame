using System.Collections.Generic;
using GameControls.Elements;

namespace PacmanGame.Engine
{
    /// <summary>
    /// Represents an object that is responsible for moving enemies on the board.
    /// </summary>
    public interface IEnemyMovementManager
    {
        /// <summary>
        /// Gets the collection of the enemies.
        /// </summary>
        IEnumerable<Enemy> Enemies { get; }
        /// <summary>
        /// Moves all the enemies.
        /// </summary>
        void MoveEnemies();
        /// <summary>
        /// Starts working the IEnemyMovementManager.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops working the IEnemyMovementManager.
        /// </summary>
        void Stop();
        /// <summary>
        /// Increases the difficulty of the game.
        /// </summary>
        void NextLevel();
    }
}
