using Hangman.Models;
using Hangman.Repository;

namespace Hangman.GameLogic
{
    public class ScoringSystem
    {
        PlayerXMLRepository playerXMLRepository = new PlayerXMLRepository();
        public int GetScore(byte numberOfGuesses)
        {
            return (10 - numberOfGuesses) * 10;
        }
        public void AddRecord(string playerName, float score)
        {
            var recordData = playerXMLRepository.GetAllPlayers();

            var existingPlayer = recordData.FirstOrDefault(x => x.Name == playerName);
            if (existingPlayer != null)
            {
                if (existingPlayer.Score < score)
                {
                    existingPlayer.Score = score;
                    playerXMLRepository.RemovePlayer(existingPlayer.Id);
                    playerXMLRepository.UpdatePlayer(existingPlayer);
                }
            }
            else
            {
                var newPlayer = new Player() { Name = playerName, Score = score };
                playerXMLRepository.AddNewPlayer(newPlayer);
            }
        }
        public void DisplayScore()
        {
            Console.WriteLine("\n");
            var players = playerXMLRepository.GetAllPlayers().OrderByDescending(x => x.Score);
            for (int i = 0; i < players.Count(); i++)
            {
                Player player = players.ElementAt(i);
                Console.WriteLine($"{i + 1}.Player: {player.Name}, Score: {player.Score}");
                if (9 == i)
                {
                    break;
                }
            }
            Console.WriteLine("------------");
        }
    }
}
