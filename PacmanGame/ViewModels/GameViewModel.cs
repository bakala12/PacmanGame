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
    public class GameViewModel : CloseableViewModel
    {
        private GameEngine _gameEngine;
        private GameBoard _gameBoard;

        protected override void OnViewAppeared()
        {
            base.OnViewAppeared();
            GameEngine?.Timer?.Start();
        }

        public GameViewModel() : base("Game")
        {
            MoveCommand = new DelegateCommand(MovePlayer);
            PauseCommand = new DelegateCommand(x=>Pause());
            ResetState();
            var timer = GameEngine.Timer as GameTimer;
            if(timer != null)
                timer.PropertyChanged +=(x,e)=> OnPropertyChanged();
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

        public GameEngine GameEngine
        {
            get { return _gameEngine; }
            private set { _gameEngine = value; OnPropertyChanged();}
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
            GameEngine.MovePlayer(direction);
        }

        public void Pause()
        {
            GameEngine.Timer.Stop();
            var viewModelChager = (Application.Current as App)?.ViewModelChanger;
            viewModelChager?.ChangeCurrentViewModel("Pause");
        }

        public void ResetState()
        {
            var s = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative));
            IGameBoardGenerator generator = new ExampleFileGameBoardGenerator(s?.Stream);
            GameBoard = generator.GenerateBoard(30, 30, 1);
            GameEngine = new GameEngine(GameUpdateCheckerFactory.Instance.CreateUpdateChecker(GameBoard), GameBoard);
        }
    }
}
