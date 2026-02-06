using System;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Run;

// ===============================
// FASE 2 – 4 COMBATES + BOSS FIJO
// ===============================
public sealed class Phase2Runner
{
    private readonly CombatService _combat = new();

    public void Play()
    {
        Console.Clear();
        Console.WriteLine("FASE 2: 4 combates fijos + boss fijo");
        Console.WriteLine("Pulsa una tecla para empezar...");
        Console.ReadKey(true);

        // Player fijo (hardcode Fase 2)
        int playerMaxHp = 40;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 6, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 6 };

        // 4 enemigos fijos
        FightEnemy("Chaval pesado", new StatsDef { Hp = 18, Speed = 2, Damage = 3, Armor = 0 }, new AttackDef { Name = "Empujón", Damage = 3 });
        RestorePlayerHp(playerStats, playerMaxHp);

        FightEnemy("Cuñado random", new StatsDef { Hp = 22, Speed = 4, Damage = 4, Armor = 0 }, new AttackDef { Name = "Opinión gratis", Damage = 4 });
        RestorePlayerHp(playerStats, playerMaxHp);

        FightEnemy("Cansino del grupo", new StatsDef { Hp = 26, Speed = 3, Damage = 5, Armor = 0 }, new AttackDef { Name = "Spam", Damage = 5 });
        RestorePlayerHp(playerStats, playerMaxHp);

        FightEnemy("Rival picado", new StatsDef { Hp = 30, Speed = 6, Damage = 6, Armor = 0 }, new AttackDef { Name = "Rage", Damage = 6 });
        RestorePlayerHp(playerStats, playerMaxHp);

        // Boss fijo (en Fase 2 seguimos usando 1 ataque para poder reutilizar CombatService)
        Console.Clear();
        Console.WriteLine("¡¡BOSS!!");
        Console.WriteLine("Pulsa una tecla...");
        Console.ReadKey(true);

        var bossStats = new StatsDef { Hp = 55, Speed = 5, Damage = 7, Armor = 0 };
        var bossAttack = new AttackDef { Name = "Golpe del jefe", Damage = 7 };

        _combat.StartOneVsOne(playerStats, bossStats, playerAttack, bossAttack);

        Console.WriteLine("\nFASE 2 terminada. Pulsa una tecla para volver al menú...");
        Console.ReadKey(true);
    }

    private void FightEnemy(string enemyName, StatsDef enemyStats, AttackDef enemyAttack)
    {
        Console.Clear();
        Console.WriteLine($"Enemigo: {enemyName}");
        Console.WriteLine("Pulsa una tecla para combatir...");
        Console.ReadKey(true);

        // Player fijo para cada combate (stats se mantienen, HP se restaura luego)
        int playerMaxHp = 40;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 6, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 6 };

        _combat.StartOneVsOne(playerStats, enemyStats, playerAttack, enemyAttack);
    }

    private static void RestorePlayerHp(StatsDef playerStats, int maxHp)
    {
        // ==========================
        // FASE 2 – VIDA AL MÁXIMO
        // ==========================
        playerStats.Hp = maxHp;

        Console.WriteLine("\nVida restaurada al máximo.");
        Console.WriteLine("Pulsa una tecla para continuar...");
        Console.ReadKey(true);
    }
}
