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
    /// Represents a custom WPF control providing base functionality for all game elements. 
    /// </summary>
    public abstract class GameElement : Control, IGameElement
    {
        /// <summary>
        /// Static constructor.
        /// </summary>
        static GameElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameElement), new FrameworkPropertyMetadata(typeof(GameElement)));
        }

        /// <summary>
        /// Initializes a new instance of GameElement object and sets all the properties to 0.
        /// </summary>
        protected GameElement()
        {
            X = 0;
            Y = 0;
            ElementHeight = 0;
            ElementWidth = 0;
        }

        /// <summary>
        /// Initializes a new instance of GameElement object with the specified width and height.
        /// </summary>
        /// <param name="width">Width of the element.</param>
        /// <param name="height">Height of the element.</param>
        protected GameElement(double width, double height):this()
        {
            ElementHeight = height;
            ElementWidth = width;
        }

        /// <summary>
        /// A DependencyProperty for storing X value.
        /// </summary>
        public static readonly DependencyProperty XProperty = 
            DependencyProperty.Register("X", typeof(double), typeof(GameElement),
                new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender|
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// A DependencyProperty for storing Y value.
        /// </summary>
        public static readonly DependencyProperty YProperty = 
            DependencyProperty.Register("Y", typeof(double), typeof(GameElement), new FrameworkPropertyMetadata((double)0,
                FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// A DependencyProperty for storing ElementWidth value.
        /// </summary>
        public static readonly DependencyProperty ElementWidthProperty = 
            DependencyProperty.Register("ElementWidth", typeof(double), typeof(GameElement),
                new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// A DependencyProperty for storing ElementHeight value.
        /// </summary>
        public static readonly DependencyProperty ElementHeightProperty = 
            DependencyProperty.Register("ElementHeight", typeof(double), typeof(GameElement), 
                new FrameworkPropertyMetadata((double)0, FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        /// <summary>
        /// Gets or sets the current X position of the element.
        /// </summary>
        public double X
        {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value);}
        }

        /// <summary>
        /// Gets or sets the current Y position of the element.
        /// </summary>
        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value);}
        }

        /// <summary>
        /// Gets or sets the width of the element.
        /// </summary>
        public double ElementWidth
        {
            get { return (double) GetValue(ElementWidthProperty); }
            protected set { SetValue(ElementWidthProperty, value);}
        }

        /// <summary>
        /// Gets or sets the height of the element.
        /// </summary>
        public double ElementHeight
        {
            get { return (double) GetValue(ElementHeightProperty); }
            protected set { SetValue(ElementHeightProperty, value);}
        }

        /// <summary>
        /// Overrides a part of the arrangement proccess. It is used to receive
        /// the width and height of the element.
        /// </summary>
        /// <param name="arrangeBounds">The size of the element.</param>
        /// <returns>Final size of the element.</returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Size ret = base.ArrangeOverride(arrangeBounds);
            ElementWidth = ret.Width;
            ElementHeight = ret.Height;
            return ret;
        }
    }
}
