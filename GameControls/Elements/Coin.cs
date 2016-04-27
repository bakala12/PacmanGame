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

namespace GameControls.Elements
{
    public class Coin : GameElement, ICollectable
    {
        static Coin()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Coin), new FrameworkPropertyMetadata(typeof(Coin)));
        }

        public Coin()
        {
            PointReward = 2;
        }

        public void Collect()
        {
            Collected?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Collected;

        public uint PointReward
        {
            get { return (uint) GetValue(PointRewardProperty); }
            set { SetValue(PointRewardProperty, value);}
        }

        public static readonly DependencyProperty PointRewardProperty = 
            DependencyProperty.Register("PointReward", typeof(uint), typeof(Coin), new FrameworkPropertyMetadata((uint)0));
    }
}
