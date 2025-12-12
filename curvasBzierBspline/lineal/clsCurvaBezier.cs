using System.Drawing;

/// <summary>
/// Clase de utilidad para calcular puntos en curvas de Bézier (lineal y cuadrática).
/// </summary>
public static class clsCurvaBezier
{
    /// <summary>
    /// Calcula el punto P en una curva de Bézier lineal dados dos puntos de control P0 y P1 y el parámetro t.
    /// Fórmula: P(t) = (1 - t) * P0 + t * P1
    /// </summary>
    public static clsPunto CalcularPuntoLineal(clsPunto P0, clsPunto P1, float t)
    {
        float x = (1.0f - t) * P0.X + t * P1.X;
        float y = (1.0f - t) * P0.Y + t * P1.Y;

        return new clsPunto(x, y);
    }

    /// <summary>
    /// Calcula el punto P en una curva de Bézier cuadrática dados tres puntos de control P0, P1, P2 y el parámetro t.
    /// Fórmula: P(t) = (1-t)^2 * P0 + 2t(1-t) * P1 + t^2 * P2
    /// </summary>
    /// <param name="P0">Primer punto de control (clsPunto).</param>
    /// <param name="P1">Segundo punto de control (clsPunto).</param>
    /// <param name="P2">Tercer punto de control (clsPunto).</param>
    /// <param name="t">Parámetro de tiempo (0.0 a 1.0).</param>
    /// <returns>El punto calculado en la curva (clsPunto).</returns>
    public static clsPunto CalcularPuntoCuadratico(clsPunto P0, clsPunto P1, clsPunto P2, float t)
    {
        float unoMenosT = 1.0f - t;
        float unoMenosTSq = unoMenosT * unoMenosT;
        float tSq = t * t;

        // Coeficientes de Bernstein
        float B0 = unoMenosTSq;         // (1-t)^2
        float B1 = 2.0f * t * unoMenosT; // 2t(1-t)
        float B2 = tSq;                 // t^2

        // Calcular X
        float x = B0 * P0.X + B1 * P1.X + B2 * P2.X;

        // Calcular Y
        float y = B0 * P0.Y + B1 * P1.Y + B2 * P2.Y;

        return new clsPunto(x, y);
    }
}