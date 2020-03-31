using System.Windows;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using DashStyle = System.Windows.Media.DashStyle;
using Pen = System.Windows.Media.Pen;

namespace DisenioZapata_V1.Drawing
{
    public class Linea : IShape
    {
        private Point p1;

        public Point P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        private Point p2;

        public Point P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        private Color lineColor;

        public Color LineColor
        {
            get { return lineColor; }
            set { lineColor = value; }
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
        private SolidColorBrush mybrush;

        public SolidColorBrush MyBrush
        {
            get { return mybrush; }
            set { mybrush = value; }
        }

        public Linea(Point point1, Point point2, Color color, DashStyle style, float width)
        {
            P1 = point1;
            P2 = point2;
            LineColor = color;
            DashStyle = style;
            GrosorLinea = width;

            MyBrush = new SolidColorBrush(LineColor);

            pen = new Pen()
            {
                Brush = MyBrush,
                Thickness = GrosorLinea,
                DashStyle = this.DashStyle
            };
        }

        public void Dibujar(DrawingContext dc)
        {
            dc.DrawLine(Pen, P1, P2);
        }
    }
}