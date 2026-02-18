using System;
using System.Collections.Generic;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs
{
    public sealed class RewardApplier
    {
        private readonly RunContext _ctx;

        public RewardApplier(RunContext ctx) => _ctx = ctx;

        public void GiveRoomReward(PlayerRunState player, HashSet<string> inventory)
        {
            var options = _ctx.Rewards.GenerateRoomRewards(
                _ctx.Content.Items,
                _ctx.Config.Drops,
                _ctx.Rng,
                inventory
            );

            var chosen = _ctx.Rewards.AskPlayerToChoose(options, player.Stats, player.Attack);
            inventory.Add(chosen.Id);

            player.AddStats(chosen.Stats);

            PrintReward(chosen, player);
            Console.WriteLine("Pulsa una tecla para continuar...");
            Console.ReadKey(true);
        }

        private static void PrintReward(ItemDef chosen, PlayerRunState player)
        {
            Console.WriteLine($"\n¡Mejora obtenida: {chosen.Name}!");

            if (chosen.Stats.Hp > 0)
                Console.WriteLine($"Max HP +{chosen.Stats.Hp} (Total: {player.MaxHp})");
            if (chosen.Stats.Speed > 0)
                Console.WriteLine($"Speed +{chosen.Stats.Speed} (Total: {player.Stats.Speed})");
            if (chosen.Stats.Damage > 0)
                Console.WriteLine($"Damage +{chosen.Stats.Damage} (Total: {player.TotalDamageShown()})");
            if (chosen.Stats.Armor > 0)
                Console.WriteLine($"Armor +{chosen.Stats.Armor} (Total: {player.Stats.Armor})");
        }
    }
}
