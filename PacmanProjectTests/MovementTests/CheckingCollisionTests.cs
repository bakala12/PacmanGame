using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Others;
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
        [InlineData(0, 0, 0, 0, true)]
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
        [InlineData(1,1, Direction.Right, true)]
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
        [InlineData(0,0,Direction.Right, 1,0,false)]
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
    }
}
