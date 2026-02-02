using System;
using System.Collections.Generic;

namespace RoguelikeYago.Src.UI
{
    public static class Selector
    {
        public static int ArrowSelector(string title, List<string> options)
        {
            int pos = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine();

                for (int i = 0; i < options.Count; i++)
                {
                    if (i == pos)
                        Console.WriteLine($"> {options[i]}");
                    else
                        Console.WriteLine($"  {options[i]}");
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                    pos = (pos - 1 + options.Count) % options.Count;
                else if (key == ConsoleKey.DownArrow)
                    pos = (pos + 1) % options.Count;

            } while (key != ConsoleKey.Enter);

            return pos;
        }
    }
}
