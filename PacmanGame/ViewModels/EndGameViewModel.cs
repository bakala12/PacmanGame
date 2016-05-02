using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacmanGame.ViewModels
{
    public class EndGameViewModel : CloseableViewModel
    {
        private string _playerName;
        private bool _isHighscore;

        public EndGameViewModel() : base("EndGame")
        {
            PlayerName = "";
            IsHighscore = false;
            SaveCommand = new DelegateCommand(x=>SaveHighscore());
        }

        public string PlayerName
        {
            get { return _playerName; }
            set { _playerName = value; OnPropertyChanged(); }
        }

        public bool IsHighscore
        {
            get { return _isHighscore; }
            protected set { _isHighscore = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public virtual void SaveHighscore()
        {
            
        }
    }
}
