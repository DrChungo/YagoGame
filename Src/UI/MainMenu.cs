using System;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.UI;

public static class MainMenu
{
    public static void Show(ContentStore content, GameRng rng)
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== YAGO ROGUELIKE ===");
            Console.WriteLine("1. Test: mostrar conteos + config");
            Console.WriteLine("0. Salir");
            Console.Write("\nElige: ");

            var key = Console.ReadKey(true).KeyChar;

            switch (key)
            {
                case '1':
                    ShowCounts(content, rng);
                    break;
                case '0':
                    exit = true;
                    break;
                default:
                    Console.WriteLine("\nOpción no válida.");
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    private static void ShowCounts(ContentStore content, GameRng rng)
    {
        Console.Clear();
        Console.WriteLine("CONTENIDO CARGADO ✅\n");

        Console.WriteLine($"Seed usada: {rng.SeedUsed}");
        Console.WriteLine($"Enemigos por sala: {content.Config.Game.EnemiesPerRoom}");
        Console.WriteLine($"Salas entre boss: {content.Config.Game.RoomsBetweenBosses}");
        Console.WriteLine($"Total bosses: {content.Config.Game.TotalBosses}");
        Console.WriteLine($"Boss order: {string.Join(", ", content.Config.Run.BossOrder)}");

        Console.WriteLine("\n--- Conteos ---");
        Console.WriteLine($"Clases:   {content.Classes.Count}");
        Console.WriteLine($"Skills:   {content.Skills.Count}");
        Console.WriteLine($"Enemigos: {content.Enemies.Count}");
        Console.WriteLine($"Bosses:   {content.Bosses.Count}");
        Console.WriteLine($"Items:    {content.Items.Count}");
        Console.WriteLine($"NPCs:     {content.Npcs.Count}");

        Console.WriteLine("\nPulsa una tecla para volver...");
        Console.ReadKey(true);
    }
}
