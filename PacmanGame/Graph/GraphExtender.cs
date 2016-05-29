using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Graph
{
    /// <summary>
    /// Extends IGraph interface with A* algorithm computations.
    /// </summary>
    public static class GraphExtender
    {
        /// <summary>
        /// Finds the shortest path in the graph from start to end.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="start">The start verticle.</param>
        /// <param name="end">The end verticle.</param>
        /// <param name="cost">The output parameter giving information about the cost of the shortest path.</param>
        /// <returns></returns>
        public static int AStar(this IGraph graph, int start, int end, out int cost)
        {
            HashSet<int> close = new HashSet<int>();
            int[] prev = new int[graph.VerticlesCount];
            for (int i = 0; i < graph.VerticlesCount; i++) prev[i] = -1;
            int[] dist = new int[graph.VerticlesCount];
            IPriorityQueue<int> open = new PriorityQueue<int>(new GraphComparer(dist));
            open.Insert(start);
            while (!open.IsEmpty)
            {
                int current = open.DeleteFirst();
                close.Add(current);
                if (current == end) break;
                foreach (int w in graph.Neighbours[current])
                {
                    if (close.Contains(w)) continue;
                    if (!open.Contains(w))
                    {
                        open.Insert(w);
                        dist[w] = int.MaxValue;
                    }
                    if (dist[w] > dist[current] + 1)
                    {
                        dist[w] = dist[current] + 1;
                        prev[w] = current;
                    }
                }
            }
            Stack<int> s = new Stack<int>();
            int tmp = end;
            int amount = 0;
            while (prev[tmp] >= 0)
            {
                s.Push(tmp);
                tmp = prev[tmp];
                amount++;
            }
            cost = s.Count;
            return amount > 0 ? s.ToArray()[0] : start;
        }
    }
}
