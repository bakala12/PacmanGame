using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Others;

namespace PacmanGame.Engine
{
    public class GameEngine
    {
        public GameEngine(IGameUpdateChecker checker, GameBoard gameBoard)
        {
            if(checker==null) throw new ArgumentNullException(nameof(checker));
            _gameUpdateChecker = checker;
            if(gameBoard ==null) throw new ArgumentNullException(nameof(gameBoard));
            _gameBoard = gameBoard;
            _player = gameBoard.Children.OfType<Player>().Single();
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
    }
}
