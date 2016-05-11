using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.MainInterfaces;

namespace PacmanGame.ViewModels
{
    public class HelpViewModel : CloseableViewModel
    {
        public HelpViewModel() :base("Help")
        {
        }
    }
}
