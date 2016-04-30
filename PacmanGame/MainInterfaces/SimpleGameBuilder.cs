using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.Serialization;

namespace PacmanGame.MainInterfaces
{
    internal class SimpleGameBuilder : IGameBuilder
    {
        private readonly string _path;

        public SimpleGameBuilder(string path = null)
        {
            _path = path ?? "Resources/example_board.board";
        }

        public GameBoard BuildBoard(GameState gameState)
        {
            if (gameState == null)
                return new GameBoard(30, 30, ReadFile());
            return new GameBoard(30,30, GenerateElements(gameState.GameElements));
        }

        public ITimer BuildTimer(GameState gameState)
        {
            return new GameTimer(gameState?.Time ?? TimeSpan.Zero, new TimeSpan(0, 0, 0, 1));
        }

        public GameEngine BuildGameEngine(GameState gameState, GameBoard board)
        {
            GameEngine engine = new GameEngine(this, board);
            engine.Load(gameState);
            return engine;
        }

        #region private helpers
        /// <summary>
        /// Reads the information about the elements on GameBoard from file.
        /// </summary>
        /// <returns>The list of elements on the board.</returns>
        private IList<GameElement> ReadFile()
        {
            IList<GameElement> elements = new List<GameElement>();
            Stream stream = Application.GetResourceStream(new Uri(_path, UriKind.Relative))?.Stream;
            if(stream == null) throw new InvalidOperationException("Cannot load gameboard");
            using (var sr = new StreamReader(stream))
            {
                string line;
                Portal previousPortal = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] split = line.Split(' ');
                    GameElement element;
                    switch (split[0])
                    {
                        case "B":
                            element = new Block();
                            break;
                        case "C":
                            element = new Coin();
                            break;
                        case "P":
                            element = new Player();
                            break;
                        case "E":
                            element = new Enemy();
                            break;
                        case "BL":
                            element = new BonusLife();
                            break;
                        case "PO":
                            element = new Portal(previousPortal);
                            if (previousPortal == null)
                                previousPortal = (Portal)element;
                            else
                                previousPortal = null;
                            break;
                        default:
                            throw new NotSupportedException();
                    }
                    element.X = double.Parse(split[1]);
                    element.Y = double.Parse(split[2]);
                    elements.Add(element);
                }
            }
            return elements;
        }

        private IList<GameElement> GenerateElements(IEnumerable<GameElementInfo> info)
        {
            IList<GameElement> elements = new List<GameElement>();
            foreach (var gameElementInfo in info)
            {
                GameElement element;
                switch (gameElementInfo.Type)
                {
                    case GameElementType.Block:
                        element = new Block();
                        break;
                    case GameElementType.Coin:
                        element = new Coin();
                        break;
                    case GameElementType.Portal:
                        element = new Portal();
                        break;
                    case GameElementType.BonusLife:
                        element = new BonusLife();
                        break;
                    case GameElementType.Player:
                        element = new Player();
                        break;
                    case GameElementType.Enemy:
                        element = new Enemy();
                        break;
                    default:
                        throw new InvalidEnumArgumentException();
                }
                element.X = gameElementInfo.X;
                element.Y = gameElementInfo.Y;
                elements.Add(element);
            }
            return elements;
        }
        #endregion
    }
}