using NumberGuessingGame.Models;
using NumberGuessingGame.Repository;

namespace NumberGuessingGame.GameLogic
{
    public class ScoringSystem
    {
        PlayerCSVRepository playerCSVRepository = new PlayerCSVRepository();
        public float GetScore(byte numberOfGuesses, float difficultyMultiplier)
        {
            return (110 - numberOfGuesses * 10) * difficultyMultiplier;
        }
        public void AddRecord(string playerName, float score)
        {
            var recordData = playerCSVRepository.GetAllPlayers();

            var existingPlayer = recordData.FirstOrDefault(x => x.Name == playerName);
            if (existingPlayer != null)
            {
                if (existingPlayer.Score < score)
                {
                    existingPlayer.Score = score;
                    playerCSVRepository.RemovePlayer(existingPlayer.Id);
                    playerCSVRepository.UpdatePlayer(existingPlayer);
                }
            }
            else
            {
                var newPlayer = new Player() { Name = playerName, Score = score };
                playerCSVRepository.AddNewPlayer(newPlayer);
            }
        }
        public void DisplayScore()
        {
            Console.WriteLine("\n");
            var players = playerCSVRepository.GetAllPlayers().OrderByDescending(x => x.Score);
            for (int i = 0; i < players.Count(); i++)
            {
                Player player = players.ElementAt(i);
                Console.WriteLine($"{i+1}.Player: {player.Name}, Score: {player.Score}"); 
                if (9 == i)
                {
                    break;
                }
            }
            Console.WriteLine("------------");
        }
    }
}
