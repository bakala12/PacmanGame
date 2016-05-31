using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Moq;
using PacmanGame.MainInterfaces;
using PacmanGame.Validation;
using PacmanGame.ViewModels;
using Xunit;

namespace PacmanProjectTests.OtherTests
{
    public class ControlKeysTests
    {
        [Theory]
        [InlineData(Key.A, "Left", true)]
        [InlineData(Key.Left, "Left", true)]
        [InlineData(Key.Right, "Up", false)]
        [InlineData(Key.Up, "Up", true)]
        [InlineData(Key.Escape, "Left", false)]
        private void ControlKeyChange(Key pressed, string pressedKey, bool result)
        {
            GameBoardHelper.StartStaTask(() =>
            {
                var accessor = new TestHaveControlKeys();
                OptionsViewModel vm = new OptionsViewModel(accessor, new KeysValidator());
                accessor.LoadControlKeys();
                vm.ChangeKey(pressedKey);
                var args = new KeyEventArgs(Keyboard.PrimaryDevice, new Mock<PresentationSource>().Object, 0, pressed);
                vm.OnKeyDown(args);
                Assert.Equal(result, !vm.HasErrors);
                Key k = GetKeyByName(accessor, pressedKey);
                Assert.NotEqual(k, Key.None);
                if (result)
                    Assert.Equal(k, pressed);
            });
        }

        private Key GetKeyByName(IHaveControlKeys accessor, string name)
        {
            switch (name)
            {
                case "Left":
                    return accessor.LeftKey;
                case "Right":
                    return accessor.RightKey;
                case "Up":
                    return accessor.UpKey;
                case "Down":
                    return accessor.DownKey;
                default:
                    return Key.None;
            }
        }
    }
}
