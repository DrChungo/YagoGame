using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago.Src.Combat;

// ==========================
// FASE 1 – COMBATE 1v1 FIJO
// FASE 3 – SALA 3 ENEMIGOS
// ==========================
public class CombatService
{
    // ==========================
    // FASE 1 – COMBATE 1v1 FIJO
    // ==========================
    public void StartOneVsOne(
        StatsDef playerStats,
        StatsDef enemyStats,
        AttackDef playerAttack,
        AttackDef enemyAttack
    )
    {
        Console.Clear();
        PrintHeader("⚔️ COMBATE ⚔️");

        bool playerTurn = playerStats.Speed >= enemyStats.Speed;

        while (playerStats.Hp > 0 && enemyStats.Hp > 0)
        {
            if (playerTurn)
            {
                int totalDamage = playerAttack.Damage + playerStats.Damage;
                enemyStats.Hp -= totalDamage;
                if (enemyStats.Hp < 0)
                    enemyStats.Hp = 0;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(
                    $"\n💥 Atacas con {playerAttack.Name} y haces {totalDamage} de daño."
                );
                Console.ResetColor();
            }
            else
            {
                playerStats.Hp -= enemyAttack.Damage;
                if (playerStats.Hp < 0)
                    playerStats.Hp = 0;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"\n🔴 El enemigo usa {enemyAttack.Name} y hace {enemyAttack.Damage} de daño."
                );
                Console.ResetColor();
            }

            Console.WriteLine();
            PrintHealthBar("👤 Yago HP", playerStats.Hp);
            PrintHealthBar("👹 Enemigo HP", enemyStats.Hp);

            Console.WriteLine("\n" + new string('─', 50));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey(true);

            playerTurn = !playerTurn;
        }

        Console.Clear();
        if (playerStats.Hp > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintHeader("🎉 ¡VICTORIA! 🎉");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintHeader("💀 DERROTA 💀");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Pulsa una tecla para volver...");
        Console.ResetColor();
        Console.ReadKey(true);
    }

    // ====================================
    // FASE 3 – SALA CON 3 ENEMIGOS (3v1)
    // ====================================
    public void StartThreeEnemiesRoom(
        StatsDef playerStats,
        AttackDef playerAttack,
        List<EnemyInstance> enemies
    )
    {
        Console.Clear();
        PrintHeader("⚔️ SALA DE BATALLA ⚔️");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("¡Sala iniciada! Hay 3 enemigos.");
        Console.ResetColor();
        PrintStatus(playerStats, enemies);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Pulsa una tecla para empezar...");
        Console.ResetColor();
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
                    Act: () => EnemyAct(playerStats, e)
                ))
                .Append(
                    new TurnActor(
                        Name: "Yago",
                        Speed: playerStats.Speed,
                        IsPlayer: true,
                        Act: () => PlayerAct(playerStats, playerAttack, enemies)
                    )
                )
                .OrderByDescending(a => a.Speed)
                .ThenByDescending(a => a.IsPlayer) // jugador gana empates
                .ToList();

            foreach (var actor in turnOrder)
            {
                if (playerStats.Hp <= 0)
                    break;
                if (!enemies.Any(e => e.Stats.Hp > 0))
                    break;

                actor.Act();

                // PrintStatus(playerStats, enemies); // Eliminado para reducir spam
                Console.WriteLine(new string('─', 50));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey(true);
            }
        }

        Console.Clear();
        if (playerStats.Hp > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PrintHeader("🏆 ¡SALA LIMPIADA! 🏆");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            PrintHeader("💀 DERROTA 💀");
            Console.ResetColor();
        }

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("Pulsa una tecla para volver...");
        Console.ResetColor();
        Console.ReadKey(true);
    }

    // ==========================
    // FASE 3 – HELPERS
    // ==========================
    private static void PlayerAct(
        StatsDef playerStats,
        AttackDef playerAttack,
        List<EnemyInstance> enemies
    )
    {
        while (true)
        {
            // Construimos las opciones para el menú
            var options = enemies
                .Select(
                    (e, i) =>
                    {
                        string state = e.Stats.Hp > 0 ? $"HP {e.Stats.Hp}" : "☠️ DERROTADO";
                        return $"{e.Name} ({state})";
                    }
                )
                .ToList();

            // Usamos ArrowMenu para seleccionar
            Console.ForegroundColor = ConsoleColor.Cyan;
            int targetIndex = ArrowMenu.Choose(
                $"👤 Tu turno (HP: {playerStats.Hp}). ⚔️ Elige objetivo:",
                options
            );
            Console.ResetColor();

            // Si cancela (Escape), podríamos obligarle a elegir o salir.
            // En este caso, asumimos que no puede cancelar el turno, así que repetimos si es -1.
            if (targetIndex == -1)
                continue;

            var target = enemies[targetIndex];

            // Validar si está vivo
            if (target.Stats.Hp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"❌ ¡{target.Name} ya está derrotado! Elige otro.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ResetColor();
                Console.ReadKey(true);
                continue;
            }

            // Aplicar daño
            int totalDamage = playerAttack.Damage + playerStats.Damage;
            target.Stats.Hp -= totalDamage;
            if (target.Stats.Hp < 0)
                target.Stats.Hp = 0;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                $"\n💥 Atacas a {target.Name} con {playerAttack.Name} y haces {totalDamage} de daño."
            );
            Console.ResetColor();

            if (target.Stats.Hp == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"💀 ¡{target.Name} ha sido derrotado!");
                Console.ResetColor();
            }

            // Terminamos el turno
            break;
        }
    }

    private static void EnemyAct(StatsDef playerStats, EnemyInstance enemy)
    {
        if (enemy.Stats.Hp <= 0)
            return;

        playerStats.Hp -= enemy.Attack.Damage;
        if (playerStats.Hp < 0)
            playerStats.Hp = 0;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(
            $"\n🔴 {enemy.Name} usa {enemy.Attack.Name} y hace {enemy.Attack.Damage} de daño. (Tu vida: {playerStats.Hp})"
        );
        Console.ResetColor();
    }

    private static void PrintStatus(StatsDef playerStats, List<EnemyInstance> enemies)
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

    // ==========================
    // UTILIDADES VISUALES
    // ==========================
    private static void PrintHeader(string title)
    {
        Console.WriteLine("\n" + new string('═', 50));
        Console.WriteLine($"  {title}");
        Console.WriteLine(new string('═', 50) + "\n");
    }

    private static void PrintHealthBar(string label, int hp)
    {
        Console.ForegroundColor =
            hp > 50 ? ConsoleColor.Green
            : hp > 20 ? ConsoleColor.Yellow
            : ConsoleColor.Red;
        Console.WriteLine($"{label}: {hp}");
        Console.ResetColor();
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
