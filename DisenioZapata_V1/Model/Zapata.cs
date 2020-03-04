using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace DisenioZapata_V1.Model
{
    public abstract class Zapata : NotificationObject
    {
        #region Variables_que_cambian

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        private float l1;

        public float L1
        {
            get { return l1; }
            set { l1 = value; OnPropertyChanged(); }
        }

        private float l2;

        public float L2
        {
            get { return l2; }
            set { l2 = value; OnPropertyChanged(); }
        }

        private float h;

        public float H
        {
            get { return h; }
            set { h = value; OnPropertyChanged(); }
        }

        private float lcx;

        public float LcX
        {
            get { return lcx; }
            set { lcx = value; OnPropertyChanged(); }
        }

        private float lcy;

        public float LcY
        {
            get { return lcy; }
            set { lcy = value; OnPropertyChanged(); }
        }

        private float r;

        public float R
        {
            get { return r; }
            set { r = value; OnPropertyChanged(); }
        }

        private float fc;

        public float Fc
        {
            get { return fc; }
            set { fc = value; OnPropertyChanged(); }
        }

        private List<ICalculo> calculos;

        public List<ICalculo> Calculos
        {
            get { return calculos; }
            set { calculos = value; OnPropertyChanged(); }
        }

        private MPoint point;

        public MPoint Point
        {
            get { return point; }
            set { point = value; }
        }

        private float area;

        public float Area
        {
            get { return area; }
            set { area = value; OnPropertyChanged(); }
        }

        #endregion Variables_que_cambian

        public List<Fuerzas_Modelo> Fuerzas { get; set; }

        public void CalcArea()
        {
            Area = L2 * L2;
        }

        public override string ToString()
        {
            return $"ZapataId {Label}";
        }

        public Dimensionamiento ReturnDimensionamiento()
        {
            Dimensionamiento calcdimensionamiento = null;

            foreach (ICalculo calculo in Calculos)
            {
                if (calculo is Dimensionamiento)
                {
                    calcdimensionamiento = (Dimensionamiento)calculo;
                    return calcdimensionamiento;
                }
            }
            return calcdimensionamiento;
        }

        public CortanteUnidireccional ReturnCortanteUnidireccional()
        {
            CortanteUnidireccional calculoi = null;

            foreach (ICalculo calculo in Calculos)
            {
                if (calculo is CortanteUnidireccional)
                {
                    calculoi = (CortanteUnidireccional)calculo;
                    return calculoi;
                }
            }
            return calculoi;
        }

        public CortanteBiridireccional ReturnCortanteBidireccional()
        {
            CortanteBiridireccional calculoi = null;

            foreach (ICalculo calculo in Calculos)
            {
                if (calculo is CortanteBiridireccional)
                {
                    calculoi = (CortanteBiridireccional)calculo;
                    return calculoi;
                }
            }
            return calculoi;
        }

        public Flexion ReturnFlexionl()
        {
            Flexion calculoi = null;

            foreach (ICalculo calculo in Calculos)
            {
                if (calculo is Flexion)
                {
                    calculoi = (Flexion)calculo;
                    return calculoi;
                }
            }
            return calculoi;
        }
    }
}