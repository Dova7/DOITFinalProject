using NumberGuessingGame.Models;

namespace NumberGuessingGame.Repository
{
    public class PlayerCSVRepository
    {
        private const string _fileLocation = "C:\\Users\\gujar\\source\\repos\\FinalProject\\NumberGuessingGame\\RecordData\\PlayerRecords.csv";
        private List<Player> _data;

        public PlayerCSVRepository()
        {
            if (!File.Exists(_fileLocation) || new FileInfo(_fileLocation).Length == 0)
            {
                using (StreamWriter headerWriter = new StreamWriter(_fileLocation, append: true))
                {
                    headerWriter.WriteLine("Id,Name,Score"); ;
                }
            }
            _data = new List<Player>();
            LoadDataFromFile();
        }
        private void LoadDataFromFile()
        {
            using (StreamReader reader = new StreamReader(_fileLocation))
            {
                reader.ReadLine();
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Player player = ParsePlayers(line);
                    if (player != null)
                    {
                        _data.Add(player);
                    }
                }
            }
        }
        public void AddNewPlayer(Player player)
        {
            _data.Add(player);
            player.Id = _data.Max(x => x.Id) + 1;
            Save(ToCSV(player));
        }
        public void UpdatePlayer(Player player)
        {
            Save(ToCSV(player));
        }
        public void Save(string playerString)
        {
            using (StreamWriter writer = new StreamWriter(_fileLocation, append: true))
            {
                writer.WriteLine(playerString);
            }
        }
        private static Player ParsePlayers(string input)
        {
            var data = input.Split(',');
            if (data.Length != 3)
            {
                return null;
            }
            var player = new Player();
            player.Id = int.Parse(data[0]);
            player.Name = data[1];
            player.Score = float.Parse(data[2]);
            return player;
        }
        private static string ToCSV(Player player) => $"{player.Id},{player.Name},{player.Score}";
        public List<Player> GetAllPlayers()
        {
            return _data;
        }
        public void RemovePlayer(int playerId)
        {
            Player playerToRemove = _data.FirstOrDefault(player => player.Id == playerId);
            if (playerToRemove != null)
            {
                _data.Remove(playerToRemove);
                UpdateCSVFile();
            }
            else
            {
                throw new ArgumentException("Player not found.");
            }
        }
        private void UpdateCSVFile()
        {
            File.WriteAllText(_fileLocation, "Id,Name,Score\n");
            foreach (var player in _data)
            {
                Save(ToCSV(player));
            }
        }
    }
}