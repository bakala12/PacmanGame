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
    public class GameElement : Control, IGameElement
    {
        static GameElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameElement), new FrameworkPropertyMetadata(typeof(GameElement)));
        }

        public static readonly DependencyProperty XProperty = 
            DependencyProperty.Register("X", typeof(int), typeof(GameElement),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender|
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty YProperty = 
            DependencyProperty.Register("Y", typeof(int), typeof(GameElement), new FrameworkPropertyMetadata(0,
                FrameworkPropertyMetadataOptions.AffectsRender |
                    FrameworkPropertyMetadataOptions.AffectsArrange |
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange));

        public static readonly DependencyProperty ElementWidthProperty = 
            DependencyProperty.Register("ElementWidth", typeof(uint), typeof(GameElement),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ElementHeightProperty = 
            DependencyProperty.Register("ElementHeight", typeof(uint), typeof(GameElement), 
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        public uint X
        {
            get { return (uint) GetValue(XProperty); }
            set { SetValue(XProperty, value);}
        }

        public uint Y
        {
            get { return (uint) GetValue(YProperty); }
            set { SetValue(YProperty, value);}
        }

        public uint ElementWidth
        {
            get { return (uint) GetValue(ElementWidthProperty); }
            set { SetValue(ElementWidthProperty, value);}
        }

        public uint ElementHeight
        {
            get { return (uint) GetValue(ElementHeightProperty); }
            set { SetValue(ElementHeightProperty, value);}
        }
    }
}
