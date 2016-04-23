using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.Graph
{
    public static class GraphExtender
    {
        public static int? GetEdgeWeight(this Graph graph, int start, int end)
        {
            if (graph.Neighbours[start].Contains(end)) return 1;
            else return null;
        }

        public static int AStar(this Graph graph, int start, int end)
        {
            HashSet<int> close = new HashSet<int>();
            int[] prev = new int[graph.VerticlesCount];
            for (int i = 0; i < graph.VerticlesCount; i++) prev[i] = -1;
            List<int> open = new List<int>();
            int[] dist = new int[graph.VerticlesCount];
            open.Add(start);
            while (open.Count > 0)
            {
                open.Sort((x, y) => dist[x] - dist[y]);
                int current = open[0];
                open.RemoveAt(0);
                close.Add(current);
                if (current == end) break;
                foreach (int w in graph.Neighbours[current])
                {
                    if (close.Contains(w)) continue;
                    if (!open.Contains(w))
                    {
                        open.Add(w);
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
            return amount > 0 ? s.ToArray()[0] : start;
        }
    }
}
