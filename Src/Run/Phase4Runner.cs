using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Run;

// ==========================================
// FASE 4 – SALA GENERADA POR SEED + PESOS
// ==========================================
public sealed class Phase4Runner
{
    public void Play()
    {
        // Cargar config + contenido
        var loader = new JsonFileLoader();
        var config = loader.LoadObject<GameConfig>(PathConfig.ConfigFile);

        var content = new ContentService();

        // RNG determinista por seed (Fase 4)
        int seed = config.Rng.DefaultSeed;
        var rng = new Random(seed);

        // Generar sala (3 enemigos) por pesos de tier
        var roomEnemies = GenerateRoomEnemies(
            enemies: content.Enemies,
            tierWeights: config.EnemyGeneration.TierWeights,
            rng: rng,
            roomSize: 3);

        // Player fijo aún (Fase 4: seguimos sin clases/skills reales)
        int playerMaxHp = 45;
        var playerStats = new StatsDef { Hp = playerMaxHp, Speed = 5, Damage = 6, Armor = 0 };
        var playerAttack = new AttackDef { Name = "Golpe básico", Damage = 6 };

        // Convertir EnemyDef -> EnemyInstance (copias de stats para no mutar el contenido)
        var instances = roomEnemies
            .Select(e => new CombatService.EnemyInstance(
                name: e.Name,
                stats: CloneStats(e.Stats),
                attack: new AttackDef { Name = e.Attack.Name, Damage = e.Attack.Damage }))
            .ToList();

        Console.Clear();
        Console.WriteLine("FASE 4 – Sala generada por seed");
        Console.WriteLine($"Seed: {seed}");
        Console.WriteLine("Enemigos generados:");
        foreach (var e in roomEnemies)
            Console.WriteLine($"- {e.Name} (Tier {e.Tier})");
        Console.WriteLine("\nPulsa una tecla para empezar el combate...");
        Console.ReadKey(true);

        var combat = new CombatService();
        combat.StartThreeEnemiesRoom(playerStats, playerAttack, instances);

        // Regla PDF: tras combate, vida al máximo
        // ==========================
        // FASE 4 – VIDA AL MÁXIMO
        // ==========================
        playerStats.Hp = playerMaxHp;

        Console.WriteLine("\n(FASE 4) Sala terminada. Vida restaurada.");
        Console.WriteLine("Pulsa una tecla para volver...");
        Console.ReadKey(true);
    }

    // ==========================
    // FASE 4 – GENERACIÓN SALA
    // ==========================
    private static List<EnemyDef> GenerateRoomEnemies(
        IReadOnlyList<EnemyDef> enemies,
        Dictionary<string, int> tierWeights,
        Random rng,
        int roomSize)
    {
        if (tierWeights == null || tierWeights.Count == 0)
            throw new InvalidOperationException($"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). Está vacío.");

        // Parse tiers: "1"->1 etc. (LINQ)
        var parsedWeights = tierWeights
            .Select(kv => (tier: ParseTierOrThrow(kv.Key), weight: kv.Value))
            .Where(x => x.weight > 0)
            .ToList();

        if (parsedWeights.Count == 0)
            throw new InvalidOperationException($"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). No hay pesos > 0.");

        // Para cada slot de enemigo, elegimos tier por peso y luego un enemigo aleatorio de ese tier
        var result = Enumerable.Range(0, roomSize)
            .Select(_ =>
            {
                int tier = PickWeightedTier(parsedWeights, rng);

                var pool = enemies
                    .Where(e => e.Tier == tier)
                    .ToList();

                if (pool.Count == 0)
                    throw new InvalidOperationException($"No hay enemigos con tier {tier} en {PathConfig.EnemiesFile} (campo: tier).");

                int idx = rng.Next(pool.Count);
                return pool[idx];
            })
            .ToList();

        return result;
    }

    private static int PickWeightedTier(List<(int tier, int weight)> weights, Random rng)
    {
        int total = weights.Sum(w => w.weight);
        int roll = rng.Next(1, total + 1); // 1..total

        int acc = 0;
        foreach (var w in weights)
        {
            acc += w.weight;
            if (roll <= acc) return w.tier;
        }

        // Nunca debería pasar
        return weights[^1].tier;
    }

    private static int ParseTierOrThrow(string key)
    {
        if (int.TryParse(key, out int t)) return t;
        throw new InvalidOperationException($"Config inválido en {PathConfig.ConfigFile} (campo: enemyGeneration.tierWeights). Key no es int: '{key}'.");
    }

    private static StatsDef CloneStats(StatsDef s) =>
        new StatsDef { Hp = s.Hp, Speed = s.Speed, Damage = s.Damage, Armor = s.Armor };
}