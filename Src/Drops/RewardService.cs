using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Drops;


public class RewardService
{

    //Funcion que genera las recompensas de la sala.
    public List<ItemDef> GenerateRoomRewards(
        IReadOnlyList<ItemDef> allItems,
        DropsConfig drops,
        Random rng,
        HashSet<string> uniqueItemIdsSeen
    )
    {

        var tierWeights = drops.AfterRoom.TierWeightsByType.Item;

        if (tierWeights == null || tierWeights.Count == 0)
            throw new InvalidOperationException(
                $"Config inválido en {PathConfig.ConfigFile} (campo: drops.afterRoom.tierWeightsByType.item). Está vacío."
            );

        var weights = tierWeights
            .Select(kv => (tier: ParseTierOrThrow(kv.Key), weight: kv.Value))
            .Where(x => x.weight > 0)
            .ToList();

        if (weights.Count == 0)
            throw new InvalidOperationException(
                $"Config inválido en {PathConfig.ConfigFile} (campo: drops.afterRoom.tierWeightsByType.item). No hay pesos > 0."
            );

        int count = 3;

        var results = new List<ItemDef>();
        int safety = 300;

        while (results.Count < count && safety-- > 0)
        {
            int tier = PickWeightedTier(weights, rng);

            var pool = allItems
                .Where(i => i.Tier == tier)
                .Where(i => !results.Any(r => r.Id == i.Id))
                .Where(i =>
                    !i.Unique || !drops.UniqueItemsOncePerRun || !uniqueItemIdsSeen.Contains(i.Id)
                )
                .ToList();

            if (pool.Count == 0)
                continue;

            var pick = pool[rng.Next(pool.Count)];
            results.Add(pick);
        }

        if (results.Count < count)
            throw new InvalidOperationException(
                $"No hay suficientes items disponibles para generar {count} recompensas (revisa uniqueItemsOncePerRun y items.json)."
            );

        return results;
    }

//funcion que pide al jugador que elija una recompensa.
    public ItemDef AskPlayerToChoose(
        List<ItemDef> rewards,
        StatsDef currentStats,
        AttackDef currentAttack
    )
    {
     
        //  RECOMPENSAS CON FLECHAS
 
        var options = rewards
            .Select(it =>
            {
                var parts = new List<string>();
                if (it.Stats.Hp > 0)
                    parts.Add($"HP {currentStats.Hp}->{currentStats.Hp + it.Stats.Hp}");
                if (it.Stats.Speed > 0)
                    parts.Add($"Speed {currentStats.Speed}->{currentStats.Speed + it.Stats.Speed}");
                if (it.Stats.Damage > 0)
                    parts.Add(
                        $"Dmg {currentStats.Damage}->{currentStats.Damage + it.Stats.Damage}"
                    );
                if (it.Stats.Armor > 0)
                    parts.Add($"Armor {currentStats.Armor}->{currentStats.Armor + it.Stats.Armor}");

                string statsStr = parts.Count > 0 ? string.Join(", ", parts) : "Sin cambios";
                return $"{it.Name} ({statsStr}) – {it.Description}";
            })
            .ToList();

        int choice = RoguelikeYago.Src.UI.ArrowMenu.Choose("Elige una recompensa:", options);

        // Si pulsa ESC, por simplicidad elegimos la primera
        if (choice < 0)
            choice = 0;

        return rewards[choice];
    }

    private static int PickWeightedTier(List<(int tier, int weight)> weights, Random rng)
    {
        int total = weights.Sum(w => w.weight);
        int roll = rng.Next(1, total + 1);

        int acc = 0;
        foreach (var w in weights)
        {
            acc += w.weight;
            if (roll <= acc)
                return w.tier;
        }

        return weights[^1].tier;
    }

    private static int ParseTierOrThrow(string key)
    {
        if (int.TryParse(key, out int t))
            return t;
        throw new InvalidOperationException(
            $"Config inválido en drops (tierWeights key no es int): '{key}'."
        );
    }
}
