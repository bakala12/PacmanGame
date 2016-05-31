using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using PacmanGame.MainInterfaces;

namespace PacmanGame.Serialization
{
    /// <summary>
    /// Simple implementation of IGameSerializer used in the game.
    /// </summary>
    internal class GameSerializer : IGameSerializer
    {
        /// <summary>
        /// Saves the given GameState object to the file.
        /// </summary>
        /// <param name="state">The GameState object to be saved.</param>
        /// <param name="path">The path to the file in which GameState would be stored.</param>
        public void SaveGame(GameState state, string path)
        {
            using (var stream = new StreamWriter(path))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream.BaseStream, state);
            }
        }

        /// <summary>
        /// Loads the GameState from file.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The loaded GameState object.</returns>
        public GameState LoadGame(string path)
        {
            using (var stream = new StreamReader(path))
            {
                var bf = new BinaryFormatter();
                return bf.Deserialize(stream.BaseStream) as GameState;
            }
        }
    }
}
