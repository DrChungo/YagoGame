using System;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago;

internal static class Story
{
    //FASE 0
    public static void Show()
    {
        Typewriter.WriteLine(
            @"
Yago despierta una mañana cualquiera,
con el cuerpo fuera de la cama…
y el alma aún dentro.
"
        );

        Typewriter.WriteLine(
            @"
Todo parece normal hasta que, en un descuido imperdonable,
el malvado Lander irrumpe en su casa
y le roba lo más sagrado que posee:
"
        );

        Typewriter.WriteLine(
            @"
SU CAMA.
",
            40
        );

        Typewriter.WriteLine(
            @"
Sin cama no hay descanso.
Sin descanso no hay vida.
Y sin vida… no hay Yago.
"
        );

        Typewriter.WriteLine(
            @"
Forzado a abandonar la comodidad de su santuario,
Yago se ve arrastrado a una aventura absurda,
caótica y peligrosa.
"
        );

        Typewriter.WriteLine(
            @"
Porque hay héroes que luchan por gloria,
otros por honor…

Y luego está Yago,
que lucha por volver a la cama.
",
            30
        );

        Console.ReadKey(true);
    }
}
