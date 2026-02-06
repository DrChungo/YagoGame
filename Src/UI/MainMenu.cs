using System;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Run;

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
            Console.WriteLine("2. Run fija (FASE 2)");
            Console.WriteLine("3. Sala 3 enemigos (FASE 3)");
            Console.WriteLine("4. Sala por seed (FASE 4)");
            Console.WriteLine("5. Loop completo (FASE 5)");
            Console.WriteLine("6. Recompensas tras sala (FASE 6)");
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

                // ===============================
                // FASE 2 – 4 COMBATES + BOSS FIJO
                // ===============================
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    new Phase2Runner().Play();
                    break;

                // ===============================
                // FASE 3 – SALA 3 ENEMIGOS (FIJO)
                // ===============================
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    new Phase3Runner().Play();
                    break;
                // ===============================
                // FASE 4 – GENERACIÓN POR SEED
                // ===============================
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    new Phase4Runner().Play();
                    break;
                // ===============================
                // FASE 5 – LOOP 3 SALAS → BOSS x4
                // ===============================
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    new Phase5Runner().Play();
                    break;
                // ===============================
                // FASE 6 – RECOMPENSAS TRAS SALA
                // ===============================
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    new Phase6Runner().Play();
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
                    Console.ReadKey(true);
                    break;
            }
        }
    }

    // ==========================
    // FASE 1 – COMBATE 1v1 FIJO
    // ==========================
    private static void TestCombatPhase1()
    {
        var playerStats = new StatsDef { Hp = 30, Speed = 5, Damage = 6, Armor = 0 };
        var enemyStats = new StatsDef { Hp = 20, Speed = 3, Damage = 4, Armor = 0 };

        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 6 };
        var enemyAttack = new AttackDef { Name = "Mordisco", Damage = 4 };

        var combat = new CombatService();
        combat.StartOneVsOne(playerStats, enemyStats, playerAttack, enemyAttack);

        // ==========================
        // FASE 1 – VIDA AL MÁXIMO
        // ==========================
        playerStats.Hp = 30;

        Console.WriteLine("\n(FASE 1) Combate terminado. Vida restaurada al máximo.");
        Console.WriteLine("Pulsa una tecla para volver al menú...");
        Console.ReadKey(true);
    }
}
