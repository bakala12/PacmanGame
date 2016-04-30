using System.Windows.Input;

namespace PacmanGame.MainInterfaces
{
    public interface IHaveControlKeys
    {
        Key UpKey { get; set; }
        Key LeftKey { get; set; }
        Key DownKey { get; set; }
        Key RightKey { get; set; }
        void LoadControlKeys();
        void SaveControlKeys();
    }
}
