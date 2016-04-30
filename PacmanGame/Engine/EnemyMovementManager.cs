using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;

namespace PacmanGame.Engine
{
    public class TimeEnemyMovementManager : IEnemyMovementManager
    {
        public IEnumerable<Enemy> Enemies { get; }
        public TimeSpan MovementInterval { get; }
        private readonly DispatcherTimer _timer;
        private readonly IGameMovementChecker _movementChecker;

        public TimeEnemyMovementManager(IEnumerable<Enemy> enemies, IGameMovementChecker movementChecker, TimeSpan movementInterval)
        {
            if(movementChecker == null) throw new ArgumentNullException(nameof(movementChecker));
            _movementChecker = movementChecker;
            Enemies = enemies;
            MovementInterval = movementInterval;
            _timer = new DispatcherTimer();
            _timer.Interval = movementInterval;
            _timer.Tick += (x, e) => MoveEnemies();
        }

        public void MoveEnemies()
        {
            foreach (var enemy in Enemies)
            {
                Direction direction = enemy?.MovementAlgorithm?.ProvideDirection(enemy) ?? Direction.None;
                if(_movementChecker.CheckMovement(enemy, direction))
                    enemy?.Move(direction);
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
