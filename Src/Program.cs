using System;
using RoguelikeYago.Src.Services;
using RoguelikeYago.Src.UI;

class Program
{
    static void Main()
    {
        try
        {
            var content = new ContentStore();
            content.LoadAll();

            ContentValidator.Validate(content);

            var rng = new GameRng(content.Config);

            Console.Clear();
            Console.WriteLine("Contenido cargado correctamente ✅");
            Console.WriteLine("Pulsa una tecla para entrar al menú...");
            Console.ReadKey(true);

            MainMenu.Show(content, rng);
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine("ERROR AL INICIAR EL JUEGO");
            Console.WriteLine("------------------------------------");
            Console.WriteLine(ex.Message);
            Console.WriteLine("\nPulsa una tecla para cerrar...");
            Console.ReadKey(true);
        }
    }
}
