using PacmanGame.Serialization;

namespace PacmanGame.MainInterfaces
{
    public interface IGameSerializer
    {
        GameState LoadGame(string path);
        void SaveGame(GameState state, string path);
    }
}