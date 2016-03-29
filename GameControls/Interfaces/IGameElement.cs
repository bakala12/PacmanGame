using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameControls.Interfaces
{
    public interface IGameElement
    {
        uint X { get; set; }
        uint Y { get; set; }
        uint ElementWidth { get; }
        uint ElementHeight { get; }
    }
}
