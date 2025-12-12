using System.Drawing;

/// <summary>
/// Clase de utilidad para calcular el punto en una curva de Bézier lineal.
/// </summary>
public static class clsCurvaBezier
{
    /// <summary>
    /// Calcula el punto P en una curva de Bézier lineal dados dos puntos de control P0 y P1 y el parámetro t.
    /// La fórmula es: P(t) = (1 - t) * P0 + t * P1
    /// </summary>
    /// <param name="P0">Primer punto de control (clsPunto).</param>
    /// <param name="P1">Segundo punto de control (clsPunto).</param>
    /// <param name="t">Parámetro de tiempo (0.0 a 1.0).</param>
    /// <returns>El punto calculado en la curva (clsPunto).</returns>
    public static clsPunto CalcularPuntoLineal(clsPunto P0, clsPunto P1, float t)
    {
        float x = (1.0f - t) * P0.X + t * P1.X;
        float y = (1.0f - t) * P0.Y + t * P1.Y;

        return new clsPunto(x, y);
    }
}