using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Clase de utilidad para calcular puntos en curvas de Bézier (lineal, cuadrática, cúbica y grado N).
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
        float unoMenosTCubed = unoMenosT * unoMenosT * unoMenosT;
        float tSq = t * t;
        float tCubed = tSq * t;

        // Coeficientes de Bernstein
        float B0 = unoMenosTCubed;
        float B1 = 3.0f * t * unoMenosT * unoMenosT;
        float B2 = 3.0f * tSq * unoMenosT;
        float B3 = tCubed;

        float x = B0 * P0.X + B1 * P1.X + B2 * P2.X + B3 * P3.X;
        float y = B0 * P0.Y + B1 * P1.Y + B2 * P2.Y + B3 * P3.Y;

        return new clsPunto(x, y);
    }

    /// <summary>
    /// Calcula el punto P(t) en una curva de Bézier de grado N usando el algoritmo De Casteljau.
    /// También devuelve todos los niveles intermedios de cálculo para visualización.
    /// </summary>
    /// <param name="controlPoints">Lista de puntos de control (P0, P1, ..., Pn).</param>
    /// <param name="t">Parámetro de tiempo (0.0 a 1.0).</param>
    /// <param name="levels">Lista de niveles intermedios del algoritmo (para visualización De Casteljau).</param>
    /// <returns>El punto calculado en la curva P(t).</returns>
    public static clsPunto CalcularPuntoDeCasteljau(List<clsPunto> controlPoints, float t, out List<List<clsPunto>> levels)
    {
        levels = new List<List<clsPunto>>();

        // Validación: necesitamos al menos un punto
        if (controlPoints == null || controlPoints.Count == 0)
        {
            return new clsPunto(0, 0);
        }

        // Nivel 0: los puntos de control originales
        List<clsPunto> currentLevel = new List<clsPunto>(controlPoints);
        levels.Add(new List<clsPunto>(currentLevel));

        // Algoritmo De Casteljau: reducir iterativamente hasta obtener un solo punto
        while (currentLevel.Count > 1)
        {
            List<clsPunto> nextLevel = new List<clsPunto>();

            // Interpolar entre cada par de puntos consecutivos
            for (int i = 0; i < currentLevel.Count - 1; i++)
            {
                clsPunto p0 = currentLevel[i];
                clsPunto p1 = currentLevel[i + 1];

                // Interpolación lineal: P = (1-t) * P0 + t * P1
                float x = (1.0f - t) * p0.X + t * p1.X;
                float y = (1.0f - t) * p0.Y + t * p1.Y;

                nextLevel.Add(new clsPunto(x, y));
            }

            // Guardar el nivel actual para visualización
            levels.Add(new List<clsPunto>(nextLevel));
            currentLevel = nextLevel;
        }

        // El último punto restante es P(t)
        return currentLevel[0];
    }

    /// <summary>
    /// Sobrecarga simplificada que solo devuelve el punto P(t) sin los niveles intermedios.
    /// </summary>
    /// <param name="controlPoints">Lista de puntos de control.</param>
    /// <param name="t">Parámetro de tiempo (0.0 a 1.0).</param>
    /// <returns>El punto calculado en la curva P(t).</returns>
    public static clsPunto CalcularPuntoDeCasteljau(List<clsPunto> controlPoints, float t)
    {
        List<List<clsPunto>> dummyLevels;
        return CalcularPuntoDeCasteljau(controlPoints, t, out dummyLevels);
    }
}