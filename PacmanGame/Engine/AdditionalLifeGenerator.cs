using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameControls.Elements;

namespace PacmanGame.Engine
{
    public class AdditionalLifeGenerator
    {
        public event EventHandler Generated;
        private readonly DispatcherTimer _timer;
        private readonly IList<Tuple<int, int>> _coinsPositions;
        private readonly Random _random = new Random();

        public AdditionalLifeGenerator(IList<Tuple<int, int>> coinsPositions)
        {
            _coinsPositions = coinsPositions;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Tick += (sender, args) => Generate();
        }

        public void Start()
        {
            GeneratedLife = null;
            _timer.Start();
        }

        public void Stop()
        {
            GeneratedLife = null;
            _timer.Stop();
        }

        protected virtual void Generate()
        {
            GeneratedLife = new BonusLife(TimeSpan.FromSeconds(10));
            var pos = _coinsPositions[_random.Next(0, _coinsPositions.Count)];
            GeneratedLife.X = pos.Item1;
            GeneratedLife.Y = pos.Item2;
            Generated?.Invoke(this, EventArgs.Empty);
        }

        public BonusLife GeneratedLife { get; protected set; }
    }
}
