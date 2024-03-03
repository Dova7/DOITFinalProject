namespace NumberGuessingGame.Settings
{
    public class GameSettings
    {
        public static byte GenerateRandomNumber(Random random, byte max)
        {
            return (byte)random.Next(1, max + 1);
        }
    }
}
