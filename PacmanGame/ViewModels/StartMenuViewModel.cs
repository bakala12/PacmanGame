using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PacmanGame.ViewModels
{
    public class StartMenuViewModel : ViewModelBase
    {
        public ICommand NewGameCommand { get; }
        public ICommand LoadGameCommand { get; }
        public ICommand ShowHighscoresCommand { get; }
        public ICommand ShowOptionsCommand { get; }
        public ICommand ShowHelpCommand { get; }

        /// <summary>
        /// Gets a command invoked when the Exit button from associated view was pressed.
        /// </summary>
        public ICommand ExitCommand { get; }

        /// <summary>
        /// Initializes a new instance of StartMenuViewModel class
        /// </summary>
        public StartMenuViewModel()
        {
            NewGameCommand = new DelegateCommand(x=>NewGame());
            LoadGameCommand = new DelegateCommand(x=>LoadGame());
            ShowHighscoresCommand = new DelegateCommand(x=>ShowHighscores());
            ShowOptionsCommand = new DelegateCommand(x=>ShowOptions());
            ShowHelpCommand = new DelegateCommand(x=>ShowHelp());
            ExitCommand = new DelegateCommand(x=>Exit());
        }

        protected virtual void NewGame()
        {
            
        }

        protected virtual void LoadGame()
        {
            
        }

        protected virtual void ShowHighscores()
        {
            
        }

        protected virtual void ShowOptions()
        {
            
        }

        protected virtual void ShowHelp()
        {
            
        }

        /// <summary>
        /// Shutdowns the application.
        /// </summary>
        protected virtual void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
