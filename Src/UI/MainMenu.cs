using System;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.UI;

public static class MainMenu
{
    // ==========================
    // FASE 0 – MENÚ PRINCIPAL
    // ==========================
    public static void Show()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=== YAGO GAME ===");
            Console.WriteLine("1. Test combate (FASE 1)");
            Console.WriteLine("0. Salir");
            Console.Write("\nElige una opción: ");

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                // ==========================
                // FASE 1 – TEST COMBATE 1v1
                // ==========================
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    TestCombatPhase1();
                    break;

                // ==========================
                // FASE 0 – SALIR
                // ==========================
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    exit = true;
                    break;

                default:
                    Console.WriteLine("\nOpción no válida.");
                    Console.WriteLine("Pulsa una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }

 
    private static void TestCombatPhase1()
    {
        // Stats hardcodeados a propósito en Fase 1 (sin JSON, sin RNG)
        var playerStats = new StatsDef { Hp = 30, Speed = 5, Damage = 6, Armor = 0 };
        var enemyStats  = new StatsDef { Hp = 20, Speed = 3, Damage = 4, Armor = 0 };

        // Ataques fijos (enemigo 1 ataque, jugador 1 ataque) para test de la Fase 1
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 6 };
        var enemyAttack  = new AttackDef { Name = "Mordisco", Damage = 4 };

        var combat = new CombatService();
        combat.StartOneVsOne(playerStats, enemyStats, playerAttack, enemyAttack);

        // Regla del PDF: tras combate, vida al máximo (en Fase 1 lo simulamos así)
        playerStats.Hp = 30;

        Console.WriteLine("\n(FASE 1) Combate terminado. Vida restaurada al máximo.");
        Console.WriteLine("Pulsa una tecla para volver al menú...");
        Console.ReadKey();
    }
}
