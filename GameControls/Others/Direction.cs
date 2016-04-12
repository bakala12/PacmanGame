using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameControls.Others
{
    /// <summary>
    /// Represents the direction in which the element is moving
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// The left direction.
        /// </summary>
        Left,
        /// <summary>
        /// The right direction.
        /// </summary>
        Right,
        /// <summary>
        /// The up direction.
        /// </summary>
        Up,
        /// <summary>
        /// The down direction.
        /// </summary>
        Down,
        /// <summary>
        /// Value that informs about no movement.
        /// </summary>
        None
    }
}
