using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace curvasBzierBspline
{
    public partial class frmBspline : Form
    {
        // --- Variables de Estado y Puntos de Control ---
        private static frmBspline instancia;

        private List<clsPunto> controlPoints = new List<clsPunto>();

        // Variables de Dibujo (Optimización por capas)
        private Graphics g;
        private Bitmap canvas;
        private Bitmap staticCurveBitmap;
        private Graphics staticCurveGraphics;
        private Bitmap trailBitmap;
        private Graphics trailGraphics;

        // Parámetros B-spline
        private int gradoK = 3; // Grado inicial (Grado 3 - Cúbica)
        private KnotType tipoNodos = KnotType.AbiertoUniforme;
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
        private Pen penCurva = new Pen(Color.Red, 2); // B-spline
        private Pen penPoligono = new Pen(Color.Gray, 1);
        private Brush brushPuntoT = new SolidBrush(Color.LimeGreen);
        private Brush brushRastro = new SolidBrush(Color.FromArgb(100, Color.LightGray));
        private Pen penKnots = new Pen(Color.Teal, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dot };


        public frmBspline()
        {
            InitializeComponent();

            // Inicialización de Bitmaps y Graphics
            int w = panelDibujo.Width;
            int h = panelDibujo.Height;

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

            // Configuración de eventos de UI
            InicializarControlesUI();

            // Configurar Eventos del Panel para interactividad
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

        private void InicializarControlesUI()
        {
            // GRADO K (ComboBox)
            cboGrado.Items.Clear();
            cboGrado.Items.Add("2 (Cuadrática)");
            cboGrado.Items.Add("3 (Cúbica)");
            cboGrado.Items.Add("4");
            cboGrado.Items.Add("5");
            cboGrado.SelectedIndex = 1; // Grado 3 Cúbica por defecto
            cboGrado.SelectedIndexChanged += CboGrado_SelectedIndexChanged;

            // TRACKBARS
            trackBarT.Minimum = 0;
            trackBarT.Maximum = 100;
            trackBarT.Value = 0;
            trackBarT.Scroll += TrackBarT_Scroll;

            trackBarVelocidad.Minimum = 1;
            trackBarVelocidad.Maximum = 20;
            trackBarVelocidad.Value = 5;
            trackBarVelocidad.Scroll += TrackBarVelocidad_Scroll;

            timerAnimacion.Interval = 1000 / trackBarVelocidad.Value;
            timerAnimacion.Tick += TimerAnimacion_Tick;

            // RADIO BUTTONS (Tipo de Nodos)
            rbUniforme.CheckedChanged += RbKnotType_CheckedChanged;
            rbAbiertoUniforme.CheckedChanged += RbKnotType_CheckedChanged;
            rbPeriodica.CheckedChanged += RbKnotType_CheckedChanged;

            // BOTONES
            btnPlayPause.Click += BtnPlayPause_Click;
            btnReset.Click += BtnReset_Click;
            btnLimpiar.Click += BtnLimpiar_Click;
            btnAplicar.Click += BtnAplicar_Click;

            // CHECKBOXES
            chkMostrarPoligono.CheckedChanged += ChkMostrarPoligono_CheckedChanged;
            chkNumerarPuntos.CheckedChanged += ChkNumerarPuntos_CheckedChanged;
            chkMostrarKnots.CheckedChanged += ChkMostrarKnots_CheckedChanged;

            // Estado inicial del ComboBox
            ActualizarGradoK();
            ActualizarTipoNodos();
        }

        // --- Manejo de Eventos de Controles ---

        private void CboGrado_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarGradoK();
        }

        private void RbKnotType_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTipoNodos();
        }

        private void BtnAplicar_Click(object sender, EventArgs e)
        {
            // Forzar la actualización del tipo y grado, y recalcular la curva estática
            ActualizarGradoK();
            ActualizarTipoNodos();
            DibujarCurvaEstatica();
            ActualizarEtiquetas();
            DibujarElementosDinamicos();
        }

        // --- Lógica de Control ---

        private void ActualizarGradoK()
        {
            // El índice + 2 da el grado (0=2, 1=3, 2=4, 3=5)
            gradoK = cboGrado.SelectedIndex + 2;

            // Si el número de puntos es insuficiente, deshabilitar Play
            if (controlPoints.Count < gradoK)
            {
                btnPlayPause.Enabled = false;
                btnPlayPause.Text = $"Necesita >= {gradoK} puntos";
            }
            else
            {
                btnPlayPause.Enabled = true;
                if (!isPlaying) btnPlayPause.Text = "Play";
            }

            // Redibujar si hay cambios
            if (controlPoints.Count >= gradoK)
            {
                DibujarCurvaEstatica();
                DibujarElementosDinamicos();
            }
        }

        private void ActualizarTipoNodos()
        {
            if (rbAbiertoUniforme.Checked)
            {
                tipoNodos = KnotType.AbiertoUniforme;
                lblDescripcionTipo.Text = "Abierta/Uniforme: La curva toca los puntos extremos";
            }
            else if (rbUniforme.Checked)
            {
                tipoNodos = KnotType.Uniforme;
                lblDescripcionTipo.Text = "Uniforme: La curva no toca los extremos";
            }
            else if (rbPeriodica.Checked)
            {
                tipoNodos = KnotType.Periodica;
                lblDescripcionTipo.Text = "Periódica: Curva cerrada (conecta inicio y fin)";
            }

            // Forzar redibujo al cambiar el tipo de nodo
            if (controlPoints.Count >= gradoK)
            {
                DibujarCurvaEstatica();
                DibujarElementosDinamicos();
            }
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
            timerAnimacion.Interval = Math.Max(1, 1000 / trackBarVelocidad.Value);
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
            btnPlayPause.Enabled = false;
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

            // Control de Redibujo: Si el mouse movió un punto
            if (curveNeedsRedraw)
            {
                DibujarCurvaEstatica();
                curveNeedsRedraw = false;
            }

            DibujarElementosDinamicos();
        }

        // --- MÉTODOS DE DIBUJO Y B-SPLINE ---

        /// <summary>
        /// Dibuja el fondo y la curva B-spline completa en la capa estática.
        /// </summary>
        private void DibujarCurvaEstatica()
        {
            staticCurveGraphics.Clear(panelDibujo.BackColor);

            if (controlPoints.Count < gradoK) return;

            try
            {
                // Usar el método optimizado de CalcularCurva
                List<clsPunto> puntosCurva = clsBspline.CalcularCurva(controlPoints, gradoK, tipoNodos, 200);

                if (puntosCurva.Count > 1)
                {
                    Point[] puntos = puntosCurva.Select(p => p.ToPoint()).ToArray();
                    staticCurveGraphics.DrawLines(penCurva, puntos);
                }
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar mensaje en consola
                System.Diagnostics.Debug.WriteLine($"Error al dibujar curva: {ex.Message}");
            }
        }

        /// <summary>
        /// Dibuja los elementos que cambian en cada fotograma.
        /// </summary>
        private void DibujarElementosDinamicos()
        {
            g.Clear(panelDibujo.BackColor);
            g.DrawImage(staticCurveBitmap, 0, 0); // Curva estática
            g.DrawImage(trailBitmap, 0, 0); // Rastro

            if (controlPoints.Count < 1)
            {
                panelDibujo.Invalidate();
                return;
            }

            // Si hay suficientes puntos, calcular el punto animado
            if (controlPoints.Count >= gradoK)
            {
                try
                {
                    // --- CÁLCULO DE PUNTO ANIMADO ---
                    clsPunto puntoAnimado = clsBspline.CalcularPuntoCurva(controlPoints, gradoK, t, tipoNodos);

                    // 1. Dibujar el punto animado P(t)
                    g.FillEllipse(brushPuntoT, puntoAnimado.X - RADIO_PUNTO_CURVA, puntoAnimado.Y - RADIO_PUNTO_CURVA,
                                  RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);
                    g.DrawEllipse(Pens.DarkGreen, puntoAnimado.X - RADIO_PUNTO_CURVA, puntoAnimado.Y - RADIO_PUNTO_CURVA,
                                  RADIO_PUNTO_CURVA * 2, RADIO_PUNTO_CURVA * 2);

                    // 2. Dibujar el rastro durante la animación
                    if (isPlaying)
                    {
                        trailGraphics.FillEllipse(brushRastro, puntoAnimado.X - 1, puntoAnimado.Y - 1, 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error al calcular punto animado: {ex.Message}");
                }
            }


            // 3. Dibujar el polígono de control
            if (chkMostrarPoligono.Checked || isDragging)
            {
                if (controlPoints.Count >= 2)
                {
                    g.DrawLines(penPoligono, controlPoints.Select(p => p.ToPoint()).ToArray());
                }
            }

            // 4. Mostrar Nodos (opcional)
            if (chkMostrarKnots.Checked && controlPoints.Count >= gradoK)
            {
                DibujarVectorNodos();
            }

            // 5. Dibujar los puntos de control (al final para que estén encima)
            for (int i = 0; i < controlPoints.Count; i++)
            {
                DibujarPuntoControl(controlPoints[i], Brushes.Black, i);
            }

            panelDibujo.Invalidate();
        }

        private void DibujarVectorNodos()
        {
            try
            {
                List<float> nodos = clsBspline.CrearVectorNodos(controlPoints.Count, gradoK, tipoNodos);
                if (nodos.Count == 0) return;

                // Posición vertical para la visualización de nodos
                int yPos = panelDibujo.Height - 30;

                // Rango horizontal para dibujar
                float margen = 20;
                float rangeStart = margen;
                float rangeEnd = panelDibujo.Width - margen;
                float rangeLength = rangeEnd - rangeStart;

                // Normalizar nodos al rango [0, 1]
                float minNodo = nodos[0];
                float maxNodo = nodos[nodos.Count - 1];
                float rangoNodos = maxNodo - minNodo;

                if (Math.Abs(rangoNodos) < 0.001f) return;

                // Dibujar línea base
                g.DrawLine(Pens.Black, (int)rangeStart, yPos, (int)rangeEnd, yPos);
                g.DrawString("Vector de Nodos", this.Font, Brushes.Black, rangeStart, yPos + 10);

                // Dibujar cada nodo
                for (int i = 0; i < nodos.Count; i++)
                {
                    float normalizado = (nodos[i] - minNodo) / rangoNodos;
                    int xPos = (int)(rangeStart + normalizado * rangeLength);

                    // Línea del nodo
                    g.DrawLine(penKnots, xPos, yPos - 8, xPos, yPos + 8);

                    // Etiqueta (solo algunos para evitar saturación)
                    if (i % Math.Max(1, nodos.Count / 10) == 0 || i == 0 || i == nodos.Count - 1)
                    {
                        g.DrawString($"u{i}", new Font(this.Font.FontFamily, 7), Brushes.Teal, xPos - 8, yPos - 20);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al dibujar nodos: {ex.Message}");
            }
        }

        private void DibujarPuntoControl(clsPunto p, Brush brush, int index)
        {
            Brush currentBrush = brush;
            // P0 y Pn-1 (Extremos del polígono)
            if (index == 0)
                currentBrush = Brushes.Blue;
            else if (index == controlPoints.Count - 1 && controlPoints.Count >= 2)
                currentBrush = Brushes.Red;
            else
                currentBrush = Brushes.Black;

            g.FillEllipse(currentBrush, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);
            g.DrawEllipse(Pens.White, p.X - RADIO_PUNTO_CONTROL, p.Y - RADIO_PUNTO_CONTROL,
                          RADIO_PUNTO_CONTROL * 2, RADIO_PUNTO_CONTROL * 2);

            if (chkNumerarPuntos.Checked)
            {
                string etiqueta = $"P{index}";
                SizeF tamaño = g.MeasureString(etiqueta, this.Font);
                g.FillRectangle(Brushes.White, p.X + RADIO_PUNTO_CONTROL + 2, p.Y - RADIO_PUNTO_CONTROL - 2,
                               tamaño.Width, tamaño.Height);
                g.DrawString(etiqueta, this.Font, Brushes.Black, p.X + RADIO_PUNTO_CONTROL + 2, p.Y - RADIO_PUNTO_CONTROL - 2);
            }
        }

        private void ActualizarEtiquetas()
        {
            lblValorT.Text = $"t = {t:F2}";

            // Actualizar estado del botón
            if (controlPoints.Count < gradoK)
            {
                btnPlayPause.Enabled = false;
                btnPlayPause.Text = $"Necesita >= {gradoK} puntos";
            }
            else
            {
                btnPlayPause.Enabled = true;
                if (!isPlaying) btnPlayPause.Text = "Play";
            }
        }

        // --- Lógica de Interacción (Agregar/Arrastrar Puntos) ---

        private void PanelDibujo_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPlaying) return;

            if (e.Button == MouseButtons.Left)
            {
                // 1. Intentar arrastrar un punto existente
                clsPunto pFound = controlPoints.FirstOrDefault(p => Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2);

                if (pFound != null)
                {
                    puntoArrastrado = pFound;
                    offsetArrastre = new Point((int)pFound.X - e.X, (int)pFound.Y - e.Y);
                    isDragging = true;
                    return;
                }

                // 2. Si no se está arrastrando, agregar un nuevo punto
                clsPunto nuevoPunto = new clsPunto(e.X, e.Y);
                controlPoints.Add(nuevoPunto);

                // Si tenemos suficientes puntos para el grado actual, redibujar
                if (controlPoints.Count >= gradoK)
                {
                    DibujarCurvaEstatica();
                }

                ActualizarEtiquetas();
                DibujarElementosDinamicos();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Eliminar un punto con clic derecho
                clsPunto puntoAEliminar = controlPoints.FirstOrDefault(p => Distancia(e.Location, p.ToPoint()) < RADIO_PUNTO_CONTROL * 2);

                if (puntoAEliminar != null)
                {
                    controlPoints.Remove(puntoAEliminar);

                    if (controlPoints.Count >= gradoK)
                    {
                        DibujarCurvaEstatica();
                    }
                    else
                    {
                        staticCurveGraphics.Clear(panelDibujo.BackColor);
                    }

                    ActualizarEtiquetas();
                    DibujarElementosDinamicos();
                }
            }
        }

        private void PanelDibujo_MouseMove(object sender, MouseEventArgs e)
        {
            if (puntoArrastrado != null && isDragging)
            {
                // Mueve el punto en memoria
                puntoArrastrado.X = e.X + offsetArrastre.X;
                puntoArrastrado.Y = e.Y + offsetArrastre.Y;

                // Limitar al área del panel
                puntoArrastrado.X = Math.Max(0, Math.Min(panelDibujo.Width, puntoArrastrado.X));
                puntoArrastrado.Y = Math.Max(0, Math.Min(panelDibujo.Height, puntoArrastrado.Y));

                // Marcar que necesita redibujo de curva
                curveNeedsRedraw = true;

                // Redibujar inmediatamente para retroalimentación visual
                DibujarCurvaEstatica();
                DibujarElementosDinamicos();
            }
        }

        private void PanelDibujo_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                puntoArrastrado = null;
                isDragging = false;

                // Redibujo final de ALTA RESOLUCIÓN
                DibujarCurvaEstatica();
                DibujarElementosDinamicos();

                curveNeedsRedraw = false;
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

        // --- Limpieza de recursos ---
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerAnimacion.Stop();

            // Liberar recursos gráficos
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