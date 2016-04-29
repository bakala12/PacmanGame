using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.ViewModels;

namespace PacmanGame.R
{
    public class GameEngine : PropertyChangeNotifier
    {
        private ITimer _timer;
        private uint _points;
        private uint _difficulty;
        private uint _lifes;
        private readonly IGameBuilder _builder;
        private GameBoard _gameBoard;
        private IGameMovementChecker _movementChecker;

        public GameEngine(IGameBuilder builder)
        {
            _builder = builder;
        }

        public void Load(GameState state)
        {
            _gameBoard = _builder.BuildBoard(state);
            _movementChecker = GameMovementCheckerFactory.Instance.CreateUpdateChecker(_gameBoard);
            Points = state?.Points ?? 0;
            Difficulty = state?.Difficulty ?? 0;
            Lifes = state?.Lifes ?? 3;
            Timer = _builder.BuildTimer(state);
        }

        public ITimer Timer
        {
            get { return _timer; }
            protected set { _timer = value; OnPropertyChanged(); }
        }

        public uint Points
        {
            get { return _points; }
            protected set { _points = value; OnPropertyChanged(); }
        }

        public uint Difficulty
        {
            get { return _difficulty; }
            protected set { _difficulty = value; OnPropertyChanged(); }
        }

        public uint Lifes
        {
            get { return _lifes; }
            protected set { _lifes = value; OnPropertyChanged(); }
        }

        public virtual void MovePlayer(Direction direction)
        {
            
        }
    }
}
