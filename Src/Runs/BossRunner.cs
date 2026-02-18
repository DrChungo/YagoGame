using System.Linq;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs
{
    public sealed class BossRunner
    {
        private readonly RunContext _ctx;

        public BossRunner(RunContext ctx) => _ctx = ctx;

        public bool FightBoss(PlayerRunState player, BossDef boss)
{
    var bossAttack = boss.Attacks.First();

    _ctx.Combat.Combat(player.Stats, boss.Stats, player.Attack, bossAttack);

    // Validacion de muerte
    if (player.Stats.Hp <= 0)
        return false;

    player.HealToFull();
    return true;
}

    }
}
