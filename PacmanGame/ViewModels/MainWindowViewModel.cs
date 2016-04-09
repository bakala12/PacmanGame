using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PacmanGame.Annotations;

namespace PacmanGame.ViewModels
{
    public interface IViewModelChanger
    {
        ViewModelBase CurrentViewModel { get; }
        void ChangeCurrentViewModel(string name);
    }

    public class MainWindowViewModel : IViewModelChanger, INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            var vm = new List<ViewModelBase>
            {
                new StartMenuViewModel(),
                new HelpViewModel(),
                new OptionsViewModel(),
                new HighscoresViewModel(),
                new GameViewModel(),
                new EndGameViewModel(),
                new PauseViewModel()
            };
            foreach (var result in vm.OfType<CloseableViewModel>())
            {
                result.RequestClose += (sender, args) => OnClose((sender as ViewModelBase)?.Name);
            }
            ViewModels = vm;
        }

        private void OnClose(string name)
        {
            if (name == "Help" || name == "Options" || name == "Highscores" || name == "EndGame")
            {
                (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("StartMenu");
            }
            else if (name == "Pause")
            {
                (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Game");
            }
        }

        public IList<ViewModelBase> ViewModels { get; }

        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public void ChangeCurrentViewModel(string name)
        {
            if(string.IsNullOrEmpty(name)) return;
            var viewModel = ViewModels.FirstOrDefault(vm => vm.Name?.Equals(name) ?? false);
            if (viewModel == null) return;
            CurrentViewModel = viewModel;
        }

        public ViewModelBase GetViewModelByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty"); 
            var viewModel = ViewModels.FirstOrDefault(vm => vm.Name?.Equals(name) ?? false);
            if (viewModel == null)
                throw new ArgumentException("Cannot find view model with the given name");
            return viewModel;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
