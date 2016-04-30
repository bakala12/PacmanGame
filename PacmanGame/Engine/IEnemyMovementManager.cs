using System.Collections.Generic;
using GameControls.Elements;

namespace PacmanGame.Engine
{
    public interface IEnemyMovementManager
    {
        IEnumerable<Enemy> Enemies { get; }
        void MoveEnemies();
        void Start();
        void Stop();
    }
}
