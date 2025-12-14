using System;
using System.Collections.Generic;
using System.Linq;

// ============================================================================
// Implementación de curvas B-Spline usando el algoritmo recursivo Cox-de Boor
// Soporta tres tipos de vectores de nodos: Uniforme, Abierto Uniforme y Periódica
// ============================================================================

public enum KnotType
{
    Uniforme,
    AbiertoUniforme,
    Periodica
}

public static class clsBspline
{
    private const float Epsilon = 1e-6f;

    public static List<float> CrearVectorNodos(int n, int k, KnotType type)
    {
        List<float> nodos = new List<float>();
        if (n < k + 1) return nodos;

        int m = n + k + 1;

        switch (type)
        {
            case KnotType.AbiertoUniforme:
                for (int i = 0; i <= k; i++)
                    nodos.Add(0.0f);

                int numIntermedios = n - k - 1;
                for (int i = 1; i <= numIntermedios; i++)
                    nodos.Add((float)i);

                float valorFinal = (float)(numIntermedios + 1);
                for (int i = 0; i <= k; i++)
                    nodos.Add(valorFinal);
                break;

            case KnotType.Uniforme:
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;

            case KnotType.Periodica:
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;
        }

        return nodos;
    }

    private static float FuncionBase(int i, int k, float u, List<float> nodos)
    {
        if (i < 0 || i >= nodos.Count)
            return 0.0f;

        if (k == 0)
        {
            if (i + 1 >= nodos.Count)
                return 0.0f;

            if (u >= nodos[i] && u < nodos[i + 1])
                return 1.0f;

            if (Math.Abs(u - nodos[i + 1]) < Epsilon &&
                Math.Abs(u - nodos[nodos.Count - 1]) < Epsilon)
                return 1.0f;

            return 0.0f;
        }

        if (i + k >= nodos.Count || i + k + 1 >= nodos.Count)
            return 0.0f;

        float coef1 = 0.0f;
        float den1 = nodos[i + k] - nodos[i];
        if (Math.Abs(den1) > Epsilon)
            coef1 = (u - nodos[i]) / den1;

        float coef2 = 0.0f;
        float den2 = nodos[i + k + 1] - nodos[i + 1];
        if (Math.Abs(den2) > Epsilon)
            coef2 = (nodos[i + k + 1] - u) / den2;

        return coef1 * FuncionBase(i, k - 1, u, nodos) +
               coef2 * FuncionBase(i + 1, k - 1, u, nodos);
    }

    public static (float uMin, float uMax) ObtenerRangoU(int n, int k, KnotType tipoNodos)
    {
        List<float> nodos = CrearVectorNodos(n, k, tipoNodos);
        if (nodos.Count == 0) return (0, 0);

        int indiceMin = k;
        int indiceMax = n;

        if (indiceMin >= nodos.Count || indiceMax >= nodos.Count)
            return (nodos[0], nodos[nodos.Count - 1]);

        return (nodos[indiceMin], nodos[indiceMax]);
    }

    public static clsPunto CalcularPuntoCurva(List<clsPunto> controlPoints, int gradoK, float t, KnotType tipoNodos)
    {
        int n = controlPoints.Count;

        if (n < gradoK + 1)
            return n > 0 ? controlPoints[0] : new clsPunto(0, 0);

        try
        {
            List<clsPunto> puntosParaCalculo = new List<clsPunto>(controlPoints);
            int nCalculo = n;

            if (tipoNodos == KnotType.Periodica)
            {
                for (int i = 0; i < gradoK; i++)
                {
                    puntosParaCalculo.Add(new clsPunto(controlPoints[i].X, controlPoints[i].Y));
                }
                nCalculo = puntosParaCalculo.Count;
            }

            List<float> nodos = CrearVectorNodos(nCalculo, gradoK, tipoNodos);
            if (nodos.Count == 0) return controlPoints[0];

            var (uMin, uMax) = ObtenerRangoU(nCalculo, gradoK, tipoNodos);

            if (Math.Abs(uMax - uMin) < Epsilon) return controlPoints[0];

            float u = uMin + t * (uMax - uMin);

            if (tipoNodos == KnotType.AbiertoUniforme)
            {
                if (t >= 0.999f)
                    u = uMax;
                else if (t <= 0.001f)
                    u = uMin;
                else
                    u = Math.Max(uMin, Math.Min(uMax, u));
            }
            else if (tipoNodos == KnotType.Periodica)
            {
                if (t >= 0.999f)
                {
                    u = uMax - Epsilon;
                }
                else
                {
                    u = Math.Max(uMin, Math.Min(uMax, u));
                }
            }
            else
            {
                u = Math.Max(uMin, Math.Min(uMax, u));
                if (Math.Abs(t - 1.0f) < Epsilon)
                    u = uMax - Epsilon * 10;
            }

            float x = 0.0f, y = 0.0f, sumaBases = 0.0f;

            for (int i = 0; i < nCalculo; i++)
            {
                float Nik = FuncionBase(i, gradoK, u, nodos);
                x += puntosParaCalculo[i].X * Nik;
                y += puntosParaCalculo[i].Y * Nik;
                sumaBases += Nik;
            }

            if (Math.Abs(sumaBases) > Epsilon && Math.Abs(sumaBases - 1.0f) > Epsilon)
            {
                x /= sumaBases;
                y /= sumaBases;
            }

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