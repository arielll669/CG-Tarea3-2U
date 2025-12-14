using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace curvasBzierBspline
{
    public partial class frmBspline : Form
    {
        private static frmBspline instancia;
        private List<clsPunto> controlPoints = new List<clsPunto>();

        private int gradoK = 2;
        private KnotType tipoNodos = KnotType.AbiertoUniforme;
        private float t = 0.0f;

        // Animación
        private bool direccionHaciaFinal = true;
        private bool isPlaying = false;

        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;
        private bool isDragging = false;
        private bool needsFullRedraw = false;

        private bool isInitializing = true;

        private Graphics g;
        private Bitmap canvas;
        private Bitmap staticCurveBitmap;
        private Graphics staticCurveGraphics;
        private Bitmap trailBitmap;
        private Graphics trailGraphics;

        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_CURVA = 3;
        private Pen penCurva = new Pen(Color.Red, 2);
        private Pen penPoligono = new Pen(Color.Gray, 1);
        private Brush brushPuntoT = new SolidBrush(Color.LimeGreen);
        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));
        private Pen penKnots = new Pen(Color.Teal, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };

        public frmBspline()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        public static frmBspline ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmBspline();
            }
            return instancia;
        }

        private void InicializarFormulario()
        {
            isInitializing = true; 

            int w = panelDibujo.Width;
            int h = panelDibujo.Height;

            panelDibujo.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(panelDibujo, true, null);

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

            cboGrado.Items.Clear();
            cboGrado.Items.Add("B-spline Lineal (1)");
            cboGrado.Items.Add("B-spline Cuadrática (2)");
            cboGrado.Items.Add("B-spline Grado N");
            cboGrado.SelectedIndex = 1; 
            cboGrado.SelectedIndexChanged += CboGrado_SelectedIndexChanged;

            trackBarT.Minimum = 0;
            trackBarT.Maximum = 100;
            trackBarT.Value = 0;
            trackBarT.Scroll += TrackBarT_Scroll;

            trackBarVelocidad.Minimum = 1;
            trackBarVelocidad.Maximum = 20;
            trackBarVelocidad.Value = 17;
            trackBarVelocidad.Scroll += TrackBarVelocidad_Scroll;

            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
            timerAnimacion.Tick += TimerAnimacion_Tick;

            // Configurar RadioButtons de tipo de nodos
            rbAbiertoUniforme.CheckedChanged += RbKnotType_CheckedChanged;
            rbUniforme.CheckedChanged += RbKnotType_CheckedChanged;
            rbPeriodica.CheckedChanged += RbKnotType_CheckedChanged;

            // Configurar botones
            btnPlayPause.Click += BtnPlayPause_Click;
            btnReset.Click += BtnReset_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnAplicar.Click += BtnAplicar_Click;

            // Configurar checkboxes
            chkMostrarPoligono.CheckedChanged += ChkMostrarPoligono_CheckedChanged;
            chkNumerarPuntos.CheckedChanged += ChkNumerarPuntos_CheckedChanged;
            chkMostrarKnots.CheckedChanged += ChkMostrarKnots_CheckedChanged;

            // Configurar eventos del panel
            panelDibujo.Paint += PanelDibujo_Paint;
            panelDibujo.MouseDown += PanelDibujo_MouseDown;
            panelDibujo.MouseMove += PanelDibujo_MouseMove;
            panelDibujo.MouseUp += PanelDibujo_MouseUp;

            // Dibujo inicial
            ActualizarEtiquetas();
            DibujarCurvaEstatica();
            DibujarElementosDinamicos();

            isInitializing = false; // Permitir eventos
        }

        // === EVENTOS DE CONTROLES ===

        private void CboGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isInitializing) return; // Ignorar durante inicialización

            try
            {
                // Mapear selección a grado
                switch (cboGrado.SelectedIndex)
                {
                    case 0: gradoK = 1; break; // Lineal
                    case 1: gradoK = 2; break; // Cuadrática
                    case 2: // Grado N (usa n-1 si hay suficientes puntos)
                        gradoK = Math.Max(3, Math.Min(controlPoints.Count - 1, 6));
                        break;
                    default: gradoK = 2; break;
                }

                ActualizarEstadoBotones();

                if (controlPoints.Count >= gradoK + 1)
                {
                    DibujarCurvaEstatica();
                }
                else
                {
                    staticCurveGraphics.Clear(panelDibujo.BackColor);
                }

                DibujarElementosDinamicos();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CboGrado: {ex.Message}");
            }
        }

        private void RbKnotType_CheckedChanged(object sender, EventArgs e)
        {
            if (isInitializing) return;

            try
            {
                if (rbAbiertoUniforme.Checked)
                {
                    tipoNodos = KnotType.AbiertoUniforme;
                    lblDescripcionTipo.Text = "La curva toca los puntos extremos";
                }
                else if (rbUniforme.Checked)
                {
                    tipoNodos = KnotType.Uniforme;
                    lblDescripcionTipo.Text = "La curva NO toca los extremos";
                }
                else if (rbPeriodica.Checked)
                {
                    tipoNodos = KnotType.Periodica;
                    lblDescripcionTipo.Text = "Curva cerrada (conecta inicio y fin)";
                }

                if (controlPoints.Count >= gradoK + 1)
                {
                    DibujarCurvaEstatica();
                    DibujarElementosDinamicos();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en RbKnotType: {ex.Message}");
            }
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                if (controlPoints.Count >= gradoK + 1)
                {
                    DibujarCurvaEstatica();
                }
                DibujarElementosDinamicos();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en BtnAplicar: {ex.Message}");
            }
        }

        private void TrackBarT_Scroll(object sender, EventArgs e)
        {
            t = trackBarT.Value / 100.0f;
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void TrackBarVelocidad_Scroll(object sender, EventArgs e)
        {
            lblVelocidad.Text = $"Velocidad: {trackBarVelocidad.Value}";
            timerAnimacion.Interval = Math.Max(1, 1000 / trackBarVelocidad.Value);
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (controlPoints.Count < gradoK + 1) return;

            isPlaying = !isPlaying;
            btnPlayPause.Text = isPlaying ? "Pause" : "Play";

            if (isPlaying)
                timerAnimacion.Start();
            else
                timerAnimacion.Stop();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            timerAnimacion.Stop();
            isPlaying = false;
            btnPlayPause.Text = "Play";

            t = 0.0f;
            trackBarT.Value = 0;
            direccionHaciaFinal = true;

            trailGraphics.Clear(Color.Transparent);

            if (controlPoints.Count >= gradoK + 1)
            {
                DibujarCurvaEstatica();
            }

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
            trackBarT.Value = 0;

            trailGraphics.Clear(Color.Transparent);
            staticCurveGraphics.Clear(panelDibujo.BackColor);

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

        private void ChkMostrarKnots_CheckedChanged(object sender, EventArgs e)
        {
            DibujarElementosDinamicos();
        }

        // === ANIMACIÓN ===

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (controlPoints.Count < gradoK + 1) return;

            float incremento = 0.01f * trackBarVelocidad.Value / 5.0f;

            if (direccionHaciaFinal)
            {
                t += incremento;
                if (t >= 1.0f)
                {
                    t = 1.0f;
                    direccionHaciaFinal = false;
                }
            }
            else
            {
                t -= incremento;
                if (t <= 0.0f)
                {
                    t = 0.0f;
                    direccionHaciaFinal = true;
                }
            }

            trackBarT.Value = Math.Max(0, Math.Min(100, (int)(t * 100)));
            ActualizarEtiquetas();

            if (needsFullRedraw && !isDragging)
            {
                DibujarCurvaEstatica();
                needsFullRedraw = false;
            }

            DibujarElementosDinamicos();
        }

        // === MÉTODOS DE DIBUJO ===

        private void DibujarCurvaEstatica(int numPuntos = 200)
        {
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            if (controlPoints.Count < gradoK + 1) return;

            try
            {
                List<clsPunto> puntosCurva = clsBspline.CalcularCurva(controlPoints, gradoK, tipoNodos, numPuntos);

                if (puntosCurva.Count > 1)
                {
                    Point[] puntos = puntosCurva.Select(p => p.ToPoint()).ToArray();
                    staticCurveGraphics.DrawLines(penCurva, puntos);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al dibujar curva: {ex.Message}");
            }
        }

        private void DibujarElementosDinamicos()
        {
            g.Clear(panelDibujo.BackColor);
            g.DrawImage(staticCurveBitmap, 0, 0);
            g.DrawImage(trailBitmap, 0, 0);

            if (controlPoints.Count < 1)
            {
                panelDibujo.Refresh();
                return;
            }

            // Dibujar punto animado si hay suficientes puntos
            if (controlPoints.Count >= gradoK + 1)
            {
                try
                {
                    clsPunto puntoAnimado = clsBspline.CalcularPuntoCurva(controlPoints, gradoK, t, tipoNodos);

                    g.FillEllipse(brushPuntoT,
                        puntoAnimado.X - RADIO_PUNTO_CURVA,
                        puntoAnimado.Y - RADIO_PUNTO_CURVA,
                        RADIO_PUNTO_CURVA * 2,
                        RADIO_PUNTO_CURVA * 2);

                    g.DrawEllipse(Pens.DarkGreen,
                        puntoAnimado.X - RADIO_PUNTO_CURVA,
                        puntoAnimado.Y - RADIO_PUNTO_CURVA,
                        RADIO_PUNTO_CURVA * 2,
                        RADIO_PUNTO_CURVA * 2);

                    if (isPlaying && !isDragging)
                    {
                        trailGraphics.FillEllipse(brushRastro,
                            puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al calcular punto: {ex.Message}");
                }
            }

            // Dibujar polígono de control
            if ((chkMostrarPoligono.Checked || isDragging) && controlPoints.Count >= 2)
            {
                g.DrawLines(penPoligono, controlPoints.Select(p => p.ToPoint()).ToArray());
            }

            // Dibujar vector de nodos
            if (chkMostrarKnots.Checked && controlPoints.Count >= gradoK + 1)
            {
                DibujarVectorNodos();
            }

            // Dibujar puntos de control
            for (int i = 0; i < controlPoints.Count; i++)
            {
                DibujarPuntoControl(controlPoints[i], i);
            }

            panelDibujo.Refresh();
        }

        private void DibujarPuntoControl(clsPunto p, int index)
        {
            Brush color = Brushes.Black;
            if (index == 0) color = Brushes.Blue;
            else if (index == controlPoints.Count - 1) color = Brushes.Red;

            g.FillEllipse(color,
                p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);

            g.DrawEllipse(Pens.White,
                p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);

            if (chkNumerarPuntos.Checked)
            {
                string etiqueta = $"P{index}";
                SizeF tamaño = g.MeasureString(etiqueta, this.Font);
                g.FillRectangle(Brushes.White,
                    p.X + RADIO_PUNTO_CONTROL + 2, p.Y - RADIO_PUNTO_CONTROL - 2,
                    tamaño.Width, tamaño.Height);
                g.DrawString(etiqueta, this.Font, Brushes.Black,
                    p.X + RADIO_PUNTO_CONTROL + 2, p.Y - RADIO_PUNTO_CONTROL - 2);
            }
        }

        private void DibujarVectorNodos()
        {
            try
            {
                List<float> nodos = clsBspline.CrearVectorNodos(controlPoints.Count, gradoK, tipoNodos);
                if (nodos.Count == 0) return;

                int yPos = panelDibujo.Height - 30;
                float margen = 20;
                float rangeStart = margen;
                float rangeEnd = panelDibujo.Width - margen;
                float rangeLength = rangeEnd - rangeStart;

                float minNodo = nodos[0];
                float maxNodo = nodos[nodos.Count - 1];
                float rangoNodos = maxNodo - minNodo;

                if (Math.Abs(rangoNodos) < 0.001f) return;

                g.DrawLine(Pens.Black, (int)rangeStart, yPos, (int)rangeEnd, yPos);
                g.DrawString("Vector de Nodos", this.Font, Brushes.Black, rangeStart, yPos + 10);

                for (int i = 0; i < nodos.Count; i++)
                {
                    float normalizado = (nodos[i] - minNodo) / rangoNodos;
                    int xPos = (int)(rangeStart + normalizado * rangeLength);

                    g.DrawLine(penKnots, xPos, yPos - 8, xPos, yPos + 8);

                    if (i % Math.Max(1, nodos.Count / 10) == 0 || i == 0 || i == nodos.Count - 1)
                    {
                        g.DrawString($"u{i}", new Font(this.Font.FontFamily, 7),
                            Brushes.Teal, xPos - 8, yPos - 20);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al dibujar nodos: {ex.Message}");
            }
        }

        // === INTERACCIÓN CON MOUSE ===

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Intentar arrastrar punto existente
                clsPunto pFound = controlPoints.FirstOrDefault(p =>
                    Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2);

                if (pFound != null)
                {
                    puntoArrastrado = pFound;
                    offsetArrastre = new Point((int)pFound.X - e.X, (int)pFound.Y - e.Y);
                    isDragging = true;
                    trailGraphics.Clear(Color.Transparent);
                    return;
                }

                // Agregar nuevo punto (solo si no está en Play)
                if (!isPlaying)
                {
                    controlPoints.Add(new clsPunto(e.X, e.Y));

                    if (controlPoints.Count >= gradoK + 1)
                    {
                        DibujarCurvaEstatica();
                    }

                    ActualizarEstadoBotones();
                    DibujarElementosDinamicos();
                }
            }
            else if (e.Button == MouseButtons.Right && !isPlaying)
            {
                // Eliminar punto
                clsPunto pFound = controlPoints.FirstOrDefault(p =>
                    Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2);

                if (pFound != null)
                {
                    controlPoints.Remove(pFound);

                    if (controlPoints.Count >= gradoK + 1)
                    {
                        DibujarCurvaEstatica();
                    }
                    else
                    {
                        staticCurveGraphics.Clear(panelDibujo.BackColor);
                    }

                    ActualizarEstadoBotones();
                    DibujarElementosDinamicos();
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null && isDragging)
            {
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                if (!isPlaying)
                {
                    DibujarCurvaEstatica(50);
                    DibujarElementosDinamicos();
                }
                else
                {
                    DibujarCurvaEstatica(50);
                    needsFullRedraw = true;
                }
            }
        }

        private void PanelDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                puntoArrastrado = null;
                isDragging = false;

                DibujarCurvaEstatica();
                needsFullRedraw = false;
                DibujarElementosDinamicos();
            }
        }

        private void PanelDibujo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }

        // === MÉTODOS AUXILIARES ===

        private void ActualizarEtiquetas()
        {
            lblValorT.Text = $"t = {t:F2}";
            ActualizarEstadoBotones();
        }

        private void ActualizarEstadoBotones()
        {
            int puntosMinimos = gradoK + 1;

            if (controlPoints.Count < puntosMinimos)
            {
                btnPlayPause.Enabled = false;
                btnPlayPause.Text = $"Necesita >= {puntosMinimos} puntos";
            }
            else
            {
                btnPlayPause.Enabled = true;
                if (!isPlaying) btnPlayPause.Text = "Play";
            }
        }

        private float Distancia(Point p1, Point p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        // === LIMPIEZA ===

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerAnimacion.Stop();

            g?.Dispose();
            canvas?.Dispose();
            staticCurveGraphics?.Dispose();
            staticCurveBitmap?.Dispose();
            trailGraphics?.Dispose();
            trailBitmap?.Dispose();
            penCurva?.Dispose();
            penPoligono?.Dispose();
            penKnots?.Dispose();
            brushPuntoT?.Dispose();
            brushRastro?.Dispose();

            base.OnFormClosing(e);
        }
    }
}