using System;
using System.Threading;

namespace RoguelikeYago.Src.UI;

public static class Typewriter  //Tipo de escritura
{
    //Write es el método que se encarga de escribir el texto
    public static void Write(string text, int delayMs = 15)
        {
            Console.CursorVisible = false;

            for (int i = 0; i < text.Length; i++)
            {
                // Skip  de la intro (intro es la introducción del juego)
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true); 
                    Console.Write(text.Substring(i));
                    return;
                }

                Console.Write(text[i]);
                Thread.Sleep(delayMs);
            }
        }
    public static void WriteLine(string text, int delayMs = 15)
    {
        Write(text + Environment.NewLine, delayMs);
    }
}
