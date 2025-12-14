using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Define los tipos de vectores de nodos B-spline.
/// </summary>
public enum KnotType
{
    Uniforme,           // No toca extremos
    AbiertoUniforme,    // Toca extremos (Clamped)
    Periodica          // Curva cerrada
}

/// <summary>
/// Implementación de B-Spline usando el algoritmo Cox-de Boor.
/// </summary>
public static class clsBspline
{
    private const float Epsilon = 1e-6f;

    /// <summary>
    /// Crea el vector de nodos para n puntos de control y grado k.
    /// </summary>
    public static List<float> CrearVectorNodos(int n, int k, KnotType type)
    {
        List<float> nodos = new List<float>();
        if (n < k + 1) return nodos;

        int p = k + 1; // orden = grado + 1
        int m = n + k + 2; // ✓ CORREGIDO: número total de nodos = n + k + 2

        switch (type)
        {
            case KnotType.AbiertoUniforme:
                // p nodos repetidos al inicio (multiplicidad p)
                for (int i = 0; i < p; i++)
                    nodos.Add(0.0f);

                // Nodos intermedios uniformemente espaciados
                int numIntermedios = m - 2 * p;
                for (int i = 1; i <= numIntermedios; i++)
                    nodos.Add((float)i);

                // p nodos repetidos al final (multiplicidad p)
                float maxVal = (float)(numIntermedios + 1);
                for (int i = 0; i < p; i++)
                    nodos.Add(maxVal);
                break;

            case KnotType.Uniforme:
                // Todos los nodos uniformemente espaciados (sin repetición)
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;

            case KnotType.Periodica:
                // Para curvas cerradas - nodos uniformes
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;
        }

        return nodos;
    }

    /// <summary>
    /// Función de Base B-spline N_{i,p}(u) usando Cox-de Boor recursivo.
    /// </summary>
    private static float FuncionBase(int i, int p, float u, List<float> nodos)
    {
        // Validación de índices
        if (i < 0 || i >= nodos.Count - 1)
            return 0.0f;

        if (i + p >= nodos.Count)
            return 0.0f;

        // Caso base: orden 1 (p=1)
        if (p == 1)
        {
            // Intervalo [t_i, t_{i+1})
            if (u >= nodos[i] && u < nodos[i + 1])
                return 1.0f;

            // Caso especial: último nodo (incluir el extremo final)
            if (Math.Abs(u - nodos[i + 1]) < Epsilon &&
                Math.Abs(u - nodos[nodos.Count - 1]) < Epsilon)
                return 1.0f;

            return 0.0f;
        }

        // Caso recursivo: Fórmula de Cox-de Boor
        // N_{i,p}(u) = ((u - t_i) / (t_{i+p-1} - t_i)) * N_{i,p-1}(u) +
        //              ((t_{i+p} - u) / (t_{i+p} - t_{i+1})) * N_{i+1,p-1}(u)

        float coef1 = 0.0f;
        float den1 = nodos[i + p - 1] - nodos[i];
        if (Math.Abs(den1) > Epsilon)
            coef1 = (u - nodos[i]) / den1;

        float coef2 = 0.0f;
        float den2 = nodos[i + p] - nodos[i + 1];
        if (Math.Abs(den2) > Epsilon)
            coef2 = (nodos[i + p] - u) / den2;

        return coef1 * FuncionBase(i, p - 1, u, nodos) +
               coef2 * FuncionBase(i + 1, p - 1, u, nodos);
    }

    /// <summary>
    /// Obtiene el rango válido del parámetro u para la curva.
    /// </summary>
    public static (float uMin, float uMax) ObtenerRangoU(int n, int k, KnotType tipoNodos)
    {
        List<float> nodos = CrearVectorNodos(n, k, tipoNodos);
        if (nodos.Count == 0) return (0, 0);

        int p = k + 1; // orden

        // Para una B-spline, el rango válido es [t_p, t_n]
        // donde p es el orden y n es el número de puntos de control
        if (p >= nodos.Count || n >= nodos.Count)
            return (nodos[0], nodos[nodos.Count - 1]);

        return (nodos[p], nodos[n]);
    }

    /// <summary>
    /// Calcula un punto en la curva B-spline para t normalizado [0,1].
    /// </summary>
    public static clsPunto CalcularPuntoCurva(List<clsPunto> controlPoints, int gradoK, float t, KnotType tipoNodos)
    {
        int n = controlPoints.Count;

        if (n < gradoK + 1)
            return n > 0 ? controlPoints[0] : new clsPunto(0, 0);

        try
        {
            List<float> nodos = CrearVectorNodos(n, gradoK, tipoNodos);
            if (nodos.Count == 0) return controlPoints[0];

            var (uMin, uMax) = ObtenerRangoU(n, gradoK, tipoNodos);

            if (Math.Abs(uMax - uMin) < Epsilon) return controlPoints[0];

            // Mapear t [0,1] al rango [uMin, uMax]
            float u = uMin + t * (uMax - uMin);
            u = Math.Max(uMin, Math.Min(uMax, u));

            // Ajuste para el extremo final
            if (Math.Abs(t - 1.0f) < Epsilon)
                u = uMax - Epsilon * 10;

            float x = 0.0f, y = 0.0f, sumaBases = 0.0f;
            int p = gradoK + 1; // orden

            // Calcular C(u) = Σ P_i * N_{i,p}(u)
            for (int i = 0; i < n; i++)
            {
                float Nip = FuncionBase(i, p, u, nodos);
                x += controlPoints[i].X * Nip;
                y += controlPoints[i].Y * Nip;
                sumaBases += Nip;
            }

            // Normalizar si la suma no es 1 (puede pasar en casos extremos)
            if (Math.Abs(sumaBases) > Epsilon && Math.Abs(sumaBases - 1.0f) > Epsilon)
            {
                x /= sumaBases;
                y /= sumaBases;
            }

            // Validar resultado
            if (float.IsNaN(x) || float.IsNaN(y) ||
                float.IsInfinity(x) || float.IsInfinity(y))
                return controlPoints[0];

            return new clsPunto(x, y);
        }
        catch
        {
            return n > 0 ? controlPoints[0] : new clsPunto(0, 0);
        }
    }

    /// <summary>
    /// Calcula múltiples puntos en la curva para dibujo.
    /// </summary>
    public static List<clsPunto> CalcularCurva(List<clsPunto> controlPoints, int gradoK, KnotType tipoNodos, int numPuntos = 100)
    {
        List<clsPunto> puntosCurva = new List<clsPunto>();
        if (controlPoints.Count < gradoK + 1) return puntosCurva;

        try
        {
            for (int i = 0; i <= numPuntos; i++)
            {
                float t = (float)i / numPuntos;
                clsPunto punto = CalcularPuntoCurva(controlPoints, gradoK, t, tipoNodos);

                if (!float.IsNaN(punto.X) && !float.IsNaN(punto.Y) &&
                    !float.IsInfinity(punto.X) && !float.IsInfinity(punto.Y))
                {
                    puntosCurva.Add(punto);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error en CalcularCurva: {ex.Message}");
        }

        return puntosCurva;
    }
}