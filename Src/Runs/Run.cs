using System.Collections.Generic;
using System.Linq;

namespace RoguelikeYago.Src.Runs
{
    public class Run
    {
      public void Play()
{
    var ctx = RunContext.Create();
    var player = PlayerRunState.CreateDefault();
    var inventory = new HashSet<string>();

    var roomRunner = new RoomRunner(ctx);
    var rewardApplier = new RewardApplier(ctx);
    var npcRunner = new NpcRunner(ctx);
    var bossRunner = new BossRunner(ctx);

    var bosses = ctx.Content.Bosses.OrderBy(b => b.Order).ToList();

    foreach (var boss in bosses)
    {
        for (int room = 1; room <= 3; room++)
        {
            if (!roomRunner.PlayRoom(player))
            {
                RunUi.ShowGameOver();
                return; 
            }

            rewardApplier.GiveRoomReward(player, inventory);
            npcRunner.MaybeSpawnShop();
        }

        if (!bossRunner.FightBoss(player, boss))
        {
            RunUi.ShowGameOver();
            return; // ✅ vuelve al menú
        }

        npcRunner.ShowPostBossNpc();
    }

    RunUi.ShowEndScreen();
}

    }
}
