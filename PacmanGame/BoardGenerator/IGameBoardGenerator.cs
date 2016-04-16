using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;

namespace PacmanGame.BoardGenerator
{
    /// <summary>
    /// Provides a way to generate GameBoard to the game.
    /// </summary>
    public interface IGameBoardGenerator
    {
        /// <summary>
        /// Generates a new game board and all the elements on it.
        /// </summary>
        /// <param name="rows">The number of rows of GameBoard.</param>
        /// <param name="columns">The number of columns of GameBoard.</param>
        /// <param name="stage">The stage of the game.</param>
        /// <returns>Newly generated GameBoard which is ready to the game.</returns>
        GameBoard GenerateBoard(uint rows, uint columns, uint stage);
    }
}