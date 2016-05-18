using System;
using System.Collections.Generic;
using System.Linq;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.EnemyMovementAlgorithms;
using PacmanGame.Extensions;
using PacmanGame.Graph;
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
        private readonly ISettingsProvider _provider;
        private IGraph _graph;

        public ITimer Timer { get; protected set; }
        public uint Points { get; protected set; }
        public uint Difficulty { get; protected set; }
        public uint Lifes { get; protected set; }
        public IEnemyMovementManager EnemyMovementManager { get; private set; }

        public GameEngine(IGameBuilder builder, GameBoard board, ISettingsProvider provider)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (board == null)
                throw new ArgumentNullException(nameof(board));
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
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
            _player.Direction = state?.PlayerDirection ?? Direction.Left;
            LoadCoinsPositions();
            ConfigureEnemies();
            SetCoinsColleted();
            ConfigureLifesGeneratior();
        }
        public void MovePlayer(Direction direction)
        {
            if (!_movementChecker.CheckMovement(_player, direction)) return;
            _player.Move(direction);
        }
        public GameState SaveState()
        {
            GameState gameState = new GameState()
            {
                Points = Points,
                Difficulty = Difficulty,
                Lifes = Lifes,
                Time = Timer.TimeLeft,
                PlayerDirection = _player.Direction
            };
            SaveElements(gameState);
            return gameState;
        }

        private void ConfigureEnemies()
        {
            _graph = new Graph.Graph(_gameBoard);
            foreach (var result in _gameBoard.Elements.OfType<Enemy>())
            {
                result.MovementAlgorithm = 
                    new AStarEnemyMovementAlgorithm(_graph, _player, (int)_provider.BoardWidth, (int)_provider.BoardHeight);
                result.Moved += OnEnemyMoved;
            }
            uint enemyMovementInterval = _provider.EnemyMovementInterval;
            EnemyMovementManager = new TimeEnemyMovementManager(_gameBoard.Elements.OfType<Enemy>(),
                _movementChecker, TimeSpan.FromMilliseconds(enemyMovementInterval));
        }
        private void LoadCoinsPositions()
        {
            _coinsPosition = new List<Tuple<int, int>>();
            foreach (var result in _gameBoard.Elements.OfType<Coin>())
            {
                _coinsPosition.Add(new Tuple<int, int>((int)result.X, (int)result.Y));
            }
            _coinsPosition.Add(new Tuple<int, int>((int)_player.X, (int)_player.Y));
        }
        private void ConfigureLifesGeneratior()
        { 
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
        protected virtual void SaveElements(GameState state)
        {
            int id = 1;
            List<GameElementInfo> list = new List<GameElementInfo>();
            PortalConnetcionList portals = new PortalConnetcionList();
            foreach (var result in _gameBoard.Children.OfType<GameElement>())
            {
                var info = new GameElementInfo();
                info.X = result.X;
                info.Y = result.Y;
                if (result is Player) info.Type = GameElementType.Player;
                if (result is Coin) info.Type = GameElementType.Coin;
                if (result is BonusLife) info.Type = GameElementType.BonusLife;
                if (result is Enemy) info.Type = GameElementType.Enemy;
                if (result is Block) info.Type = GameElementType.Block;
                if (result is Portal)
                {
                    var p = (Portal)result;
                    int connId = 0;
                    if (p.PortalId == 0)
                    {
                        info.Id = id++;
                        connId = id++;
                        p.PortalId = info.Id;
                        if (p.ConnectedPortal != null)
                            p.ConnectedPortal.PortalId = connId;
                    }
                    else
                    {
                        info.Id = p.PortalId;
                        connId = p.ConnectedPortal.PortalId;
                    }
                    info.Type = GameElementType.Portal;
                    portals.Add(new Tuple<int, int>(connId, info.Id));
                }
                list.Add(info);
            }
            state.GameElements = list;
            state.ConnetedPortals = portals;
        }
        protected virtual void OnPlayerMoved(object sender, MovementEventArgs e)
        {
            CheckCollisionWithCoins();
            CheckCollisionWithEnemies();
            var portal =
                    _gameBoard.Elements.OfType<Portal>().FirstOrDefault(p => _movementChecker.CheckCollision(p, _player));
            _player.MoveViaPortal(portal);
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
            var portal =
                _gameBoard.Elements.OfType<Portal>().FirstOrDefault(x => _movementChecker.CheckCollision(x, enemy));
            enemy.MoveViaPortal(portal);
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
                _gameBoard.Children.Insert(0, coin);
            }
            SetCoinsColleted();
        }
    }
}
