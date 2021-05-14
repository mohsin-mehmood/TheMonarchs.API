using System.Text.Json;

namespace TheMonarchs.Client.ConsoleApp.Utility
{
    public static class JsonDeserializer
    {

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
