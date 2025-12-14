using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Define los tipos de vectores de nodos B-spline.
/// </summary>
public enum KnotType
{
    /// <summary>Vector de nodos uniforme (no toca los extremos).</summary>
    Uniforme,
    /// <summary>Vector de nodos abierto uniforme (toca los extremos, Clamped).</summary>
    AbiertoUniforme,
    /// <summary>Vector de nodos periódico (curva cerrada).</summary>
    Periodica
}

/// <summary>
/// Implementación de los algoritmos B-Spline utilizando el algoritmo Cox-de Boor.
/// </summary>
public static class clsBspline
{
    private const float Epsilon = 1e-6f;

    /// <summary>
    /// Crea el vector de nodos para n puntos de control y orden p (grado k = p - 1).
    /// </summary>
    /// <param name="n">Número de puntos de control</param>
    /// <param name="k">Grado de la curva (orden = k + 1)</param>
    /// <param name="type">Tipo de vector de nodos</param>
    /// <returns>Vector de nodos</returns>
    public static List<float> CrearVectorNodos(int n, int k, KnotType type)
    {
        List<float> nodos = new List<float>();

        if (n < k + 1) return nodos; // Se necesitan al menos k+1 puntos

        int p = k + 1; // orden = grado + 1
        int m = n + p; // Número total de nodos

        switch (type)
        {
            case KnotType.AbiertoUniforme:
                // Abierto Uniforme (Clamped/Open Uniform)
                // p nodos repetidos al inicio, p nodos repetidos al final
                // Esto hace que la curva pase por el primer y último punto de control

                // Primeros p nodos en 0
                for (int i = 0; i < p; i++)
                {
                    nodos.Add(0.0f);
                }

                // Nodos intermedios uniformemente espaciados: 1, 2, 3, ...
                int numIntermedios = m - 2 * p;
                for (int i = 1; i <= numIntermedios; i++)
                {
                    nodos.Add((float)i);
                }

                // Últimos p nodos en el valor máximo
                float maxVal = (float)(numIntermedios + 1);
                for (int i = 0; i < p; i++)
                {
                    nodos.Add(maxVal);
                }
                break;

            case KnotType.Uniforme:
                // Uniforme: Todos los nodos uniformemente espaciados
                // La curva NO toca los extremos del polígono de control
                for (int i = 0; i < m; i++)
                {
                    nodos.Add((float)i);
                }
                break;

            case KnotType.Periodica:
                // Periódica: Para curvas cerradas
                // Nodos uniformemente espaciados, similar a Uniforme
                for (int i = 0; i < m; i++)
                {
                    nodos.Add((float)i);
                }
                break;
        }

        return nodos;
    }

    /// <summary>
    /// Función de Base B-spline N_{i,p}(u) usando el algoritmo recursivo Cox-de Boor.
    /// </summary>
    /// <param name="i">Índice del punto de control</param>
    /// <param name="p">Orden de la función base (grado + 1)</param>
    /// <param name="u">Parámetro de la curva</param>
    /// <param name="nodos">Vector de nodos</param>
    /// <returns>Valor de la función base</returns>
    private static float FuncionBase(int i, int p, float u, List<float> nodos)
    {
        // Validación de índices
        if (i < 0 || i >= nodos.Count - 1) return 0.0f;
        if (i + p > nodos.Count - 1) return 0.0f;

        // Caso base: Orden 1 (p = 1, grado 0)
        if (p == 1)
        {
            // N_{i,1}(u) = 1 si u_i <= u < u_{i+1}, 0 en caso contrario
            if (u >= nodos[i] && u < nodos[i + 1])
            {
                return 1.0f;
            }
            // Caso especial: incluir el último nodo
            if (Math.Abs(u - nodos[i + 1]) < Epsilon && Math.Abs(u - nodos[nodos.Count - 1]) < Epsilon)
            {
                return 1.0f;
            }
            return 0.0f;
        }

        // Caso recursivo: Fórmula de Cox-de Boor
        // N_{i,p}(u) = [(u - u_i)/(u_{i+p-1} - u_i)] * N_{i,p-1}(u) + 
        //              [(u_{i+p} - u)/(u_{i+p} - u_{i+1})] * N_{i+1,p-1}(u)

        float coef1 = 0.0f;
        float den1 = nodos[i + p - 1] - nodos[i];
        if (Math.Abs(den1) > Epsilon)
        {
            coef1 = (u - nodos[i]) / den1;
        }

        float coef2 = 0.0f;
        float den2 = nodos[i + p] - nodos[i + 1];
        if (Math.Abs(den2) > Epsilon)
        {
            coef2 = (nodos[i + p] - u) / den2;
        }

        float result1 = coef1 * FuncionBase(i, p - 1, u, nodos);
        float result2 = coef2 * FuncionBase(i + 1, p - 1, u, nodos);

        return result1 + result2;
    }

    /// <summary>
    /// Obtiene el rango válido del parámetro u para el tipo de curva especificado.
    /// </summary>
    /// <param name="n">Número de puntos de control</param>
    /// <param name="k">Grado de la curva</param>
    /// <param name="tipoNodos">Tipo de vector de nodos</param>
    /// <returns>Tupla con (uMin, uMax)</returns>
    public static (float uMin, float uMax) ObtenerRangoU(int n, int k, KnotType tipoNodos)
    {
        List<float> nodos = CrearVectorNodos(n, k, tipoNodos);
        if (nodos.Count == 0) return (0, 0);

        int p = k + 1; // orden

        switch (tipoNodos)
        {
            case KnotType.AbiertoUniforme:
                // Para B-splines abiertas (Clamped):
                // El rango válido es [u_p-1, u_{n}]
                // donde p es el orden (grado + 1)
                return (nodos[p - 1], nodos[n]);

            case KnotType.Uniforme:
                // Para uniformes: el rango también es [u_p-1, u_n]
                return (nodos[p - 1], nodos[n]);

            case KnotType.Periodica:
                // Para periódicas: rango completo para cerrar la curva
                return (nodos[p - 1], nodos[n]);

            default:
                return (nodos[p - 1], nodos[n]);
        }
    }

    /// <summary>
    /// Calcula el punto en la curva B-spline para el parámetro t normalizado [0,1].
    /// </summary>
    /// <param name="controlPoints">Lista de puntos de control</param>
    /// <param name="gradoK">Grado de la curva</param>
    /// <param name="t">Parámetro normalizado [0,1]</param>
    /// <param name="tipoNodos">Tipo de vector de nodos</param>
    /// <returns>Punto en la curva</returns>
    public static clsPunto CalcularPuntoCurva(List<clsPunto> controlPoints, int gradoK, float t, KnotType tipoNodos)
    {
        int n = controlPoints.Count;

        // Validar que hay suficientes puntos
        if (n < gradoK + 1)
        {
            // Si no hay suficientes puntos, devolver el primero
            return n > 0 ? controlPoints[0] : new clsPunto(0, 0);
        }

        // Crear vector de nodos
        List<float> nodos = CrearVectorNodos(n, gradoK, tipoNodos);

        if (nodos.Count == 0)
        {
            return controlPoints[0];
        }

        // Obtener rango válido de u
        var (uMin, uMax) = ObtenerRangoU(n, gradoK, tipoNodos);

        // Convertir t [0,1] al rango [uMin, uMax]
        float u = uMin + t * (uMax - uMin);

        // Asegurar que u esté dentro del rango válido
        u = Math.Max(uMin, Math.Min(uMax, u));

        // Ajuste para t=1: posicionar u ligeramente antes del final
        if (Math.Abs(t - 1.0f) < Epsilon)
        {
            u = uMax - Epsilon * 10;
        }

        float x = 0.0f;
        float y = 0.0f;
        float sumaBases = 0.0f;

        int p = gradoK + 1; // orden = grado + 1

        // Calcular P(u) = Σ P_i * N_{i,p}(u)
        for (int i = 0; i < n; i++)
        {
            float Nip = FuncionBase(i, p, u, nodos);
            x += controlPoints[i].X * Nip;
            y += controlPoints[i].Y * Nip;
            sumaBases += Nip;
        }

        // Normalizar si la suma de bases no es 1 (para robustez numérica)
        if (Math.Abs(sumaBases) > Epsilon && Math.Abs(sumaBases - 1.0f) > Epsilon)
        {
            x /= sumaBases;
            y /= sumaBases;
        }

        return new clsPunto(x, y);
    }

    /// <summary>
    /// Calcula múltiples puntos en la curva B-spline para dibujo.
    /// </summary>
    /// <param name="controlPoints">Lista de puntos de control</param>
    /// <param name="gradoK">Grado de la curva</param>
    /// <param name="tipoNodos">Tipo de vector de nodos</param>
    /// <param name="numPuntos">Número de puntos a calcular</param>
    /// <returns>Lista de puntos en la curva</returns>
    public static List<clsPunto> CalcularCurva(List<clsPunto> controlPoints, int gradoK, KnotType tipoNodos, int numPuntos = 100)
    {
        List<clsPunto> puntosCurva = new List<clsPunto>();

        if (controlPoints.Count < gradoK + 1) return puntosCurva;

        try
        {
            // Calcular puntos a lo largo de la curva
            for (int i = 0; i <= numPuntos; i++)
            {
                float t = (float)i / numPuntos;
                clsPunto punto = CalcularPuntoCurva(controlPoints, gradoK, t, tipoNodos);

                // Validar que el punto es válido (no NaN, no infinito)
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