using System.Drawing;

/// <summary>
/// Clase de utilidad para calcular puntos en curvas de Bézier (lineal, cuadrática y cúbica).
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
    public static clsPunto CalcularPuntoCuadratico(clsPunto P0, clsPunto P1, clsPunto P2, float t)
    {
        float unoMenosT = 1.0f - t;
        float tSq = t * t;

        // Coeficientes de Bernstein
        float B0 = unoMenosT * unoMenosT;
        float B1 = 2.0f * t * unoMenosT;
        float B2 = tSq;

        float x = B0 * P0.X + B1 * P1.X + B2 * P2.X;
        float y = B0 * P0.Y + B1 * P1.Y + B2 * P2.Y;

        return new clsPunto(x, y);
    }

    /// <summary>
    /// Calcula el punto P en una curva de Bézier cúbica dados cuatro puntos de control P0, P1, P2, P3 y el parámetro t.
    /// Fórmula: P(t) = (1-t)^3 * P0 + 3t(1-t)^2 * P1 + 3t^2(1-t) * P2 + t^3 * P3
    /// </summary>
    public static clsPunto CalcularPuntoCubico(clsPunto P0, clsPunto P1, clsPunto P2, clsPunto P3, float t)
    {
        float unoMenosT = 1.0f - t;
        float unoMenosTCubed = unoMenosT * unoMenosT * unoMenosT; // (1-t)^3
        float tSq = t * t;
        float tCubed = tSq * t; // t^3

        // Coeficientes de Bernstein (Binomial * t^i * (1-t)^(n-i))
        float B0 = unoMenosTCubed;                  // (1-t)^3
        float B1 = 3.0f * t * unoMenosT * unoMenosT; // 3t(1-t)^2
        float B2 = 3.0f * tSq * unoMenosT;           // 3t^2(1-t)
        float B3 = tCubed;                          // t^3

        // Calcular X
        float x = B0 * P0.X + B1 * P1.X + B2 * P2.X + B3 * P3.X;

        // Calcular Y
        float y = B0 * P0.Y + B1 * P1.Y + B2 * P2.Y + B3 * P3.Y;

        return new clsPunto(x, y);
    }
}