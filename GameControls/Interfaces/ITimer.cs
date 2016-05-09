using System;

namespace GameControls.Interfaces
{
    /// <summary>
    /// Represents a timer which can measure time.
    /// </summary>
    public interface ITimer
    {
        /// <summary>
        /// The time that left from the starting the timer.
        /// </summary>
        TimeSpan TimeLeft { get; }
        /// <summary>
        /// Starts the timer.
        /// </summary>
        void Start();
        /// <summary>
        /// Stops the timer.
        /// </summary>
        void Stop();
        /// <summary>
        /// Resets the timer.
        /// </summary>
        void Reset();
    }
}