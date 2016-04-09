using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacmanGame.ViewModels
{
    public class HelpViewModel : CloseableViewModel
    {
        public ICommand ReturnCommand { get; }

        public HelpViewModel() :base("Help")
        {
            ReturnCommand = new DelegateCommand(x=>Close());
        }
    }
}
