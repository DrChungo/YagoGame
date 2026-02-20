using System;
using System.Collections.Generic;
using System.Threading;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Combat;

/// Utilidades de consola para mostrar combates (headers, barras de vida, estado).

internal static class CombatConsoleUi
{
    private const int PauseAfterEnemyAttackMs = 1000;

    //PrintHeader es el método que se encarga de imprimir el título de la pantalla
    public static void PrintHeader(string title)
    {
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('═', 50) + "\n");
    }

    //PrintHealthBar es el método que se encarga de imprimir la barra de vida del jugador y los enemigos
    public static void PrintHealthBar(string label, int hp)
    {
        Console.ForegroundColor =
            hp > 50 ? ConsoleColor.Green
            : hp > 20 ? ConsoleColor.Yellow
            : ConsoleColor.Red;
        Console.WriteLine($"{label}: {hp}");
        Console.ResetColor();
    }

    //PrintStatus es el método que se encarga de imprimir el estado del jugador y los enemigos
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

    //WaitForKey es el método que se encarga de esperar a que el usuario pulse una tecla para continuar
    public static void WaitForKey(string message = "Pulsa una tecla para continuar...")
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(message);
        Console.ResetColor();
        Console.ReadKey(true);
    }

    public static void PauseAfterEnemyAttack() => Thread.Sleep(PauseAfterEnemyAttackMs);
}
