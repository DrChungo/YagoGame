using System.IO;
using System.Text.Json;

namespace RoguelikeYago.Src.Content
{
    public static class JsonLoader
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static T Load<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json, Options)!;
        }
    }
}
