using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace curvasBzierBspline
{
    public partial class frmBspline : Form
    {
        private static frmBspline instancia;
        // --- Variables de Estado y Puntos de Control ---
        private List<clsPunto> controlPoints = new List<clsPunto>();

        // Variables de Dibujo (Optimización por capas)
        private Graphics g;
        private Bitmap canvas;
        private Bitmap staticCurveBitmap;
        private Graphics staticCurveGraphics;
        private Bitmap trailBitmap;
        private Graphics trailGraphics;

        // Parámetros B-spline
        private int gradoK = 3; // Grado inicial (cúbica)
        private float t = 0.0f;

        // Animación cíclica
        private bool direccionHaciaFinal = true;
        private bool isPlaying = false;

        // Control de arrastre y fluidez
        private clsPunto puntoArrastrado = null;
        private Point offsetArrastre;
        private bool isDragging = false;
        private bool curveNeedsRedraw = false;

        // Constantes y Pinceles
        private const int RADIO_PUNTO_CONTROL = 6;
        private const int RADIO_PUNTO_CURVA = 3;
        private Pen penCurva = new Pen(Color.Red, 2);
        private Pen penPoligono = new Pen(Color.Gray, 1);
        private Brush brushPuntoT = new SolidBrush(Color.LimeGreen);
        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));

        public frmBspline()
        {
            InitializeComponent();

            // Configuración inicial del NumericUpDown para el grado
            numGrado.Minimum = 2;
            numGrado.Maximum = 10;
            numGrado.Value = gradoK;

            // Configuración de TrackBars
            trackBarT.Minimum = 0;
            trackBarT.Maximum = 100;
            trackBarT.Value = 0;

            trackBarVelocidad.Minimum = 1;
            trackBarVelocidad.Maximum = 20;
            trackBarVelocidad.Value = 5;

            int w = panelDibujo.Width > 0 ? panelDibujo.Width : 800;
            int h = panelDibujo.Height > 0 ? panelDibujo.Height : 600;

            // Inicialización de Bitmaps y Graphics
            canvas = new Bitmap(w, h);
            g = Graphics.FromImage(canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            panelDibujo.BackgroundImage = canvas;
            panelDibujo.BackgroundImageLayout = ImageLayout.None;

            staticCurveBitmap = new Bitmap(w, h);
            staticCurveGraphics = Graphics.FromImage(staticCurveBitmap);
            staticCurveGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            trailBitmap = new Bitmap(w, h);
            trailGraphics = Graphics.FromImage(trailBitmap);
            trailGraphics.Clear(Color.Transparent);
            trailGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Configuración de eventos
            trackBarT.Scroll += TrackBarT_Scroll;
            trackBarVelocidad.Scroll += TrackBarVelocidad_Scroll;
            numGrado.ValueChanged += NumGrado_ValueChanged;
            btnPlayPause.Click += BtnPlayPause_Click;
            btnReset.Click += BtnReset_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnAplicar.Click += BtnAplicar_Click;

            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
            timerAnimacion.Tick += TimerAnimacion_Tick;

            panelDibujo.Paint += PanelDibujo_Paint;
            panelDibujo.MouseDown += PanelDibujo_MouseDown;
            panelDibujo.MouseMove += PanelDibujo_MouseMove;
            panelDibujo.MouseUp += PanelDibujo_MouseUp;

            // Estado inicial
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }
        public static frmBspline ObtenerInstancia()
        {
            if (instancia == null || instancia.IsDisposed)
            {
                instancia = new frmBspline();
            }
            return instancia;
        }
        // --- Manejo de Eventos de Controles ---

        private void NumGrado_ValueChanged(object sender, EventArgs e)
        {
            gradoK = (int)numGrado.Value;
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            gradoK = (int)numGrado.Value;
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void TrackBarT_Scroll(object sender, EventArgs e)
        {
            t = (float)trackBarT.Value / 100.0f;
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        private void TrackBarVelocidad_Scroll(object sender, EventArgs e)
        {
            lblVelocidad.Text = $"Velocidad: {trackBarVelocidad.Value}";
            if (trackBarVelocidad.Value > 0)
                timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
        }

        private void BtnPlayPause_Click(object sender, EventArgs e)
        {
            if (controlPoints.Count < gradoK) return;

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
            direccionHaciaFinal = true;

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
            trackBarT.Value = 0;

            trailGraphics.Clear(Color.Transparent);
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        // --- Lógica de Animación (Cíclica) ---

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            if (controlPoints.Count < gradoK) return;

            float incremento = 0.01f * (float)trackBarVelocidad.Value / 5.0f;

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

            if (curveNeedsRedraw)
            {
                DibujarCurvaEstatica(0.05f);
                curveNeedsRedraw = false;
            }

            DibujarElementosDinamicos();
        }

        // --- MÉTODOS DE DIBUJO Y B-SPLINE ---

        private void DibujarCurvaEstatica(float paso = 0.01f)
        {
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            if (controlPoints.Count < gradoK) return;

            clsPunto puntoAnterior = null;

            for (float i = 0.0f; i <= 1.0f; i += paso)
            {
                clsPunto puntoCurva = clsBspline.CalcularPuntoCurva(controlPoints, gradoK, i);

                if (puntoAnterior != null)
                {
                    staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoCurva.ToPoint());
                }
                puntoAnterior = puntoCurva;
            }

            clsPunto puntoFinal = clsBspline.CalcularPuntoCurva(controlPoints, gradoK, 1.0f);
            if (puntoAnterior != null)
            {
                staticCurveGraphics.DrawLine(penCurva, puntoAnterior.ToPoint(), puntoFinal.ToPoint());
            }
        }

        private void DibujarElementosDinamicos()
        {
            g.Clear(panelDibujo.BackColor);
            g.DrawImage(staticCurveBitmap, 0, 0);
            g.DrawImage(trailBitmap, 0, 0);

            if (controlPoints.Count < 1)
            {
                ActualizarEtiquetas();
                panelDibujo.Invalidate();
                return;
            }

            clsPunto puntoAnimado = controlPoints.Count >= gradoK
                ? clsBspline.CalcularPuntoCurva(controlPoints, gradoK, t)
                : controlPoints[0];

            // 1. Dibujar el polígono de control
            if (chkMostrarPoligono.Checked || isDragging)
            {
                if (controlPoints.Count >= 2)
                {
                    g.DrawLines(penPoligono, controlPoints.Select(p => p.ToPoint()).ToArray());
                }
            }

            // 2. Dibujar el punto animado P(t)
            if (controlPoints.Count >= gradoK)
            {
                g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_CURVA, puntoAnimado.Y - RADIO_PUNTO_CURVA,
                    RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);
            }

            // 3. Dibujar el rastro
            if (chkMostrarPoligono.Checked && isPlaying && controlPoints.Count >= gradoK)
            {
                trailGraphics.FillEllipse(brushRastro, puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
            }

            // 4. Dibujar los puntos de control
            for (int i = 0; i < controlPoints.Count; i++)
            {
                DibujarPuntoControl(controlPoints[i], Brushes.Black, i);
            }

            panelDibujo.Invalidate();
        }

        private void DibujarPuntoControl(clsPunto p, Brush brush, int index)
        {
            Brush currentBrush;
            if (index == 0)
                currentBrush = Brushes.Blue;
            else if (index == controlPoints.Count - 1 && controlPoints.Count >= 2)
                currentBrush = Brushes.Red;
            else
                currentBrush = Brushes.Black;

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
            int n = controlPoints.Count;
            lblCantidadPuntos.Text = $"Puntos: {n} (Grado k: {gradoK})";
            lblGrado.Text = $"Grado de la B-Spline: {gradoK}";
            lblValorT.Text = $"t = {t:F2}";
        }

        // --- Lógica de Interacción ---

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPlaying) return;

            if (e.Button == MouseButtons.Left)
            {
                foreach (clsPunto p in controlPoints)
                {
                    if (Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2)
                    {
                        puntoArrastrado = p;
                        offsetArrastre = new Point((int)puntoArrastrado.X - e.X, (int)puntoArrastrado.Y - e.Y);
                        isDragging = true;
                        curveNeedsRedraw = true;
                        return;
                    }
                }

                clsPunto nuevoPunto = new clsPunto(e.X, e.Y);
                controlPoints.Add(nuevoPunto);

                if (controlPoints.Count >= gradoK)
                {
                    DibujarCurvaEstatica();
                }

                ActualizarEtiquetas();
                DibujarElementosDinamicos();
            }
            else if (e.Button == MouseButtons.Right)
            {
                clsPunto puntoAEliminar = controlPoints.FirstOrDefault(p => Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2);

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
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                ActualizarEtiquetas();
                curveNeedsRedraw = true;

                // Redibujo inmediato durante arrastre
                DibujarCurvaEstatica(0.05f);
                DibujarElementosDinamicos();
            }
        }

        private void PanelDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            puntoArrastrado = null;
            isDragging = false;

            DibujarCurvaEstatica();
            DibujarElementosDinamicos();

            curveNeedsRedraw = false;
        }

        private float Distancia(Point p1, Point p2)
        {
            float dx = p1.X - p2.X;
            float dy = p1.Y - p2.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        private void PanelDibujo_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);
        }
    }
}