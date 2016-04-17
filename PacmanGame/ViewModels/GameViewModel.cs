using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameControls.Board;
using PacmanGame.BoardGenerator;

namespace PacmanGame.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel() : base("Game")
        {
            var s = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative));
            IGameBoardGenerator generator = new ExampleFileGameBoardGenerator(s.Stream);
            GameBoard = generator.GenerateBoard(30, 30, 1);
        }

        private GameBoard _gameBoard;

        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            set
            {
                _gameBoard = value;
                OnPropertyChanged();
            }
        }
    }
}
