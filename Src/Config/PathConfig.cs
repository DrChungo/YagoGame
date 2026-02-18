using System;
using System.IO;

namespace RoguelikeYago.Src.Config;

public static class PathConfig
{
//Rutas de los archivos que ultilizamos.
    private static string? _rootDir;

 

    public static string RootDir => _rootDir ??= FindProjectRoot();

    public static string DataDir => Path.Combine(RootDir, "Data");
    public static string SavesDir => Path.Combine(RootDir, "Saves");

    public static string ConfigFile => Path.Combine(DataDir, "config.json");
    public static string ClassesFile => Path.Combine(DataDir, "classes.json");
    public static string SkillsFile => Path.Combine(DataDir, "skills.json");
    public static string EnemiesFile => Path.Combine(DataDir, "enemies.json");
    public static string BossesFile => Path.Combine(DataDir, "bosses.json");
    public static string ItemsFile => Path.Combine(DataDir, "items.json");
    public static string NpcsFile => Path.Combine(DataDir, "npcs.json");

    // esto lo necesita SaveService (ya existía)
    public static string SaveFile(int slot) => Path.Combine(SavesDir, $"save_{slot}.json");


    private static string FindProjectRoot()
    {
  
        var dir = new DirectoryInfo(AppContext.BaseDirectory);

   
        while (dir != null)
        {
            var candidate = Path.Combine(dir.FullName, "Data", "config.json");
            if (File.Exists(candidate))
                return dir.FullName;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException(
            "No se encontró la carpeta 'Data' en ningún padre de AppContext.BaseDirectory. "
                + "Asegúrate de que existe 'Data/config.json' en la raíz del proyecto."
        );
    }
}
