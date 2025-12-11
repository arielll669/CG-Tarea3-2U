using curvasBzierBspline;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace C
{
    public class cLineal
    {
        // Propiedades
        public cPunto P0 { get; set; }  // Punto de control inicial
        public cPunto P1 { get; set; }  // Punto de control final
        public double T { get; set; }   // Parámetro t (0 a 1)

        // Constructor
        public cLineal(cPunto p0, cPunto p1)
        {
            P0 = p0;
            P1 = p1;
            T = 0;
        }

        // Constructor alternativo con coordenadas
        public cLineal(float x0, float y0, float x1, float y1)
        {
            P0 = new cPunto(x0, y0, Color.Red);
            P1 = new cPunto(x1, y1, Color.Green);
            T = 0;
        }

        // Fórmula de Bézier Lineal: B(t) = (1-t)*P0 + t*P1
        public cPunto CalcularPuntoEnCurva(double t)
        {
            float x = (float)((1 - t) * P0.X + t * P1.X);
            float y = (float)((1 - t) * P0.Y + t * P1.Y);
            return new cPunto(x, y, Color.Blue, 8);
        }

        // Dibujar la curva completa (línea entre P0 y P1)
        public void DibujarCurva(Graphics g)
        {
            // Configurar calidad de dibujo
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Dibujar línea de la curva (en este caso es una línea recta)
            using (Pen penCurva = new Pen(Color.Black, 3))
            {
                g.DrawLine(penCurva, P0.ToPointF(), P1.ToPointF());
            }

            // Dibujar puntos de control
            P0.Dibujar(g);
            P1.Dibujar(g);

            // Dibujar etiquetas de los puntos
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                g.DrawString("P0", font, brush, P0.X - 25, P0.Y - 25);
                g.DrawString("P1", font, brush, P1.X + 15, P1.Y - 25);
            }
        }

        // Dibujar animación con líneas auxiliares
        public void DibujarAnimacion(Graphics g, bool mostrarLineasAuxiliares)
        {
            // Primero dibujar la curva base
            DibujarCurva(g);

            // Calcular punto actual en la curva
            cPunto puntoActual = CalcularPuntoEnCurva(T);

            // Dibujar líneas auxiliares si está activado
            if (mostrarLineasAuxiliares)
            {
                // Línea punteada desde P0 al punto actual
                using (Pen penAux = new Pen(Color.Gray, 2))
                {
                    penAux.DashStyle = DashStyle.Dash;
                    g.DrawLine(penAux, P0.ToPointF(), puntoActual.ToPointF());
                }

                // Línea punteada desde el punto actual a P1
                using (Pen penAux2 = new Pen(Color.LightGray, 2))
                {
                    penAux2.DashStyle = DashStyle.Dash;
                    g.DrawLine(penAux2, puntoActual.ToPointF(), P1.ToPointF());
                }
            }

            // Dibujar el punto actual en la curva (más grande y destacado)
            puntoActual.Radio = 12;
            puntoActual.ColorPunto = Color.Blue;
            puntoActual.Dibujar(g);

            // Dibujar etiqueta del punto actual
            using (Font font = new Font("Arial", 10, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(Color.Blue))
            {
                string etiqueta = $"B(t) = {puntoActual}";
                g.DrawString(etiqueta, font, brush, puntoActual.X + 15, puntoActual.Y + 5);
            }

            // Dibujar línea de trayectoria (traza del movimiento)
            DibujarTrayectoria(g);
        }

        // Dibujar la trayectoria hasta el punto actual
        private void DibujarTrayectoria(Graphics g)
        {
            using (Pen penTrayectoria = new Pen(Color.FromArgb(100, Color.Blue), 2))
            {
                for (double t = 0; t <= T; t += 0.01)
                {
                    cPunto p1 = CalcularPuntoEnCurva(t);
                    cPunto p2 = CalcularPuntoEnCurva(t + 0.01);

                    if (t + 0.01 <= T)
                    {
                        g.DrawLine(penTrayectoria, p1.ToPointF(), p2.ToPointF());
                    }
                }
            }
        }

        // Actualizar el valor de T
        public void ActualizarT(double nuevoT)
        {
            T = Math.Max(0, Math.Min(1, nuevoT)); // Limitar entre 0 y 1
        }

        // Reiniciar animación
        public void Reiniciar()
        {
            T = 0;
        }

        // Verificar si algún punto de control fue clickeado
        public cPunto ObtenerPuntoClickeado(float x, float y)
        {
            if (P0.ContieneClick(x, y))
                return P0;
            if (P1.ContieneClick(x, y))
                return P1;
            return null;
        }
    }
}