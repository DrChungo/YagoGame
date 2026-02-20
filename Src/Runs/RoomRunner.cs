using RoguelikeYago.Src.Runs;
using RoguelikeYago.Src.Services;

namespace RoguelikeYago.Src.Runs
{

    //RoomRunner es la clase que se encarga de la ejecución de la sala
   public sealed class RoomRunner
    {
        private readonly RunContext _ctx;

        public RoomRunner(RunContext ctx) => _ctx = ctx;

      public bool PlayRoom(PlayerRunState player)
{
    var roomEnemies = RunHelpers.GenerateRoom(
        _ctx.Content.Enemies,
        _ctx.Config.EnemyGeneration.TierWeights,
        _ctx.Rng
    );

    var instances = RunHelpers.ToInstances(roomEnemies);

    _ctx.Combat.StartThreeEnemiesRoom(player.Stats, player.Attack, instances);

    // Validacion si mueres
    if (player.Stats.Hp <= 0)
        return false;

    player.HealToFull();
    return true;
}


}
}   