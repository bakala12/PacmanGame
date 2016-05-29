using System.Collections.Generic;

namespace PacmanGame.Graph
{
    /// <summary>
    /// Represents a graph.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Gets the array which illustrates edges in the graph.
        /// </summary>
        List<int>[] Neighbours { get; }
        /// <summary>
        /// Gets the number of verticles in the graph.
        /// </summary>
        int VerticlesCount { get; }
    }
}