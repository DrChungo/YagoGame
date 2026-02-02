using System;
using System.Collections.Generic;
using RoguelikeYago.Src.UI;
using RoguelikeYago.Src.Content;
using RoguelikeYago.Src.Definitions;

namespace RoguelikeYago.Src.Core
{
    public class Game
    {
        private ContentStore? _contentStore;

        public void Start()
        {
            // 1️⃣ Cargar contenido del juego
            try
            {
                _contentStore = LoadContent();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("ERROR AL CARGAR EL CONTENIDO DEL JUEGO");
                Console.WriteLine("------------------------------------");
                Console.WriteLine(ex.Message);
                Console.WriteLine("\nPulsa una tecla para cerrar...");
                Console.ReadKey();
                return;
            }

            // 2️⃣ Loop principal
            GameState state = GameState.Menu;

            while (state != GameState.Exit)
            {
                switch (state)
                {
                    case GameState.Menu:
                        state = MainMenuScreen.Show();
                        break;

                    case GameState.Run:
                    case GameState.Combat:
                        // Todavía no implementado
                        state = GameState.Menu;
                        break;

                    default:
                        state = GameState.Exit;
                        break;
                }
            }

            Console.Clear();
            Console.WriteLine("¡Hasta luego!");
            Console.ReadKey();
        }

        // ⬇⬇⬇ AQUÍ VA EXACTAMENTE TU MÉTODO ⬇⬇⬇
        private ContentStore LoadContent()
        {
            var itemsFile = JsonLoader.Load<ItemsFile>("Data/items.json");
            var skillsFile = JsonLoader.Load<SkillsFile>("Data/skills.json");
            var enemiesFile = JsonLoader.Load<EnemiesFile>("Data/enemies.json");
            var bossesFile = JsonLoader.Load<BossesFile>("Data/bosses.json");
            var npcsFile = JsonLoader.Load<NpcsFile>("Data/npcs.json");
            var dropFile = JsonLoader.Load<DropTablesFile>("Data/dropTables.json");

            var store = new ContentStore(
                itemsFile.Items,
                skillsFile.Skills,
                enemiesFile.Enemies,
                bossesFile.Bosses,
                npcsFile.Npcs,
                dropFile.DropTables
            );

            ContentValidator.Validate(store);
            return store;


        }
    }
}
