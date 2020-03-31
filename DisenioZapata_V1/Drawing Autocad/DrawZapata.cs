using DisenioZapata_V1.Model;

namespace DisenioZapata_V1.Drawing_Autocad
{
    public class DrawZapata : NotificationObject
    {
        public Zapata Zapata { get; set; }
        public double[] XY { get; set; }
        public string LayerZapata { get; set; }
        public string LayerPedestal { get; set; }

        public DrawZapata()
        {
        }

        public DrawZapata(Zapata zapatai, double[] insertionPoint, string Layer, string Layer2)
        {
            Zapata = zapatai;
            XY = insertionPoint;
            LayerZapata = Layer;
            LayerPedestal = Layer2;
            DibujarZapata();
            DibujarPedestal();
        }

        private void DibujarZapata()
        {
            double X1 = XY[0] + Zapata.Point.X - Zapata.L2 / 2;
            double X2 = XY[0] + Zapata.Point.X + Zapata.L2 / 2;
            double Y1 = XY[1] + Zapata.Point.Y - Zapata.L1 / 2;
            double Y2 = XY[1] + Zapata.Point.Y + Zapata.L1 / 2;

            double[] Vertices = new double[]
            {
                X1,Y1,
                X2,Y1,
                X2,Y2,
                X1,Y2
            };

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices, LayerZapata);
        }

        private void DibujarPedestal()
        {
            double Xc = (Zapata.L2 - Zapata.LcX) / 2;
            double Yc = (Zapata.L1 - Zapata.LcY) / 2;

            double X1 = XY[0] + Zapata.Point.X - (Zapata.L2 / 2)+Xc;
            double X2 = X1+Zapata.LcX;
            double Y1 = XY[1] + Zapata.Point.Y -( Zapata.L1 / 2)+Yc;
            double Y2 = Y1+Zapata.LcY;

            double[] Vertices = new double[]
            {
               X1,Y1,
               X2,Y1,
               X2,Y2,
               X1,Y2
            };

            FunctionsAutoCAD.FunctionsAutoCAD.AddPolyline2D(Vertices, LayerPedestal);
        }
    }
}