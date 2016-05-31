using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.MainInterfaces;
using PacmanGame.Serialization;

namespace PacmanGame
{
    /// <summary>
    /// Simple implementation of IGameBuilder used in the game.
    /// </summary>
    internal class SimpleGameBuilder : IGameBuilder
    {
        private readonly string _path;
        private readonly uint _width;
        private readonly uint _height;

        /// <summary>
        /// Initializes a new instance of SimpleGameBuilder object with the given ISettingsProvider.
        /// </summary>
        /// <param name="provider">An object that stores some configuration values used in the game.</param>
        public SimpleGameBuilder(ISettingsProvider provider)
        {
            _path = provider.BoardFilePath;
            _width = provider.BoardWidth;
            _height = provider.BoardHeight;
        }

        /// <summary>
        /// Builds the GameBoard based on the given GameState.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <returns>The newly builded GameBoard.</returns>
        public GameBoard BuildBoard(GameState gameState)
        {
            if (gameState == null)
                return new GameBoard(_width, _height, ReadFile());
            var elements = GenerateElements(gameState.GameElements);
            ConnectPortals(elements.OfType<Portal>().ToList(), gameState);
            return new GameBoard(_width,_height, elements);
        }

        /// <summary>
        /// Builds the timer used in the game.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <returns>THe builded timer used in the game.</returns>
        public ITimer BuildTimer(GameState gameState)
        {
            return new GameTimer(gameState?.Time ?? TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Builds the GameEngine which manages all core game features.
        /// </summary>
        /// <param name="gameState">An object which stores the state of the game.</param>
        /// <param name="board">The GameBoard used in the game.</param>
        /// <param name="provider">The object that provide some configuration settings.</param>
        /// <returns>The GameEngine object used in the game.</returns>
        public GameEngine BuildGameEngine(GameState gameState, GameBoard board, ISettingsProvider provider)
        {
            GameEngine engine = new GameEngine(this, board, provider);
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
            if (stream == null) throw new InvalidOperationException("Cannot load gameboard");
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
                        element = new Portal() { PortalId = gameElementInfo.Id };
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

        private void ConnectPortals(IList<Portal> portals, GameState state)
        {
            foreach (var connetedPortal in state.ConnetedPortals)
            {
                Portal p1 = portals.FirstOrDefault(x => x.PortalId == connetedPortal.Item1);
                Portal p2 = portals.FirstOrDefault(x => x.PortalId == connetedPortal.Item2);
                if (p1 != null && p1.ConnectedPortal == null)
                    p1.ConnectedPortal = p2;
                if (p2 != null && p2.ConnectedPortal == null)
                    p2.ConnectedPortal = p1;
            }
        }

        #endregion
    }
}