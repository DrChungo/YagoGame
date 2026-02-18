using System.Text.Json;

namespace RoguelikeYago.Src.Config;

public static class JsonOptions
{
    //Opciones de serializacion y deserializacion de los archivos json.
   public static readonly JsonSerializerOptions Default = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };
}
