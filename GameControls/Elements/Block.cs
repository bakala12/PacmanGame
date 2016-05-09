using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameControls.Elements
{
    /// <summary>
    /// Represents a simple block element in the game.
    /// </summary>
    public class Block : GameElement
    {
        static Block()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Block), new FrameworkPropertyMetadata(typeof(Block)));
        }
    }
}
