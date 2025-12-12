using System.Drawing;

/// <summary>
/// Clase para representar un punto en 2D con coordenadas de punto flotante.
/// Esto permite cálculos de Bézier más precisos antes de la conversión a píxeles.
/// </summary>
public class clsPunto
{
    public float X { get; set; }
    public float Y { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="x">Coordenada X.</param>
    /// <param name="y">Coordenada Y.</param>
    public clsPunto(float x, float y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Convierte el clsPunto a un objeto Point (coordenadas enteras) para dibujo.
    /// </summary>
    /// <returns>Un objeto Point de System.Drawing.</returns>
    public Point ToPoint()
    {
        // Se asegura de que la conversión sea a coordenadas enteras.
        return new Point((int)X, (int)Y);
    }
}