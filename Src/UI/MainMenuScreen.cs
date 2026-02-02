using System.Collections.Generic;
using RoguelikeYago.Src.Core;

namespace RoguelikeYago.Src.UI
{
    public static class MainMenuScreen
    {
        public static GameState Show()
        {
            var options = new List<string>
            {
                "Nueva partida",
                "Cargar partida",
                "Salir"
            };

            int selected = Selector.ArrowSelector("=== ROGUELIKE YAGO ===", options);

            switch (selected)
            {
                case 0:
                    Console.WriteLine("\n>> Nueva partida (todavía no implementado 😏)");
                    Console.ReadKey();
                    return GameState.Menu;

                case 1:
                    Console.WriteLine("\n>> Cargar partida (todavía no implementado 😏)");
                    Console.ReadKey();
                    return GameState.Menu;

                case 2:
                    return GameState.Exit;

                default:
                    return GameState.Menu;
            }
        }
    }
}
