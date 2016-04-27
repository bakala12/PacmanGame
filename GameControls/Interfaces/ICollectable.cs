using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameControls.Interfaces
{
    public interface ICollectable : IGameElement
    {
        void Collect();
    }
}
