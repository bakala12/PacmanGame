using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;

namespace PacmanGame.Graph
{
    public class GraphCreator
    {
        private GraphCreator() { }

        private static readonly object SyncRoot = new object();
        private static GraphCreator _instance;

        public static GraphCreator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if(_instance==null)
                            _instance = new GraphCreator();
                    }
                }
                return _instance;
            }
        }

        public IGraph Create(GameBoard gameBoard)
        {
            return new Graph(gameBoard);   
        }
    }
}
