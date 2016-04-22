using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacmanGame
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
