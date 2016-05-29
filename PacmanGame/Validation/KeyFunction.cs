using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Validation
{
    /// <summary>
    /// An enum representing a function of the key in the game.
    /// </summary>
    public enum KeyFunction
    {
        /// <summary>
        /// Represents a function for movement in the left direction.
        /// </summary>
        Left,
        /// <summary>
        /// Represents a function for movement in the right direction.
        /// </summary>
        Right,
        /// <summary>
        /// Represents a function for movement in the up direction.
        /// </summary>
        Up,
        /// <summary>
        /// Represents a function for movement in the down direction.
        /// </summary>
        Down,
        /// <summary>
        /// Represents a function which pauses the game.
        /// </summary>
        Pause
    }
}
