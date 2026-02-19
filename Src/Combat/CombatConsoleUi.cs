using System;
using System.Collections.Generic;
using System.Threading;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Combat;

/// <summary>
/// Utilidades de consola para mostrar combates (headers, barras de vida, estado).
/// </summary>
internal static class CombatConsoleUi
{
    private const int PausaTrasAtaqueEnemigoMs = 1000;

    public static void PrintHeader(string title)
    {
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('═', 50) + "\n");
    }

    public static void PrintHealthBar(string label, int hp)
    {
        Console.ForegroundColor =
            hp > 50 ? ConsoleColor.Green
            : hp > 20 ? ConsoleColor.Yellow
            : ConsoleColor.Red;
        Console.WriteLine($"{label}: {hp}");
        Console.ResetColor();
    }

    public static void PrintStatus(StatsDef playerStats, IReadOnlyList<EnemyInstance> enemies)
    {
        Console.WriteLine("\n" + new string('═', 50));
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"👤 Jugador HP: {playerStats.Hp}");
        Console.ResetColor();

        for (int i = 0; i < enemies.Count; i++)
        {
            var e = enemies[i];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"👹 {i + 1}. {e.Name} HP: {e.Stats.Hp}");
            Console.ResetColor();
        }
        Console.WriteLine(new string('═', 50) + "\n");
    }

    public static void WaitForKey(string message = "Pulsa una tecla para continuar...")
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadKey(true);
    }

    public static void PauseAfterEnemyAttack() => Thread.Sleep(PausaTrasAtaqueEnemigoMs);
}
