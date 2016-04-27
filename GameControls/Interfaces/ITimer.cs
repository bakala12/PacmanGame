using System;

namespace GameControls.Interfaces
{
    public interface ITimer
    {
        TimeSpan TimeLeft { get; }
        void Start();
        void Stop();
        void Reset();
    }
}