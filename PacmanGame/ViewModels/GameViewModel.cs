using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Commander;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Others;
using PacmanGame.Annotations;
using PacmanGame.Engine;
using PacmanGame.MainInterfaces;
using PacmanGame.Serialization;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a view model associated with the GameView.
    /// </summary>
    public class GameViewModel : CloseableViewModel
    {
        private readonly IGameBuilder _builder;
        private readonly IHaveControlKeys _accessor;
        private readonly IViewModelChanger _viewModelChanger;
        private readonly ISettingsProvider _provider;

        /// <summary>
        /// Gets the GameBoard.
        /// </summary>
        public GameBoard GameBoard { get; protected set; }

        /// <summary>
        /// Gets the GameEngine.
        /// </summary>
        public GameEngine GameEngine { get; protected set; }

        /// <summary>
        /// Initializes a new instance of GameViewModel object.
        /// </summary>
        /// <param name="builder">An object that builds core game objects.</param>
        /// <param name="viewModelChanger">An object that changes views in the application.</param>
        /// <param name="accessor">An object that have access to control keys.</param>
        /// <param name="provider">An object that provides some configuration settings used in the game.</param>
        public GameViewModel(IGameBuilder builder, IViewModelChanger viewModelChanger, IHaveControlKeys accessor, ISettingsProvider provider) : base("Game")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            _builder = builder;
            if(accessor==null) throw new ArgumentNullException(nameof(accessor));
            _accessor = accessor;
            if(viewModelChanger==null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
            if(provider == null) throw new ArgumentNullException(nameof(provider));
            _provider = provider;
        }

        protected override void OnViewAppeared()
        {
            base.OnViewAppeared();
            GameEngine?.Timer?.Start();
            GameEngine?.EnemyMovementManager?.Start();
        }

        /// <summary>
        /// Starts new game.
        /// </summary>
        public virtual void StartGame()
        {
            StartGame(null);
        }

        /// <summary>
        /// Starts new game with the given state.
        /// </summary>
        /// <param name="state">The state of the game.</param>
        public virtual void StartGame(GameState state)
        {
            GameBoard = _builder.BuildBoard(state);
            GameEngine = _builder.BuildGameEngine(state, GameBoard, _provider);
            Player player = GameBoard.Elements.OfType<Player>().Single();
            player.Dead += (x,e)=> OnGameEnded();
        }

        /// <summary>
        /// Moves the player in the specified direction.
        /// </summary>
        /// <param name="parameter">Informs about the movement direction.</param>
        [OnCommand("MoveCommand")]
        public virtual void MovePlayer(object parameter)
        {
            Key key = parameter as Key? ?? Key.None;
            if (key == Key.None) return;
            Direction direction;
            if (_accessor.LeftKey == key) direction = Direction.Left;
            else if (_accessor.RightKey == key) direction = Direction.Right;
            else if (_accessor.UpKey == key) direction = Direction.Up;
            else if (_accessor.DownKey == key) direction = Direction.Down;
            else direction = Direction.None;
            GameEngine.MovePlayer(direction);
        }

        /// <summary>
        /// Switches to the pause mode.
        /// </summary>
        [OnCommand("PauseCommand")]
        public virtual void Pause()
        {
            GameEngine.Timer.Stop();
            GameEngine?.EnemyMovementManager?.Stop();
            _viewModelChanger.ChangeCurrentViewModel("Pause");
        }

        /// <summary>
        /// Switches to the EndGame view.
        /// </summary>
        public virtual void OnGameEnded()
        {
            GameEngine.Timer.Stop();
            GameEngine.EnemyMovementManager.Stop();
            TimeSpan time = GameEngine.Timer.TimeLeft;
            var endGameViewModel = _viewModelChanger.GetViewModelByName("EndGame") as EndGameViewModel;
            if(endGameViewModel == null) throw new InvalidOperationException();
            endGameViewModel.Points = GameEngine.Points;
            endGameViewModel.GameTime = time;
            _viewModelChanger?.ChangeCurrentViewModel("EndGame");
        }
    }
}
