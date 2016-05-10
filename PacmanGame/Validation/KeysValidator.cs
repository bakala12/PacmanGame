using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.MainInterfaces;

namespace PacmanGame.Validation
{
    public class KeysValidator : IKeysValidator
    {
        public bool ValidateKey(Key keyToChange, KeyFunction function, IEnumerable<KeyValuePair<KeyFunction, Key>> allKeys)
        {
            return !allKeys.Any(x => x.Key != function && x.Value == keyToChange);
        }
    }
}
