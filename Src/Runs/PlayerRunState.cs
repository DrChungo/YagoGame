using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Runs
{
    public sealed class PlayerRunState
    {
        public int MaxHp { get; private set; }
        public StatsDef Stats { get; }
        public AttackDef Attack { get; }

        private PlayerRunState(int maxHp, StatsDef stats, AttackDef attack)
        {
            MaxHp = maxHp;
            Stats = stats;
            Attack = attack;
        }

        //CreateDefault es el método que se encarga de crear el estado del jugador por defecto
        public static PlayerRunState CreateDefault()
        {
            int maxHp = 75;

            var stats = new StatsDef
            {
                Hp = maxHp,
                Speed = 5,
                Damage = 0,
                Armor = 3
            };

            var attack = new AttackDef { Name = "Te tiltea", Damage = 50 };

            return new PlayerRunState(maxHp, stats, attack);
        }

        //HealToFull es el método que se encarga de curar al jugador a su vida máxima
        public void HealToFull() => Stats.Hp = MaxHp;

        public void IncreaseMaxHp(int delta)
        {
            MaxHp += delta;
            Stats.Hp = MaxHp;
        }

        //AddStats es el método que se encarga de sumar las stats del jugador
        public void AddStats(StatsDef delta)
        {
            Stats.Armor += delta.Armor;
            Stats.Damage += delta.Damage;

            IncreaseMaxHp(delta.Hp);
        }

        public int TotalDamageShown() => Attack.Damage + Stats.Damage;
    }
}
