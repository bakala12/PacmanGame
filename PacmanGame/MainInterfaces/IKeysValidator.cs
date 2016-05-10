using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Validation;

namespace PacmanGame.MainInterfaces
{
    public interface IKeysValidator
    {
        bool ValidateKey(Key keyToChange, KeyFunction function, IEnumerable<KeyValuePair<KeyFunction, Key>> allKeys);
    }
}
