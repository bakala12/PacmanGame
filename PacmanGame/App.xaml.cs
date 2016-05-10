using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PacmanGame.Highscores;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;
using PacmanGame.Serialization;
using PacmanGame.Validation;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Configurates application startup behaviour.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DefaultControls();
            IGameSerializer serializer = new GameSerializer();
            IGameBuilder builder = new SimpleGameBuilder();
            IKeysValidator validator = new KeysValidator();
            MainWindowViewModel vm = new MainWindowViewModel(builder, new HighscoreList(), serializer, validator);
            MainWindow window = new MainWindow();
            Current.MainWindow = window;
            window.DataContext = vm;
            vm.ChangeCurrentViewModel("StartMenu");
            vm.LoadControlKeys();
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Loads the dafault player controls if they are not set.
        /// </summary>
        private void DefaultControls()
        {
            if (Settings.Default.LeftKey == Key.None)
                Settings.Default.LeftKey = Key.Left;
            if (Settings.Default.RightKey == Key.None)
                Settings.Default.RightKey = Key.Right;
            if (Settings.Default.UpKey == Key.None)
                Settings.Default.UpKey = Key.Up;
            if (Settings.Default.DownKey == Key.None)
                Settings.Default.DownKey = Key.Down;
            Settings.Default.Save();
        }
    } 
}
