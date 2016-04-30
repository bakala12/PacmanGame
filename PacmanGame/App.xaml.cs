using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IViewModelChanger ViewModelChanger { get; private set; }
        public IHaveControlKeys ControlKeysAccessor { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DefaultControls();
            IGameBuilder builder = new SimpleGameBuilder();
            MainWindowViewModel vm = new MainWindowViewModel(builder);
            ViewModelChanger = vm;
            ControlKeysAccessor = vm;
            ViewModelChanger.ChangeCurrentViewModel("StartMenu");
            MainWindow window = new MainWindow();
            Current.MainWindow = window;
            window.DataContext = ViewModelChanger;
            ControlKeysAccessor.LoadControlKeys();
            Current.MainWindow.Show();
        }

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
