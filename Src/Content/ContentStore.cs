using System.Collections.Generic;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Content
{
    public class ContentStore
    {
        // Listas crudas (como el JSON)
        public List<ItemDef> Items { get; }
        public List<SkillDef> Skills { get; }
        public List<EnemyDef> Enemies { get; }
        public List<BossDef> Bosses { get; }
        public List<NpcDef> Npcs { get; }
        public List<DropTableDef> DropTables { get; }

        // Índices por id (para LINQ rápido)
        public Dictionary<string, ItemDef> ItemsById { get; }
        public Dictionary<string, SkillDef> SkillsById { get; }
        public Dictionary<string, EnemyDef> EnemiesById { get; }
        public Dictionary<string, BossDef> BossesById { get; }
        public Dictionary<string, NpcDef> NpcsById { get; }
        public Dictionary<string, DropTableDef> DropTablesById { get; }

        public ContentStore(
            List<ItemDef> items,
            List<SkillDef> skills,
            List<EnemyDef> enemies,
            List<BossDef> bosses,
            List<NpcDef> npcs,
            List<DropTableDef> dropTables
        )
        {
            Items = items;
            Skills = skills;
            Enemies = enemies;
            Bosses = bosses;
            Npcs = npcs;
            DropTables = dropTables;

            ItemsById = BuildIndex(items, i => i.Id);
            SkillsById = BuildIndex(skills, s => s.Id);
            EnemiesById = BuildIndex(enemies, e => e.Id);
            BossesById = BuildIndex(bosses, b => b.Id);
            NpcsById = BuildIndex(npcs, n => n.Id);
            DropTablesById = BuildIndex(dropTables, d => d.Id);
        }

        private static Dictionary<string, T> BuildIndex<T>(
            IEnumerable<T> list,
            System.Func<T, string> getId
        )
        {
            var dict = new Dictionary<string, T>();

            foreach (var item in list)
            {
                dict[getId(item)] = item;
            }

            return dict;
        }
    }
}
