using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.ViewModels;

namespace PacmanGame.R
{
    public class GameEngine : PropertyChangeNotifier
    {
        private ITimer _timer;
        private uint _points;
        private uint _difficulty;
        private uint _lifes;
        private readonly IGameBuilder _builder;
        private readonly GameBoard _gameBoard;
        private IGameMovementChecker _movementChecker;
        private Player _player;

        public GameEngine(IGameBuilder builder, GameBoard board)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (board == null)
                throw new ArgumentNullException(nameof(board));
            _builder = builder;
            _gameBoard = board;
        }

        public void Load(GameState state)
        {
            _movementChecker = GameMovementCheckerFactory.Instance.CreateUpdateChecker(_gameBoard);
            Points = state?.Points ?? 0;
            Difficulty = state?.Difficulty ?? 1;
            Lifes = state?.Lifes ?? 3;
            Timer = _builder.BuildTimer(state);
            _player = _gameBoard.Children.OfType<Player>().Single();
            _player.Moved += OnPlayerMoved;
            foreach (var result in _gameBoard.Elements.OfType<Enemy>())
            {
                result.Moved += OnEnemyMoved;
            }
            foreach (var result in _gameBoard.Elements.OfType<Coin>())
            {
                result.Collected += (x, e) =>
                {
                    _gameBoard.Children.Remove(x as Coin);
                    uint points = 0;
                    if (x is BonusLife)
                    {
                        Lifes++;
                        points += (x as BonusLife).PointReward;
                    }
                    else
                        points += (x as Coin)?.PointReward * Difficulty ?? 0;
                    Points += points;
                };
            }
        }

        #region Game properties
        public ITimer Timer
        {
            get { return _timer; }
            protected set { _timer = value; OnPropertyChanged(); }
        }

        public uint Points
        {
            get { return _points; }
            protected set { _points = value; OnPropertyChanged(); }
        }

        public uint Difficulty
        {
            get { return _difficulty; }
            protected set { _difficulty = value; OnPropertyChanged(); }
        }

        public uint Lifes
        {
            get { return _lifes; }
            protected set { _lifes = value; OnPropertyChanged(); }
        }
        #endregion

        public void MovePlayer(Direction direction)
        {
            if (!_movementChecker.CheckMovement(_player, direction)) return;
            MoveElement(_player, direction);
        }
        protected virtual void MoveElement(IMovable movable, Direction direction)
        {
            movable.Move(direction);
        }

        protected virtual void OnPlayerMoved(object sender, MovementEventArgs e)
        {
            CheckCollisionWithCoins();
            MoveViaPortal(_player);
        }

        protected virtual void OnEnemyMoved(object sender, MovementEventArgs e)
        {
            Enemy enemy = sender as Enemy;
            if (enemy == null) return;

        }

        protected virtual void MoveViaPortal(MovableElement movable)
        {
            var portal =
                _gameBoard.Elements.OfType<Portal>().FirstOrDefault(p => _movementChecker.CheckCollision(p, movable));
            if (portal?.ConnectedPortal != null)
            {
                movable.X = portal.ConnectedPortal.X;
                movable.Y = portal.ConnectedPortal.Y;
            }
        }

        protected virtual void CheckCollisionWithCoins()
        {
            var coins = _gameBoard.Elements.OfType<Coin>().Where(x => _movementChecker.CheckCollision(x, _player)).ToList();
            foreach (var coin in coins)
            {
                coin?.Collect();
            }
        }

        protected virtual void CheckCollisionWithEnemies()
        {
            
        }
    }
}
