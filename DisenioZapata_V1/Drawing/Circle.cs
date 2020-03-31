using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Pen = System.Windows.Media.Pen;

namespace DisenioZapata_V1.Drawing
{
    public class Circle : IShape
    {
        private Point insertionPoint;

        public Point InsertionPoint
        {
            get { return insertionPoint; }
            set { insertionPoint = value; }
        }

        private List<Point> points;

        public List<Point> Points
        {
            get { return points; }
            set { points = value; }
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

        private float radio;

        public float Radio
        {
            get { return radio; }
            set { radio = value; }
        }

        public Circle(Point insertionpoint, float r, Color backcolor, Color LineColor, DashStyle style, float widthLine)
        {
            this.InsertionPoint = insertionpoint;
            this.Radio = r;
            this.Background = backcolor;
            this.LineColor = LineColor;
            this.DashStyle = style;
            this.GrosorLinea = widthLine;
            //this.Seccion_Path = new GraphicsPath();
            this.Points = new List<Point>();

            pen = new Pen()
            {
                Brush = System.Windows.Media.Brushes.Black,
                Thickness = GrosorLinea,
                DashStyle = this.DashStyle
            };

            //Brush = new SolidBrush(Background);
            //Set_Puntos(50, this.Radio);
        }

        public void Dibujar(DrawingContext dc)
        {
            dc.DrawEllipse(Brush, Pen, InsertionPoint, Radio, Radio);
        }
    }
}