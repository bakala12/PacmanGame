using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;
using PacmanGame.BoardGenerator;

namespace PacmanGame.R
{
    public interface IGameBuilder
    {
        GameBoard BuildBoard(GameState gameState);
        ITimer BuildTimer(GameState gameState);
    }

    public class SimpleGameBuilder : IGameBuilder
    {
        private readonly Stream _stream;

        public SimpleGameBuilder(Stream stream = null)
        {
            if (stream == null)
                _stream = Application.GetResourceStream(new Uri("Resources/example_board.board", UriKind.Relative))?.Stream;
            _stream = stream;
        }

        public GameBoard BuildBoard(GameState gameState)
        {
            if (gameState == null)
                return new GameBoard(30, 30, ReadFile());
            return new GameBoard(30,30, GenerateElements(gameState.GameElements));
        }

        public ITimer BuildTimer(GameState gameState)
        {
            return new GameTimer(gameState.Time, new TimeSpan(0, 0, 0, 1));
        }

        #region private helpers
        /// <summary>
        /// Reads the information about the elements on GameBoard from file.
        /// </summary>
        /// <returns>The list of elements on the board.</returns>
        private IList<GameElement> ReadFile()
        {
            IList<GameElement> elements = new List<GameElement>();
            using (var sr = new StreamReader(_stream))
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
