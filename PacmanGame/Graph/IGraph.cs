using System.Collections.Generic;

namespace PacmanGame.Graph
{
    public interface IGraph
    {
        List<int>[] Neighbours { get; }
        int VerticlesCount { get; }
    }
}