using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs
{
    public static class PlayerSetup
    {
        public sealed record PlayerBundle(int MaxHp, StatsDef Stats, AttackDef Attack);
        //Funcion que crea el jugador por defecto.
        public static PlayerBundle CreateDefault()
        {
            int playerMaxHp = 50;

            var playerStats = new StatsDef
            {
                Hp = playerMaxHp,
                Speed = 5,
                Damage = 15,
                Armor = 3,
            };

            var playerAttack = new AttackDef
            {
                Name = "Te tiltea",
                Damage = 10
            };

            return new PlayerBundle(playerMaxHp, playerStats, playerAttack);
        }
    }
}
