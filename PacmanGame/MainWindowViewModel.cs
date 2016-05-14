using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
    internal class MainWindowViewModel : IViewModelChanger, IHaveControlKeys
    {
        private readonly ISettingsProvider _provider;

        public MainWindowViewModel(IGameBuilder builder, HighscoreList highscores, IGameSerializer gameSerializer,
            IKeysValidator validator, ISettingsProvider provider)
        {
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
            var vm = new List<ViewModelBase>
            {
                new StartMenuViewModel(this, gameSerializer),
                new HelpViewModel(),
                new OptionsViewModel(this, validator),
                new HighscoresViewModel(highscores),
                new GameViewModel(builder, this, this, provider),
                new EndGameViewModel(highscores),
                new PauseViewModel(this, gameSerializer)
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
            LeftKey = _provider.LeftKey;
            RightKey = _provider.RightKey;
            UpKey = _provider.UpKey;
            DownKey = _provider.DownKey;
        }

        public void SaveControlKeys()
        {
            _provider.DownKey = DownKey;
            _provider.LeftKey = LeftKey;
            _provider.RightKey = RightKey;
            _provider.UpKey = UpKey;
            _provider.Save();
        }
        #endregion
    }
}