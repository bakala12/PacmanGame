using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GameControls.Board;
using GameControls.Elements;
using GameControls.Others;
using PacmanGame.Engine;
using PacmanGame.MainInterfaces;
using PacmanGame.Serialization;

namespace PacmanGame.ViewModels
{
    public class GameViewModel : CloseableViewModel
    {
        private readonly IGameBuilder _builder;

        public ICommand PauseCommand { get; }
        public ICommand MoveCommand { get; }

        public GameBoard GameBoard { get; protected set; }

        public GameEngine GameEngine { get; protected set; }

        public GameViewModel(IGameBuilder builder) : base("Game")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            _builder = builder;
            PauseCommand = new DelegateCommand(x => Pause());
            MoveCommand = new DelegateCommand(MovePlayer);
        }

        protected override void OnViewAppeared()
        {
            base.OnViewAppeared();
            GameEngine?.Timer?.Start();
            GameEngine?.EnemyMovementManager?.Start();
        }

        public virtual void StartGame()
        {
            StartGame(null);
        }

        public virtual void StartGame(GameState state)
        {
            GameBoard = _builder.BuildBoard(state);
            GameEngine = _builder.BuildGameEngine(state, GameBoard);
            Player player = GameBoard.Elements.OfType<Player>().Single();
            player.Dead += (x,e)=> OnGameEnded();
        }

        public virtual void MovePlayer(object parameter)
        {
            Key key = parameter as Key? ?? Key.None;
            if (key == Key.None) return;
            IHaveControlKeys controlKeysAccessor = (Application.Current as App)?.ControlKeysAccessor;
            if (controlKeysAccessor == null) throw new InvalidOperationException();
            Direction direction;
            if (controlKeysAccessor.LeftKey == key) direction = Direction.Left;
            else if (controlKeysAccessor.RightKey == key) direction = Direction.Right;
            else if (controlKeysAccessor.UpKey == key) direction = Direction.Up;
            else if (controlKeysAccessor.DownKey == key) direction = Direction.Down;
            else direction = Direction.None;
            GameEngine.MovePlayer(direction);
        }

        public virtual void Pause()
        {
            GameEngine.Timer.Stop();
            GameEngine?.EnemyMovementManager?.Stop();
            var viewModelChager = (Application.Current as App)?.ViewModelChanger;
            viewModelChager?.ChangeCurrentViewModel("Pause");
        }

        public virtual void OnGameEnded()
        {
            GameEngine.Timer.Stop();
            GameEngine.EnemyMovementManager.Stop();
            TimeSpan time = GameEngine.Timer.TimeLeft;
            var viewModelChager = (Application.Current as App)?.ViewModelChanger;
            var endGameViewModel = viewModelChager?.GetViewModelByName("EndGame") as EndGameViewModel;
            if(endGameViewModel == null) throw new InvalidOperationException();
            endGameViewModel.Points = GameEngine.Points;
            endGameViewModel.GameTime = time;
            viewModelChager?.ChangeCurrentViewModel("EndGame");
        }
    }
}
