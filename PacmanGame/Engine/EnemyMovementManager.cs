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
        /// <param name="movementInterval">The interval between two moves of enemies.</param>
        public TimeEnemyMovementManager(IEnumerable<Enemy> enemies, IGameMovementChecker movementChecker, TimeSpan movementInterval)
        {
            if(movementChecker == null) throw new ArgumentNullException(nameof(movementChecker));
            _movementChecker = movementChecker;
            Enemies = enemies;
            MovementInterval = movementInterval;
            _timer = new DispatcherTimer();
            _timer.Interval = movementInterval;
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
    }
}
