using System;
using System.Drawing;
using System.Windows.Forms;

namespace curvasBzierBspline
{
    public partial class frmLineal : Form
    {
        // --- Variables de Estado ---
        private clsPunto P0;
        private clsPunto P1;

        // Variables de Dibujo
        private Graphics g;
        private Bitmap canvas;

        // Parámetro de la curva (de 0.0 a 1.0)
        private float t = 0.0f;

        // **NUEVA VARIABLE:** Indica la dirección del movimiento: true (P0 -> P1), false (P1 -> P0)
        private bool direccionHaciaP1 = true;

        // Estado de control
        private bool isPlaying = false;
        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;

        // Constantes para dibujo
        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_CURVA = 3;
        private Pen penCurva = new Pen(Color.Green, 2);
        private Pen penLineaAux = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };
        private Brush brushP0 = new SolidBrush(Color.Blue);
        private Brush brushP1 = new SolidBrush(Color.Red);
        private Brush brushPuntoT = new SolidBrush(Color.Orange);

        private static frmLineal instancia;
        public frmLineal()
        {
            InitializeComponent();

            // Inicializar puntos de control en el panel de dibujo
            P0 = new clsPunto(50, panelDibujo.Height / 2);
            P1 = new clsPunto(panelDibujo.Width - 50, panelDibujo.Height / 2);

            // Inicializar el canvas de dibujo (doble buffer para evitar parpadeos)
            canvas = new Bitmap(panelDibujo.Width, panelDibujo.Height);
            g = Graphics.FromImage(canvas);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            panelDibujo.BackgroundImage = canvas;
            panelDibujo.BackgroundImageLayout = ImageLayout.None;

            // Configurar TrackBar T (0 a 100 para mapear a 0.0 a 1.0)
            trackBarT.Minimum = 0;
            trackBarT.Maximum = 100;
            trackBarT.Value = 0;
            trackBarT.TickFrequency = 10;
            trackBarT.Scroll += TrackBarT_Scroll;

            // Configurar TrackBar Velocidad (1 a 20)
            trackBarVelocidad.Minimum = 1;
            trackBarVelocidad.Maximum = 20;
            trackBarVelocidad.Value = 20; // Velocidad inicial
            trackBarVelocidad.Scroll += TrackBarVelocidad_Scroll;

            // Configurar Timer
            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value; // Velocidad inicial
            timerAnimacion.Tick += TimerAnimacion_Tick;

            // Configurar Eventos del Panel para interactividad
            panelDibujo.Paint += PanelDibujo_Paint;
            panelDibujo.MouseDown += PanelDibujo_MouseDown;
            panelDibujo.MouseMove += PanelDibujo_MouseMove;
            panelDibujo.MouseUp += PanelDibujo_MouseUp;

            // Configurar botones y checkboxes
            btnReset.Click += BtnReset_Click;
            chkMostrarLineas.CheckedChanged += ChkMostrarLineas_CheckedChanged;

            // Establecer el estado inicial de las etiquetas
            ActualizarEtiquetas();

            // Dibujo inicial
            DibujarCurva();
        }

        public static frmLineal ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmLineal();
            }
            return instancia;
        }
        // --- Manejo de Eventos de Interfaz ---

        private void TrackBarT_Scroll(object sender, EventArgs e)
        {
            // Mapear el valor de TrackBar (0-100) a 't' (0.0-1.0)
            t = (float)trackBarT.Value / 100.0f;
            ActualizarEtiquetas();
            DibujarCurva();
        }

        private void TrackBarVelocidad_Scroll(object sender, EventArgs e)
        {
            // Actualizar el intervalo del timer y la etiqueta
            lblVelocidad.Text = $"Velocidad: {trackBarVelocidad.Value}";
            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            // Detener y reiniciar la animación
            timerAnimacion.Stop();
            isPlaying = false;
            btnPlayPause.Text = "Play";

            // Reiniciar t a 0 y la TrackBar, y la dirección
            t = 0.0f;
            trackBarT.Value = 0;
            direccionHaciaP1 = true;

            ActualizarEtiquetas();
            DibujarCurva();
        }

        private void ChkMostrarLineas_CheckedChanged(object sender, EventArgs e)
        {
            // Vuelve a dibujar para aplicar/remover las líneas auxiliares
            DibujarCurva();
        }

        private void button1_Click(object sender, EventArgs e) // Evento de btnPlayPause
        {
            isPlaying = !isPlaying;
            if (isPlaying)
            {
                btnPlayPause.Text = "Pause";
                timerAnimacion.Start();
            }
            else
            {
                btnPlayPause.Text = "Play";
                timerAnimacion.Stop();
            }
        }

        // --- Lógica de Animación ---

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            // El incremento de t depende de la velocidad
            float incremento = 0.01f * (float)trackBarVelocidad.Value / 5.0f;

            if (direccionHaciaP1)
            {
                t += incremento;
                if (t >= 1.0f)
                {
                    t = 1.0f;
                    direccionHaciaP1 = false; // Cambia la dirección: ahora va de P1 a P0
                }
            }
            else // Dirección hacia P0
            {
                t -= incremento;
                if (t <= 0.0f)
                {
                    t = 0.0f;
                    direccionHaciaP1 = true; // Cambia la dirección: ahora va de P0 a P1
                }
            }

            // Mapear t de nuevo a la TrackBar para mantener sincronía
            trackBarT.Value = Math.Max(0, Math.Min(100, (int)(t * 100)));

            ActualizarEtiquetas();
            DibujarCurva();
        }

        // --- Lógica de Dibujo y Curva ---

        private void DibujarCurva()
        {
            // 1. Limpiar el canvas
            g.Clear(panelDibujo.BackColor);

            // 2. Trazar la curva completa (del 0 al 1)
            clsPunto puntoAnterior = P0;
            for (float i = 0.0f; i <= 1.0f; i += 0.01f) // Pequeños pasos para dibujar la curva
            {
                clsPunto puntoCurva = clsCurvaBezier.CalcularPuntoLineal(P0, P1, i);
                g.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoCurva.ToPoint());
                puntoAnterior = puntoCurva;
            }

            // 3. Dibujar las líneas auxiliares (si el checkbox está marcado)
            if (chkMostrarLineas.Checked)
            {
                // Línea que conecta P0 y P1
                g.DrawLine(penLineaAux, P0.ToPoint(), P1.ToPoint());

                // Calcular el punto intermedio Q (que es P(t) en la curva lineal)
                clsPunto puntoIntermedio = clsCurvaBezier.CalcularPuntoLineal(P0, P1, t);

                // Dibujar el segmento interpolado desde P0 hasta P(t) (línea de construcción)
                g.DrawLine(new Pen(Color.OrangeRed, 1), P0.ToPoint(), puntoIntermedio.ToPoint());
            }

            // 4. Calcular y dibujar el punto P(t) (el punto animado)
            clsPunto puntoAnimado = clsCurvaBezier.CalcularPuntoLineal(P0, P1, t);
            g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_CURVA, puntoAnimado.Y - RADIO_PUNTO_CURVA,
                          RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);

            // 5. Dibujar los puntos de control (al final para que queden sobre las líneas)
            DibujarPuntoControl(P0, brushP0);
            DibujarPuntoControl(P1, brushP1);

            // Refrescar el Panel para mostrar el nuevo dibujo
            panelDibujo.Invalidate();
        }

        private void DibujarPuntoControl(clsPunto p, Brush brush)
        {
            // Dibuja un círculo para el punto de control
            g.FillEllipse(brush, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);
            // Dibuja un borde negro
            g.DrawEllipse(Pens.Black, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);
        }

        private void ActualizarEtiquetas()
        {
            // Mostrar los valores de las coordenadas de los puntos de control
            lblP0.Text = $"P0: ({P0.X:F0}, {P0.Y:F0})";
            lblP1.Text = $"P1: ({P1.X:F0}, {P1.Y:F0})";

            // Mostrar el valor actual de 't'
            lblValorT.Text = $"{t:F2}";
        }

        // --- Lógica de Interacción (Arrastre de Puntos) ---

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Verifica si el clic está cerca de P0
                if (Distancia(e.Location, P0.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P0;
                    offsetArrastre = new Point((int)P0.X - e.X, (int)P0.Y - e.Y);
                }
                // Verifica si el clic está cerca de P1
                else if (Distancia(e.Location, P1.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P1;
                    offsetArrastre = new Point((int)P1.X - e.X, (int)P1.Y - e.Y);
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null)
            {
                // Si estamos arrastrando, actualiza la posición del punto
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                // Limitar el arrastre al área del panel
                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                // Redibujar y actualizar etiquetas
                ActualizarEtiquetas();
                DibujarCurva();
            }
        }

        private void PanelDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            // Deja de arrastrar
            puntoArrastrado = null;
        }

        private float Distancia(Point p1, Point p2)
        {
            // Calcula la distancia euclidiana entre dos puntos
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // --- Evento Paint (Usado para el doble buffer) ---

        private void PanelDibujo_Paint(object sender, PaintEventArgs e)
        {
            // Dibuja la imagen del canvas en el panel
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}