using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Drops;
using RoguelikeYago.Src.Npcs;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Run;

// ==================================================
// FASE 7 – NPCs (shop 15% + post-boss)
// ==================================================
public sealed class Phase7Runner
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

        int playerMaxHp = 500;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 600, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 600 };

        var bosses = content.Bosses.OrderBy(b => b.Order).ToList();

        foreach (var boss in bosses)
        {
            for (int room = 1; room <= 3; room++)
            {
                // -------- SALA --------
                var roomEnemies = PhaseHelpers.GenerateRoom(
                    content.Enemies,
                    config.EnemyGeneration.TierWeights,
                    rng);

                var instances = PhaseHelpers.ToInstances(roomEnemies);

                combat.StartThreeEnemiesRoom(playerStats, playerAttack, instances);
                playerStats.Hp = playerMaxHp;

                // -------- RECOMPENSA --------
                var rewardOptions = rewards.GenerateRoomRewards(
                    content.Items,
                    config.Drops,
                    rng,
                    inventory);

                var chosen = rewards.AskPlayerToChoose(rewardOptions);
                inventory.Add(chosen.Id);

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
