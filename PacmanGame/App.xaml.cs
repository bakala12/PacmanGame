using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac;
using PacmanGame.MainInterfaces;
using PacmanGame.Properties;

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
            var container = Configuration.Configure();
            var provider = container.Resolve<ISettingsProvider>();
            DefaultControls(provider);
            MainWindowViewModel vm = container.Resolve<MainWindowViewModel>();
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
        private void DefaultControls(ISettingsProvider provider)
        {
            if (provider.LeftKey == Key.None)
                provider.LeftKey = Key.Left;
            if (provider.RightKey == Key.None)
                provider.RightKey = Key.Right;
            if (provider.UpKey == Key.None)
                provider.UpKey = Key.Up;
            if (provider.DownKey == Key.None)
                provider.DownKey = Key.Down;
            provider.Save();
        }
    }
}
