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
        private readonly DispatcherTimer _timer;
        private readonly IList<Tuple<int, int>> _coinsPositions;
        private readonly Random _random = new Random();

        /// <summary>
        /// An event which is raised when the new bonus has been generated.
        /// </summary>
        public event EventHandler Generated;

        /// <summary>
        /// Initializes a new instance of AdditionalLifeGenerator.
        /// </summary>
        /// <param name="coinsPositions">A list of coins' positions.</param>
        /// <param name="interval">The time interval between two generation of bonuses.</param>
        public AdditionalLifeGenerator(IList<Tuple<int, int>> coinsPositions, TimeSpan interval)
        {
            _coinsPositions = coinsPositions;
            _timer = new DispatcherTimer();
            _timer.Interval = interval;
            _timer.Tick += (sender, args) => Generate();
        }

        /// <summary>
        /// Starts working the AdditionalLifeGenerator.
        /// </summary>
        public void Start()
        {
            GeneratedLife = null;
            _timer.Start();
        }

        /// <summary>
        /// Stops working the AdditionalLifeGenerator.
        /// </summary>
        public void Stop()
        {
            GeneratedLife = null;
            _timer.Stop();
        }

        /// <summary>
        /// Perform generation of new BonusLife.
        /// </summary>
        protected virtual void Generate()
        {
            GeneratedLife = new BonusLife(TimeSpan.FromSeconds(10));
            var pos = _coinsPositions[_random.Next(0, _coinsPositions.Count)];
            GeneratedLife.X = pos.Item1;
            GeneratedLife.Y = pos.Item2;
            Generated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets the generated BonusLife object.
        /// </summary>
        public BonusLife GeneratedLife { get; protected set; }
    }
}
