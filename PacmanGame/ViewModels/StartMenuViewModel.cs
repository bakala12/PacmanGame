﻿using System;
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
        public StartMenuViewModel() : base("StartMenu")
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
            IViewModelChanger changer =(Application.Current as App)?.ViewModelChanger;
            GameViewModel gvm = changer?.GetViewModelByName("Game") as GameViewModel;
            gvm.StartGame();
            changer?.ChangeCurrentViewModel("Game");
        }

        protected virtual void LoadGame()
        {
            //Load game here
        }

        protected virtual void ShowHighscores()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Highscores");
        }

        protected virtual void ShowOptions()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Options");
        }

        protected virtual void ShowHelp()
        {
            (Application.Current as App)?.ViewModelChanger.ChangeCurrentViewModel("Help");
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
