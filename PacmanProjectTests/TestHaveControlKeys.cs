using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.MainInterfaces;

namespace PacmanProjectTests
{
    public class TestHaveControlKeys : IHaveControlKeys
    {
        public Key LeftKey { get; set; }
        public Key RightKey { get; set; }
        public Key UpKey { get; set; }
        public Key DownKey { get; set; }
        public void LoadControlKeys()
        {
            LeftKey = Key.Left;
            RightKey = Key.Right;
            UpKey = Key.Up;
            DownKey = Key.Down;
        }

        public void SaveControlKeys()
        {
        }
    }
}
