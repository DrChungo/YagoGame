using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Drops;

// =======================================
// FASE 6 – GENERACIÓN DE RECOMPENSAS (3)
// =======================================
public sealed class RewardService
{
    public List<ItemDef> GenerateRoomRewards(
    IReadOnlyList<ItemDef> allItems,
    DropsConfig drops,
    Random rng,
    HashSet<string> uniqueItemIdsSeen)
{
    // ================================
    // FASE 6 – DROPS DESDE afterRoom
    // ================================
    var tierWeights = drops.AfterRoom.TierWeightsByType.Item;

    if (tierWeights == null || tierWeights.Count == 0)
        throw new InvalidOperationException(
            $"Config inválido en {PathConfig.ConfigFile} (campo: drops.afterRoom.tierWeightsByType.item). Está vacío.");

    var weights = tierWeights
        .Select(kv => (tier: ParseTierOrThrow(kv.Key), weight: kv.Value))
        .Where(x => x.weight > 0)
        .ToList();

    if (weights.Count == 0)
        throw new InvalidOperationException(
            $"Config inválido en {PathConfig.ConfigFile} (campo: drops.afterRoom.tierWeightsByType.item). No hay pesos > 0.");

    int count = 3; // PDF: 3 recompensas tras sala (Fase 6)

    var results = new List<ItemDef>();
    int safety = 300;

    while (results.Count < count && safety-- > 0)
    {
        int tier = PickWeightedTier(weights, rng);

        var pool = allItems
            .Where(i => i.Tier == tier)
            .Where(i => !results.Any(r => r.Id == i.Id))
            .Where(i =>
                !i.Unique ||
                !drops.UniqueItemsOncePerRun ||
                !uniqueItemIdsSeen.Contains(i.Id))
            .ToList();

        if (pool.Count == 0) continue;

        var pick = pool[rng.Next(pool.Count)];
        results.Add(pick);
    }

    if (results.Count < count)
        throw new InvalidOperationException(
            $"No hay suficientes items disponibles para generar {count} recompensas (revisa uniqueItemsOncePerRun y items.json).");

    return results;
}

    public ItemDef AskPlayerToChoose(List<ItemDef> rewards)
    {
        Console.Clear();
        Console.WriteLine("Elige una recompensa (FASE 6):\n");

        for (int i = 0; i < rewards.Count; i++)
        {
            var it = rewards[i];
            Console.WriteLine($"{i + 1}. {it.Name} (Tier {it.Tier})");
            Console.WriteLine($"   {it.Description}");
            Console.WriteLine();
        }

        while (true)
        {
            Console.Write("Opción (1-3): ");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int v) && v >= 1 && v <= rewards.Count)
                return rewards[v - 1];

            Console.WriteLine("Entrada inválida. Escribe 1, 2 o 3.");
        }
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
        throw new InvalidOperationException($"Config inválido en drops (tierWeights key no es int): '{key}'.");
    }
}