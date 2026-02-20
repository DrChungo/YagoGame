namespace RoguelikeYago.Src.Runs
{
    public sealed class NpcRunner
    {
        private readonly RunContext _ctx;
//NpcRunner es el constructor de la clase NpcRunner
        public NpcRunner(RunContext ctx) => _ctx = ctx;

        //MaybeSpawnShop es el método que se encarga de verificar si se debe spawner un shop
        public void MaybeSpawnShop()
        {
            if (_ctx.Npcs.ShouldSpawnShop(_ctx.Rng))
            {
                var shop = _ctx.Npcs.PickShopNpc(_ctx.Content.Npcs, _ctx.Rng);
                _ctx.Npcs.ShowNpc(shop);
            }
        }

        //ShowPostBossNpc es el método que se encarga de mostrar el NPC de tipo post_boss
        public void ShowPostBossNpc()
        {
            var npc = _ctx.Npcs.PickPostBossNpc(_ctx.Content.Npcs, _ctx.Rng);
            _ctx.Npcs.ShowNpc(npc);
        }
    }
}
