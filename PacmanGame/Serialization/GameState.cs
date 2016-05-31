using System;
using System.Collections;
using System.Collections.Generic;
using GameControls.Others;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// Stores the information about the game.
    /// </summary>
    [Serializable]
    public class GameState
    {
        /// <summary>
        /// Initializes a new instance of GameState object.
        /// </summary>
        public GameState()
        {
            Points = 0;
            Difficulty = 0;
            Lifes = 3;
            Time = TimeSpan.Zero;
            GameElements = new List<GameElementInfo>();
            ConnetedPortals = new PortalConnetcionList();
        }

        /// <summary>
        /// Gets or sets the points gained by the player.
        /// </summary>
        public uint Points { get; set; }
        /// <summary>
        /// Gets or sets the difficulty of the game.
        /// </summary>
        public uint Difficulty { get; set; }
        /// <summary>
        /// Gets or sets the number of player's lifes.
        /// </summary>
        public uint Lifes { get; set; }
        /// <summary>
        /// Gets or sets the time of the game.
        /// </summary>
        public TimeSpan Time { get; set; }
        /// <summary>
        /// Gets or sets the elements in the game.
        /// </summary>
        public List<GameElementInfo> GameElements { get; set; }
        /// <summary>
        /// Gets or sets information about the portal's connections. 
        /// </summary>
        public PortalConnetcionList ConnetedPortals { get; set; }
        /// <summary>
        /// Gets or sets the direction of the player.
        /// </summary>
        public Direction PlayerDirection { get; set; }
    }
}
