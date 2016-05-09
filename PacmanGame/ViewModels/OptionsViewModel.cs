using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Commander;
using PacmanGame.MainInterfaces;

namespace PacmanGame.ViewModels
{
    public class OptionsViewModel : CloseableViewModel
    {
        protected IHaveControlKeys Accessor { get; }

        public OptionsViewModel(IHaveControlKeys accessor) : base("Options")
        {
            if(accessor==null) throw new ArgumentNullException(nameof(accessor));
            Accessor = accessor;
        }

        protected override void OnViewAppeared()
        {
            Load();
            ActiveLeft = false;
            ActiveDown = false;
            ActiveUp = false;
            ActiveRight = false;
            IsLeftError = false;
            IsRightError = false;
            IsDownError = false;
            IsUpError = false;
        }

        #region Properties
        public Key UpKey { get; set; }

        public Key DownKey { get; set; }

        public Key LeftKey { get; set; }

        public Key RightKey { get; set; }

        public bool ActiveDown { get; set; }

        public bool ActiveUp { get; set; }

        public bool ActiveLeft { get; set; }

        public bool ActiveRight { get; set; }

        public bool IsLeftError { get; protected set; }

        public bool IsRightError { get; protected set; }

        public bool IsUpError { get; protected set; }

        public bool IsDownError { get; protected set; }
        #endregion

        public void Load()
        {
            Accessor.LoadControlKeys();
            RightKey = Accessor.RightKey;
            LeftKey = Accessor.LeftKey;
            UpKey = Accessor.UpKey;
            DownKey = Accessor.DownKey;
        }

        public void Save()
        {
            Accessor.RightKey = RightKey;
            Accessor.LeftKey = LeftKey;
            Accessor.UpKey = UpKey;
            Accessor.DownKey = DownKey;
            Accessor.SaveControlKeys();
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveLeft)
            {
                LeftKey = e.Key;
                ActiveLeft = false;
            }
            else if (ActiveRight)
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

        protected override void Close()
        {
            Save();
            base.Close();
        }

        [OnCommand("ChangeKeyCommand")]
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
