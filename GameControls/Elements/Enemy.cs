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
using GameControls.Interfaces;
using GameControls.Others;

namespace GameControls.Elements
{
    public class Enemy : MovableElement
    {
        static Enemy()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Enemy), new FrameworkPropertyMetadata(typeof(Enemy)));
        }

        public Enemy() : this(null)
        {
        }

        public Enemy(IMovementAlgorithm movementAlgorithm)
        {
            MovementAlgorithm = movementAlgorithm;
            Speed = 1;
        }

        public IMovementAlgorithm MovementAlgorithm
        {
            get { return (IMovementAlgorithm) GetValue(MovementAlgorithmProperty); }
            set { SetValue(MovementAlgorithmProperty, value);}
        }

        public static readonly DependencyProperty MovementAlgorithmProperty = 
            DependencyProperty.Register("MovementAlgorithm", typeof(IMovementAlgorithm), typeof(Enemy), new FrameworkPropertyMetadata(null));
    }
}
