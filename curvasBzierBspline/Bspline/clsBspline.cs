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
    /// Fórmula: m = n + k + 1 (total de nodos)
    /// </summary>
    public static List<float> CrearVectorNodos(int n, int k, KnotType type)
    {
        List<float> nodos = new List<float>();
        if (n < k + 1) return nodos;

        int m = n + k + 1; // Total de nodos según fórmula estándar

        switch (type)
        {
            case KnotType.AbiertoUniforme:
                // Vector de nodos abierto (clamped): Toca extremos
                // Primeros (k+1) nodos = 0, últimos (k+1) nodos = valor_max

                // Repetir k+1 veces el valor inicial
                for (int i = 0; i <= k; i++)
                    nodos.Add(0.0f);

                // Nodos intermedios uniformemente espaciados
                int numIntermedios = n - k - 1;
                for (int i = 1; i <= numIntermedios; i++)
                    nodos.Add((float)i);

                // Repetir k+1 veces el valor final
                float valorFinal = (float)(numIntermedios + 1);
                for (int i = 0; i <= k; i++)
                    nodos.Add(valorFinal);
                break;

            case KnotType.Uniforme:
                // Vector uniforme: NO toca extremos
                // Todos los nodos uniformemente espaciados sin repetición
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;

            case KnotType.Periodica:
                // Vector periódico: Para curvas cerradas
                // Nodos uniformes sin multiplicidad en extremos
                // Esto permite que la curva se cierre suavemente
                for (int i = 0; i < m; i++)
                    nodos.Add((float)i);
                break;
        }

        return nodos;
    }

    /// <summary>
    /// Función de Base B-spline N_{i,k}(u) usando Cox-de Boor recursivo.
    /// </summary>
    private static float FuncionBase(int i, int k, float u, List<float> nodos)
    {
        // Validación de índices
        if (i < 0 || i >= nodos.Count)
            return 0.0f;

        // Caso base: k = 0 (orden 1)
        if (k == 0)
        {
            // Verificar si i+1 está dentro de los límites
            if (i + 1 >= nodos.Count)
                return 0.0f;

            // Función característica: 1 si u está en [t_i, t_{i+1}), 0 en otro caso
            if (u >= nodos[i] && u < nodos[i + 1])
                return 1.0f;

            // Caso especial: incluir el extremo final
            if (Math.Abs(u - nodos[i + 1]) < Epsilon &&
                Math.Abs(u - nodos[nodos.Count - 1]) < Epsilon)
                return 1.0f;

            return 0.0f;
        }

        // Caso recursivo: Fórmula de Cox-de Boor
        // N_{i,k}(u) = ((u - t_i) / (t_{i+k} - t_i)) * N_{i,k-1}(u) +
        //              ((t_{i+k+1} - u) / (t_{i+k+1} - t_{i+1})) * N_{i+1,k-1}(u)

        // Verificar límites para evitar accesos fuera de rango
        if (i + k >= nodos.Count || i + k + 1 >= nodos.Count)
            return 0.0f;

        // Primer término
        float coef1 = 0.0f;
        float den1 = nodos[i + k] - nodos[i];
        if (Math.Abs(den1) > Epsilon)
            coef1 = (u - nodos[i]) / den1;

        // Segundo término
        float coef2 = 0.0f;
        float den2 = nodos[i + k + 1] - nodos[i + 1];
        if (Math.Abs(den2) > Epsilon)
            coef2 = (nodos[i + k + 1] - u) / den2;

        return coef1 * FuncionBase(i, k - 1, u, nodos) +
               coef2 * FuncionBase(i + 1, k - 1, u, nodos);
    }

    /// <summary>
    /// Obtiene el rango válido del parámetro u para la curva.
    /// </summary>
    public static (float uMin, float uMax) ObtenerRangoU(int n, int k, KnotType tipoNodos)
    {
        List<float> nodos = CrearVectorNodos(n, k, tipoNodos);
        if (nodos.Count == 0) return (0, 0);

        // El rango válido es [t_k, t_n]
        // donde k es el grado y n es el número de puntos de control
        int indiceMin = k;
        int indiceMax = n;

        if (indiceMin >= nodos.Count || indiceMax >= nodos.Count)
            return (nodos[0], nodos[nodos.Count - 1]);

        return (nodos[indiceMin], nodos[indiceMax]);
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
            // Para periódica, usar puntos envueltos
            List<clsPunto> puntosParaUsar = new List<clsPunto>(controlPoints);

            if (tipoNodos == KnotType.Periodica)
            {
                // Agregar los primeros gradoK puntos al final para cerrar
                for (int i = 0; i < gradoK; i++)
                {
                    puntosParaUsar.Add(new clsPunto(controlPoints[i].X, controlPoints[i].Y));
                }
                n = puntosParaUsar.Count; // Actualizar n con puntos envueltos
            }

            List<float> nodos = CrearVectorNodos(n, gradoK, tipoNodos);
            if (nodos.Count == 0) return controlPoints[0];

            var (uMin, uMax) = ObtenerRangoU(n, gradoK, tipoNodos);

            if (Math.Abs(uMax - uMin) < Epsilon) return controlPoints[0];

            // Mapear t [0,1] al rango [uMin, uMax]
            float u = uMin + t * (uMax - uMin);

            // Manejo de extremos según el tipo
            if (tipoNodos == KnotType.AbiertoUniforme)
            {
                // Para Abierto/Uniforme: permitir llegar exactamente a los extremos
                if (t >= 0.999f) // Cuando está muy cerca del final
                    u = uMax; // Llegar exactamente al extremo
                else if (t <= 0.001f) // Cuando está muy cerca del inicio
                    u = uMin; // Estar exactamente en el inicio
                else
                    u = Math.Max(uMin, Math.Min(uMax, u));
            }
            else if (tipoNodos == KnotType.Periodica)
            {
                // Para Periódica: cerrar el ciclo suavemente
                if (t >= 0.999f)
                {
                    // Cuando t→1, el punto debe estar muy cerca del inicio para cerrar el loop
                    u = uMax - Epsilon;
                }
                else
                {
                    u = Math.Max(uMin, Math.Min(uMax, u));
                }
            }
            else // Uniforme
            {
                // Para Uniforme: mantener comportamiento original
                u = Math.Max(uMin, Math.Min(uMax, u));
                if (Math.Abs(t - 1.0f) < Epsilon)
                    u = uMax - Epsilon * 10;
            }

            float x = 0.0f, y = 0.0f, sumaBases = 0.0f;

            // Calcular C(u) = Σ P_i * N_{i,k}(u)
            for (int i = 0; i < n; i++)
            {
                float Nik = FuncionBase(i, gradoK, u, nodos);
                x += puntosParaUsar[i].X * Nik;
                y += puntosParaUsar[i].Y * Nik;
                sumaBases += Nik;
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
            // Para curvas periódicas, envolver puntos de control
            List<clsPunto> puntosParaUsar = new List<clsPunto>(controlPoints);

            if (tipoNodos == KnotType.Periodica)
            {
                // Agregar los primeros k puntos al final para cerrar la curva
                for (int i = 0; i < gradoK; i++)
                {
                    puntosParaUsar.Add(new clsPunto(controlPoints[i].X, controlPoints[i].Y));
                }
            }

            for (int i = 0; i <= numPuntos; i++)
            {
                float t = (float)i / numPuntos;
                clsPunto punto = CalcularPuntoCurva(puntosParaUsar, gradoK, t, tipoNodos);

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