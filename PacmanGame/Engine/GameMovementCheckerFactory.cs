using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;

namespace PacmanGame.Engine
{
    public class GameMovementCheckerFactory
    {
        private GameMovementCheckerFactory() { }

        private static readonly object SyncRoot = new object();
        private static GameMovementCheckerFactory _instance;

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

        public IGameMovementChecker CreateUpdateChecker(GameBoard gameBoard)
        {
            return new GameMovementChecker(gameBoard);
        }
    }
}
