using System;

namespace curvasBzierBspline
{
    /// <summary>
    /// Clase con métodos estáticos para calcular puntos en curvas Bézier.
    /// </summary>
    public static class clsCurvaBezier
    {
        /// <summary>
        /// Interpolación lineal entre dos puntos (Bézier de grado 1).
        /// </summary>
        public static clsPunto CalcularPuntoLineal(clsPunto p0, clsPunto p1, float t)
        {
            float x = (1 - t) * p0.X + t * p1.X;
            float y = (1 - t) * p0.Y + t * p1.Y;
            return new clsPunto(x, y);
        }

        /// <summary>
        /// Calcula un punto en una curva Bézier cuadrática (3 puntos de control).
        /// </summary>
        public static clsPunto CalcularPuntoCuadratico(clsPunto p0, clsPunto p1, clsPunto p2, float t)
        {
            float u = 1 - t;
            float x = u * u * p0.X + 2 * u * t * p1.X + t * t * p2.X;
            float y = u * u * p0.Y + 2 * u * t * p1.Y + t * t * p2.Y;
            return new clsPunto(x, y);
        }
    }
}