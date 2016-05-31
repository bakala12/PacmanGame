using System;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// The type of serialized game element.
    /// </summary>
    [Serializable]
    public enum GameElementType
    {
        Block, Coin, Portal, BonusLife, Player, Enemy
    }
}