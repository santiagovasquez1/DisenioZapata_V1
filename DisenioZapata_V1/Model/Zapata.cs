using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    public abstract class Zapata : NotificationObject
    {
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
            set
            {
                l1 = value;
                CalcArea(L1, L2);
                CalcPesoPropio();
                OnPropertyChanged();
            }
        }

        private float l2;

        public float L2
        {
            get { return l2; }
            set { l2 = value; CalcArea(L1, L2); CalcPesoPropio(); OnPropertyChanged(); }
        }

        private float h;

        public float H
        {
            get { return h; }
            set { h = value; CalcPesoPropio(); OnPropertyChanged(); }
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

        private float fy;

        public float Fy
        {
            get { return fy; }
            set { fy = value; OnPropertyChanged(); }
        }

        private float gammaConcreto;

        public float GammaConcreto
        {
            get { return gammaConcreto; }
            set { gammaConcreto = value; OnPropertyChanged(); }
        }

        private float pesopropio;

        public float PesoPropio
        {
            get { return pesopropio; }
            set { pesopropio = value; OnPropertyChanged(); }
        }

        private Suelo suelo;

        public Suelo Suelo
        {
            get { return suelo; }
            set { suelo = value; CalcArea(); OnPropertyChanged(); }
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

        private ETipoColumnas tipoColumna;

        public ETipoColumnas TipoColumna
        {
            get { return tipoColumna; }
            set { tipoColumna = value; OnPropertyChanged(); }
        }

        private List<Fuerzas_Modelo> fuerzas;

        public List<Fuerzas_Modelo> Fuerzas
        {
            get { return fuerzas; }
            set { fuerzas = value; OnPropertyChanged(); }
        }

        private Dimensionamiento dimensionamiento;

        public Dimensionamiento Dimensionamiento
        {
            get { return dimensionamiento; }
            set { dimensionamiento = value; OnPropertyChanged(); }
        }

        private CortanteUnidireccional cortanteUnidireccional;

        public CortanteUnidireccional CortanteUnidireccional
        {
            get { return cortanteUnidireccional; }
            set { cortanteUnidireccional = value; OnPropertyChanged(); }
        }

        private CortanteBiridireccional cortanteBiridireccional;

        public CortanteBiridireccional CortanteBiridireccional
        {
            get { return cortanteBiridireccional; }
            set { cortanteBiridireccional = value; OnPropertyChanged(); }
        }

        private Flexion flexion;

        public Flexion Flexion
        {
            get { return flexion; }
            set { flexion = value; OnPropertyChanged(); }
        }

        public void CalcArea()
        {
            if (Fuerzas != null)
            {
                float Pmax = (float)Fuerzas.Select(x => x.Fz).Max();
                Area = Pmax / Suelo.SigmaAdmi;
                CalcLado();
            }
        }

        public void CalcArea(float L1, float L2)
        {
            if (L1 > 0 & L2 > 0)
                Area = L1 * L2;
        }

        public void CalcPesoPropio()
        {
            if (L1 > 0 & L2 > 0 & H > 0)
                PesoPropio = GammaConcreto * L1 * L2 * H;
        }

        public override string ToString()
        {
            return $"ZapataId {Label}";
        }

        private void CalcLado()
        {
            L1 = (float)Math.Sqrt(Area);
            L2 = (float)Math.Sqrt(Area);
        }

        public abstract void SetCalculos();
    }
}