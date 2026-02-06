using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Run;

// ===================================================
// FASE 5 – LOOP 3 SALAS → BOSS (x4 BOSSES), POR SEED
// ===================================================
public sealed class Phase5Runner
{
    public void Play()
    {
        var loader = new JsonFileLoader();
        var config = loader.LoadObject<GameConfig>(PathConfig.ConfigFile);

        var content = new ContentService();

        int seed = config.Rng.DefaultSeed;
        var rng = new Random(seed);

        // Player aún fijo (hasta fases de clases/skills)
        int playerMaxHp = 200;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 6, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 20 };

        var combat = new CombatService();

        // Bosses en orden (usamos BossDef.Order del JSON)
        var bossesOrdered = content.Bosses
            .OrderBy(b => b.Order)
            .ToList();

        Console.Clear();
        Console.WriteLine("FASE 5 - Loop completo 3 salas → boss (x4)");
        Console.WriteLine($"Seed: {seed}");
        Console.WriteLine("Pulsa una tecla para empezar...");
        Console.ReadKey(true);

        foreach (var boss in bossesOrdered)
        {
            // ==========================
            // FASE 5 – 3 SALAS
            // ==========================
            for (int roomIndex = 1; roomIndex <= 3; roomIndex++)
            {
                Console.Clear();
                Console.WriteLine($"BOSS {boss.Order}/4   Antes del boss: Sala {roomIndex}/3");
                Console.WriteLine("Generando sala...");
                Console.ReadKey(true);

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
                Console.WriteLine($"SALA {roomIndex}/3 - Enemigos:");
                foreach (var e in roomEnemies)
                    Console.WriteLine($"- {e.Name} (Tier {e.Tier})");

                Console.WriteLine("\nPulsa una tecla para combatir...");
                Console.ReadKey(true);

                combat.StartThreeEnemiesRoom(playerStats, playerAttack, instances);

                // ==========================
                // FASE 5 – VIDA AL MÁXIMO
                // ==========================
                playerStats.Hp = playerMaxHp;
            }

            // ==========================
            // FASE 5 – BOSS
            // ==========================
            Console.Clear();
            Console.WriteLine($"¡¡BOSS {boss.Order}/4!! {boss.Name}");
            Console.WriteLine("Pulsa una tecla...");
            Console.ReadKey(true);

            // Fase 5: para reutilizar StartOneVsOne,
            // cogemos el primer ataque del boss (luego mejoraremos)
            var bossAttack = boss.Attacks.First();
            var bossStats = CloneStats(boss.Stats);

            combat.StartOneVsOne(playerStats, bossStats, playerAttack, bossAttack);

            // Vida al máximo tras boss también
            playerStats.Hp = playerMaxHp;

            Console.WriteLine($"\nHas derrotado a {boss.Name}. Pulsa una tecla para continuar...");
            Console.ReadKey(true);
        }

        Console.Clear();
        Console.WriteLine("FASE 5 completada: has derrotado a los 4 bosses.");
        Console.WriteLine("Pulsa una tecla para volver al menú...");
        Console.ReadKey(true);
    }

    // ==========================
    // FASE 5 – GENERACIÓN SALA
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