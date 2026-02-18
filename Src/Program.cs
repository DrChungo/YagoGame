using System;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago;

internal static class Program
{
    //FASE 0
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Story.Show();
        MainMenu.Show();
    }
}
