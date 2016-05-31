using System;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// Stores the information about the single serialized element.
    /// </summary>
    [Serializable]
    public class GameElementInfo
    {
        /// <summary>
        /// Gets or sets the id of the element.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the X coordinate of the element.
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Gets or sets the Y coordinate of the element.
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        public GameElementType Type { get; set; }
    }
}