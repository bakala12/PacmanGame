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
    /// <summary>
    /// Represents a coin element that can be collected.
    /// </summary>
    public class Coin : GameElement, ICollectable
    {
        /// <summary>
        /// Static constructor which overrides the default style.
        /// </summary>
        static Coin()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Coin), new FrameworkPropertyMetadata(typeof(Coin)));
        }

        /// <summary>
        /// Initializes a new instance of the Coin element.
        /// </summary>
        public Coin()
        {
            PointReward = 2;
        }

        /// <summary>
        /// Collects the current coin.
        /// </summary>
        public void Collect()
        {
            Collected?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occures when the coin was collected.
        /// </summary>
        public event EventHandler Collected;

        /// <summary>
        /// Gets or sets a point reward given for collecting the coin.
        /// </summary>
        public uint PointReward
        {
            get { return (uint) GetValue(PointRewardProperty); }
            set { SetValue(PointRewardProperty, value);}
        }

        /// <summary>
        /// A DependencyProperty for storing the value of PointReward property.
        /// </summary>
        public static readonly DependencyProperty PointRewardProperty = 
            DependencyProperty.Register("PointReward", typeof(uint), typeof(Coin), new FrameworkPropertyMetadata((uint)0));
    }
}
