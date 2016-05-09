using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Elements;
using GameControls.Others;

namespace GameControls.Interfaces
{
    /// <summary>
    /// Represents an alghoritm of movement for enemies.
    /// </summary>
    public interface IMovementAlgorithm
    {
        /// <summary>
        /// Provide a direction in which the given enemy should move.
        /// </summary>
        /// <param name="enemy">The enemy which should be moved.</param>
        /// <returns>The direcition in which enemy would like to move.</returns>
        Direction ProvideDirection(Enemy enemy);
    }
}
