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
    public class GameTimer : INotifyPropertyChanged, ITimer
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly DispatcherTimer _timer;
        private TimeSpan _timeLeft;

        public TimeSpan TimeLeft
        {
            get { return _timeLeft; }
            protected set { _timeLeft = value; OnPropertyChanged(); } 
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Reset()
        {
            Stop();
            TimeLeft = TimeSpan.Zero;
            Start();
        }

        public GameTimer() : this(TimeSpan.Zero, new TimeSpan(0, 0, 0, 1))
        {
        }

        public GameTimer(TimeSpan initialValue) : this(initialValue, new TimeSpan(0, 0, 0, 1))
        {
        }

        public GameTimer(TimeSpan initialValue, TimeSpan timePeriod)
        {
            _timer = new DispatcherTimer();
            TimeLeft = initialValue;
            _timer.Interval = timePeriod;
            _timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            TimeLeft += new TimeSpan(0,0,0,1);
        }
    }
}
