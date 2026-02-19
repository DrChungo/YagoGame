using System;

namespace RoguelikeYago.Src.Combat;

/// <summary>
/// Cálculo de daño efectivo teniendo en cuenta la armadura del defensor.
/// </summary>
public static class CombatDamage
{
    // BALANCEO ARMADURA:
 //Este es el porcentaje del maximo que puede reducir el daño
 //Como maximo te harian uin 20% de daño
    private const int PorcentajeMinimo = 20;

    public static int CalcularDañoEfectivo(int dañoBruto, int armaduraDefensor)
    {
        int dañoReducido = dañoBruto - armaduraDefensor;
        int dañoMinimoPorcentaje = Math.Max(1, dañoBruto * PorcentajeMinimo / 100);
        return Math.Max(dañoMinimoPorcentaje, dañoReducido);
    }
}
