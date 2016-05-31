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
    /// <summary>
    /// The view model for the main window. 
    /// This provides implementation of IViewModelChanger and IHaveControlKeys interfaces.
    /// </summary>
    [ImplementPropertyChanged]
    internal class MainWindowViewModel : ViewModelBase, IViewModelChanger, IHaveControlKeys
    {
        private readonly ISettingsProvider _provider;

        /// <summary>
        /// Initializes a new instance of MainWindowViewModel.
        /// </summary>
        /// <param name="builder">An object that is responsible for building core game objects.</param>
        /// <param name="highscores">The list of the highscores in the game.</param>
        /// <param name="gameSerializer">An object that is responsible for saving and loading GameState.</param>
        /// <param name="validator">An object that validates control keys used in the game.</param>
        /// <param name="provider">An object that provides some configuration settings used in the game.</param>
        public MainWindowViewModel(IGameBuilder builder, HighscoreList highscores, IGameSerializer gameSerializer,
            IKeysValidator validator, ISettingsProvider provider) : base("Main")
        {
            if(provider==null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
            var vm = new List<ViewModelBase>
            {
                new StartMenuViewModel(this, gameSerializer),
                new OptionsViewModel(this, validator),
                new HighscoresViewModel(highscores),
                new GameViewModel(builder, this, this, provider),
                new EndGameViewModel(highscores),
                new PauseViewModel(this, gameSerializer)
            };
            foreach (var result in vm.OfType<CloseableViewModel>())
            {
                result.RequestClose += (sender, args) => OnCloseView((sender as ViewModelBase)?.Name);
            }
            ViewModels = vm;
        }

        private void OnCloseView(string name)
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

        /// <summary>
        /// Gets the list of supported view models.
        /// </summary>
        public IList<ViewModelBase> ViewModels { get; }

        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Gets the view model associated with the currently displaying view in the application.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            private set
            {
                _currentViewModel = value;      
                (CurrentViewModel as CloseableViewModel)?.RaiseViewAppearedEvent();
            }
        }

        /// <summary>
        /// Changes currently displaying view.
        /// </summary>
        /// <param name="name">The name of the view to be displayed.</param>
        public void ChangeCurrentViewModel(string name)
        {
            if (string.IsNullOrEmpty(name)) return;
            var viewModel = ViewModels.FirstOrDefault(vm => vm.Name?.Equals(name) ?? false);
            if (viewModel == null) return;
            CurrentViewModel = viewModel;
        }

        /// <summary>
        /// Gets the ViewModelBase object specified by the given name.
        /// </summary>
        /// <param name="name">The name f the view model.</param>
        /// <returns>A view model associated with the given name.</returns>
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
        /// <summary>
        /// Gets or sets the Key that move player in left direction.
        /// </summary>
        public Key LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in right direction.
        /// </summary>
        public Key RightKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in up direction.
        /// </summary>
        public Key UpKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in down direction.
        /// </summary>
        public Key DownKey { get; set; }
        /// <summary>
        /// Loads the current configuration of control keys.
        /// </summary>
        public void LoadControlKeys()
        {
            LeftKey = _provider.LeftKey;
            RightKey = _provider.RightKey;
            UpKey = _provider.UpKey;
            DownKey = _provider.DownKey;
        }
        /// <summary>
        /// Saves the current configuration of control keys.
        /// </summary>
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