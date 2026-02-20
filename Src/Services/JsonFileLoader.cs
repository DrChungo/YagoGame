using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RoguelikeYago.Src.Config;

namespace RoguelikeYago.Src.Services;

//JsonFileLoader es la clase que se encarga de cargar los archivos json
public class JsonFileLoader
{
    //LoadList es el método que se encarga de cargar la lista de objetos desde un archivo json
    public List<T> LoadList<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, JsonOptions.Default)
            ?? throw new InvalidDataException($"No se pudo deserializar la lista: {path}");
    }

    //LoadObject es el método que se encarga de cargar el objeto desde un archivo json
    public T LoadObject<T>(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<T>(json, JsonOptions.Default)
            ?? throw new InvalidDataException($"No se pudo deserializar el objeto: {path}");
    }
}
