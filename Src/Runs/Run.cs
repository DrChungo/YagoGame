using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Drops;
using RoguelikeYago.Src.Npcs;
using RoguelikeYago.Src.Runs;
using RoguelikeYago.Src.Services;

public class Run
{
    public void Play()
    {
        var loader = new JsonFileLoader();
        var config = loader.LoadObject<GameConfig>(PathConfig.ConfigFile);

        var content = new ContentService();
        var rng = new Random(config.Rng.DefaultSeed);

        var combat = new CombatService();
        var rewards = new RewardService();
        var npcs = new NpcService();

        var inventory = new HashSet<string>();

        int playerMaxHp = 100;
        var playerStats = new StatsDef
        {
            Hp = playerMaxHp,
            Speed = 5,
            Damage = 10,
            Armor = 0,
        };
        var playerAttack = new AttackDef { Name = "Te tiltea", Damage = 10 };

        var bosses = content.Bosses.OrderBy(b => b.Order).ToList();

        foreach (var boss in bosses)
        {
            for (int room = 1; room <= 3; room++)
            {
                // -------- SALA --------
                var roomEnemies = PhaseHelpers.GenerateRoom(
                    content.Enemies,
                    config.EnemyGeneration.TierWeights,
                    rng
                );

                var instances = PhaseHelpers.ToInstances(roomEnemies);

                combat.StartThreeEnemiesRoom(playerStats, playerAttack, instances);
                playerStats.Hp = playerMaxHp;

                // -------- RECOMPENSA --------
                var rewardOptions = rewards.GenerateRoomRewards(
                    content.Items,
                    config.Drops,
                    rng,
                    inventory
                );

                var chosen = rewards.AskPlayerToChoose(rewardOptions, playerStats, playerAttack);
                inventory.Add(chosen.Id);

                // Aplicar mejoras de estadísticas
                playerMaxHp += chosen.Stats.Hp;
                playerStats.Speed += chosen.Stats.Speed;
                playerStats.Armor += chosen.Stats.Armor;
                // Nota: playerStats.Damage se usa para stats base, pero el ataque tiene su propio daño. Sumamos a ambos por consistencia o solo al ataque?
                // En el código original playerAttack.Damage es lo que se usa para atacar.
                // playerStats.Damage estaba en 600 también. Vamos a subir ambos por si acaso se usa el stat base en otro lado.
                playerStats.Damage += chosen.Stats.Damage;
                // playerAttack.Damage += chosen.Stats.Damage;  <-- Eliminado, ahora el daño base se suma en combate

                // Curar al jugador (y aplicar nuevo máximo)
                playerStats.Hp = playerMaxHp;

                Console.WriteLine($"\n¡Mejora obtenida: {chosen.Name}!");
                if (chosen.Stats.Hp > 0)
                    Console.WriteLine($"Max HP +{chosen.Stats.Hp} (Total: {playerMaxHp})");
                if (chosen.Stats.Speed > 0)
                    Console.WriteLine($"Speed +{chosen.Stats.Speed} (Total: {playerStats.Speed})");
                if (chosen.Stats.Damage > 0)
                    Console.WriteLine(
                        $"Damage +{chosen.Stats.Damage} (Total: {playerAttack.Damage + playerStats.Damage})"
                    );
                if (chosen.Stats.Armor > 0)
                    Console.WriteLine($"Armor +{chosen.Stats.Armor} (Total: {playerStats.Armor})");

                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey(true);

                // -------- NPC TIENDA (15%) --------
                if (npcs.ShouldSpawnShop(rng))
                {
                    var shop = npcs.PickShopNpc(content.Npcs, rng);
                    npcs.ShowNpc(shop);
                }
            }

            // -------- BOSS --------
            var bossAttack = boss.Attacks.First();
            combat.StartOneVsOne(playerStats, boss.Stats, playerAttack, bossAttack);
            playerStats.Hp = playerMaxHp;

            // -------- NPC POST-BOSS (SIEMPRE) --------
            var postBossNpc = npcs.PickPostBossNpc(content.Npcs);
            npcs.ShowNpc(postBossNpc);
        }

        Console.Clear();
        Console.WriteLine("FASE 7 completada.");
        Console.WriteLine("Pulsa una tecla para volver al menú...");
        Console.ReadKey(true);
    }
}
