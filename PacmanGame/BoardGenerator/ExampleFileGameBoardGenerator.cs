﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Elements;

namespace PacmanGame.BoardGenerator
{
    /// <summary>
    /// Simple implemetation of IGameBoardGenerator interface which can generate only one GameBoard.
    /// </summary>
    internal class ExampleFileGameBoardGenerator : IGameBoardGenerator
    {
        /// <summary>
        /// Initializes a new instance of ExampleFileGameBoardGenerator.
        /// </summary>
        internal ExampleFileGameBoardGenerator(Stream s)
        {
            _stream = s;
        }

        /// <summary>
        /// Stores a path to the file with info about the board.
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Fixed number of rows.
        /// </summary>
        private const uint Rows = 30;

        /// <summary>
        /// Fixed number of columns.
        /// </summary>
        private const uint Columns = 30;

        /// <summary>
        /// Generates a GameBoard based on internal file. This method ignores all
        /// the parameters and create a fixed object. Can be invoked only with interface 
        /// instance (explicit implementation of interface).
        /// </summary>
        /// <param name="rows">This parameter is ignored.</param>
        /// <param name="columns">This parameter is ignored.</param>
        /// <param name="stage">This parameter is ignored.</param>
        /// <returns></returns>
        /// <remarks>
        /// This class is to internal use only!
        /// To support generating GameBoard with various dimensions and depending on the game stage
        /// create another implementation of IGameBoardGenerator interface.
        /// </remarks>
        GameBoard IGameBoardGenerator.GenerateBoard(uint rows, uint columns, uint stage)
        {
            return new GameBoard(Rows, Columns, ReadFile());
        }

        /// <summary>
        /// Reads the information about the elements on GameBoard from file.
        /// </summary>
        /// <returns>The list of elements on the board.</returns>
        private IList<GameElement> ReadFile()
        {
            IList<GameElement> elements = new List<GameElement>();
            using (var sr = new StreamReader(_stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(' ');
                    GameElement element;
                    switch (split[0])
                    {
                        case "B":
                            element = new Block();
                            break;
                        case "C":
                            element = new Coin();
                            break;
                        case "P":
                            element = new Player();
                            break;
                        case "E":
                            element = new Enemy();
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    element.X = double.Parse(split[1]);
                    element.Y = double.Parse(split[2]);
                    elements.Add(element);
                }
            }
            return elements;
        }
    }
}