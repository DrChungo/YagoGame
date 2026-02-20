using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago.Src.Combat;

public sealed class CombatService
{
    public void Combat(
        StatsDef playerStats,
        StatsDef enemyStats,
        AttackDef playerAttack,
        IReadOnlyList<AttackDef> enemyAttacks,
        Random rng,
        string enemyName = "Enemigo"
    )
    {
        Console.Clear();
        CombatConsoleUi.PrintHeader("⚔️ COMBATE ⚔️");

        bool playerTurn = playerStats.Speed >= enemyStats.Speed;

        while (playerStats.Hp > 0 && enemyStats.Hp > 0)
        {
            if (playerTurn)
            {
                int rawDamage = playerAttack.Damage + playerStats.Damage;
                int totalDamage = CombatDamage.CalculateEffectiveDamage(rawDamage, enemyStats.Armor);
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
                var enemyAttack = enemyAttacks[rng.Next(enemyAttacks.Count)];
                int rawDamage = enemyAttack.Damage + enemyStats.Damage;
                int totalDamage = CombatDamage.CalculateEffectiveDamage(rawDamage, playerStats.Armor);
                playerStats.Hp -= totalDamage;
                if (playerStats.Hp < 0)
                    playerStats.Hp = 0;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                    $"\n🔴 {enemyName} usa {enemyAttack.Name} y hace {totalDamage} de daño."
                );
                Console.ResetColor();
            }

            Console.WriteLine();
            CombatConsoleUi.PrintHealthBar("👤 Yago HP", playerStats.Hp);
            CombatConsoleUi.PrintHealthBar($"👹 {enemyName} HP", enemyStats.Hp);

            Console.WriteLine("\n" + new string('─', 50));
            if (playerTurn)
                CombatConsoleUi.WaitForKey();
            else
                CombatConsoleUi.PauseAfterEnemyAttack();
            playerTurn = !playerTurn;
        }

        Console.Clear();
        if (playerStats.Hp > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            CombatConsoleUi.PrintHeader("🎉 ¡VICTORIA! 🎉");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            CombatConsoleUi.PrintHeader("💀 DERROTA 💀");
            Console.ResetColor();
        }

        CombatConsoleUi.WaitForKey("Pulsa una tecla para volver...");
    }
    public void StartThreeEnemiesRoom(
        StatsDef playerStats,
        AttackDef playerAttack,
        List<EnemyInstance> enemies
    )
    {
        Console.Clear();
        CombatConsoleUi.PrintHeader("⚔️ SALA DE BATALLA ⚔️");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("¡Sala iniciada! Hay 3 enemigos.");
        Console.ResetColor();
        CombatConsoleUi.PrintStatus(playerStats, enemies);
        CombatConsoleUi.WaitForKey("Pulsa una tecla para empezar...");

        while (playerStats.Hp > 0 && enemies.Any(e => e.Stats.Hp > 0))
        {
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
                .ThenByDescending(a => a.IsPlayer)
                .ToList();

            for (int i = 0; i < turnOrder.Count; i++)
            {
                var actor = turnOrder[i];
                if (playerStats.Hp <= 0)
                    break;
                if (!enemies.Any(e => e.Stats.Hp > 0))
                    break;

                actor.Act();

                Console.WriteLine(new string('─', 50));
                if (actor.IsPlayer)
                    CombatConsoleUi.WaitForKey();
                else
                {
                    bool nextIsPlayer = i + 1 < turnOrder.Count && turnOrder[i + 1].IsPlayer;
                    bool isLastInTurn = i + 1 >= turnOrder.Count;
                    if (nextIsPlayer || isLastInTurn)
                        CombatConsoleUi.WaitForKey();
                    else
                        CombatConsoleUi.PauseAfterEnemyAttack();
                }
            }
        }

        Console.Clear();
        if (playerStats.Hp > 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            CombatConsoleUi.PrintHeader("🏆 ¡SALA LIMPIADA! 🏆");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            CombatConsoleUi.PrintHeader("💀 DERROTA 💀");
            Console.ResetColor();
        }

        CombatConsoleUi.WaitForKey("Pulsa una tecla para volver...");
    }

 
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
                CombatConsoleUi.WaitForKey();
                continue;
            }

            int rawDamage = playerAttack.Damage + playerStats.Damage;
            int totalDamage = CombatDamage.CalculateEffectiveDamage(rawDamage, target.Stats.Armor);
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

    //EnemyAct es el método que se encarga de que el enemigo actúe
    private static void EnemyAct(StatsDef playerStats, EnemyInstance enemy)
    {
        if (enemy.Stats.Hp <= 0)
            return;

        int rawDamage = enemy.Attack.Damage + enemy.Stats.Damage;
        int totalDamage = CombatDamage.CalculateEffectiveDamage(rawDamage, playerStats.Armor);
        playerStats.Hp -= totalDamage;
        if (playerStats.Hp < 0)
            playerStats.Hp = 0;

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(
            $"\n🔴 {enemy.Name} usa {enemy.Attack.Name} y hace {totalDamage} de daño. (Tu vida: {playerStats.Hp})"
        );
        Console.ResetColor();
    }
}
