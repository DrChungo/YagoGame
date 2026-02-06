using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Combat;

// ==========================
// FASE 1 – COMBATE 1v1 FIJO
// FASE 3 – SALA 3 ENEMIGOS
// ==========================
public sealed class CombatService
{
    // ==========================
    // FASE 1 – COMBATE 1v1 FIJO
    // ==========================
    public void StartOneVsOne(
        StatsDef playerStats,
        StatsDef enemyStats,
        AttackDef playerAttack,
        AttackDef enemyAttack)
    {
        Console.Clear();
        Console.WriteLine("¡Comienza el combate!");

        bool playerTurn = playerStats.Speed >= enemyStats.Speed;

        while (playerStats.Hp > 0 && enemyStats.Hp > 0)
        {
            if (playerTurn)
            {
                enemyStats.Hp -= playerAttack.Damage;
                if (enemyStats.Hp < 0) enemyStats.Hp = 0;

                Console.WriteLine($"Atacas con {playerAttack.Name} y haces {playerAttack.Damage} de daño.");
            }
            else
            {
                playerStats.Hp -= enemyAttack.Damage;
                if (playerStats.Hp < 0) playerStats.Hp = 0;

                Console.WriteLine($"El enemigo usa {enemyAttack.Name} y hace {enemyAttack.Damage} de daño.");
            }

            Console.WriteLine($"Jugador HP: {playerStats.Hp}");
            Console.WriteLine($"Enemigo HP: {enemyStats.Hp}");
            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ReadKey(true);

            playerTurn = !playerTurn;
        }

        Console.WriteLine(playerStats.Hp > 0 ? "¡Has ganado el combate!" : "Has sido derrotado...");
        Console.WriteLine("Pulsa una tecla para volver...");
        Console.ReadKey(true);
    }

    // ====================================
    // FASE 3 – SALA CON 3 ENEMIGOS (3v1)
    // ====================================
    public void StartThreeEnemiesRoom(
        StatsDef playerStats,
        AttackDef playerAttack,
        List<EnemyInstance> enemies)
    {
        Console.Clear();
        Console.WriteLine("¡Sala iniciada! Hay 3 enemigos.");
        Console.WriteLine("Pulsa una tecla para empezar...");
        Console.ReadKey(true);

        while (playerStats.Hp > 0 && enemies.Any(e => e.Stats.Hp > 0))
        {
            // Orden fijo por Speed (más speed = antes). En empates: jugador primero.
            var turnOrder = enemies
                .Where(e => e.Stats.Hp > 0)
                .Select(e => new TurnActor(
                    Name: e.Name,
                    Speed: e.Stats.Speed,
                    IsPlayer: false,
                    Act: () => EnemyAct(playerStats, e)))
                .Append(new TurnActor(
                    Name: "Jugador",
                    Speed: playerStats.Speed,
                    IsPlayer: true,
                    Act: () => PlayerAct(playerAttack, enemies)))
                .OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.IsPlayer) // jugador gana empates
                .ToList();

            foreach (var actor in turnOrder)
            {
                if (playerStats.Hp <= 0) break;
                if (!enemies.Any(e => e.Stats.Hp > 0)) break;

                actor.Act();

                PrintStatus(playerStats, enemies);
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey(true);
            }
        }

        Console.Clear();
        Console.WriteLine(playerStats.Hp > 0
            ? "¡Has limpiado la sala!"
            : "Has sido derrotado...");
        Console.WriteLine("Pulsa una tecla para volver...");
        Console.ReadKey(true);
    }

    // ==========================
    // FASE 3 – HELPERS
    // ==========================
    private static void PlayerAct(AttackDef playerAttack, List<EnemyInstance> enemies)
    {
        Console.WriteLine("\nTu turno. Elige objetivo:");
        var alive = enemies.Where(e => e.Stats.Hp > 0).ToList();

        for (int i = 0; i < enemies.Count; i++)
        {
            var e = enemies[i];
            string state = e.Stats.Hp > 0 ? $"HP {e.Stats.Hp}" : "DERROTADO";
            Console.WriteLine($"{i + 1}. {e.Name} ({state})");
        }

        int targetIndex = ReadTargetIndex(enemies.Count) - 1;

        // Si eligió un muerto, repite (simple y claro)
        while (targetIndex < 0 || targetIndex >= enemies.Count || enemies[targetIndex].Stats.Hp <= 0)
        {
            Console.WriteLine("Ese objetivo no es válido. Elige un enemigo vivo (1-3):");
            targetIndex = ReadTargetIndex(enemies.Count) - 1;
        }

        var target = enemies[targetIndex];
        target.Stats.Hp -= playerAttack.Damage;
        if (target.Stats.Hp < 0) target.Stats.Hp = 0;

        Console.WriteLine($"Atacas a {target.Name} con {playerAttack.Name} y haces {playerAttack.Damage} de daño.");
        if (target.Stats.Hp == 0)
            Console.WriteLine($"¡{target.Name} ha sido derrotado!");
    }

    private static void EnemyAct(StatsDef playerStats, EnemyInstance enemy)
    {
        if (enemy.Stats.Hp <= 0) return;

        playerStats.Hp -= enemy.Attack.Damage;
        if (playerStats.Hp < 0) playerStats.Hp = 0;

        Console.WriteLine($"\n{enemy.Name} usa {enemy.Attack.Name} y hace {enemy.Attack.Damage} de daño.");
    }

    private static void PrintStatus(StatsDef playerStats, List<EnemyInstance> enemies)
    {
        Console.WriteLine("\n--- ESTADO ---");
        Console.WriteLine($"Jugador HP: {playerStats.Hp}");

        for (int i = 0; i < enemies.Count; i++)
        {
            var e = enemies[i];
            Console.WriteLine($"{i + 1}. {e.Name} HP: {e.Stats.Hp}");
        }
        Console.WriteLine("--------------\n");
    }

    private static int ReadTargetIndex(int max)
    {
        while (true)
        {
            Console.Write("Objetivo (1-3): ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int value) && value >= 1 && value <= max)
                return value;

            Console.WriteLine("Entrada inválida. Escribe 1, 2 o 3.");
        }
    }

    // ==========================
    // FASE 3 – MODELOS INTERNOS
    // ==========================
    public sealed class EnemyInstance
    {
        public string Name { get; }
        public StatsDef Stats { get; }
        public AttackDef Attack { get; }

        public EnemyInstance(string name, StatsDef stats, AttackDef attack)
        {
            Name = name;
            Stats = stats;
            Attack = attack;
        }
    }

    private sealed record TurnActor(string Name, int Speed, bool IsPlayer, Action Act);
}
