using System.IO;
using System.Text.Json;
using RoguelikeYago.Src.Config;

namespace RoguelikeYago.Src.Persistence;

// NUEVO
public class SaveService
{
    public SaveFile Load(int slot)
    {
        var path = PathConfig.SaveFile(slot);
        var json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<SaveFile>(json, JsonOptions.Default)
            ?? throw new InvalidDataException("Save corrupto");
    }

    public void Save(int slot, SaveFile save)
    {
        Directory.CreateDirectory(PathConfig.SavesDir);

        var path = PathConfig.SaveFile(slot);
        var json = JsonSerializer.Serialize(save, JsonOptions.Default);
        File.WriteAllText(path, json);
    }
}
