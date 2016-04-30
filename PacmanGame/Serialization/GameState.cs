using System;
using System.Collections.Generic;

namespace PacmanGame.Serialization
{
    public enum GameElementType
    {
        Block, Coin, Portal, BonusLife, Player, Enemy
    }

    public class GameElementInfo
    {
        public double X { get; set; }
        public double Y { get; set; }
        public GameElementType Type { get; set; }
    }

    public class GameState
    {
        public GameState()
        {
            Points = 0;
            Difficulty = 0;
            Lifes = 3;
            Time = TimeSpan.Zero;
            GameElements = new List<GameElementInfo>();
        }

        public uint Points { get; set; }
        public uint Difficulty { get; set; }
        public uint Lifes { get; set; }
        public TimeSpan Time { get; set; }
        public IList<GameElementInfo> GameElements { get; set; } 
    }
}
