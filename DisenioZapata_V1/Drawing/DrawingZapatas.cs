using DisenioZapata_V1.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace DisenioZapata_V1.Drawing
{
    public class DrawingZapatas : Canvas
    {
        private Rectangle Rectangle { get; set; }
        private List<DrawingVisual> drawingVisuals = new List<DrawingVisual>();
        private Datos_Zapatas Datos_Zapatas { get; set; }
        private float ex { get; set; } = 1;
        private float ey { get; set; } = 1;

        private float yh;

        public float Yh
        {
            get { return yh; }
            set { yh = value; }
        }

        private float xw;

        public float Xw
        {
            get { return xw; }
            set { xw = value; }
        }

        private float escala;

        public float Escala
        {
            get { return escala; }
            set { escala = value; }
        }

        private float dx;

        public float Dx
        {
            get { return dx; }
            set { dx = value; }
        }

        private float dy;

        public float Dy
        {
            get { return dy; }
            set { dy = value; }
        }

        public DrawingZapatas() : base()
        {
            Xw = (float)Width - 30f;
            Yh = (float)Height - 30f;

            drawingVisuals.Add(new DrawingVisual());

            foreach (var dv in drawingVisuals)
                AddVisualChild(dv);
        }

        private void Draw_Lines(DrawingContext dc)
        {
            var X = Width;
            var Y = Height;
            var Point1 = new System.Windows.Point(0, 0);
            var Point2 = new System.Windows.Point(Width, Height);
            var Style = new DashStyle(new double[] { 2, 3, 4 }, 3);

            var line = new Linea(Point1, Point2, Color.FromRgb(134, 134, 134), Style, 4);
            line.Dibujar(dc);
        }

        public void DibujarPlantas()
        {
            Datos_Zapatas = GetResources.Get_DatosZapatas();

            using (DrawingContext dc = drawingVisuals[0].RenderOpen())
            {
                DibujarGrilla(dc);
                Dibujar_Zapatas(dc);
            }
        }

        private void DibujarGrilla(DrawingContext dc)
        {
            int LineasX = 25;
            int LineasY = 25;

            float Ancho_Cuadro = Xw / LineasX;
            float Alto_Cuadro = Yh / LineasY;

            var Style = new DashStyle(new double[] { 2, 2, 2 }, 2);

            for (int i = 1; i < LineasX; i++)
            {
                var Point1 = new System.Windows.Point(Ancho_Cuadro * i, 0);
                var Point2 = new System.Windows.Point(Ancho_Cuadro * i, Yh);
                Linea linea = new Linea(Point1, Point2, Color.FromRgb(134, 134, 134), Style, 0.75f);
                linea.Dibujar(dc);
            }

            for (int i = 1; i < LineasY; i++)
            {
                var Point1 = new System.Windows.Point(0, Alto_Cuadro * i);
                var Point2 = new System.Windows.Point(Xw, Alto_Cuadro * i);
                Linea linea = new Linea(Point1, Point2, Color.FromRgb(134, 134, 134), Style, 0.75f);
                linea.Dibujar(dc);
            }
        }

        private void Dibujar_Zapatas(DrawingContext dc)
        {
            if (Datos_Zapatas.Zapatas != null)
            {
                float MaxX = (float)Datos_Zapatas.Zapatas.Select(x => x.Point.X).Max();
                float MinX = (float)Datos_Zapatas.Zapatas.Select(x => x.Point.X).Min();
                float MaxY = (float)Datos_Zapatas.Zapatas.Select(x => x.Point.Y).Max();
                float MinY = (float)Datos_Zapatas.Zapatas.Select(x => x.Point.Y).Min();
                float Xescalado;
                float Yescalado;
                float XI = 15;
                float YI = 15;

                if (ex * (Xw - 15) / (Math.Abs(MaxX - MinX)) <= ey * (Yh - 15) / (Math.Abs(MinY - MaxY)))
                    Escala = ex * (Xw - 15) / (Math.Abs(MaxX - MinX));
                else
                    Escala = ey * (Yh - 15) / (Math.Abs(MinY - MaxY));

                foreach (var Zapatai in Datos_Zapatas.Zapatas)
                {
                    var H = Zapatai.L1 * Escala;
                    var W = Zapatai.L2 * Escala;

                    DashStyle dash = new DashStyle();

                    if (Zapatai.Point.X < 0)
                        Xescalado = -MinX * Escala - (Math.Abs((float)Zapatai.Point.X) - Zapatai.L2) * Escala;
                    else
                        Xescalado = -MinX * Escala + (Math.Abs((float)Zapatai.Point.X) - Zapatai.L2) * Escala;

                    if (Zapatai.Point.Y < 0)
                        Yescalado = MinY * Escala + (Math.Abs((float)Zapatai.Point.Y) + Zapatai.L1) * Escala + Yh;
                    else
                        Yescalado = MinY * Escala - (Math.Abs((float)Zapatai.Point.Y) + Zapatai.L1) * Escala + Yh;

                    var IPoint = new System.Windows.Point(Xescalado + XI, Yescalado + YI);

                    Rectangle = new Rectangle(IPoint, H, W, Color.FromRgb(62, 62, 66), dash, 1);
                    Rectangle.Dibujar(dc);
                }
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return drawingVisuals.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return drawingVisuals[index];
        }
    }
}