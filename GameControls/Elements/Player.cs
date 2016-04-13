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
using GameControls.Others;

namespace GameControls.Elements
{
    public class Player : MovableElement
    {
        static Player()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Player), new FrameworkPropertyMetadata(typeof(Player)));
        }

        /// <summary>
        /// The direction of pacman.
        /// </summary>
        public Direction Direction
        {
            get { return (Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        /// <summary>
        /// Dependency property for storing Direction property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(Direction), typeof(Player), new PropertyMetadata(Direction.None));


    }
}
