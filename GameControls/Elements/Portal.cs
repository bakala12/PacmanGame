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

namespace GameControls.Elements
{
    public class Portal : GameElement
    {
        static Portal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Portal), new FrameworkPropertyMetadata(typeof(Portal)));
        }

        public Portal() : this(null)
        {
        }

        public Portal(Portal connected)
        {
            if(connected?.ConnectedPortal != null)
                throw new ArgumentException();
            ConnectedPortal = connected;
            if(connected!=null)
                connected.ConnectedPortal = this;
        }

        public Portal ConnectedPortal
        {
            get { return (Portal) GetValue(ConnectedPortalProperty); }
            protected set { SetValue(ConnectedPortalProperty, value);}
        }

        public static DependencyProperty ConnectedPortalProperty = 
            DependencyProperty.Register("ConnectedPortal", typeof(Portal), typeof(Portal), new FrameworkPropertyMetadata(null));
    }
}
