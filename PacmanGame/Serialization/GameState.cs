using System;
using System.Collections;
using System.Collections.Generic;

namespace PacmanGame.Serialization
{
    [Serializable]
    public class GameState
    {
        public GameState()
        {
            Points = 0;
            Difficulty = 0;
            Lifes = 3;
            Time = TimeSpan.Zero;
            GameElements = new List<GameElementInfo>();
            ConnetedPortals = new PortalConnetcionList();
        }

        public uint Points { get; set; }
        public uint Difficulty { get; set; }
        public uint Lifes { get; set; }
        public TimeSpan Time { get; set; }
        public List<GameElementInfo> GameElements { get; set; }
        public PortalConnetcionList ConnetedPortals { get; set; }
    }
}
