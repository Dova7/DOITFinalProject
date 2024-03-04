using Hangman.Models;
using System.Xml;
using System.Xml.Serialization;

namespace Hangman.Repository
{
    public class PlayerXMLRepository
    {
        private string _fileLocation = Path.Combine(Directory.GetCurrentDirectory(),"PlayerRecord.xml");
        private List<Player> _data = new();
        public PlayerXMLRepository()
        {
            if (!File.Exists(_fileLocation))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_fileLocation));

                using (var stream = File.Create(_fileLocation))
                {
                    var emptyXmlContent = "<?xml version=\"1.0\" encoding=\"utf-8\"?><Players></Players>";
                    var buffer = System.Text.Encoding.UTF8.GetBytes(emptyXmlContent);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            _data = Parse(File.ReadAllText(_fileLocation));
        }
        private static List<Player> Parse(string data)
        {
            var result = new List<Player>();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(data);
            XmlNodeList? rowNodes = xmlDocument.SelectNodes("//Player");
            foreach (XmlNode rowNode in rowNodes)
            {
                Player player = new Player();
                player.Id = int.Parse(rowNode.SelectSingleNode("Id").InnerText);
                player.Name = rowNode.SelectSingleNode("Name").InnerText;
                player.Score = int.Parse(rowNode.SelectSingleNode("Score").InnerText);
                result.Add(player);
            }
            return result;
        }
        private void Save(List<Player> players)
        {
            using (var stream = new FileStream(_fileLocation, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(List<Player>));
                serializer.Serialize(stream, players);
            }
        }
        public void AddNewPlayer(Player player)
        {
            if (_data.Count == 0)
            {
                player.Id = 1;
            }
            else
            {
                player.Id = _data.Max(x => x.Id) + 1;
            }
            _data.Add(player);
            Save(_data);
        }
        public List<Player> GetAllPlayers()
        {
            return _data;
        }
        public void RemovePlayer(int playerId)
        {
            _data.RemoveAll(player => player.Id == playerId);
            Save(_data);
        }
        public void UpdatePlayer(Player updatedPlayer)
        {
            int index = _data.FindIndex(player => player.Id == updatedPlayer.Id);
            if (index != -1)
            {
                _data[index] = updatedPlayer;
                Save(_data);
            }
            else
            {
                throw new ArgumentException($"Player with ID {updatedPlayer.Id} not found.");
            }
        }

    }
}