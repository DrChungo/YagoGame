using System.Collections.Generic;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Run;

// ===================================
// FASE 3 – SALA CON 3 ENEMIGOS (FIJO)
// ===================================
public sealed class Phase3Runner
{
    public void Play()
    {
        var combat = new CombatService();

        int playerMaxHp = 100;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 10, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 10 };

        var enemies = new List<CombatService.EnemyInstance>
        {
            new("Pesado nivel 1", new StatsDef { Hp = 16, Speed = 2, Damage = 3, Armor = 0 }, new AttackDef { Name = "Empujón", Damage = 3 }),
            new("Pesado nivel 2", new StatsDef { Hp = 20, Speed = 4, Damage = 4, Armor = 0 }, new AttackDef { Name = "Tostón", Damage = 4 }),
            new("Pesado nivel 3", new StatsDef { Hp = 24, Speed = 3, Damage = 5, Armor = 0 }, new AttackDef { Name = "Spam", Damage = 5 }),
        };

        combat.StartThreeEnemiesRoom(playerStats, playerAttack, enemies);

        // ==========================
        // FASE 3 – VIDA AL MÁXIMO
        // ==========================
        playerStats.Hp = playerMaxHp;
    }
}
