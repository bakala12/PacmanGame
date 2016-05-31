using GameControls.Board;
using GameControls.Interfaces;
using PacmanGame.Engine;
using PacmanGame.Serialization;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// An interface that provide support for building all core game objects.
    /// </summary>
    public interface IGameBuilder
    {
        /// <summary>
        /// Builds the GameBoard based on the given GameState.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <returns>The newly builded GameBoard.</returns>
        GameBoard BuildBoard(GameState gameState);
        /// <summary>
        /// Builds the timer used in the game.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <returns>THe builded timer used in the game.</returns>
        ITimer BuildTimer(GameState gameState);
        /// <summary>
        /// Builds the GameEngine which manages all core game features.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <param name="board">The GameBoard used in the game.</param>
        /// <param name="provider">The object that provide some configuration settings.</param>
        /// <returns>The GameEngine object used in the game.</returns>
        GameEngine BuildGameEngine(GameState gameState, GameBoard board, ISettingsProvider provider);
    }
}
