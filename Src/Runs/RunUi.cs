using System;

namespace RoguelikeYago.Src.Runs
{
    public static class RunUi
    {
        public static void ShowEndScreen()
        {
            Console.Clear();
            Console.WriteLine("FASE 7 completada.");
            Console.WriteLine("Pulsa una tecla para volver al menú...");
            Console.ReadKey(true);
        }
        public static void ShowGameOver()
{
    Console.Clear();
    Console.WriteLine("💀 Has muerto. Fin de la run.");
    Console.WriteLine("Pulsa una tecla para volver al menú...");
    Console.ReadKey(true);
}

    }
}
