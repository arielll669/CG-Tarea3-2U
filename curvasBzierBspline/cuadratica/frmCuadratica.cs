using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace curvasBzierBspline
{
    public partial class frmCuadratica : Form
    {
        // --- Variables de Estado y Puntos de Control ---

        // Puntos de control para la curva cuadrática (P0, P1, P2)
        private clsPunto P0;
        private clsPunto P1;
        private clsPunto P2;

        // Variables de Dibujo (DOBLE BUFFERING: canvas es la imagen final)
        private Graphics g; // Graphics del canvas principal
        private Bitmap canvas;

        // Bitmap y Graphics para la capa estática (curva verde)
        private Bitmap staticCurveBitmap;
        private Graphics staticCurveGraphics;

        // Bitmap y Graphics para la capa de Rastro
        private Bitmap trailBitmap;
        private Graphics trailGraphics;


        // Parámetro de la curva (de 0.0 a 1.0)
        private float t = 0.0f;

        // Animación cíclica (P0 -> P2, P2 -> P0)
        private bool direccionHaciaP2 = true;

        // Control de arrastre
        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;

        // Banderas de fluidez
        private bool isDragging = false;
        private bool needsFullRedraw = false;

        // Constantes y Pinceles para dibujo
        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_CURVA = 3;
        private Pen penCurva = new Pen(Color.Green, 2);
        private Pen penPoligono = new Pen(Color.Gray, 1); // Líneas del polígono de control
        private Pen penConstruccion = new Pen(Color.OrangeRed, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot }; // Líneas De Casteljau

        private Brush brushP0 = new SolidBrush(Color.Red);
        private Brush brushP1 = new SolidBrush(Color.Yellow); // Punto de control intermedio
        private Brush brushP2 = new SolidBrush(Color.Blue);
        private Brush brushPuntoT = new SolidBrush(Color.Magenta); // Punto animado final

        // Rastro ahora es transparente o muy suave para no tapar la curva.
        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));

        private static frmCuadratica instancia;
        private bool isPlaying = false;

        public frmCuadratica()
        {
            InitializeComponent();

            int w = panelDibujo.Width;
            int h = panelDibujo.Height;
            P0 = new clsPunto(w * 0.1f, h * 0.5f);
            P1 = new clsPunto(w * 0.5f, h * 0.1f); // Punto de control
            P2 = new clsPunto(w * 0.9f, h * 0.5f);

            // Activar doble buffer en el panel para eliminar parpadeo
            panelDibujo.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(panelDibujo, true, null);

            // Inicializar el canvas principal
            canvas = new Bitmap(w, h);
            g = Graphics.FromImage(canvas);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            panelDibujo.BackgroundImage = canvas;
            panelDibujo.BackgroundImageLayout = ImageLayout.None;

            // Inicializar el canvas estático (CURVA VERDE)
            staticCurveBitmap = new Bitmap(w, h);
            staticCurveGraphics = Graphics.FromImage(staticCurveBitmap);
            staticCurveGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Inicializar el canvas de RASTRO (capa que acumula los puntos grises)
            trailBitmap = new Bitmap(w, h);
            trailGraphics = Graphics.FromImage(trailBitmap);
            trailGraphics.Clear(Color.Transparent); // Fondo transparente
            trailGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;


            // Inicialización de TrackBars y Timer (se mantiene igual)
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
            chkMostrarConstruccion.CheckedChanged += ChkMostrarConstruccion_CheckedChanged;

            // Configurar el CheckBox para mostrar rastro
            chkDejarRastro.Text = "Mostrar Rastro";
            chkDejarRastro.Checked = false;
            chkDejarRastro.CheckedChanged += ChkMostrarRastro_CheckedChanged;


            // Estado inicial
            DibujarCurvaEstatica(); // Dibuja la curva verde en staticCurveBitmap
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        public static frmCuadratica ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmCuadratica();
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
            direccionHaciaP2 = true;

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
            DibujarCurvaEstatica();
            DibujarElementosDinamicos();
        }

        private void ChkMostrarPoligono_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }

        private void ChkMostrarConstruccion_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }

        private void ChkMostrarRastro_CheckedChanged(object sender, EventArgs e)
        {
            // Si desactivamos el rastro, debemos limpiar la capa del rastro inmediatamente.
            if (!chkDejarRastro.Checked)
            {
                trailGraphics.Clear(Color.Transparent);
            }
            DibujarElementosDinamicos();
        }

        // --- Lógica de Animación (Cíclica) ---

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            float incremento = 0.01f * (float)trackBarVelocidad.Value / 5.0f;

            if (direccionHaciaP2)
            {
                t += incremento;
                if (t >= 1.0f)
                {
                    t = 1.0f;
                    direccionHaciaP2 = false;
                }
            }
            else // Dirección hacia P0
            {
                t -= incremento;
                if (t <= 0.0f)
                {
                    t = 0.0f;
                    direccionHaciaP2 = true;
                }
            }

            trackBarT.Value = Math.Max(0, Math.Min(100, (int)(t * 100)));

            ActualizarEtiquetas();

            // Si hay un redibujado completo pendiente y no estamos arrastrando
            if (needsFullRedraw && !isDragging)
            {
                DibujarCurvaEstatica();
                needsFullRedraw = false;
            }

            DibujarElementosDinamicos();
        }

        // --- MÉTODOS DE DIBUJO OPTIMIZADOS ---

        /// <summary>
        /// Dibuja el fondo y la curva verde completa en la capa estática (staticCurveBitmap).
        /// </summary>
        private void DibujarCurvaEstatica(float paso = 0.01f)
        {
            // 1. Limpiar el canvas estático (solo la curva verde)
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            // 2. Trazar la curva completa
            clsPunto puntoAnterior = P0;
            for (float i = 0.0f; i <= 1.0f; i += paso)
            {
                clsPunto puntoCurva = clsCurvaBezier.CalcularPuntoCuadratico(P0, P1, P2, i);
                staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoCurva.ToPoint());
                puntoAnterior = puntoCurva;
            }
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
            clsPunto Q0 = clsCurvaBezier.CalcularPuntoLineal(P0, P1, t);
            clsPunto Q1 = clsCurvaBezier.CalcularPuntoLineal(P1, P2, t);
            clsPunto puntoAnimado = clsCurvaBezier.CalcularPuntoLineal(Q0, Q1, t);

            // 4. Dibujar el polígono de control (si está activo)
            if (chkMostrarPoligono.Checked || isDragging)
            {
                g.DrawLine(penPoligono, P0.ToPoint(), P1.ToPoint());
                g.DrawLine(penPoligono, P1.ToPoint(), P2.ToPoint());
            }

            // 5. Dibujar las líneas de construcción (De Casteljau)
            if (chkMostrarConstruccion.Checked)
            {
                g.DrawLine(penConstruccion, Q0.ToPoint(), Q1.ToPoint());

                g.FillEllipse(Brushes.Gray, Q0.X - RADIO_PUNTO_CURVA, Q0.Y - RADIO_PUNTO_CURVA, RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);
                g.FillEllipse(Brushes.Gray, Q1.X - RADIO_PUNTO_CURVA, Q1.Y - RADIO_PUNTO_CURVA, RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);
            }

            // 6. Dibujar el rastro (solo si el checkbox está activo y estamos en animación)
            if (chkDejarRastro.Checked && isPlaying && !isDragging)
            {
                // Dibuja el punto en la capa de RASTRO para que se acumule.
                trailGraphics.FillEllipse(brushRastro, puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
            }

            // 7. Dibujar el punto animado P(t) (siempre visible)
            g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_CURVA, puntoAnimado.Y - RADIO_PUNTO_CURVA,
                          RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);

            // 8. Dibujar los puntos de control (al final)
            DibujarPuntoControl(P0, brushP0, "P0");
            DibujarPuntoControl(P1, brushP1, "P1");
            DibujarPuntoControl(P2, brushP2, "P2");

            // Usar Refresh para actualización inmediata
            panelDibujo.Refresh();
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

                // Redibujo final de alta resolución
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

        private void frmCuadratica_Load(object sender, EventArgs e)
        {

        }
    }
}