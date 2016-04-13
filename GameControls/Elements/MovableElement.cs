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
    /// <summary>
    /// Represents an element in the game which has the movement ability.
    /// </summary>
    public abstract class MovableElement : GameElement, IMovable
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static MovableElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovableElement), new FrameworkPropertyMetadata(typeof(MovableElement)));
        }

        /// <summary>
        /// Initializes a new instance of MovableElement object.
        /// </summary>
        protected MovableElement()
        {

        }

        /// <summary>
        /// Initializes a new instance of MovableElement class with the given width, height and speed.
        /// </summary>
        /// <param name="width">Width of the element.</param>
        /// <param name="height">Height of the element.</param>
        /// <param name="speed">Movement speed of the element.</param>
        protected MovableElement(uint width, uint height, uint speed=1) : base(width, height)
        {
            Speed = speed;
        }

        /// <summary>
        /// Gets or sets the movement speed of the current element.
        /// </summary>
        public uint Speed
        {
            get { return (uint)GetValue(SpeedProperty); }
            set { SetValue(SpeedProperty, value); }
        }

        /// <summary>
        /// Dependency property for storing Speed property.
        /// </summary>
        public static readonly DependencyProperty SpeedProperty =
            DependencyProperty.Register("Speed", typeof(uint), typeof(MovableElement), new PropertyMetadata(0));

        /// <summary>
        /// Moves the object in the specified direction.
        /// </summary>
        /// <param name="direction">The dierction in which the element should be moved.</param>
        public virtual void Move(Direction direction)
        {
            RaiseMovementEvents(direction);
        }

        protected virtual void RaiseMovementEvents(Direction direction)
        {
            RoutedEvent routedEvent;
            switch (direction)
            {
                case Direction.Left:
                    routedEvent = MoveLeftEvent;
                    break;
                case Direction.Right:
                    routedEvent = MoveRightEvent;
                    break;
                case Direction.Up:
                    routedEvent = MoveUpEvent;
                    break;
                case Direction.Down:
                    routedEvent = MoveDownEvent;
                    break;
                default:
                    return;
            }
            MovementEventArgs e = new MovementEventArgs(direction, MovedEvent, this);
            MovementEventArgs e1 = new MovementEventArgs(direction, routedEvent, this);
            RaiseEvent(e);
            RaiseEvent(e1);
        }

        /// <summary>
        /// An event which is raised when the object move in the left direction.
        /// </summary>
        public event MovementEventHandler MoveLeft
        {
            add { AddHandler(MoveLeftEvent, value); }
            remove { RemoveHandler(MoveLeftEvent, value); }
        }

        /// <summary>
        /// An event which is raised when the object move in the right direction.
        /// </summary>
        public event MovementEventHandler MoveRight
        {
            add { AddHandler(MoveRightEvent, value); }
            remove { RemoveHandler(MoveRightEvent, value); }
        }

        /// <summary>
        /// An event which is raised when the object move in the up direction.
        /// </summary>
        public event MovementEventHandler MoveUp
        {
            add { AddHandler(MoveUpEvent, value); }
            remove { RemoveHandler(MoveUpEvent, value); }
        }

        /// <summary>
        /// An event which is raised when the object move in the down direction.
        /// </summary>
        public event MovementEventHandler MoveDown
        {
            add { AddHandler(MoveDownEvent, value); }
            remove { RemoveHandler(MoveDownEvent, value); }
        }

        /// <summary>
        /// An event raised when the element moves in any direction.
        /// </summary>
        public event MovementEventHandler Moved
        {
            add { AddHandler(MovedEvent, value); }
            remove { RemoveHandler(MovedEvent, value); }
        }

        /// <summary>
        /// A RoutedEvent which handles MoveLeft event. 
        /// </summary>
        public static readonly RoutedEvent MoveLeftEvent =
            EventManager.RegisterRoutedEvent("MoveLeft", RoutingStrategy.Bubble, typeof(MovementEventHandler), typeof(MovableElement));

        /// <summary>
        /// A RoutedEvent which handles MoveLeft event. 
        /// </summary>
        public static readonly RoutedEvent MoveRightEvent =
            EventManager.RegisterRoutedEvent("MoveRight", RoutingStrategy.Bubble, typeof(MovementEventHandler), typeof(MovableElement));

        /// <summary>
        /// A RoutedEvent which handles MoveRight event. 
        /// </summary>
        public static readonly RoutedEvent MoveUpEvent = 
            EventManager.RegisterRoutedEvent("MoveUp", RoutingStrategy.Bubble, typeof(MovementEventHandler), typeof(MovableElement));

        /// <summary>
        /// A RoutedEvent which handles MoveDown event. 
        /// </summary>
        public static readonly RoutedEvent MoveDownEvent =
            EventManager.RegisterRoutedEvent("MoveDown", RoutingStrategy.Bubble, typeof(MovementEventHandler), typeof(MovableElement));

        /// <summary>
        /// A RoutedEvent which handles Moved event. 
        /// </summary>
        public static readonly RoutedEvent MovedEvent = 
            EventManager.RegisterRoutedEvent("Moved", RoutingStrategy.Bubble, typeof(MovementEventHandler), typeof(MovableElement));
    }
}
