using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;

namespace PacmanGame.Graph
{
    internal class Graph : IGraph
    {
        private readonly bool[,] _blocks;
        public int VerticlesCount { get; private set; }
        public List<int>[] Neighbours { get; private set; }

        public Graph(GameBoard gameBoard)
        {
            var blocks = gameBoard.Elements.OfType<Block>();
            bool[,] bl = new bool[gameBoard.Rows, gameBoard.Columns];
            foreach (var block in blocks)
            {
                bl[(int) block.X, (int) block.Y] = true;
            }
            this._blocks = bl;
            ConstructGraph();
        }

        public Graph(bool[,] blocks)
        {
            this._blocks = blocks;
            ConstructGraph();
        }

        private void ConstructGraph()
        {
            VerticlesCount = _blocks.Length;
            Neighbours = new List<int>[VerticlesCount];
            for (int i = 0; i < _blocks.GetLength(0); i++)
            {
                for (int j = 0; j < _blocks.GetLength(1); j++)
                {
                    Neighbours[_blocks.GetLength(1) * i + j] = new List<int>();
                    if (_blocks[i, j]) continue;
                    if (i - 1 >= 0 && !_blocks[i - 1, j]) Neighbours[_blocks.GetLength(1) * i + j].Add(_blocks.GetLength(1) * (i - 1) + j);
                    if (i + 1 < _blocks.GetLength(0) && !_blocks[i + 1, j]) Neighbours[_blocks.GetLength(1) * i + j].Add(_blocks.GetLength(1) * (i + 1) + j);
                    if (j - 1 >= 0 && !_blocks[i, j - 1]) Neighbours[_blocks.GetLength(1) * i + j].Add(_blocks.GetLength(1) * i + j - 1);
                    if (j + 1 < _blocks.GetLength(1) && !_blocks[i, j + 1]) Neighbours[_blocks.GetLength(1) * i + j].Add(_blocks.GetLength(1) * i + j + 1);
                }
            }
        }
    }
}
