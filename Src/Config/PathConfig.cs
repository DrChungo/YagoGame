using System;
using System.IO;

namespace RoguelikeYago.Src.Config;

public static class PathConfig
{
    public static string RootDir => AppContext.BaseDirectory;

    // Asumimos que Data/ y Saves/ están junto al ejecutable.
    public static string DataDir => Path.Combine(RootDir, "Data");
    public static string SavesDir => Path.Combine(RootDir, "Saves");

    public static string ConfigFile => Path.Combine(DataDir, "config.json");
    public static string ClassesFile => Path.Combine(DataDir, "classes.json");
    public static string SkillsFile => Path.Combine(DataDir, "skills.json");
    public static string EnemiesFile => Path.Combine(DataDir, "enemies.json");
    public static string BossesFile => Path.Combine(DataDir, "bosses.json");
    public static string ItemsFile => Path.Combine(DataDir, "items.json");
    public static string NpcsFile => Path.Combine(DataDir, "npcs.json");

    public static string SaveFile(int slot) => Path.Combine(SavesDir, $"save_{slot}.json");
}
