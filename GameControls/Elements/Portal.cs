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
    /// <summary>
    /// Represents an element that provides a special two-way connection between two places.
    /// </summary>
    public class Portal : GameElement
    {
        /// <summary>
        /// Static constructor that overrides the default style.
        /// </summary>
        static Portal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Portal), new FrameworkPropertyMetadata(typeof(Portal)));
        }

        /// <summary>
        /// Initializes a new instance of single not connected Portal.
        /// </summary>
        public Portal() : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of portal, connected with the given Portal element.
        /// </summary>
        /// <param name="connected">A portal to be connected with the newly created one.</param>
        /// <exception cref="ArgumentException">Occurs when the given portal is null or is currently connected with another one portal.</exception>
        public Portal(Portal connected)
        {
            if(connected?.ConnectedPortal != null)
                throw new ArgumentException();
            ConnectedPortal = connected;
            if(connected!=null)
                connected.ConnectedPortal = this;
        }

        /// <summary>
        /// Gets the connected Portal with the current one.
        /// </summary>
        public Portal ConnectedPortal
        {
            get { return (Portal) GetValue(ConnectedPortalProperty); }
            set { SetValue(ConnectedPortalProperty, value);}
        }

        /// <summary>
        /// The Id of the portal.
        /// </summary>
        public int PortalId
        {
            get { return (int) GetValue(PortalIdProperty); }
            set { SetValue(PortalIdProperty, value);}
        }

        /// <summary>
        /// A DependencyProperty for storing ConnectedPortal property.
        /// </summary>
        public static readonly DependencyProperty ConnectedPortalProperty = 
            DependencyProperty.Register("ConnectedPortal", typeof(Portal), typeof(Portal), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// A DependencyProperty for storing PortalId property.
        /// </summary>
        public static readonly DependencyProperty PortalIdProperty = 
            DependencyProperty.Register("PortalId", typeof(int), typeof(Portal), new FrameworkPropertyMetadata(0));
    }
}
