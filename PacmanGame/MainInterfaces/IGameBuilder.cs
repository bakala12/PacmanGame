using GameControls.Board;
using GameControls.Interfaces;
using PacmanGame.Engine;
using PacmanGame.Serialization;

namespace PacmanGame.MainInterfaces
{
    public interface IGameBuilder
    {
        GameBoard BuildBoard(GameState gameState);
        ITimer BuildTimer(GameState gameState);
        GameEngine BuildGameEngine(GameState gameState, GameBoard board);
    }
}
