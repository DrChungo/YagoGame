using System;
using System.Collections.Generic;

namespace RoguelikeYago.Src.UI;


//  UI con flechas + Enter

public static class ArrowMenu
{
    //Choose es el método que se encarga de mostrar el menú con flechas y Enter
    public static int Choose(string title, IReadOnlyList<string> options)
    {
        if (options.Count == 0) throw new ArgumentException("No hay opciones para mostrar.", nameof(options));

        int index = 0;

        while (true)
        {
            Console.Clear();
        
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine(title);
                Console.WriteLine(new string('-', Math.Min(60, title.Length)));
            }

            for (int i = 0; i < options.Count; i++)
            {
                string prefix = (i == index) ? "➤ " : "  ";
                Console.WriteLine(prefix + options[i]);
            }

            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    index = (index - 1 + options.Count) % options.Count;
                    break;

                case ConsoleKey.DownArrow:
                    index = (index + 1) % options.Count;
                    break;

                case ConsoleKey.Enter:
                    return index;

                case ConsoleKey.Escape:
                    return -1; 
            }
        }
    }
}
