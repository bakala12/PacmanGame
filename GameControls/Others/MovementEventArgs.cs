using System;

namespace GameControls.Others
{
    /// <summary>
    /// Represents a delegate that is used as handler for movement events.
    /// </summary>
    /// <param name="sender">An object that raises the event.</param>
    /// <param name="e">Additional information of the event.</param>
    public delegate void MovementEventHandler(object sender, MovementEventArgs e);

    /// <summary>
    /// An object which is used to pass additional information with the movement events.
    /// </summary>
    public class MovementEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the direction of the movement.
        /// </summary>
        public Direction MovementDirection { get; }

        /// <summary>
        /// Initializes a new instance of MovementEventArgs class with the specified direction.
        /// </summary>
        /// <param name="direction"></param>
        public MovementEventArgs(Direction direction)
        {
            MovementDirection = direction;
        }
    }
}
