using System;
using System.Drawing;

namespace curvasBzierBspline
{
    public class cPunto
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Color ColorPunto { get; set; }
        public float Radio { get; set; }
        public bool EstaSeleccionado { get; set; }

        public cPunto(float x, float y, Color color, float radio = 10)
        {
            X = x;
            Y = y;
            ColorPunto = color;
            Radio = radio;
            EstaSeleccionado = false;
        }

        public cPunto(Point punto, Color color, float radio = 10)
        {
            X = punto.X;
            Y = punto.Y;
            ColorPunto = color;
            Radio = radio;
            EstaSeleccionado = false;
        }

        public void Dibujar(Graphics g)
        {
            using (SolidBrush brush = new SolidBrush(ColorPunto))
            {
                g.FillEllipse(brush, X - Radio, Y - Radio, Radio * 2, Radio * 2);
            }

            using (Pen pen = new Pen(Color.Black, 2))
            {
                g.DrawEllipse(pen, X - Radio, Y - Radio, Radio * 2, Radio * 2);
            }

            if (EstaSeleccionado)
            {
                using (Pen penSeleccion = new Pen(Color.Yellow, 3))
                {
                    g.DrawEllipse(penSeleccion, X - Radio - 5, Y - Radio - 5, Radio * 2 + 10, Radio * 2 + 10);
                }
            }
        }

        public bool ContieneClick(float clickX, float clickY)
        {
            float distancia = (float)Math.Sqrt(Math.Pow(clickX - X, 2) + Math.Pow(clickY - Y, 2));
            return distancia <= Radio;
        }

        public float DistanciaA(cPunto otroPunto)
        {
            return (float)Math.Sqrt(Math.Pow(otroPunto.X - X, 2) + Math.Pow(otroPunto.Y - Y, 2));
        }

        public void Mover(float nuevoX, float nuevoY)
        {
            X = nuevoX;
            Y = nuevoY;
        }

        public PointF ToPointF()
        {
            return new PointF(X, Y);
        }

        // Override ToString para debugging
        public override string ToString()
        {
            return $"({X:F0}, {Y:F0})";
        }
    }
}