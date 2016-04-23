using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GameControls.Board;
using GameControls.Others;
using PacmanGame.BoardGenerator;
using PacmanGame.Engine;

namespace PacmanGame.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        public GameViewModel() : base("Game")
        {
            var s = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative));
            IGameBoardGenerator generator = new ExampleFileGameBoardGenerator(s.Stream);
            GameBoard = generator.GenerateBoard(30, 30, 1);
            MoveCommand = new DelegateCommand(MovePlayer);
            _gameEngine = new GameEngine(GameUpdateCheckerFactory.Instance.CreateUpdateChecker(GameBoard), GameBoard);
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

        private GameEngine _gameEngine;

        public ICommand MoveCommand { get; }

        private void MovePlayer(object parameter)
        {
            Key key = parameter as Key? ?? Key.None;
            if(key==Key.None) return;
            IHaveControlKeys controlKeysAccessor = (Application.Current as App)?.ControlKeysAccessor;
            if(controlKeysAccessor==null) throw new InvalidOperationException();
            Direction direction;
            if(controlKeysAccessor.LeftKey == key) direction=Direction.Left;
            else if (controlKeysAccessor.RightKey == key) direction = Direction.Right;
            else if (controlKeysAccessor.UpKey == key) direction = Direction.Up;
            else if (controlKeysAccessor.DownKey == key) direction = Direction.Down;
            else direction = Direction.None;
            _gameEngine.MovePlayer(direction);
        }
    }
}
