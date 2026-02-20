using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs;

//BossRunner es la clase que se encarga de los bosses
public sealed class BossRunner
{
    private readonly RunContext _ctx;
//BossRunner es el constructor de la clase BossRunner
    public BossRunner(RunContext ctx) => _ctx = ctx;

    //FightBoss es el método que se encarga de hacer el combate con el boss
    public bool FightBoss(PlayerRunState player, BossDef boss)
    {
        _ctx.Combat.Combat(player.Stats, boss.Stats, player.Attack, boss.Attacks, _ctx.Rng, boss.Name);

        if (player.Stats.Hp <= 0)
            return false;

        player.HealToFull();
        return true;
    }
}
