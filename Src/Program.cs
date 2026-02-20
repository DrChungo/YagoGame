using System;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago;

internal static class Program
{

    //Main es el método principal que se encarga de ejecutar el programa
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Story.Show();
        MainMenu.Show();
    }
}
