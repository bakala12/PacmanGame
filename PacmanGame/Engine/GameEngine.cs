using System;
using System.Collections.Generic;
using System.Linq;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.EnemyMovementAlgorithms;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;
using PacmanGame.Serialization;
using PacmanGame.ViewModels;
using PropertyChanged;

namespace PacmanGame.Engine
{
    [ImplementPropertyChanged]
    public class GameEngine
    {
        private readonly IGameBuilder _builder;
        private readonly GameBoard _gameBoard;
        private IGameMovementChecker _movementChecker;
        private Player _player;
        private IList<Tuple<int, int>> _coinsPosition;
        private AdditionalLifeGenerator _lifesGenerator;

        public GameEngine(IGameBuilder builder, GameBoard board) : this(builder, board, null) { }

        public GameEngine(IGameBuilder builder, GameBoard board, IEnemyMovementManager enemyMovementManager)
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
            _coinsPosition = new List<Tuple<int, int>>();
            foreach (var result in _gameBoard.Elements.OfType<Coin>())
            {
                _coinsPosition.Add(new Tuple<int, int>((int)result.X, (int)result.Y));
            }
            _coinsPosition.Add(new Tuple<int, int>((int)_player.X, (int)_player.Y));
            foreach (var result in _gameBoard.Elements.OfType<Enemy>())
            {
                result.MovementAlgorithm =
                    EnemyMovementAlgorithmsFactory.Instance.CreateEnemyMovementAlgorithm(Difficulty);
                result.Moved += OnEnemyMoved;
            }
            uint enemyMovementInterval = Settings.Default.EnemyMovementInterval;
            EnemyMovementManager = new TimeEnemyMovementManager(_gameBoard.Elements.OfType<Enemy>(),
                _movementChecker, TimeSpan.FromMilliseconds(enemyMovementInterval));
            SetCoinsColleted();
            //refactor needed
            _lifesGenerator = new AdditionalLifeGenerator(_coinsPosition);
            _lifesGenerator.Generated += (sender, args) =>
            {
                BonusLife life = _lifesGenerator.GeneratedLife;
                if (life == null) return;
                life.Collected += (o, eventArgs) =>
                {
                    _gameBoard.Children.Remove(o as Coin);
                    Lifes++;
                    Points += (o as BonusLife)?.PointReward ?? 0;
                };
                life.Disappeared += (o, eventArgs) => _gameBoard.Children.Remove(o as BonusLife);
                _gameBoard.Children.Add(life);
                life.Appear();
            };
            _lifesGenerator.Start();
        }

        public GameState SaveState()
        {
            GameState gameState = new GameState()
            {
                Points = Points, Difficulty = Difficulty, Lifes = Lifes, Time = Timer.TimeLeft
            };
            List<GameElementInfo> list= new List<GameElementInfo>();
            foreach (var result in _gameBoard.Children.OfType<GameElement>())
            {
                var info = new GameElementInfo();
                info.X = result.X;
                info.Y = result.Y;
                if(result is Player) info.Type = GameElementType.Player;
                if(result is Coin) info.Type = GameElementType.Coin;
                if(result is BonusLife) info.Type = GameElementType.BonusLife;
                if(result is Enemy) info.Type = GameElementType.Enemy;
                if(result is Block) info.Type = GameElementType.Block;
                if (result is Portal) info.Type = GameElementType.Portal;
                list.Add(info);
            }
            gameState.GameElements = list;
            return gameState;
        }

        private void SetCoinsColleted()
        {
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

        public ITimer Timer { get; protected set; }

        public uint Points { get; protected set; }

        public uint Difficulty { get; protected set; }

        public uint Lifes { get; protected set; }

        public IEnemyMovementManager EnemyMovementManager { get; private set; }

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
            CheckCollisionWithEnemies();
            MoveViaPortal(_player);
            if (!_gameBoard.Elements.OfType<Coin>().Any())
            {
                Difficulty++;
                FillBoardWithCoins();
            }
        }

        protected virtual void OnEnemyMoved(object sender, MovementEventArgs e)
        {
            Enemy enemy = sender as Enemy;
            if (enemy == null) return;
            CheckCollisionWithEnemies();
            MoveViaPortal(enemy);
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
            Enemy enemy = EnemyMovementManager.Enemies.FirstOrDefault(x => _movementChecker.CheckCollision(_player, x));
            if (enemy != null)
            {
                Lifes--;
            }
            if (Lifes == 0)
                _player.Die();
        }

        protected virtual void FillBoardWithCoins()
        {
            foreach (var coin in _coinsPosition.Select(tuple => new Coin
            {
                X = tuple.Item1,
                Y = tuple.Item2
            }))
            {
                _gameBoard.Children.Add(coin);
            }
            SetCoinsColleted();
        }
    }
}
