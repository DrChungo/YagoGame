using System;
using System.Threading;

namespace RoguelikeYago.Src.UI;

public static class Typewriter
{
    public static void Write(string text, int delayMs = 15)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs);
        }
    }

    public static void WriteLine(string text, int delayMs = 15)
    {
        Write(text + Environment.NewLine, delayMs);
    }
}
