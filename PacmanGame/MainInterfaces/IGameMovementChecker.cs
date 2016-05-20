using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;

namespace PacmanGame.MainInterfaces
{
    public interface IGameMovementChecker
    {
        bool CheckCollision(IGameElement element1, IGameElement element2);
        bool CheckMovement(MovableElement element, Direction direction);
        bool IsElementNextTo<T>(MovableElement movable, Direction direction) where T : ICanCollide;
    }
}