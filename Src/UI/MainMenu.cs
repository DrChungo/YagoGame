using System.Collections.Generic;
using RoguelikeYago.Src.Runs;

namespace RoguelikeYago.Src.UI;

public static class MainMenu
{
    // ==========================
    // FASE 9 – MENÚ CON FLECHAS
    // ==========================
    public static void Show()
    {
        while (true)
        {
            var options = new List<string> { "Jugar ", "Salir" };

            int choice = ArrowMenu.Choose("=== YAGO GAME ===", options);

            if (choice == 0)
            {
                new Run().Play();
            }
            else
            {
                return;
            }
        }
    }
}
