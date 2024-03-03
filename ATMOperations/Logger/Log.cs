using ATMOperations.Models;
using System.Reflection;
using System.Text.Json;

namespace ATMOperations.Logger
{
    public class Log
    {
        private const string _fileLocation = "C:\\Users\\gujar\\source\\repos\\FinalProject\\ATMOperations\\Logger\\Log.json";

        public void LogOperation(string message)
        {
            LogEntry logEntry = new LogEntry() { Timestamp = DateTime.Now, Message = message};
            var logEntryString = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true });

            if (!logEntryString.StartsWith("{") || !logEntryString.EndsWith("}"))
            {
                throw new FormatException("Input is not valid JSON format");
            }

            if (!File.Exists(_fileLocation))
            {
                File.WriteAllText(_fileLocation, "[]");
            }

            string existingJson = File.ReadAllText(_fileLocation);

            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                existingJson = existingJson.Trim(']');
            }
            logEntryString = $",\n{logEntryString}";

            File.WriteAllText(_fileLocation, $"{existingJson}{logEntryString}\n]");
        }
    }
}