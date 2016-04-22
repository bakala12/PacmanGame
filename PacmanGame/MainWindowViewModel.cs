using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PacmanGame.Annotations;
using PacmanGame.Properties;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    public class MainWindowViewModel : IViewModelChanger, IHaveControlKeys, INotifyPropertyChanged
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

        private Key _upKey;
        private Key _downKey;
        private Key _leftKey;
        private Key _rightKey;

        public Key UpKey
        {
            get { return _upKey; }
            set { _upKey = value; OnPropertyChanged(); }
        }

        public Key LeftKey
        {
            get { return _leftKey; }
            set { _leftKey = value; OnPropertyChanged(); }
        }

        public Key DownKey
        {
            get { return _downKey; }
            set { _downKey = value; OnPropertyChanged(); }
        }

        public Key RightKey
        {
            get { return _rightKey; }
            set { _rightKey = value; OnPropertyChanged(); }
        }

        public void LoadControlKeys()
        {
            LeftKey = Settings.Default.LeftKey;
            RightKey = Settings.Default.RightKey;
            UpKey = Settings.Default.UpKey;
            DownKey = Settings.Default.DownKey;
        }

        public void SaveControlKeys()
        {
            Settings.Default.DownKey = DownKey;
            Settings.Default.LeftKey = LeftKey;
            Settings.Default.RightKey = RightKey;
            Settings.Default.UpKey = UpKey;
            Settings.Default.Save();
        }
    }
}