﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GameControls.Board;
using GameControls.Others;
using PacmanGame.ViewModels;

namespace PacmanGame.R
{
    public class GameViewModel : CloseableViewModel
    {
        private IGameBuilder _builder;
        private GameBoard _gameBoard;
        private GameEngine _gameEngine;

        public ICommand PauseCommand { get; }
        public ICommand MovePlayerCommand { get; }

        public GameBoard GameBoard
        {
            get { return _gameBoard; }
            protected set { _gameBoard = value; OnPropertyChanged(); }
        }

        public GameEngine GameEngine
        {
            get { return _gameEngine; }
            protected set { _gameEngine = value; OnPropertyChanged(); }
        }

        public GameViewModel(IGameBuilder builder)
        {
            if(builder==null) throw new ArgumentNullException(nameof(builder));
            _builder = builder;
            PauseCommand = new DelegateCommand(x=>Pause());
            MovePlayerCommand = new DelegateCommand(MovePlayer);
        }

        protected override void OnViewAppeared()
        {
            base.OnViewAppeared();
            GameEngine?.Timer?.Start();
        }

        public virtual void StartGame()
        {
            StartGame(null);
        }

        public virtual void StartGame(GameState state)
        {
            GameBoard = _builder.BuildBoard(state);
            GameEngine = _builder.BuildGameEngine(state);
        }

        public virtual void MovePlayer(object parameter)
        {
            Key key = parameter as Key? ?? Key.None;
            if (key == Key.None) return;
            IHaveControlKeys controlKeysAccessor = (Application.Current as App)?.ControlKeysAccessor;
            if (controlKeysAccessor == null) throw new InvalidOperationException();
            Direction direction;
            if (controlKeysAccessor.LeftKey == key) direction = Direction.Left;
            else if (controlKeysAccessor.RightKey == key) direction = Direction.Right;
            else if (controlKeysAccessor.UpKey == key) direction = Direction.Up;
            else if (controlKeysAccessor.DownKey == key) direction = Direction.Down;
            else direction = Direction.None;
            GameEngine.MovePlayer(direction);
        }

        public virtual void Pause()
        {
            GameEngine.Timer.Stop();
            var viewModelChager = (Application.Current as App)?.ViewModelChanger;
            viewModelChager?.ChangeCurrentViewModel("Pause");
        }
    }
}
