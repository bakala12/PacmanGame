using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Engine
{
    public class GameUpdateCheckerFactory
    {
        private GameUpdateCheckerFactory() { }

        private static readonly object SyncRoot = new object();
        private static GameUpdateCheckerFactory _instance;

        public static GameUpdateCheckerFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new GameUpdateCheckerFactory();
                    }
                }
                return _instance;
            }
        }

        public IGameUpdateChecker CreateUpdateChecker()
        {
            return new GameUpdateUpdateChecker();
        }
    }
}
