using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Others;

namespace GameControls.Interfaces
{
    /// <summary>
    /// Represents an object that is able make movement.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Moves the object in the specified direction.
        /// </summary>
        /// <param name="direction">The dierction in which the element should be moved.</param>
        void Move(Direction direction);
        /// <summary>
        /// An event which is raised when the object move in the left direction.
        /// </summary>
        event MovementEventHandler MoveLeft;
        /// <summary>
        /// An event which is raised when the object move in the right direction.
        /// </summary>
        event MovementEventHandler MoveRight;
        /// <summary>
        /// An event which is raised when the object move in the up direction.
        /// </summary>
        event MovementEventHandler MoveUp;
        /// <summary>
        /// An event which is raised when the object move in the down direction.
        /// </summary>
        event MovementEventHandler MoveDown;
    }
}
