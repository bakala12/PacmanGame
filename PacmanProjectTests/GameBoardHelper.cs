using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Elements;
using Moq;
using PacmanGame.EnemyMovementAlgorithms;
using PacmanGame.Engine;
using PacmanGame.Graph;
using PacmanGame.MainInterfaces;

namespace PacmanProjectTests
{
    static class GameBoardHelper
    {
        internal static GameBoard GenerateEmptyGameBoard(uint width, uint height)
        {
            var gameBoard = new GameBoard(width, height);
            for (int i = 0; i < width; i += 2)
            {
                for (int j = 1; j < height - 1; j += 2)
                {
                    gameBoard.Children.Add(new Block() { X = j, Y = i });
                }
            }
            return gameBoard;
        }

        internal static IGameMovementChecker GetGameMovementChecker(GameBoard board)
        {
            return GameMovementCheckerFactory.Instance.CreateUpdateChecker(board);
        }

        public static Task<T> StartStaTask<T>(Func<T> action)
        {
            var tcs = new TaskCompletionSource<T>();
            var thread = new Thread(() =>
            {
                try
                {
                    T res = action();
                    tcs.SetResult(res);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Task StartStaTask(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                    tcs.SetResult(new object());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        internal static async Task<Enemy> GetEnemyAsync(double x, double y)
        {
            return await StartStaTask(() => new Enemy()
            {
                X = x,
                Y = y
            });
        }
    }
}
