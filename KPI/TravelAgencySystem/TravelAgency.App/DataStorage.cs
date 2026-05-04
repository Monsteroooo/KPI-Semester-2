using System.IO;
using System.Text.Json;
using TravelAgency.Models;

namespace TravelAgency.App
{
    public static class DataStorage
    {
        private const string FilePath = "data.json";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        public static AppData Load()
        {
            if (!File.Exists(FilePath))
                return new AppData();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<AppData>(json, JsonOptions) ?? new AppData();
        }

        public static void Save(AppData data)
        {
            string json = JsonSerializer.Serialize(data, JsonOptions);
            File.WriteAllText(FilePath, json);
        }
    }
}
