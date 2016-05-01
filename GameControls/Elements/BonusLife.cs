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
    public class BonusLife : Coin
    {
        static BonusLife()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BonusLife), new FrameworkPropertyMetadata(typeof(BonusLife)));
        }

        public BonusLife() : this(TimeSpan.FromSeconds(10)) { }

        public BonusLife(TimeSpan timeAvailability)
        {
            PointReward = 250;
            TimeAvailability = timeAvailability;
            IsAvailable = false;
        }

        public TimeSpan TimeAvailability
        {
            get {return (TimeSpan) GetValue(TimeAvailabilityProperty.DependencyProperty); }
            protected set { SetValue(TimeAvailabilityProperty, value);}
        }

        public static readonly DependencyPropertyKey TimeAvailabilityProperty =
            DependencyProperty.RegisterAttachedReadOnly("TimeAvailability", typeof (TimeSpan), typeof (BonusLife),
                new FrameworkPropertyMetadata(TimeSpan.MaxValue));

        public static readonly RoutedEvent AppearedEvent =
            EventManager.RegisterRoutedEvent("Appeared", RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (BonusLife));

        public static readonly RoutedEvent DisappearedEvent =
            EventManager.RegisterRoutedEvent("Disappeared", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(BonusLife));

        public static readonly DependencyProperty IsAvailableProperty = 
            DependencyProperty.Register("IsAvailable", typeof(bool), typeof(BonusLife), new FrameworkPropertyMetadata(false));

        public bool IsAvailable
        {
            get { return (bool) GetValue(IsAvailableProperty); }
            protected set { SetValue(IsAvailableProperty, value);}
        }

        public event RoutedEventHandler Appeared
        {
            add { AddHandler(AppearedEvent, value);}
            remove { RemoveHandler(AppearedEvent, value);}
        }

        public event RoutedEventHandler Disappeared
        {
            add { AddHandler(DisappearedEvent, value); }
            remove { RemoveHandler(DisappearedEvent, value); }
        }

        public void Appear()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeAvailability;
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
