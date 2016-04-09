using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PacmanGame.ViewModels;

namespace PacmanGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IViewModelChanger ViewModelChanger { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModelChanger = new MainWindowViewModel();
            ViewModelChanger.ChangeCurrentViewModel("StartMenu");
            MainWindow window = new MainWindow();
            Current.MainWindow = window;
            window.DataContext = ViewModelChanger;
            Current.MainWindow.Show();
        }
    } 
}
