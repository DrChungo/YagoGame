using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RoguelikeYago.Src.Config;

namespace RoguelikeYago.Src.Services;

public class JsonFileLoader
{
    public List<T> LoadList<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, JsonOptions.Default)
            ?? throw new InvalidDataException($"No se pudo deserializar la lista: {path}");
    }

    public T LoadObject<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, JsonOptions.Default)
            ?? throw new InvalidDataException($"No se pudo deserializar el objeto: {path}");
    }
}
