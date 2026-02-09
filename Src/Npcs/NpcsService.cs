using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Npcs;

// ===================================
// FASE 7 – NPCs (shop + post-boss)
// ===================================
public sealed class NpcService
{
    public bool ShouldSpawnShop(Random rng)
    {
        // ==========================
        // FASE 7 – TIENDA 15%
        // ==========================
        return rng.Next(1, 101) <= 15;
    }

    public NpcDef PickShopNpc(IReadOnlyList<NpcDef> npcs, Random rng)
    {
        return npcs
            .Where(n => n.Type == "shop")
            .OrderBy(_ => rng.Next())
            .First();
    }

 public NpcDef PickPostBossNpc(IReadOnlyList<NpcDef> npcs)
{
    // ==========================
    // FASE 7 – NPC POST-BOSS
    // ==========================
    var npc = npcs.FirstOrDefault(n =>
        string.Equals(n.Type, "post_boss", StringComparison.OrdinalIgnoreCase));

    if (npc == null)
        throw new InvalidOperationException(
            $"Config inválido en {RoguelikeYago.Src.Config.PathConfig.NpcsFile} (campo: type). " +
            "No existe ningún NPC con type='post_boss' en npcs.json.");

    return npc;
}

    public void ShowNpc(NpcDef npc)
    {
        Console.Clear();
        Console.WriteLine($"{npc.Name}");
        Console.WriteLine(npc.Description);
        Console.WriteLine("\nPulsa una tecla para continuar...");
        Console.ReadKey(true);
    }
}
