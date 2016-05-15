using System;

namespace PacmanGame.Serialization
{
    [Serializable]
    public class GameElementInfo
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public GameElementType Type { get; set; }
    }
}