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
using System.Windows.Threading;

namespace GameControls.Elements
{
    /// <summary>
    /// Represents the additional life element which can be collected in the game.
    /// This element is available only for the specified time and after that time it disappears.
    /// </summary>
    public class BonusLife : Coin
    {
        /// <summary>
        /// Static constructor that overrides the default style.
        /// </summary>
        static BonusLife()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BonusLife), new FrameworkPropertyMetadata(typeof(BonusLife)));
        }

        /// <summary>
        /// Initializes a new instance of BonusLife element.
        /// </summary>
        public BonusLife() : this(TimeSpan.FromSeconds(10)) { }

        /// <summary>
        /// Initializes a new instance of BonusLife element with the given element lifetime.
        /// </summary>
        /// <param name="lifeTime">The lifetime of the element.</param>
        public BonusLife(TimeSpan lifeTime)
        {
            PointReward = 250;
            LifeTime = lifeTime;
            IsAvailable = false;
        }

        /// <summary>
        /// Gets the lifetime of the current element.
        /// </summary>
        public TimeSpan LifeTime
        {
            get {return (TimeSpan) GetValue(LifeTimeProperty.DependencyProperty); }
            protected set { SetValue(LifeTimeProperty, value);}
        }

        /// <summary>
        /// A DependencyProperty storing value of LifeTime property.
        /// </summary>
        public static readonly DependencyPropertyKey LifeTimeProperty =
            DependencyProperty.RegisterAttachedReadOnly("LifeTime", typeof (TimeSpan), typeof (BonusLife),
                new FrameworkPropertyMetadata(TimeSpan.MaxValue));

        /// <summary>
        /// The RoutedEvent associated with Appeared event.
        /// </summary>
        public static readonly RoutedEvent AppearedEvent =
            EventManager.RegisterRoutedEvent("Appeared", RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (BonusLife));

        /// <summary>
        /// The RoutedEvent associated with Disappeared event.
        /// </summary>
        public static readonly RoutedEvent DisappearedEvent =
            EventManager.RegisterRoutedEvent("Disappeared", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(BonusLife));

        /// <summary>
        /// A DependencyProperty for storing the value of IsAvailableProperty.
        /// </summary>
        public static readonly DependencyProperty IsAvailableProperty = 
            DependencyProperty.Register("IsAvailable", typeof(bool), typeof(BonusLife), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets the information whether the element is still available to be collected.
        /// </summary>
        public bool IsAvailable
        {
            get { return (bool) GetValue(IsAvailableProperty); }
            protected set { SetValue(IsAvailableProperty, value);}
        }

        /// <summary>
        /// Occurs when the element has just appeared in the game.
        /// </summary>
        public event RoutedEventHandler Appeared
        {
            add { AddHandler(AppearedEvent, value);}
            remove { RemoveHandler(AppearedEvent, value);}
        }

        /// <summary>
        /// Occures when the element has just disappeared.
        /// </summary>
        public event RoutedEventHandler Disappeared
        {
            add { AddHandler(DisappearedEvent, value); }
            remove { RemoveHandler(DisappearedEvent, value); }
        }

        /// <summary>
        /// Make the element appear in the game and become available.
        /// </summary>
        public void Appear()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = LifeTime;
            timer.Tick += (x, e) =>
            {
                IsAvailable = false;
                RoutedEventArgs arg = new RoutedEventArgs(DisappearedEvent);
                RaiseEvent(arg);
            };
            IsAvailable = true;
            RoutedEventArgs args = new RoutedEventArgs(AppearedEvent);
            RaiseEvent(args);
            timer.Start();
        }
    }
}
