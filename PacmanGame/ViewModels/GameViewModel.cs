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
        private GameEngine _gameEngine;
        private GameBoard _gameBoard;

        public GameViewModel() : base("Game")
        {
            var s = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative));
            IGameBoardGenerator generator = new ExampleFileGameBoardGenerator(s?.Stream);
            GameBoard = generator.GenerateBoard(30, 30, 1);
            MoveCommand = new DelegateCommand(MovePlayer);
            PauseCommand = new DelegateCommand(x=>Pause());
            _gameEngine = new GameEngine(GameUpdateCheckerFactory.Instance.CreateUpdateChecker(GameBoard), GameBoard);
        }

        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            set
            {
                _gameBoard = value;
                OnPropertyChanged();
            }
        }

        public ICommand MoveCommand { get; }
        public ICommand PauseCommand { get; }

        public void MovePlayer(object parameter)
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

        public void Pause()
        {
            var viewModelChager = (Application.Current as App)?.ViewModelChanger;
            //stop timer here for example
            viewModelChager?.ChangeCurrentViewModel("Pause");
        }

        public virtual void ResetState()
        {
            var s = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative));
            IGameBoardGenerator generator = new ExampleFileGameBoardGenerator(s?.Stream);
            GameBoard = generator.GenerateBoard(30, 30, 1);
            _gameEngine = new GameEngine(GameUpdateCheckerFactory.Instance.CreateUpdateChecker(GameBoard), GameBoard);
        }
    }
}
