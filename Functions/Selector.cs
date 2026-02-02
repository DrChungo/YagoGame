using System;
using System.Collections.Generic;

public static class Selector
{
    public static int ArrowSelector(string titulo, List<string> opciones)
    {
        int pos = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine(titulo);
            Console.WriteLine();

            for (int i = 0; i < opciones.Count; i++)
            {
                if (i == pos)
                    Console.WriteLine($"> {opciones[i]}");
                else
                    Console.WriteLine($"  {opciones[i]}");
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
                pos = (pos - 1 + opciones.Count) % opciones.Count;
            else if (key == ConsoleKey.DownArrow)
                pos = (pos + 1) % opciones.Count;

        } while (key != ConsoleKey.Enter);

        return pos;
    }
}
