using System.Windows.Input;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// An interface that provide support to manage and change control keys in the game.
    /// </summary>
    public interface IHaveControlKeys
    {
        /// <summary>
        /// Gets or sets the Key that move player in left direction.
        /// </summary>
        Key LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in right direction.
        /// </summary>
        Key RightKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in up direction.
        /// </summary>
        Key UpKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in down direction.
        /// </summary>
        Key DownKey { get; set; }
        /// <summary>
        /// Loads the current configuration of control keys.
        /// </summary>
        void LoadControlKeys();
        /// <summary>
        /// Saves the current configuration of control keys.
        /// </summary>
        void SaveControlKeys();
    }
}
