namespace Hangman.Settings
{
    public class GameSettings
    {   
        public string GenerateRandomWord(Random random,List<string> wordList)
        {
            int index = random.Next(wordList.Count);
            return wordList[index];
        }
    }
}