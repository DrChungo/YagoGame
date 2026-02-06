using System;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Combat;

// FASE 1 

public sealed class CombatService
{
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
                Console.WriteLine($"Atacas y haces {playerAttack.Damage} de daño.");
            }
            else
            {
                playerStats.Hp -= enemyAttack.Damage;
                Console.WriteLine($"El enemigo ataca y hace {enemyAttack.Damage} de daño.");
            }

            Console.WriteLine($"Jugador HP: {playerStats.Hp}");
            Console.WriteLine($"Enemigo HP: {enemyStats.Hp}");
            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ReadKey();

            playerTurn = !playerTurn;
        }

        Console.WriteLine(playerStats.Hp > 0
            ? "¡Has ganado el combate!"
            : "Has sido derrotado...");
        Console.ReadKey();
    }
}
