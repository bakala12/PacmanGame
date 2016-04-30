using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameControls.Elements;
using GameControls.Others;

namespace GameControls.Interfaces
{
    public interface IMovementAlgorithm
    {
        Direction ProvideDirection(Enemy enemy);
    }
}
