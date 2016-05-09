using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameControls.Interfaces;

namespace GameControls.Others
{
    /// <summary>
    /// An implementation of ITimer interface to measure game time.
    /// </summary>
    public class GameTimer : INotifyPropertyChanged, ITimer
    {
        /// <summary>
        /// An event which is raised when a property has changed. This informs graphical 
        /// interface about the change and it can update binding to properties.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the change of the given property.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed. It is optional parameter,
        /// the CallerMemberName attribute can take the calling property name automatically.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// A timer which measure time internally.
        /// </summary>
        private readonly DispatcherTimer _timer;
        /// <summary>
        /// Backing store field for TimeLeft property.
        /// </summary>
        private TimeSpan _timeLeft;

        /// <summary>
        /// Gets or sets the value of the time which has left since the timer was started.
        /// </summary>
        public TimeSpan TimeLeft
        {
            get { return _timeLeft; }
            protected set { _timeLeft = value; OnPropertyChanged(); } 
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Resets the timer. It stops it, and start again.
        /// </summary>
        public void Reset()
        {
            Stop();
            TimeLeft = TimeSpan.Zero;
            Start();
        }

        /// <summary>
        /// Initializes a new instance of GameTimer class.
        /// </summary>
        public GameTimer() : this(TimeSpan.Zero, new TimeSpan(0, 0, 0, 1))
        {
        }

        /// <summary>
        /// Initializes a timer with the initial value of time left.
        /// </summary>
        /// <param name="initialValue">The initial value of time left.</param>
        public GameTimer(TimeSpan initialValue) : this(initialValue, new TimeSpan(0, 0, 0, 1))
        {
        }

        /// <summary>
        /// Initializes a new instance of GameTimer class with the initial value of time left 
        /// and with the period in which timer time would be updated.
        /// </summary>
        /// <param name="initialValue">The initial value of time left.</param>
        /// <param name="timePeriod">The period of time in which timer time left would be updated.</param>
        public GameTimer(TimeSpan initialValue, TimeSpan timePeriod)
        {
            _timer = new DispatcherTimer();
            TimeLeft = initialValue;
            _timer.Interval = timePeriod;
            _timer.Tick += OnTimerTick;
        }

        /// <summary>
        /// Private method that updates the timer time.
        /// </summary>
        /// <param name="sender">Sender of the Tick event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnTimerTick(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0,0,0,1);
        }
    }
}
