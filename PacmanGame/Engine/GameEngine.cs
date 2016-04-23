﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
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
            _player.Moved += (sender, args) => OnPlayerMoved();
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
            foreach (var result in _gameBoard.Elements.OfType<Coin>())
            {
                if (_gameUpdateChecker.CheckCollision(_player, result))
                {
                    result.Collect();
                    toRemove.Add(result);
                    //points++
                }
            }
            foreach (var gameElement in toRemove)
            {
                _gameBoard.Children.Remove(gameElement);
            }
        }
    }
}
