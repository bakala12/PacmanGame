using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Annotations;

namespace PacmanGame.Engine
{
    public class GameEngine : INotifyPropertyChanged
    {
        public GameEngine(IGameUpdateChecker checker, GameBoard gameBoard)
        {
            if(checker==null) throw new ArgumentNullException(nameof(checker));
            _gameUpdateChecker = checker;
            if(gameBoard ==null) throw new ArgumentNullException(nameof(gameBoard));
            _gameBoard = gameBoard;
            _player = gameBoard.Children.OfType<Player>().Single();
            _player.Moved += (sender, args) => OnPlayerMoved();
            Timer = new GameTimer();
            foreach (var result in _gameBoard.Elements.OfType<BonusLife>())
            {
                result.Collected += (x, e) => { };
            }
        }

        private readonly IGameUpdateChecker _gameUpdateChecker;
        private readonly GameBoard _gameBoard;
        private readonly Player _player;

        public void MovePlayer(Direction direction)
        {
            if(!_gameUpdateChecker.CheckMovement(_player, direction)) return;
            //move player here
            _player.Move(direction);
        }

        private void OnPlayerMoved()
        {
            IList<GameElement> toRemove = new List<GameElement>();
            foreach (var result in _gameBoard.Elements.OfType<ICollectable>())
            {
                if (_gameUpdateChecker.CheckCollision(_player, result))
                {
                    result.Collect();
                    toRemove.Add(result as GameElement);
                    //points++
                }
            }
            foreach (var gameElement in toRemove)
            {
                _gameBoard.Children.Remove(gameElement);
            }
            if (!_gameBoard.Children.OfType<Coin>().Any())
            {
                //koniec levelu
                MessageBox.Show("Gratulacje, koniec gry!");
            }
        }

        private ITimer _timer;
        private uint _points;
        private uint _difficulty;
        private uint _lifes;

        public ITimer Timer
        {
            get { return _timer; }
            private set { _timer = value; OnPropertyChanged(); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
