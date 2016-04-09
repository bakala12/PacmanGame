using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame.ViewModels
{
    public interface IViewModelChanger
    {
        ViewModelBase CurrentViewModel { get; }
        void ChangeCurrentViewModel(string name);
    }

    public class MainWindowViewModel : ViewModelBase, IViewModelChanger
    {
        public MainWindowViewModel(IList<ViewModelBase> viewModels)
        {
            if(viewModels==null || viewModels.Count==0)
                throw new ArgumentException("Empty view model list is not accessible");
            ViewModels = viewModels;
        }

        public IList<ViewModelBase> ViewModels { get; }
        
        public ViewModelBase CurrentViewModel { get; set; }

        public void ChangeCurrentViewModel(string name)
        {
            if(string.IsNullOrEmpty(name)) return;
            var viewModel = ViewModels.FirstOrDefault(vm => vm.Name?.Equals(name) ?? false);
            if (viewModel == null) return;
            CurrentViewModel = viewModel;
        }
    }
}
