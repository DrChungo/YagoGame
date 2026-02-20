using System;
using RoguelikeYago.Src.UI;

namespace RoguelikeYago;

internal static class Story
{
    //LA YAGO HISTORIA
    public static void Show()
    {
        Typewriter.WriteLine(
            @"
Yago se despierta como siempre, hecho mierda.

Se levanta, va al baño medio dormido y cuando vuelve a la habitación…
algo no cuadra.
"
        );

        Typewriter.WriteLine(
            @"
La cama no está.

Se queda mirando el suelo unos segundos.
Ni colchón.
Ni almohada.
Ni la sábana esa que no ha lavado en meses.
"
        );

        Typewriter.WriteLine(
            @"
Nada.
",
            40
        );

        Typewriter.WriteLine(
            @"
Después de procesarlo cinco segundos,
solo puede llegar a una conclusión lógica:

Ha sido Lander.
"
        );

        Typewriter.WriteLine(
            @"
Porque nadie más en el planeta tendría la cara
de entrar a tu casa y llevarse la cama.
No el móvil.
No la tele.
La cama.
"
        );

        Typewriter.WriteLine(
            @"
Y claro, sin cama no puedes dormir.
Sin dormir te rayas.
Y si te rayas demasiado igual empiezas a madrugar.

Eso no puede pasar.
"
        );

        Typewriter.WriteLine(
            @"
Así que, todavía en pijama y con cero ganas de existir,
Yago decide que va a recuperarla.

Le da igual quién se ponga delante.
Le da igual cuánta gente rara haya por el camino.

La cama vuelve a casa.

Por lo civil…
o por lo criminal.


Pulsa una tecla para continuar...
",
            30
        );

        Console.ReadKey(true);
    }
}
