using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;

namespace PacmanGame.Graph
{
    /// <summary>
    /// A singleton which is responsible for production of IGraph objects.
    /// </summary>
    public class GraphCreator
    {
        private GraphCreator() { }
        private static readonly object SyncRoot = new object();
        private static GraphCreator _instance;

        /// <summary>
        /// Gets the instance of GraphCreator.
        /// </summary>
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

        /// <summary>
        /// Creates the graph for the given GameBoard object.
        /// </summary>
        /// <param name="gameBoard">The GameBoard object.</param>
        /// <returns>The created graph instance.</returns>
        public IGraph Create(GameBoard gameBoard)
        {
            return new Graph(gameBoard);   
        }
    }
}
