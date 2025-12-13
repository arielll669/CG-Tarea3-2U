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
    /// Crea el vector de nodos para n puntos de control y grado k.
    /// </summary>
    /// <param name="n">Número de puntos de control</param>
    /// <param name="k">Grado de la curva</param>
    /// <param name="type">Tipo de vector de nodos</param>
    /// <returns>Vector de nodos</returns>
    public static List<float> CrearVectorNodos(int n, int k, KnotType type)
    {
        List<float> nodos = new List<float>();

        if (n < k) return nodos;

        // Número de nodos = n + k + 1 (para grado k)
        int m = n + k; // Índice del último nodo

        switch (type)
        {
            case KnotType.AbiertoUniforme:
                // Abierto Uniforme (Clamped): k+1 nodos al inicio y k+1 al final
                // Esto asegura que la curva interpola los puntos extremos

                // Primeros k+1 nodos en 0
                for (int i = 0; i <= k; i++)
                {
                    nodos.Add(0.0f);
                }

                // Nodos intermedios uniformemente espaciados
                int numIntermedios = n - k;
                for (int i = 1; i < numIntermedios; i++)
                {
                    nodos.Add((float)i);
                }

                // Últimos k+1 nodos en el valor máximo
                float maxVal = Math.Max(1.0f, (float)(numIntermedios));
                for (int i = 0; i <= k; i++)
                {
                    nodos.Add(maxVal);
                }
                break;

            case KnotType.Uniforme:
                // Uniforme: Todos los nodos uniformemente espaciados
                for (int i = 0; i <= m; i++)
                {
                    nodos.Add((float)i);
                }
                break;

            case KnotType.Periodica:
                // Periódica: Nodos uniformemente espaciados
                // Para curvas cerradas, los nodos se repiten de forma cíclica
                for (int i = 0; i <= m; i++)
                {
                    nodos.Add((float)i);
                }
                break;
        }

        return nodos;
    }

    /// <summary>
    /// Función de Base B-spline N_{i,k}(u) usando el algoritmo recursivo Cox-de Boor.
    /// </summary>
    /// <param name="i">Índice del punto de control</param>
    /// <param name="k">Grado de la función base</param>
    /// <param name="u">Parámetro de la curva</param>
    /// <param name="nodos">Vector de nodos</param>
    /// <returns>Valor de la función base</returns>
    private static float FuncionBase(int i, int k, float u, List<float> nodos)
    {
        // Validación de índices
        if (i < 0 || i + k >= nodos.Count) return 0.0f;

        // Caso base: Grado 1 (función escalón)
        if (k == 1)
        {
            // N_{i,1}(u) = 1 si u_i <= u < u_{i+1}, 0 en caso contrario
            if (u >= nodos[i] && u < nodos[i + 1])
            {
                return 1.0f;
            }
            // Caso especial: incluir el último nodo
            if (Math.Abs(u - nodos[i + 1]) < Epsilon && Math.Abs(nodos[i + 1] - nodos.Last()) < Epsilon)
            {
                return 1.0f;
            }
            return 0.0f;
        }

        // Caso recursivo: Cox-de Boor
        // N_{i,k}(u) = [(u - u_i)/(u_{i+k-1} - u_i)] * N_{i,k-1}(u) + 
        //              [(u_{i+k} - u)/(u_{i+k} - u_{i+1})] * N_{i+1,k-1}(u)

        float coef1 = 0.0f;
        float den1 = nodos[i + k - 1] - nodos[i];
        if (Math.Abs(den1) > Epsilon)
        {
            coef1 = (u - nodos[i]) / den1;
        }

        float coef2 = 0.0f;
        float den2 = nodos[i + k] - nodos[i + 1];
        if (Math.Abs(den2) > Epsilon)
        {
            coef2 = (nodos[i + k] - u) / den2;
        }

        float result1 = coef1 * FuncionBase(i, k - 1, u, nodos);
        float result2 = coef2 * FuncionBase(i + 1, k - 1, u, nodos);

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

        switch (tipoNodos)
        {
            case KnotType.AbiertoUniforme:
                // Para B-splines abiertas uniformes, el rango es [u_k, u_n]
                // donde u_k es el (k+1)-ésimo nodo y u_n es el (n+1)-ésimo nodo
                return (nodos[k], nodos[n]);

            case KnotType.Uniforme:
                // Para uniformes, el rango útil es similar
                return (nodos[k], nodos[nodos.Count - k - 1]);

            case KnotType.Periodica:
                // Para periódicas, usamos el rango completo
                return (nodos[0], nodos[n]);

            default:
                return (nodos[0], nodos[nodos.Count - 1]);
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
        if (n < gradoK)
        {
            return n > 0 ? controlPoints[0] : new clsPunto(0, 0);
        }

        // Crear vector de nodos
        List<float> nodos = CrearVectorNodos(n, gradoK, tipoNodos);

        // Obtener rango válido de u
        var (uMin, uMax) = ObtenerRangoU(n, gradoK, tipoNodos);

        // Convertir t [0,1] al rango [uMin, uMax]
        float u = uMin + t * (uMax - uMin);

        // Asegurar que u esté dentro del rango
        u = Math.Max(uMin, Math.Min(uMax, u));

        float x = 0.0f;
        float y = 0.0f;

        // Calcular P(u) = Σ P_i * N_{i,k}(u)
        for (int i = 0; i < n; i++)
        {
            float NiK = FuncionBase(i, gradoK, u, nodos);
            x += controlPoints[i].X * NiK;
            y += controlPoints[i].Y * NiK;
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

        if (controlPoints.Count < gradoK) return puntosCurva;

        for (int i = 0; i <= numPuntos; i++)
        {
            float t = (float)i / numPuntos;
            clsPunto punto = CalcularPuntoCurva(controlPoints, gradoK, t, tipoNodos);
            puntosCurva.Add(punto);
        }

        return puntosCurva;
    }
}