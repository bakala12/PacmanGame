using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.MainInterfaces;
using PacmanGame.Extensions;

namespace PacmanGame.Engine
{
    /// <summary>
    /// An implementation of IEnemyMovementManager which moves the enemies with the time interval.
    /// </summary>
    public class TimeEnemyMovementManager : IEnemyMovementManager
    {
        private readonly DispatcherTimer _timer;
        private readonly IGameMovementChecker _movementChecker;
        private readonly ISettingsProvider _provider;

        /// <summary>
        /// Gets the collection of the enemies.
        /// </summary>
        public IEnumerable<Enemy> Enemies { get; }
        /// <summary>
        /// Gets the time between enemies are moved.
        /// </summary>
        public TimeSpan MovementInterval { get; }

        /// <summary>
        /// Initializes a new instance of TimeEnemyMovementManager.
        /// </summary>
        /// <param name="enemies">The collection of the enemies.</param>
        /// <param name="movementChecker">The IGameMovementChecker for checking collision between elements.</param>
        /// <param name="provider">An object that provide some game setting values.</param>
        public TimeEnemyMovementManager(IEnumerable<Enemy> enemies, IGameMovementChecker movementChecker, ISettingsProvider provider)
        {
            if(movementChecker == null) throw new ArgumentNullException(nameof(movementChecker));
            _movementChecker = movementChecker;
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
            if(enemies==null) enemies = new List<Enemy>();
            Enemies = enemies;
            MovementInterval = TimeSpan.FromMilliseconds(_provider.EnemyMovementInterval);
            _timer = new DispatcherTimer();
            _timer.Interval = MovementInterval;
            _timer.Tick += (x, e) => ((IEnemyMovementManager)this).MoveEnemies();
        }

        /// <summary>
        /// Moves all the enemies. This is explicit interface implementation.
        /// </summary>
        void IEnemyMovementManager.MoveEnemies()
        {
            foreach (var enemy in Enemies)
            {
                Direction direction = enemy?.MovementAlgorithm?.ProvideDirection(enemy) ?? Direction.None;
                if(_movementChecker.CheckMovement(enemy, direction) && !_movementChecker.IsElementNextTo<Enemy>(enemy, direction))
                    enemy?.Move(direction);
            }
        }

        /// <summary>
        /// Starts working the IEnemyMovementManager.
        /// </summary>
        public void Start()
        {
            _timer.Start();
        }

        /// <summary>
        /// Stops working the IEnemyMovementManager.
        /// </summary>
        public void Stop()
        {
            _timer.Stop();
        }

        /// <summary>
        /// Increases the difficulty of the game.
        /// </summary>
        void IEnemyMovementManager.NextLevel()
        {
            Stop();
            _timer.Interval = MovementInterval.Subtract(TimeSpan.FromMilliseconds(_provider.EnemyDifficultyIncreaseSpeed));
            Start();
        }
    }
}
