using System;

namespace RoguelikeYago.Src.Runs
{
    public static class RunUi
    {
     
       //ShowEndScreen es el método que se encarga de mostrar la pantalla de fin de juego
       public static void ShowEndScreen()
        {
            Console.Clear();
            Console.WriteLine("FASE 7 completada.");
            Console.WriteLine("Pulsa una tecla para volver al menú...");
            Console.ReadKey(true);
        }

        //ShowGameOver es el método que se encarga de mostrar la pantalla de fin de juego
        
        public static void ShowGameOver()
{
    Console.Clear();
    Console.WriteLine("💀 Has muerto. Fin de la run.");
    Console.WriteLine("Pulsa una tecla para volver al menú...");
    Console.ReadKey(true);
}

    }
}
