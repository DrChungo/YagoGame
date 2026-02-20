using System;
using System.Collections.Generic;
using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Npcs;

//NpcService es la clase que se encarga de los NPCs
public class NpcService
{
    //ShouldSpawnShop es el método que se encarga de verificar si se debe spawner un shop
    public bool ShouldSpawnShop(Random rng)
    {

        return rng.Next(1, 101) <= 15;
    }

    //PickShopNpc es el método que se encarga de elegir un NPC de tipo shop
    public NpcDef PickShopNpc(IReadOnlyList<NpcDef> npcs, Random rng)
    {
        return npcs.Where(n => n.Type == "shop").OrderBy(_ => rng.Next()).First();
    }
//PickPostBossNpc es el método que se encarga de elegir un NPC de tipo post_boss
    public NpcDef PickPostBossNpc(IReadOnlyList<NpcDef> npcs, Random rng)
    {
        var pool = npcs
            .Where(n => string.Equals(n.Type, "post_boss", StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (pool.Count == 0)
            throw new InvalidOperationException(
                $"Config inválido en {RoguelikeYago.Src.Config.PathConfig.NpcsFile} (campo: type). "
                    + "No existe ningún NPC con type='post_boss' en npcs.json."
            );

        return pool.OrderBy(_ => rng.Next()).First();
    }

    //ShowNpc es el método que se encarga de mostrar el NPC
    public void ShowNpc(NpcDef npc)
    {
        Console.Clear();
        Console.WriteLine($"{npc.Name}");
        Console.WriteLine(npc.Description);
        Console.WriteLine("\nPulsa una tecla para continuar...");
        Console.ReadKey(true);
    }
}
