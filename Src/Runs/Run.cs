using System.Collections.Generic;
using System.Linq;

namespace RoguelikeYago.Src.Runs
{
    //Run es la clase que se encarga de la ejecución del juego
    public class Run
    {
      //Play es el método que se encarga de ejecutar el juego
      public void Play()
{
    var ctx = RunContext.Create();
    //CreateDefault es el método que se encarga de crear el estado del jugador por defecto
    var player = PlayerRunState.CreateDefault();
    //inventory es el conjunto de items que el jugador tiene
    var inventory = new HashSet<string>();

    var roomRunner = new RoomRunner(ctx);
    var rewardApplier = new RewardApplier(ctx);
    var npcRunner = new NpcRunner(ctx);
    var bossRunner = new BossRunner(ctx);

    var bosses = ctx.Content.Bosses.OrderBy(b => b.Order).ToList();

    //bosses es la lista de bosses que se van a enfrentar
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
            return; 
        }

        npcRunner.ShowPostBossNpc();
    }

    //ShowEndScreen es el método que se encarga de mostrar la pantalla de fin de juego
    RunUi.ShowEndScreen();
}

    }
}
