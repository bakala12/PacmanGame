using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PacmanGame.Annotations;
using PacmanGame.Highscores;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;
using PacmanGame.ViewModels;
using PropertyChanged;

namespace PacmanGame
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel : IViewModelChanger, IHaveControlKeys
    {
        public MainWindowViewModel(IGameBuilder builder, HighscoreList highscores)
        {
            var vm = new List<ViewModelBase>
            {
                new StartMenuViewModel(),
                new HelpViewModel(),
                new OptionsViewModel(this),
                new HighscoresViewModel(highscores),
                new GameViewModel(builder),
                new EndGameViewModel(highscores),
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
                ChangeCurrentViewModel("StartMenu");
            }
            else if (name == "Pause")
            {
                ChangeCurrentViewModel("Game");
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
                (CurrentViewModel as CloseableViewModel)?.RaiseViewAppearedEvent();
            }
        }

        public void ChangeCurrentViewModel(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
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

        #region IHaveControlKeys implementation

        public Key UpKey { get; set; }

        public Key LeftKey { get; set; }

        public Key DownKey { get; set; }

        public Key RightKey { get; set; }

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
        #endregion
    }
}