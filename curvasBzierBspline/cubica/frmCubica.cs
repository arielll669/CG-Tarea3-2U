using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace curvasBzierBspline
{
    public partial class frmCubica : Form
    {
        // --- Variables de Estado y Puntos de Control ---

        // Puntos de control para la curva cúbica (P0, P1, P2, P3)
        private clsPunto P0;
        private clsPunto P1;
        private clsPunto P2;
        private clsPunto P3;

        // Variables de Dibujo (Optimización por capas)
        private Graphics g; // Graphics del canvas principal
        private Bitmap canvas;
        private Bitmap staticCurveBitmap; // Curva verde estática
        private Graphics staticCurveGraphics;
        private Bitmap trailBitmap; // Rastro (marcas acumuladas)
        private Graphics trailGraphics;


        // Parámetro de la curva (de 0.0 a 1.0)
        private float t = 0.0f;

        // Animación cíclica (P0 -> P3, P3 -> P0)
        private bool direccionHaciaP3 = true;

        // Control de arrastre
        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;

        // Banderas de fluidez
        private bool isDragging = false;
        private bool needsFullRedraw = false;

        // Constantes y Pinceles para dibujo
        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_INTERMEDIO = 3;
        private Pen penCurva = new Pen(Color.Green, 2);
        private Pen penPoligono = new Pen(Color.Gray, 1);
        private Pen penConstruccion1 = new Pen(Color.OrangeRed, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot }; // Nivel 1 (Q)
        private Pen penConstruccion2 = new Pen(Color.Purple, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };    // Nivel 2 (R)
        private Pen penConstruccion3 = new Pen(Color.Blue, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };       // Nivel 3 (S)

        private Brush brushP0 = new SolidBrush(Color.Red);
        private Brush brushP1 = new SolidBrush(Color.Orange);
        private Brush brushP2 = new SolidBrush(Color.Purple);
        private Brush brushP3 = new SolidBrush(Color.Blue);
        private Brush brushPuntoT = new SolidBrush(Color.Magenta); // Punto animado final

        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));

        private static frmCubica instancia;
        private bool isPlaying = false;

        public frmCubica()
        {
            InitializeComponent();

            int w = panelDibujo.Width;
            int h = panelDibujo.Height;

            // Inicialización de los 4 puntos de control
            P0 = new clsPunto(w * 0.1f, h * 0.5f);
            P1 = new clsPunto(w * 0.3f, h * 0.1f);
            P2 = new clsPunto(w * 0.7f, h * 0.9f);
            P3 = new clsPunto(w * 0.9f, h * 0.5f);

            // Activar doble buffer en el panel para eliminar parpadeo
            panelDibujo.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(panelDibujo, true, null);

            // Inicialización de Bitmaps y Graphics
            canvas = new Bitmap(w, h);
            g = Graphics.FromImage(canvas);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            panelDibujo.BackgroundImage = canvas;
            panelDibujo.BackgroundImageLayout = ImageLayout.None;

            staticCurveBitmap = new Bitmap(w, h);
            staticCurveGraphics = Graphics.FromImage(staticCurveBitmap);
            staticCurveGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            trailBitmap = new Bitmap(w, h);
            trailGraphics = Graphics.FromImage(trailBitmap);
            trailGraphics.Clear(Color.Transparent);
            trailGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            // Configuración de controles
            trackBarT.Minimum = 0;
            trackBarT.Maximum = 100;
            trackBarT.Value = 0;
            trackBarT.Scroll += TrackBarT_Scroll;

            trackBarVelocidad.Minimum = 1;
            trackBarVelocidad.Maximum = 20;
            trackBarVelocidad.Value = 20;
            trackBarVelocidad.Scroll += TrackBarVelocidad_Scroll;

            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
            timerAnimacion.Tick += TimerAnimacion_Tick;

            // Configurar Eventos del Panel para interactividad
            panelDibujo.Paint += PanelDibujo_Paint;
            panelDibujo.MouseDown += PanelDibujo_MouseDown;
            panelDibujo.MouseMove += PanelDibujo_MouseMove;
            panelDibujo.MouseUp += PanelDibujo_MouseUp;

            // Configurar botones y checkboxes
            btnPlayPause.Click += BtnPlayPause_Click;
            btnReset.Click += BtnReset_Click;
            btnLimpiarRastro.Click += BtnLimpiarRastro_Click;
            chkMostrarPoligono.CheckedChanged += ChkMostrarPoligono_CheckedChanged;
            chkMostrarNivel1.CheckedChanged += ChkMostrarNivel_CheckedChanged;
            chkMostrarNivel2.CheckedChanged += ChkMostrarNivel_CheckedChanged;
            chkMostrarNivel3.CheckedChanged += ChkMostrarNivel_CheckedChanged;


            // Estado inicial
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        public static frmCubica ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmCubica();
            }
            return instancia;
        }

        // --- Manejo de Eventos de Interfaz ---

        private void TrackBarT_Scroll(object sender, EventArgs e)
        {
            t = (float)trackBarT.Value / 100.0f;
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void TrackBarVelocidad_Scroll(object sender, EventArgs e)
        {
            lblVelocidad.Text = $"Velocidad: {trackBarVelocidad.Value}";
            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                isPlaying = true;
                btnPlayPause.Text = "Pause";
                timerAnimacion.Start();
            }
            else
            {
                isPlaying = false;
                btnPlayPause.Text = "Play";
                timerAnimacion.Stop();
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            isPlaying = false;
            btnPlayPause.Text = "Play";
            timerAnimacion.Stop();

            t = 0.0f;
            trackBarT.Value = 0;
            direccionHaciaP3 = true;

            // Limpia el rastro
            trailGraphics.Clear(Color.Transparent);

            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void BtnLimpiarRastro_Click(object sender, EventArgs e)
        {
            // Simplemente limpia la capa de rastro
            trailGraphics.Clear(Color.Transparent);
            DibujarElementosDinamicos();
        }

        private void ChkMostrarPoligono_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }

        private void ChkMostrarNivel_CheckedChanged(object sender, EventArgs e)
        {
            // Se llama cuando cualquier checkbox de nivel cambia
            DibujarElementosDinamicos();
        }


        // --- Lógica de Animación (Cíclica) ---

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            // 1. Manejo del Ciclo de Animación (t)
            float incremento = 0.01f * (float)trackBarVelocidad.Value / 5.0f;

            if (direccionHaciaP3)
            {
                t += incremento;
                if (t >= 1.0f)
                {
                    t = 1.0f;
                    direccionHaciaP3 = false;
                }
            }
            else // Dirección hacia P0
            {
                t -= incremento;
                if (t <= 0.0f)
                {
                    t = 0.0f;
                    direccionHaciaP3 = true;
                }
            }

            trackBarT.Value = Math.Max(0, Math.Min(100, (int)(t * 100)));

            ActualizarEtiquetas();

            // 2. Si hay un redibujado completo pendiente y no estamos arrastrando
            if (needsFullRedraw && !isDragging)
            {
                DibujarCurvaEstatica();
                needsFullRedraw = false;
            }

            DibujarElementosDinamicos();
        }

        // --- MÉTODOS DE DIBUJO OPTIMIZADOS ---

        /// <summary>
        /// Dibuja el fondo y la curva verde completa en la capa estática.
        /// Acepta un parámetro 'paso' para controlar la resolución (optimización de rendimiento).
        /// </summary>
        private void DibujarCurvaEstatica(float paso = 0.01f)
        {
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            clsPunto puntoAnterior = P0;
            for (float i = 0.0f; i <= 1.0f; i += paso)
            {
                clsPunto puntoCurva = clsCurvaBezier.CalcularPuntoCubico(P0, P1, P2, P3, i);
                staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoCurva.ToPoint());
                puntoAnterior = puntoCurva;
            }

            // Asegurar que el último punto P3 se dibuje
            clsPunto puntoFinal = clsCurvaBezier.CalcularPuntoCubico(P0, P1, P2, P3, 1.0f);
            staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoFinal.ToPoint());
        }

        /// <summary>
        /// Dibuja los elementos que cambian en cada fotograma: polígono, De Casteljau, P(t) y puntos de control.
        /// </summary>
        private void DibujarElementosDinamicos()
        {
            // 1. Limpiar el canvas principal
            g.Clear(panelDibujo.BackColor);

            // 2. Dibujar la base estática (la curva verde)
            g.DrawImage(staticCurveBitmap, 0, 0);

            // 3. Dibujar la capa de Rastro acumulado (si existe).
            g.DrawImage(trailBitmap, 0, 0);

            // --- Lógica de Animación / De Casteljau (Cálculo) ---

            // Nivel 1 (Interpolación Lineal: Px -> Px+1)
            clsPunto Q0 = clsCurvaBezier.CalcularPuntoLineal(P0, P1, t);
            clsPunto Q1 = clsCurvaBezier.CalcularPuntoLineal(P1, P2, t);
            clsPunto Q2 = clsCurvaBezier.CalcularPuntoLineal(P2, P3, t);

            // Nivel 2 (Interpolación Lineal: Qx -> Qx+1)
            clsPunto R0 = clsCurvaBezier.CalcularPuntoLineal(Q0, Q1, t);
            clsPunto R1 = clsCurvaBezier.CalcularPuntoLineal(Q1, Q2, t);

            // Nivel 3 (Interpolación Lineal: Rx -> Rx+1)
            clsPunto puntoAnimado = clsCurvaBezier.CalcularPuntoLineal(R0, R1, t); // S0 = P(t)

            // 4. Dibujar el polígono de control (P0-P3)
            if (chkMostrarPoligono.Checked || isDragging) // Dibujar Polígono siempre al arrastrar para feedback
            {
                g.DrawLine(penPoligono, P0.ToPoint(), P1.ToPoint());
                g.DrawLine(penPoligono, P1.ToPoint(), P2.ToPoint());
                g.DrawLine(penPoligono, P2.ToPoint(), P3.ToPoint());
            }

            // 5. Dibujar las líneas de Construcción De Casteljau

            // Nivel 1 (Líneas Q0-Q1, Q1-Q2)
            if (chkMostrarNivel1.Checked)
            {
                g.DrawLine(penConstruccion1, Q0.ToPoint(), Q1.ToPoint());
                g.DrawLine(penConstruccion1, Q1.ToPoint(), Q2.ToPoint());
                DibujarPuntoIntermedio(Q0, Brushes.Orange, g);
                DibujarPuntoIntermedio(Q1, Brushes.Orange, g);
                DibujarPuntoIntermedio(Q2, Brushes.Orange, g);
            }

            // Nivel 2 (Línea R0-R1)
            if (chkMostrarNivel2.Checked)
            {
                g.DrawLine(penConstruccion2, R0.ToPoint(), R1.ToPoint());
                DibujarPuntoIntermedio(R0, Brushes.Purple, g);
                DibujarPuntoIntermedio(R1, Brushes.Purple, g);
            }

            // Nivel 3 (Punto final S0 = P(t))
            if (chkMostrarNivel3.Checked)
            {
                DibujarPuntoIntermedio(puntoAnimado, Brushes.Blue, g);
            }


            // 6. Dibujar el rastro (si está activo y en modo animación)
            if (chkMostrarNivel3.Checked && isPlaying && !isDragging)
            {
                trailGraphics.FillEllipse(brushRastro, puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
            }

            // 7. Dibujar el punto animado P(t) (siempre visible, color magenta)
            g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_INTERMEDIO, puntoAnimado.Y - RADIO_PUNTO_INTERMEDIO,
             RADIO_PUNTO_INTERMEDIO * 2, RADIO_PUNTO_INTERMEDIO * 2);

            // 8. Dibujar los puntos de control (al final)
            DibujarPuntoControl(P0, brushP0, "P0");
            DibujarPuntoControl(P1, brushP1, "P1");
            DibujarPuntoControl(P2, brushP2, "P2");
            DibujarPuntoControl(P3, brushP3, "P3");

            // Usar Refresh para actualización inmediata
            panelDibujo.Refresh();
        }

        private void DibujarPuntoIntermedio(clsPunto p, Brush brush, Graphics gr)
        {
            gr.FillEllipse(brush, p.X - RADIO_PUNTO_INTERMEDIO, p.Y - RADIO_PUNTO_INTERMEDIO,
                  RADIO_PUNTO_INTERMEDIO * 2, RADIO_PUNTO_INTERMEDIO * 2);
        }

        private void DibujarPuntoControl(clsPunto p, Brush brush, string etiqueta)
        {
            g.FillEllipse(brush, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                   RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);
            g.DrawEllipse(Pens.Black, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                   RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);

            g.DrawString(etiqueta, this.Font, Brushes.Black, p.X + RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL);
        }

        private void ActualizarEtiquetas()
        {
            lblP0.Text = $"({P0.X:F0}, {P0.Y:F0})";
            lblP1.Text = $"({P1.X:F0}, {P1.Y:F0})";
            lblP2.Text = $"({P2.X:F0}, {P2.Y:F0})";
            lblP3.Text = $"({P3.X:F0}, {P3.Y:F0})";
            lblValorT.Text = $"{t:F2}";
        }

        // --- Lógica de Interacción (Arrastre de Puntos) ---

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Distancia(e.Location, P0.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P0;
                }
                else if (Distancia(e.Location, P1.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P1;
                }
                else if (Distancia(e.Location, P2.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P2;
                }
                else if (Distancia(e.Location, P3.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                {
                    puntoArrastrado = P3;
                }

                if (puntoArrastrado != null)
                {
                    offsetArrastre = new Point((int)puntoArrastrado.X - e.X, (int)puntoArrastrado.Y - e.Y);
                    isDragging = true;
                    // Limpiar rastro al iniciar arrastre
                    trailGraphics.Clear(Color.Transparent);
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null)
            {
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                ActualizarEtiquetas();

                // Solo redibujar la curva con resolución baja si NO está en Play
                if (!isPlaying)
                {
                    DibujarCurvaEstatica(0.05f);
                    DibujarElementosDinamicos();
                }
                else
                {
                    // Marcar que necesitamos redibujado completo, pero no bloquear
                    DibujarCurvaEstatica(0.05f);
                    needsFullRedraw = true;
                }
            }
        }

        private void PanelDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null)
            {
                puntoArrastrado = null;
                isDragging = false;

                // Redibujo final de ALTA RESOLUCIÓN
                DibujarCurvaEstatica();
                needsFullRedraw = false;
                DibujarElementosDinamicos();
            }
        }

        private float Distancia(Point p1, Point p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // --- Evento Paint (Usado para el doble buffer) ---

        private void PanelDibujo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}