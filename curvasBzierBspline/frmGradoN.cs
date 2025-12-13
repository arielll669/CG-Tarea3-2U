using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace curvasBzierBspline
{
    public partial class frmGradoN : Form
    {
        // --- Variables de Estado y Puntos de Control ---

        // Lista dinámica de puntos de control (P0, P1, ..., Pn)
        private List<clsPunto> controlPoints = new List<clsPunto>();

        // Variables de Dibujo (Optimización por capas)
        private Graphics g;
        private Bitmap canvas;
        private Bitmap staticCurveBitmap; // Curva estática (verde)
        private Graphics staticCurveGraphics;
        private Bitmap trailBitmap; // Rastro (marcas acumuladas)
        private Graphics trailGraphics;


        // Puntos intermedios del algoritmo De Casteljau
        private List<List<clsPunto>> casteljauLevels = new List<List<clsPunto>>();

        // Parámetro de la curva (de 0.0 a 1.0)
        private float t = 0.0f;

        // Animación cíclica (P0 <-> Pn)
        private bool direccionHaciaPn = true;

        // Control de arrastre
        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;

        // Banderas de Fluidez
        private bool isDragging = false;
        private bool isPlaying = false;
        private bool needsFullRedraw = false;
        private static frmGradoN instancia;

        // Constantes y Pinceles para dibujo
        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_INTERMEDIO = 3;
        private Pen penCurva = new Pen(Color.Green, 2);
        private Pen penPoligono = new Pen(Color.Gray, 1);
        private Brush brushPuntoT = new SolidBrush(Color.Magenta);
        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));

        // Colores para los niveles de De Casteljau (hasta grado 6)
        private Brush[] intermediateBrushes = { Brushes.Orange, Brushes.Purple, Brushes.Teal, Brushes.Brown, Brushes.Crimson, Brushes.DarkGreen };
        private Pen[] intermediatePens = {
            new Pen(Color.OrangeRed, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
            new Pen(Color.Purple, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
            new Pen(Color.Teal, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
            new Pen(Color.Brown, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
            new Pen(Color.Crimson, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot },
            new Pen(Color.DarkGreen, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot }
        };


        public frmGradoN()
        {
            InitializeComponent();

            int w = panelDibujo.Width;
            int h = panelDibujo.Height;

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

            // Configuración inicial de controles
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
            btnLimpiar.Click += BtnLimpiar_Click;
            chkMostrarPoligono.CheckedChanged += ChkMostrarPoligono_CheckedChanged;
            chkNumerarPuntos.CheckedChanged += ChkNumerarPuntos_CheckedChanged;

            // Inicializar el estado de la curva estática (vacía)
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        public static frmGradoN ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmGradoN();
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
            if (controlPoints.Count < 2) return;

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

        private void BtnReset_Click(object sender, EventArgs e)
        {
            timerAnimacion.Stop();
            isPlaying = false;
            btnPlayPause.Text = "Play";

            t = 0.0f;
            trackBarT.Value = 0;
            direccionHaciaPn = true;

            trailGraphics.Clear(Color.Transparent);

            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            timerAnimacion.Stop();
            isPlaying = false;
            btnPlayPause.Text = "Play";
            controlPoints.Clear();
            t = 0.0f;

            trailGraphics.Clear(Color.Transparent);
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void ChkMostrarPoligono_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }

        private void ChkNumerarPuntos_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }


        // --- Lógica de Animación (Cíclica) ---

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (controlPoints.Count < 2) return;

            float incremento = 0.01f * (float)trackBarVelocidad.Value / 5.0f;

            if (direccionHaciaPn)
            {
                t += incremento;
                if (t >= 1.0f)
                {
                    t = 1.0f;
                    direccionHaciaPn = false;
                }
            }
            else
            {
                t -= incremento;
                if (t <= 0.0f)
                {
                    t = 0.0f;
                    direccionHaciaPn = true;
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

        // --- MÉTODOS DE DIBUJO Y ALGORITMO ---

        /// <summary>
        /// Dibuja el fondo y la curva verde completa en la capa estática.
        /// </summary>
        private void DibujarCurvaEstatica(float paso = 0.01f)
        {
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            if (controlPoints.Count < 2) return;

            clsPunto puntoAnterior = controlPoints[0];
            List<List<clsPunto>> dummyLevels;

            for (float i = 0.0f; i <= 1.0f; i += paso)
            {
                clsPunto puntoCurva = clsCurvaBezier.CalcularPuntoDeCasteljau(controlPoints, i, out dummyLevels);
                staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoCurva.ToPoint());
                puntoAnterior = puntoCurva;
            }

            clsPunto puntoFinal = clsCurvaBezier.CalcularPuntoDeCasteljau(controlPoints, 1.0f, out dummyLevels);
            staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoFinal.ToPoint());
        }

        /// <summary>
        /// Dibuja los elementos que cambian en cada fotograma.
        /// </summary>
        private void DibujarElementosDinamicos()
        {
            g.Clear(panelDibujo.BackColor);
            g.DrawImage(staticCurveBitmap, 0, 0);
            g.DrawImage(trailBitmap, 0, 0);

            if (controlPoints.Count < 2)
            {
                for (int i = 0; i < controlPoints.Count; i++)
                {
                    DibujarPuntoControl(controlPoints[i], Brushes.Gray, i);
                }
                panelDibujo.Refresh(); // Usar Refresh en lugar de Invalidate para actualización inmediata
                return;
            }

            List<List<clsPunto>> currentLevels;
            clsPunto puntoAnimado = clsCurvaBezier.CalcularPuntoDeCasteljau(controlPoints, t, out currentLevels);
            casteljauLevels = currentLevels;

            if (chkMostrarPoligono.Checked || isDragging)
            {
                if (controlPoints.Count >= 2)
                {
                    g.DrawLines(penPoligono, controlPoints.Select(p => p.ToPoint()).ToArray());
                }
            }

            for (int level = 1; level < casteljauLevels.Count; level++)
            {
                List<clsPunto> levelPoints = casteljauLevels[level];
                if (levelPoints.Count > 0)
                {
                    Pen levelPen = intermediatePens[Math.Min(level - 1, intermediatePens.Length - 1)];
                    Brush levelBrush = intermediateBrushes[Math.Min(level - 1, intermediateBrushes.Length - 1)];

                    if (levelPoints.Count > 1)
                    {
                        g.DrawLines(levelPen, levelPoints.Select(p => p.ToPoint()).ToArray());
                    }

                    for (int i = 0; i < levelPoints.Count; i++)
                    {
                        if (level < controlPoints.Count - 1 || levelPoints.Count > 1)
                        {
                            DibujarPuntoIntermedio(levelPoints[i], levelBrush, g);
                        }
                    }
                }
            }

            if (chkMostrarPoligono.Checked && isPlaying && !isDragging)
            {
                trailGraphics.FillEllipse(brushRastro, puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
            }

            g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_INTERMEDIO, puntoAnimado.Y - RADIO_PUNTO_INTERMEDIO,
             RADIO_PUNTO_INTERMEDIO * 2, RADIO_PUNTO_INTERMEDIO * 2);

            for (int i = 0; i < controlPoints.Count; i++)
            {
                DibujarPuntoControl(controlPoints[i], Brushes.Black, i);
            }

            // Usar Refresh para actualización inmediata sin esperar al mensaje de Windows
            panelDibujo.Refresh();
        }

        private void DibujarPuntoIntermedio(clsPunto p, Brush brush, Graphics gr)
        {
            gr.FillEllipse(brush, p.X - RADIO_PUNTO_INTERMEDIO, p.Y - RADIO_PUNTO_INTERMEDIO,
                         RADIO_PUNTO_INTERMEDIO * 2, RADIO_PUNTO_INTERMEDIO * 2);
        }

        private void DibujarPuntoControl(clsPunto p, Brush brush, int index)
        {
            Brush currentBrush = brush;
            if (index == 0) currentBrush = Brushes.Blue;
            else if (index == controlPoints.Count - 1 && controlPoints.Count >= 2) currentBrush = Brushes.Red;
            else currentBrush = Brushes.Black;

            g.FillEllipse(currentBrush, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);
            g.DrawEllipse(Pens.Black, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);

            if (chkNumerarPuntos.Checked)
            {
                g.DrawString($"P{index}", this.Font, Brushes.Black, p.X + RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL);
            }
        }

        private void ActualizarEtiquetas()
        {
            int grado = controlPoints.Count > 0 ? controlPoints.Count - 1 : 0;
            lblCantidadPuntos.Text = $"Puntos: {controlPoints.Count} (Grado: {grado})";
            lblValorT.Text = $"t = {t:F2}";
        }

        // --- Lógica de Interacción (Agregar/Arrastrar Puntos) ---

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 1. Intentar arrastrar un punto existente (permitido durante animación)
                foreach (clsPunto p in controlPoints)
                {
                    if (Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                    {
                        puntoArrastrado = p;
                        offsetArrastre = new Point((int)puntoArrastrado.X - e.X, (int)puntoArrastrado.Y - e.Y);
                        isDragging = true;
                        // Limpiar rastro al iniciar arrastre para evitar confusión visual
                        trailGraphics.Clear(Color.Transparent);
                        return;
                    }
                }

                // 2. Si no se está arrastrando y NO está en Play, agregar un nuevo punto
                if (!isPlaying)
                {
                    clsPunto nuevoPunto = new clsPunto(e.X, e.Y);
                    controlPoints.Add(nuevoPunto);

                    if (controlPoints.Count >= 2)
                    {
                        DibujarCurvaEstatica();
                    }

                    ActualizarEtiquetas();
                    DibujarElementosDinamicos();
                }
            }
            else if (e.Button == MouseButtons.Right && !isPlaying)
            {
                // Eliminar punto solo cuando NO está en Play
                clsPunto puntoAEliminar = null;
                foreach (clsPunto p in controlPoints)
                {
                    if (Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                    {
                        puntoAEliminar = p;
                        break;
                    }
                }

                if (puntoAEliminar != null)
                {
                    controlPoints.Remove(puntoAEliminar);
                    DibujarCurvaEstatica();
                    ActualizarEtiquetas();
                    DibujarElementosDinamicos();
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null)
            {
                // Actualizar posición del punto
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                // Limitar al panel
                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                // Solo redibujar la curva con resolución baja si NO está en Play
                // Si está en Play, el Timer se encargará del redibujado
                if (!isPlaying)
                {
                    DibujarCurvaEstatica(0.05f);
                    ActualizarEtiquetas();
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
    }
}