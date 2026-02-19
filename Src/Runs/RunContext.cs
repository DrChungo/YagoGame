using System;
using RoguelikeYago.Src.Combat;
using RoguelikeYago.Src.Config;
using RoguelikeYago.Src.Drops;
using RoguelikeYago.Src.Npcs;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Runs
{
    public sealed class RunContext
    {
        public GameConfig Config { get; }
        public ContentService Content { get; }
        public Random Rng { get; }
        public CombatService Combat { get; }
        public RewardService Rewards { get; }
        public NpcService Npcs { get; }

        private RunContext(GameConfig config, ContentService content, Random rng,
            CombatService combat, RewardService rewards, NpcService npcs)
        {
            Config = config;
            Content = content;
            Rng = rng;
            Combat = combat;
            Rewards = rewards;
            Npcs = npcs;
        }

        public static RunContext Create()
        {
            var loader = new JsonFileLoader();
            var config = loader.LoadObject<GameConfig>(PathConfig.ConfigFile);

            var content = new ContentService();
            var seed = config.Rng.UseSeed
                ? config.Rng.DefaultSeed
                : Guid.NewGuid().GetHashCode();
            var rng = new Random(seed);

            return new RunContext(
                config,
                content,
                rng,
                new CombatService(),
                new RewardService(),
                new NpcService()
            );
        }
    }
}
