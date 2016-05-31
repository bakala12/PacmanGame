using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Commander;
using PacmanGame.MainInterfaces;
using PacmanGame.Validation;

namespace PacmanGame.ViewModels
{
    /// <summary>
    /// Represents a view model used to change control keys.
    /// </summary>
    public class OptionsViewModel : CloseableViewModel
    {
        protected IHaveControlKeys Accessor { get; }
        private readonly IKeysValidator _vaidator;
        private readonly DispatcherTimer _timer;

        /// <summary>
        /// Initializes a new instance of OptionViewModel.
        /// </summary>
        /// <param name="accessor">An object that provide access to control keys.</param>
        /// <param name="validator">An object that is used to validate control keys.</param>
        public OptionsViewModel(IHaveControlKeys accessor, IKeysValidator validator) : base("Options")
        {
            if (accessor == null) throw new ArgumentNullException(nameof(accessor));
            Accessor = accessor;
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            _vaidator = validator;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += (x, e) => ClearAllActiveKeysAndErrors();
        }

        protected override void OnViewAppeared()
        {
            Load();
            ClearAllActiveKeysAndErrors();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the Key that move player in left direction.
        /// </summary>
        public Key LeftKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in right direction.
        /// </summary>
        public Key RightKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in up direction.
        /// </summary>
        public Key UpKey { get; set; }
        /// <summary>
        /// Gets or sets the Key that move player in down direction.
        /// </summary>
        public Key DownKey { get; set; }
        /// <summary>
        /// Gets or sets the information specifing whether the down button is in change mode.
        /// </summary>
        public bool ActiveDown { get; set; }
        /// <summary>
        /// Gets or sets the information specifing whether the up button is in change mode.
        /// </summary>
        public bool ActiveUp { get; set; }
        /// <summary>
        /// Gets or sets the information specifing whether the left button is in change mode.
        /// </summary>
        public bool ActiveLeft { get; set; }
        /// <summary>
        /// Gets or sets the information specifing whether the right button is in change mode.
        /// </summary>
        public bool ActiveRight { get; set; }
        /// <summary>
        /// Gets the information whether the left button error occured.
        /// </summary>
        public bool IsLeftError { get; protected set; }
        /// <summary>
        /// Gets the information whether the right button error occured.
        /// </summary>
        public bool IsRightError { get; protected set; }
        /// <summary>
        /// Gets the information whether the up button error occured.
        /// </summary>
        public bool IsUpError { get; protected set; }
        /// <summary>
        /// Gets the information whether the down button error occured.
        /// </summary>
        public bool IsDownError { get; protected set; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; protected set; }
        /// <summary>
        /// Gets the information about the error.
        /// </summary>
        public bool HasErrors => IsLeftError || IsRightError || IsUpError || IsDownError;

        /// <summary>
        /// Gets the information about the active state.
        /// </summary>
        public bool IsActive => ActiveLeft || ActiveRight || ActiveDown || ActiveUp;
        #endregion

        protected virtual void Load()
        {
            Accessor.LoadControlKeys();
            RightKey = Accessor.RightKey;
            LeftKey = Accessor.LeftKey;
            UpKey = Accessor.UpKey;
            DownKey = Accessor.DownKey;
        }

        protected virtual void Save()
        {
            Accessor.RightKey = RightKey;
            Accessor.LeftKey = LeftKey;
            Accessor.UpKey = UpKey;
            Accessor.DownKey = DownKey;
            Accessor.SaveControlKeys();
        }

        /// <summary>
        /// Occurs when the key is pressed.
        /// </summary>
        /// <param name="parameter">Provide information about the pressed key.</param>
        [OnCommand("OnKeyDownCommand")]
        public void OnKeyDown(object parameter)
        {
            KeyEventArgs e = parameter as KeyEventArgs;
            if (e == null) return;
            Key key = e.Key;
            var allKeys = ProvideKeys();
            if (ActiveLeft)
            {
                if (_vaidator.ValidateKey(key, KeyFunction.Left, allKeys))
                {
                    LeftKey = key;
                }
                else
                {
                    IsLeftError = true;
                }
            }
            else if (ActiveRight)
            {
                if (_vaidator.ValidateKey(key, KeyFunction.Right, allKeys))
                {
                    RightKey = key;
                }
                else
                {
                    IsRightError = true;
                }
            }
            else if (ActiveUp)
            {
                if (_vaidator.ValidateKey(key, KeyFunction.Up, allKeys))
                {
                    UpKey = key;
                }
                else
                {
                    IsUpError = true;
                }
            }
            else if (ActiveDown)
            {
                if (_vaidator.ValidateKey(key, KeyFunction.Down, allKeys))
                {
                    DownKey = key;
                }
                else
                {
                    IsDownError = true;
                }
            }
            if (!HasErrors)
            {
                ClearAllActiveKeysAndErrors();
                Save();
            }
            else
            {
                Error();
            }
        }

        protected virtual void Error()
        {
            _timer.Stop();
            ErrorMessage = "Podany klawisz nie może zostać ustawiony";
            _timer.Start();
        }

        protected override void Close()
        {
            if (!HasErrors && !IsActive) Save();
            _timer.Stop();
            ClearAllActiveKeysAndErrors();
            base.Close();
        }

        /// <summary>
        /// Changes the selected control key.
        /// </summary>
        /// <param name="parameter">Provide information about the control key to be changed.</param>
        [OnCommand("ChangeKeyCommand")]
        public void ChangeKey(object parameter)
        {
            ClearAllActiveKeysAndErrors();
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

        private IList<KeyValuePair<KeyFunction, Key>> ProvideKeys()
        {
            IList<KeyValuePair<KeyFunction, Key>> keys = new List<KeyValuePair<KeyFunction, Key>>();
            keys.Add(new KeyValuePair<KeyFunction, Key>(KeyFunction.Left, LeftKey));
            keys.Add(new KeyValuePair<KeyFunction, Key>(KeyFunction.Right, RightKey));
            keys.Add(new KeyValuePair<KeyFunction, Key>(KeyFunction.Up, UpKey));
            keys.Add(new KeyValuePair<KeyFunction, Key>(KeyFunction.Down, DownKey));
            keys.Add(new KeyValuePair<KeyFunction, Key>(KeyFunction.Pause, Key.Escape));
            return keys;
        }

        private void ClearAllActiveKeysAndErrors()
        {
            _timer.Stop();
            ActiveLeft = false;
            ActiveRight = false;
            ActiveDown = false;
            ActiveUp = false;
            IsLeftError = false;
            IsRightError = false;
            IsDownError = false;
            IsUpError = false;
            ErrorMessage = null;
        }
    }
}
