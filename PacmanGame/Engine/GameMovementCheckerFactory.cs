using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using PacmanGame.MainInterfaces;

namespace PacmanGame.Engine
{
    /// <summary>
    /// A factory that produces IGameMovementChecker instance. This is implemented as singleton.
    /// </summary>
    public class GameMovementCheckerFactory
    {
        private GameMovementCheckerFactory() { }
        private static readonly object SyncRoot = new object();
        private static GameMovementCheckerFactory _instance;

        /// <summary>
        /// Gets the instane of GameMovementCheckerFactory.
        /// </summary>
        public static GameMovementCheckerFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new GameMovementCheckerFactory();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Creates new instance of IGameMovementChecker.
        /// </summary>
        /// <param name="gameBoard">The gameboard for which IGameMovementChceker is initialized.</param>
        /// <returns>A newly instance of IGameMovementChecker.</returns>
        public IGameMovementChecker CreateUpdateChecker(GameBoard gameBoard)
        {
            return new GameMovementChecker(gameBoard);
        }
    }
}
