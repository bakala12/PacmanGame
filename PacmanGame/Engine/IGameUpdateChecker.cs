using GameControls.Board;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;

namespace PacmanGame.Engine
{
    public interface IGameUpdateChecker
    {
        bool CheckCollision(IGameElement element1, IGameElement element2);
        bool CheckMovement(MovableElement element, Direction direction);
    }
}