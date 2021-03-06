﻿using System;
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
    /// <summary>
    /// A control representing a pacman, the main character of the game. This include support for 
    /// moving the pacman's mouth with animation. 
    /// </summary>
    public class Player : MovableElement
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static Player()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Player), new FrameworkPropertyMetadata(typeof(Player)));
        }

        /// <summary>
        /// Initializes a new instance of Player control.
        /// </summary>
        public Player()
        {
            Direction = Direction.Left;
            Angle = 90;
            IsAlive = true;
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
        /// Gets or sets the angle of the pacman mouth.
        /// </summary>
        public double Angle
        {
            get { return (double) GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value);}
        }

        /// <summary>
        /// Gets the value indicating whether the current player is alive.
        /// </summary>
        public bool IsAlive
        {
            get { return (bool) GetValue(IsAliveProperty); }
            set { SetValue(IsAliveProperty, value);}
        }

        /// <summary>
        /// Dependency property for storing Direction property.
        /// </summary>
        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register("Direction", typeof(Direction), typeof(Player), new PropertyMetadata(Direction.None));

        /// <summary>
        /// Dependency property for storing Angle property.
        /// </summary>
        public static readonly DependencyProperty AngleProperty = 
            DependencyProperty.Register("Angle", typeof(double), typeof(Player),
                new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Dependency property for storing IsAlive property.
        /// </summary>
        public static readonly DependencyProperty IsAliveProperty =
            DependencyProperty.Register("IsAlive", typeof (bool), typeof (Player),
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Overrides the Move method. It also changes pacman direction.
        /// </summary>
        /// <param name="direction">The direction in which player would be moved.</param>
        public override void Move(Direction direction)
        {
            base.Move(direction);
            Direction = direction;
        }

        /// <summary>
        /// Kills the current Player.
        /// </summary>
        public void Die()
        {
            IsAlive = false;
            RoutedEventArgs args = new RoutedEventArgs(DeadEvent);
            RaiseEvent(args);
        }

        /// <summary>
        /// Occurs when the current Player died.
        /// </summary>
        public event RoutedEventHandler Dead
        {
            add { AddHandler(DeadEvent, value);}
            remove { RemoveHandler(DeadEvent, value);}
        }

        /// <summary>
        /// The RoutedEvent associated with the Dead event.
        /// </summary>
        public static readonly RoutedEvent DeadEvent =
            EventManager.RegisterRoutedEvent("Dead", RoutingStrategy.Bubble, typeof (RoutedEventHandler),
                typeof (Player));
    }
}
