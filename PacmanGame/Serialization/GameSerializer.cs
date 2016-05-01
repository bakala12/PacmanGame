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

namespace PacmanGame.Serialization
{
    public class GameSerializer
    {
        public void SaveGame(GameState state, string path)
        {
            using (var stream = new StreamWriter(path))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(stream.BaseStream, state);
            }
        }

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
