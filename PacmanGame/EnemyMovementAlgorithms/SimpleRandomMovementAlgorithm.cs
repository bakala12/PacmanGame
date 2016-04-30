using System;
using GameControls.Elements;
using GameControls.Interfaces;
using GameControls.Others;

namespace PacmanGame.EnemyMovementAlgorithms
{
    internal class SimpleRandomMovementAlgorithm : IMovementAlgorithm
    {
        private readonly Random _random = new Random();

        public Direction ProvideDirection(Enemy enemy)
        {
            switch (_random.Next() % 4)
            {
                case 0:
                    return Direction.Left;
                case 1:
                    return Direction.Right;
                case 2:
                    return Direction.Down;
                case 3:
                    return Direction.Up;
                default:
                    return Direction.None;
            }
        }
    }
}
