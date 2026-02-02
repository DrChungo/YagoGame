using System;
using System.Collections.Generic;

public static class MainMenu
{
    public static void Mostrar()
    {
        bool salir = false;

        var opciones = new List<string>
        {
            "Nueva partida",
            "Cargar partida",
            "Salir"
        };

        while (!salir)
        {
            int eleccion = Selector.ArrowSelector("=== ROGUELIKE ===", opciones);

            switch (eleccion)
            {
                case 0:
                    NewGame();
                    break;

                case 1:
                    LoadGame();
                    break;

                case 2:
                    salir = true;
                    break;
            }
        }
    }

    static void NewGame()
    {
        Console.Clear();
        Console.WriteLine(">> Comenzando nueva partida...");
        Console.WriteLine("(Aquí luego irá selección de clase)");
        Console.ReadKey();
    }

    static void LoadGame()
    {
        Console.Clear();
        Console.WriteLine(">> No hay partidas guardadas (todavía 😏)");
        Console.ReadKey();
    }
}
