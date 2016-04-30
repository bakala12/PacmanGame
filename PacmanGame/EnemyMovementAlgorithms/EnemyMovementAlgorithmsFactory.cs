using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Interfaces;

namespace PacmanGame.EnemyMovementAlgorithms
{
    public class EnemyMovementAlgorithmsFactory
    {
        private static readonly object SyncRoot = new object();
        private static EnemyMovementAlgorithmsFactory _instance;

        private EnemyMovementAlgorithmsFactory() { }

        public static EnemyMovementAlgorithmsFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new EnemyMovementAlgorithmsFactory();
                    }
                }
                return _instance;
            }
        }

        public IMovementAlgorithm CreateEnemyMovementAlgorithm(uint difficulty)
        {
            switch (difficulty)
            {
                default: 
                    return new SimpleRandomMovementAlgorithm();
            }
        }
    }
}
