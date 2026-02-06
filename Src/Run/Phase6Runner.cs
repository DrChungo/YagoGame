using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Drops;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Run;

// =======================================================
// FASE 6 – LOOP + RECOMPENSAS (3 OPCIONES) TRAS CADA SALA
// =======================================================
public sealed class Phase6Runner
{
    public void Play()
    {
        var loader = new JsonFileLoader();
        var config = loader.LoadObject<GameConfig>(PathConfig.ConfigFile);

        var content = new ContentService();

        int seed = config.Rng.DefaultSeed;
        var rng = new Random(seed);

        var rewardService = new RewardService();

        // Run-state en memoria (Fase 8 lo guardará)
        var inventoryItemIds = new List<string>();
        var uniqueItemIdsSeen = new HashSet<string>();

        int playerMaxHp = 500;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 6, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 50 };

        var combat = new CombatService();

        var bossesOrdered = content.Bosses
            .OrderBy(b => b.Order)
            .ToList();

        Console.Clear();
        Console.WriteLine("FASE 6 - Recompensas tras cada sala (3 opciones)");
        Console.WriteLine($"Seed: {seed}");
        Console.WriteLine("Pulsa una tecla para empezar...");
        Console.ReadKey(true);

        foreach (var boss in bossesOrdered)
        {
            for (int roomIndex = 1; roomIndex <= 3; roomIndex++)
            {
                // -------- Sala generada (como Fase 5) --------
                var roomEnemies = GenerateRoomEnemies(
                    enemies: content.Enemies,
                    tierWeights: config.EnemyGeneration.TierWeights,
                    rng: rng,
                    roomSize: 3);

                var instances = roomEnemies
                    .Select(e => new CombatService.EnemyInstance(
                        name: e.Name,
                        stats: CloneStats(e.Stats),
                        attack: new AttackDef { Name = e.Attack.Name, Damage = e.Attack.Damage }))
                    .ToList();

                Console.Clear();
                Console.WriteLine($"BOSS {boss.Order}/4 - Sala {roomIndex}/3");
                Console.WriteLine("Enemigos:");
                foreach (var e in roomEnemies)
                    Console.WriteLine($"- {e.Name} (Tier {e.Tier})");
                Console.WriteLine("\nPulsa una tecla para combatir...");
                Console.ReadKey(true);

                combat.StartThreeEnemiesRoom(playerStats, playerAttack, instances);

                // ==========================
                // FASE 6 – VIDA AL MÁXIMO
                // ==========================
                playerStats.Hp = playerMaxHp;

                // ==========================
                // FASE 6 – RECOMPENSAS (3)
                // ==========================
                var rewards = rewardService.GenerateRoomRewards(
                    allItems: content.Items,
                    drops: config.Drops,
                    rng: rng,
                    uniqueItemIdsSeen: uniqueItemIdsSeen);

                var chosen = rewardService.AskPlayerToChoose(rewards);

                inventoryItemIds.Add(chosen.Id);
                if (chosen.Unique) uniqueItemIdsSeen.Add(chosen.Id);

                Console.WriteLine($"\nHas elegido: {chosen.Name}");
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey(true);
            }

            // -------- Boss (igual que Fase 5, primer ataque) --------
            Console.Clear();
            Console.WriteLine($"¡¡BOSS {boss.Order}/4!! {boss.Name}");
            Console.WriteLine("Pulsa una tecla...");
            Console.ReadKey(true);

            var bossAttack = boss.Attacks.First();
            var bossStats = CloneStats(boss.Stats);

            combat.StartOneVsOne(playerStats, bossStats, playerAttack, bossAttack);

            playerStats.Hp = playerMaxHp;

            Console.WriteLine($"\nHas derrotado a {boss.Name}.");
            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ReadKey(true);
        }

        Console.Clear();
        Console.WriteLine("FASE 6 completada.");
        Console.WriteLine($"Items obtenidos: {inventoryItemIds.Count}");
        Console.WriteLine("Pulsa una tecla para volver al menú...");
        Console.ReadKey(true);
    }

    // ==========================
    // FASE 6 – GENERACIÓN SALA (igual que Fase 5)
    // ==========================
    private static List<EnemyDef> GenerateRoomEnemies(
        IReadOnlyList<EnemyDef> enemies,
        Dictionary<string, int> tierWeights,
        Random rng,
        int roomSize)
    {
        if (tierWeights == null || tierWeights.Count == 0)
            throw new InvalidOperationException(
                $"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). Está vacío.");

        var parsedWeights = tierWeights
            .Select(kv => (tier: ParseTierOrThrow(kv.Key), weight: kv.Value))
            .Where(x => x.weight > 0)
            .ToList();

        if (parsedWeights.Count == 0)
            throw new InvalidOperationException(
                $"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). No hay pesos > 0.");

        return Enumerable.Range(0, roomSize)
            .Select(_ =>
            {
                int tier = PickWeightedTier(parsedWeights, rng);

                var pool = enemies.Where(e => e.Tier == tier).ToList();
                if (pool.Count == 0)
                    throw new InvalidOperationException(
                        $"No hay enemigos con tier {tier} en {PathConfig.EnemiesFile} (campo: tier).");

                return pool[rng.Next(pool.Count)];
            })
            .ToList();
    }

    private static int PickWeightedTier(List<(int tier, int weight)> weights, Random rng)
    {
        int total = weights.Sum(w => w.weight);
        int roll = rng.Next(1, total + 1);

        int acc = 0;
        foreach (var w in weights)
        {
            acc += w.weight;
            if (roll <= acc) return w.tier;
        }

        return weights[^1].tier;
    }

    private static int ParseTierOrThrow(string key)
    {
        if (int.TryParse(key, out int t)) return t;
        throw new InvalidOperationException(
            $"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). Key no es int: '{key}'.");
    }

    private static StatsDef CloneStats(StatsDef s) =>
        new StatsDef { Hp = s.Hp, Speed = s.Speed, Damage = s.Damage, Armor = s.Armor };
}