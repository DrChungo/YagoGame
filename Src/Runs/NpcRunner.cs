namespace RoguelikeYago.Src.Runs
{
    public sealed class NpcRunner
    {
        private readonly RunContext _ctx;

        public NpcRunner(RunContext ctx) => _ctx = ctx;

        public void MaybeSpawnShop()
        {
            if (_ctx.Npcs.ShouldSpawnShop(_ctx.Rng))
            {
                var shop = _ctx.Npcs.PickShopNpc(_ctx.Content.Npcs, _ctx.Rng);
                _ctx.Npcs.ShowNpc(shop);
            }
        }

        public void ShowPostBossNpc()
        {
            var npc = _ctx.Npcs.PickPostBossNpc(_ctx.Content.Npcs, _ctx.Rng);
            _ctx.Npcs.ShowNpc(npc);
        }
    }
}
