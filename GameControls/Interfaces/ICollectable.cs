using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameControls.Interfaces
{
    /// <summary>
    /// Represents the object that can be collected.
    /// </summary>
    public interface ICollectable : IGameElement
    {
        /// <summary>
        /// Collects current element.
        /// </summary>
        void Collect();
    }
}
