using System;

namespace RoguelikeYago.Src.Combat;

/// <summary>
/// Cálculo de daño efectivo teniendo en cuenta la armadura del defensor.
/// </summary>
public static class CombatDamage
{
    // BALANCEO ARMADURA: mínimo porcentaje de daño original que siempre se recibe (20 = mínimo 20%).
    private const int MinPercentage = 20;

    public static int CalculateEffectiveDamage(int rawDamage, int defenderArmor)
    {
        int reducedDamage = rawDamage - defenderArmor;
        int minDamageByPercentage = Math.Max(1, rawDamage * MinPercentage / 100);
        return Math.Max(minDamageByPercentage, reducedDamage);
    }
}
