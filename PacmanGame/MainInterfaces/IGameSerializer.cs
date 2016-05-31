using PacmanGame.Serialization;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// An interface that supports serialization of GameState object.
    /// </summary>
    public interface IGameSerializer
    {
        /// <summary>
        /// Loads the GameState from file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The loaded GameState object.</returns>
        GameState LoadGame(string path);
        /// <summary>
        /// Saves the given GameState object to the file.
        /// </summary>
        /// <param name="state">The GameState object to be saved.</param>
        /// <param name="path">The path to the file in which GameState would be stored.</param>
        void SaveGame(GameState state, string path);
    }
}