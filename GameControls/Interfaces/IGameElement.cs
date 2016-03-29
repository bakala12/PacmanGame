using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameControls.Interfaces
{
    /// <summary>
    /// Provides the access to basic element's properties.
    /// </summary>
    public interface IGameElement
    {
        /// <summary>
        /// Gets the current X position of the element.
        /// </summary>
        uint X { get; set; }
        /// <summary>
        /// Gets the current Y position of the element.
        /// </summary>
        uint Y { get; set; }
        /// <summary>
        /// Gets the width of the element.
        /// </summary>
        uint ElementWidth { get; }
        /// <summary>
        /// Gets the height of the element.
        /// </summary>
        uint ElementHeight { get; }
    }
}
