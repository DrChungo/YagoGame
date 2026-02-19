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

        public void HealToFull() => Stats.Hp = MaxHp;

        public void IncreaseMaxHp(int delta)
        {
            MaxHp += delta;
            Stats.Hp = MaxHp;
        }

        public void AddStats(StatsDef delta)
        {
            Stats.Armor += delta.Armor;
            Stats.Damage += delta.Damage;

            IncreaseMaxHp(delta.Hp);
        }

        public int TotalDamageShown() => Attack.Damage + Stats.Damage;
    }
}
