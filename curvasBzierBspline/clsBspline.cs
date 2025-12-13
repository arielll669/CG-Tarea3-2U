using System;
using System.Collections.Generic;

/// <summary>
/// Implementación de los algoritmos B-Spline.
/// Utiliza el algoritmo Cox-de Boor para calcular las funciones base (blending functions).
/// </summary>
public static class clsBspline
{
    /// <summary>
    /// Calcula el vector de nodos uniforme (uniform knot vector) para n puntos de control y grado k.
    /// </summary>
    /// <param name="n">Número de puntos de control (P0 a Pn-1).</param>
    /// <param name="k">Grado de la curva (k=2 es cuadrática, k=3 es cúbica).</param>
    /// <returns>Lista de valores de nodos.</returns>
    public static List<float> CrearVectorNodosUniforme(int n, int k)
    {
        int m = n + k; // Número total de nodos (m+1 nodos, índice 0 a m)
        List<float> nodos = new List<float>();

        // Saturación en los extremos (k nodos repetidos)
        for (int i = 0; i < k; i++)
        {
            nodos.Add(0.0f);
        }

        // Nodos intermedios (distribuidos uniformemente entre 0 y 1)
        for (int i = 1; i <= n - k; i++)
        {
            // Normalizar entre 0 y 1
            nodos.Add((float)i / (n - k + 1));
        }

        // Saturación final en los extremos (k nodos repetidos)
        for (int i = 0; i < k; i++)
        {
            nodos.Add(1.0f);
        }

        // Ajuste si n < k, aunque no debería pasar en B-spline uniforme
        if (nodos.Count < m + 1)
        {
            while (nodos.Count <= m) nodos.Add(1.0f);
        }

        return nodos;
    }

    /// <summary>
    /// Función de Base B-spline N_{i,k}(u) usando el algoritmo recursivo Cox-de Boor.
    /// </summary>
    /// <param name="i">Índice del punto de control.</param>
    /// <param name="k">Grado de la base (k-1 es el grado de la curva).</param>
    /// <param name="u">Parámetro de la curva (tiempo).</param>
    /// <param name="nodos">Vector de nodos.</param>
    /// <returns>Valor de la función base.</returns>
    private static float FuncionBase(int i, int k, float u, List<float> nodos)
    {
        // Caso base: Grado 0 (k=1)
        if (k == 1)
        {
            // N_{i,1}(u) = 1 si u_i <= u < u_{i+1}, 0 en otro caso
            if (nodos[i] <= u && u < nodos[i + 1])
            {
                return 1.0f;
            }
            // Excepción para el último nodo, para incluir u=1.0f
            if (u == 1.0f && nodos[i] <= u && i == nodos.Count - 2)
            {
                return 1.0f;
            }
            return 0.0f;
        }

        // Términos recursivos
        float coef1 = 0.0f;
        float coef2 = 0.0f;

        float den1 = nodos[i + k - 1] - nodos[i];
        if (Math.Abs(den1) > float.Epsilon) // Evitar división por cero
        {
            coef1 = (u - nodos[i]) / den1 * FuncionBase(i, k - 1, u, nodos);
        }

        float den2 = nodos[i + k] - nodos[i + 1];
        if (Math.Abs(den2) > float.Epsilon) // Evitar división por cero
        {
            coef2 = (nodos[i + k] - u) / den2 * FuncionBase(i + 1, k - 1, u, nodos);
        }

        return coef1 + coef2;
    }

    /// <summary>
    /// Calcula el punto en la curva B-spline para el parámetro u.
    /// P(u) = SUM_{i=0}^{n-1} P_i * N_{i,k}(u)
    /// </summary>
    /// <param name="controlPoints">Puntos de control.</param>
    /// <param name="gradoK">Grado de la base (k).</param>
    /// <param name="u">Parámetro de tiempo (0.0 a 1.0).</param>
    /// <returns>El punto calculado en la curva (clsPunto).</returns>
    public static clsPunto CalcularPuntoCurva(List<clsPunto> controlPoints, int gradoK, float u)
    {
        int n = controlPoints.Count;
        if (n < gradoK) return n > 0 ? controlPoints[0] : new clsPunto(0, 0);

        List<float> nodos = CrearVectorNodosUniforme(n, gradoK);

        float x = 0.0f;
        float y = 0.0f;

        // Sumatoria: P(u) = SUM_{i=0}^{n-1} P_i * N_{i,k}(u)
        for (int i = 0; i < n; i++)
        {
            float NiK = FuncionBase(i, gradoK, u, nodos);
            x += controlPoints[i].X * NiK;
            y += controlPoints[i].Y * NiK;
        }

        return new clsPunto(x, y);
    }
}