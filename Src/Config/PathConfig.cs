using System;
using System.IO;

namespace RoguelikeYago.Src.Config;

public static class PathConfig
{
    // ==========================================
    // FASE 4 – RUTAS ROBUSTAS (sin romper APIs)
    // ==========================================

    private static string? _rootDir;

    // Mantengo el nombre "RootDir" porque tu proyecto ya lo usa.
    // Ahora RootDir es la raíz real del proyecto (donde está /Data).
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

    // 🔥 IMPORTANTE: esto lo necesita SaveService (ya existía)
    public static string SaveFile(int slot) => Path.Combine(SavesDir, $"save_{slot}.json");
    // ==========================
// FASE 4 – COMPATIBILIDAD CON SaveService
// ==========================

    private static string FindProjectRoot()
    {
        // Empieza en bin/Debug/netX.X/
        var dir = new DirectoryInfo(AppContext.BaseDirectory);

        // Subimos hasta encontrar /Data/config.json
        while (dir != null)
        {
            var candidate = Path.Combine(dir.FullName, "Data", "config.json");
            if (File.Exists(candidate))
                return dir.FullName;

            dir = dir.Parent;
        }

        throw new DirectoryNotFoundException(
            "No se encontró la carpeta 'Data' en ningún padre de AppContext.BaseDirectory. " +
            "Asegúrate de que existe 'Data/config.json' en la raíz del proyecto.");
    }
}