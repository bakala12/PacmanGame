using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Others;
using Moq;
using PacmanGame.EnemyMovementAlgorithms;
using PacmanGame.Engine;
using PacmanGame.Graph;
using PacmanGame.MainInterfaces;
using Xunit;
using Block = GameControls.Elements.Block;

namespace PacmanProjectTests.MovementTests
{
    public class CheckingCollisionTests
    {
        private GameBoard _gameBoard;
        private readonly uint _width = 8;
        private readonly uint _height = 8;

        [Theory]
        [InlineData(0, 0, 0, 0, false)]
        [InlineData(5, 5, 0, 0, false)]
        [InlineData(0, 1, 1, 0, false)]
        [InlineData(1, 2, 1, 2, true)]
        [InlineData(10, 1, 0, 1, false)]
        private async Task CheckStaticColisionBetweenPlayerAndBlocks(double playerX, double playerY, double blockX, double blockY, bool result)
        {
            await GameBoardHelper.StartStaTask(() =>
            {
                _gameBoard = GameBoardHelper.GenerateEmptyGameBoard(_width, _height);
                var checker = GameBoardHelper.GetGameMovementChecker(_gameBoard);
                Player player = new Player() {X = playerX, Y = playerY};
                Block block = _gameBoard.Elements.FirstOrDefault(b => Math.Abs(b.X - blockX) < 1 && Math.Abs(b.Y - blockY) < 1) as Block;
                bool res = checker.CheckCollision(player, block);
                Assert.Equal(result, res);
            });
        }

        [Theory]
        [InlineData(0,0, Direction.Left, false)]
        [InlineData(1,1, Direction.None, false)]
        [InlineData(1,1, Direction.Down, true)]
        [InlineData(2,5, Direction.Left, true)]
        [InlineData(1,4,Direction.Up, false)]
        private async Task CheckMovementTest(double playerX, double playerY, Direction direction, bool result)
        {
            await GameBoardHelper.StartStaTask(() =>
            {
                _gameBoard = GameBoardHelper.GenerateEmptyGameBoard(_width, _height);
                var checker = GameBoardHelper.GetGameMovementChecker(_gameBoard);
                Player player = new Player() { X = playerX, Y = playerY };
                bool res = checker.CheckMovement(player, direction);
                Assert.Equal(result, res);
            });
        }

        [Theory]
        [InlineData(0,0,Direction.Right, 0,1,true)]
        [InlineData(0,0,Direction.Down, 0,1, false)]
        [InlineData(0,0,Direction.None, 1,0, false)]
        [InlineData(0,0,Direction.Up, 1,0, false)]
        [InlineData(2,2,Direction.Left, 2,1, true)]
        private async Task CheckIsNextElementTest(double playerX, double playerY, Direction direction,double enemyX, double enemyY, bool result)
        {
            await GameBoardHelper.StartStaTask(() =>
            {
                _gameBoard = GameBoardHelper.GenerateEmptyGameBoard(_width, _height);
                var checker = GameBoardHelper.GetGameMovementChecker(_gameBoard);
                Player player = new Player() { X = playerX, Y = playerY };
                Enemy enemy = new Enemy() {X = enemyX, Y = enemyY};
                _gameBoard.Children.Add(enemy);
                bool res = checker.IsElementNextTo<MovableElement>(player, direction);
                Assert.Equal(result, res);
            });
        }

        [Theory]
        [InlineData(false, new double[] {0,0,1,1,2,0})]
        [InlineData(false, new double[] { 0, 0, 1, 1, 0, 2 })]
        [InlineData(false, new double[] {0,0,1,1,2,2})]
        private async Task CheckEnemiesMovement(bool result, double[] positions)
        {
            await GameBoardHelper.StartStaTask(() =>
            {
                _gameBoard = GameBoardHelper.GenerateEmptyGameBoard(_width, _height);
                IGameMovementChecker checker = GameBoardHelper.GetGameMovementChecker(_gameBoard);
                ISettingsProvider provider = new TestSettingsProvider();
                IGraph graph = new Graph(_gameBoard);
                var player = new Mock<Player>();
                player.Object.X = positions[0];
                player.Object.Y = positions[1];
                var alg = new AStarEnemyMovementAlgorithm(graph, player.Object, (int)_width, (int)_height);
                var e1 = new Enemy();
                e1.X = positions[2];
                e1.Y = positions[3];
                e1.MovementAlgorithm = alg;
                var e2 = new Enemy();
                e2.X = positions[4];
                e2.Y = positions[5];
                e2.MovementAlgorithm = alg;
                _gameBoard.Children.Add(e1);
                _gameBoard.Children.Add(e2);
                IEnemyMovementManager manager = new TimeEnemyMovementManager(new [] {e1,e2}, checker, provider);
                manager.MoveEnemies();
                bool res = checker.CheckCollision(e1, e2);
                Assert.Equal(result, res);
            });
        }
    }
}
