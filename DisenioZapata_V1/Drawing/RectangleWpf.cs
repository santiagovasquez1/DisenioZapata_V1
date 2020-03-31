using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;
using Pen = System.Windows.Media.Pen;

namespace DisenioZapata_V1.Drawing
{
    public class RectangleWpf
    {
        private PointF insertionPoint;

        public PointF InsertionPoint
        {
            get { return insertionPoint; }
            set { insertionPoint = value; }
        }

        //private GraphicsPath seccion_Path;

        //public GraphicsPath Seccion_Path
        //{
        //    get { return seccion_Path; }
        //    set { seccion_Path = value; }
        //}

        private Color lineColor;

        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
        }

        private Color background;

        public Color Background
        {
            get { return background; }
            set { background = value; }
        }

        private DashStyle dashStyle;

        public DashStyle DashStyle
        {
            get { return dashStyle; }
            set { dashStyle = value; }
        }

        private float grosorLinea;

        public float GrosorLinea
        {
            get { return grosorLinea; }
            set { grosorLinea = value; }
        }

        private Pen pen;

        public Pen Pen
        {
            get { return pen; }
            set { pen = value; }
        }

        private Brush brush;

        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        private float height;

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        private float width;

        public float Width
        {
            get { return width; }
            set { width = value; }
        }
        public RectangleWpf(PointF insertionpoint, float h, float w, DashStyle style, float widthLine)
        {
            this.InsertionPoint = insertionpoint;
            this.Height = h;
            this.Width = w;
            this.DashStyle = style;
            this.GrosorLinea = widthLine;
            Brush = System.Windows.Media.Brushes.PaleGreen;

            pen = new Pen()
            {
                Brush = System.Windows.Media.Brushes.Black,
                Thickness=GrosorLinea,
                DashStyle=this.DashStyle
            };
        }

        public void Dibujar(DrawingContext dc)
        {
            Rect rect1 = new Rect(InsertionPoint.X, InsertionPoint.Y, Width, Height);
            dc.DrawRectangle(Brush, Pen, rect1);
        }
    }
}