using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
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
    public class GameViewModel : CloseableViewModel
    {
        private readonly IGameBuilder _builder;
        private readonly IHaveControlKeys _accessor;
        private readonly IViewModelChanger _viewModelChanger;

        public GameBoard GameBoard { get; protected set; }

        public GameEngine GameEngine { get; protected set; }

        public GameViewModel(IGameBuilder builder, IViewModelChanger viewModelChanger, IHaveControlKeys accessor) : base("Game")
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            _builder = builder;
            if(accessor==null) throw new ArgumentNullException(nameof(accessor));
            _accessor = accessor;
            if(viewModelChanger==null) throw new ArgumentNullException(nameof(viewModelChanger));
            _viewModelChanger = viewModelChanger;
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

        [OnCommand("PauseCommand")]
        public virtual void Pause()
        {
            GameEngine.Timer.Stop();
            GameEngine?.EnemyMovementManager?.Stop();
            _viewModelChanger.ChangeCurrentViewModel("Pause");
        }

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
