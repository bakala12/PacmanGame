using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using PacmanGame.MainInterfaces;

namespace PacmanGame.ViewModels
{
    public class OptionsViewModel : CloseableViewModel
    {
        private Key _upKey;
        private Key _downKey;
        private Key _leftKey;
        private Key _rightKey;
        private bool _activeLeft;
        private bool _activeRight;
        private bool _activeUp;
        private bool _activeDown;

        public OptionsViewModel() : base("Options")
        {
            ChangeKeyCommand = new DelegateCommand(ChangeKey);
        }

        protected override void OnViewAppeared()
        {
            Load();
            ActiveLeft = false;
            ActiveDown = false;
            ActiveUp = false;
            ActiveRight = false;
        }

        public Key UpKey
        {
            get { return _upKey; }
            set { _upKey = value; OnPropertyChanged(); }
        }

        public Key DownKey
        {
            get { return _downKey; }
            set { _downKey = value; OnPropertyChanged(); }
        }

        public Key LeftKey
        {
            get { return _leftKey; }
            set { _leftKey = value; OnPropertyChanged(); }
        }

        public Key RightKey
        {
            get { return _rightKey; }
            set { _rightKey = value; OnPropertyChanged(); }
        }

        public ICommand ChangeKeyCommand { get; }

        public bool ActiveDown
        {
            get { return _activeDown; }
            set { _activeDown = value; OnPropertyChanged(); }
        }

        public bool ActiveUp
        {
            get { return _activeUp; }
            set { _activeUp = value; OnPropertyChanged(); }
        }

        public bool ActiveLeft
        {
            get { return _activeLeft; }
            set { _activeLeft = value; OnPropertyChanged(); }
        }

        public bool ActiveRight
        {
            get { return _activeRight; }
            set { _activeRight = value; OnPropertyChanged(); }
        }

        public void Save()
        {
            IHaveControlKeys accessor = (Application.Current as App)?.ControlKeysAccessor;
            if (accessor == null) return;
            accessor.RightKey = RightKey;
            accessor.LeftKey = LeftKey;
            accessor.UpKey = UpKey;
            accessor.DownKey = DownKey;
            accessor.SaveControlKeys();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveLeft)
            {
                LeftKey = e.Key;
                ActiveLeft = false;
            }
            else if(ActiveRight)
            {
                RightKey = e.Key;
                ActiveRight = false;
            }
            else if (ActiveDown)
            {
                DownKey = e.Key;
                ActiveDown = false;
            }
            else if (ActiveUp)
            {
                UpKey = e.Key;
                ActiveUp = false;
            }
            Save();
        }

        public void Load()
        {
            IHaveControlKeys accessor = (Application.Current as App)?.ControlKeysAccessor;
            if (accessor == null) return;
            accessor.LoadControlKeys();
            RightKey = accessor.RightKey;
            LeftKey = accessor.LeftKey;
            UpKey = accessor.UpKey;
            DownKey = accessor.DownKey;
        }

        protected override void Close()
        {
            Save();
            base.Close();
        }

        public void ChangeKey(object parameter)
        {
            ActiveLeft = false;
            ActiveRight = false;
            ActiveDown = false;
            ActiveUp = false;
            switch (parameter?.ToString())
            {
                case "Left":
                    ActiveLeft = true;
                    break;
                case "Right":
                    ActiveRight = true;
                    break;
                case "Up":
                    ActiveUp = true;
                    break;
                case "Down":
                    ActiveDown = true;
                    break;
                default:
                    return;
            }
        }
    }
}
