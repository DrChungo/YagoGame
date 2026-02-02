using System;
using System.Linq;
using RoguelikeYago.Src.Content;

namespace RoguelikeYago.Src.Content
{
    public static class ContentValidator
    {
        public static void Validate(ContentStore store)
        {
            ValidateUniqueIds(store);
            ValidateEnemies(store);
            ValidateBosses(store);
            // Drops/NPCs los puedes validar en otra fase si quieres
        }

        private static void ValidateUniqueIds(ContentStore store)
        {
            void CheckDuplicates(string name, string[] ids)
            {
                var duplicated = ids
                    .GroupBy(x => x)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key)
                    .ToList();

                if (duplicated.Any())
                    throw new Exception($"IDs duplicados en {name}: {string.Join(", ", duplicated)}");
            }

            CheckDuplicates("Items", store.Items.Select(i => i.Id).ToArray());
            CheckDuplicates("Player Skills", store.Skills.Select(s => s.Id).ToArray());
            CheckDuplicates("Enemies", store.Enemies.Select(e => e.Id).ToArray());
            CheckDuplicates("Bosses", store.Bosses.Select(b => b.Id).ToArray());
            CheckDuplicates("Npcs", store.Npcs.Select(n => n.Id).ToArray());
            CheckDuplicates("DropTables", store.DropTables.Select(d => d.Id).ToArray());
        }

        private static void ValidateEnemies(ContentStore store)
        {
            foreach (var enemy in store.Enemies)
            {
                if (string.IsNullOrWhiteSpace(enemy.Id))
                    throw new Exception("Hay un enemigo con Id vacío.");

                if (string.IsNullOrWhiteSpace(enemy.Name))
                    throw new Exception($"Enemy '{enemy.Id}' tiene Name vacío.");

                if (enemy.Stats.Hp <= 0)
                    throw new Exception($"Enemy '{enemy.Id}' tiene hp inválido.");

                // ✅ Ahora se valida la skill embebida (no SkillId)
                if (enemy.Skill == null)
                    throw new Exception($"Enemy '{enemy.Id}' no tiene skill definida.");

                if (string.IsNullOrWhiteSpace(enemy.Skill.Id))
                    throw new Exception($"Enemy '{enemy.Id}' tiene skill.id vacío.");

                if (string.IsNullOrWhiteSpace(enemy.Skill.Name))
                    throw new Exception($"Enemy '{enemy.Id}' tiene skill.name vacío.");

                if (enemy.Skill.Power <= 0)
                    throw new Exception($"Enemy '{enemy.Id}' tiene skill.power inválido.");
            }
        }

        private static void ValidateBosses(ContentStore store)
        {
            foreach (var boss in store.Bosses)
            {
                if (string.IsNullOrWhiteSpace(boss.Id))
                    throw new Exception("Hay un boss con Id vacío.");

                if (boss.Stats.Hp <= 0)
                    throw new Exception($"Boss '{boss.Id}' tiene hp inválido.");

                if (boss.Skills == null || boss.Skills.Count == 0)
                    throw new Exception($"Boss '{boss.Id}' no tiene skills.");

                foreach (var sk in boss.Skills)
                {
                    if (string.IsNullOrWhiteSpace(sk.Id))
                        throw new Exception($"Boss '{boss.Id}' tiene una skill con id vacío.");
                    if (string.IsNullOrWhiteSpace(sk.Name))
                        throw new Exception($"Boss '{boss.Id}' tiene una skill con name vacío.");
                    if (sk.Power <= 0)
                        throw new Exception($"Boss '{boss.Id}' tiene una skill con power inválido.");
                }
            }
        }
    }
}
