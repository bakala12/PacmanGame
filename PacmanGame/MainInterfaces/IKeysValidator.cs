using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PacmanGame.Validation;

namespace PacmanGame.MainInterfaces
{
    /// <summary>
    /// Represents an object that supports validation for player's control keys.
    /// </summary>
    public interface IKeysValidator
    {
        /// <summary>
        /// Validates if the key can be set as the given control function key for player.
        /// </summary>
        /// <param name="keyToChange">A key which behaviour might be changed.</param>
        /// <param name="function">A function for the key.</param>
        /// <param name="allKeys">Set of all player's control keys and their functions.</param>
        /// <returns>True, if the given key can be associated to the given function, otherwise false.</returns>
        bool ValidateKey(Key keyToChange, KeyFunction function, IEnumerable<KeyValuePair<KeyFunction, Key>> allKeys);
    }
}
