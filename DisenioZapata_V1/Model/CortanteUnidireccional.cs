using System;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class CortanteUnidireccional : NotificationObject, ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }

        private string load;

        public string Load
        {
            get { return load; }
            set { load = value; OnPropertyChanged(); }
        }

        private float fz;

        public float Fz
        {
            get { return fz; }
            set { fz = value; OnPropertyChanged(); }
        }

        private float mx;

        public float Mx
        {
            get { return mx; }
            set { mx = value; OnPropertyChanged(); }
        }

        private float my;
        private float vuX;
        private float vuY;
        private float eux;
        private float euy;
        private float phiVc;
        private float qmax;
        private string chequeoCortante;

        public float My
        {
            get { return my; }
            set { my = value; OnPropertyChanged(); }
        }

        public float VuX
        {
            get { return vuX; }
            set
            {
                vuX = value;
                OnPropertyChanged();
            }
        }

        public float VuY
        {
            get { return vuY; }
            set
            {
                vuY = value;
                OnPropertyChanged();
            }
        }

        public float Eux
        {
            get { return eux; }
            set
            {
                eux = value;
                OnPropertyChanged();
            }
        }

        public float Euy
        {
            get { return euy; }
            set
            {
                euy = value;
                OnPropertyChanged();
            }
        }

        public float PhiVc
        {
            get { return phiVc; }
            set
            {
                phiVc = value;
                OnPropertyChanged();
            }
        }

        public float Qmax
        {
            get { return qmax; }
            set
            {
                qmax = value;
                OnPropertyChanged();
            }
        }

        public string ChequeoCortante
        {
            get { return chequeoCortante; }
            set
            {
                chequeoCortante = value;
                OnPropertyChanged();
            }
        }

        public CortanteUnidireccional(Zapata zapata_I)
        {
            Zapata = zapata_I;
        }

        public void Calculo_Clase(Fuerzas_Modelo fuerza, int indice)
        {
            PhiVc = CalcPhiVc(Zapata.Fc);
            Dimensionamiento dimensionamiento = Zapata.Dimensionamientos[indice];

            Load = fuerza.Load;
            Fz = (float)fuerza.Fz;
            Mx = (float)fuerza.Mx;
            My = (float)fuerza.My;
            Qmax = new float[] { dimensionamiento.QmaxX, dimensionamiento.QminX, dimensionamiento.QmaxY, dimensionamiento.QminY }.Max();
            VuX = (CalculoVu(Zapata.L1, Zapata.L2, Zapata.LcX, Qmax, Zapata.R));
            VuY = (CalculoVu(Zapata.L2, Zapata.L1, Zapata.LcY, Qmax, Zapata.R));
            Eux = (CalculoEsfuerzoCortante(Zapata.L2, VuX, Zapata.R));
            Euy = (CalculoEsfuerzoCortante(Zapata.L1, VuY, Zapata.R));
        }

        public void Chequeos_Clase()
        {
            ChequeoCortante = Mensaje_PhiVc();
        }

        private float CalculoVu(float Lado1, float Lado2, float Dimension_Pedestal, float EsfuerzoMax, float r)
        {
            float Vu = EsfuerzoMax * Lado1 * 1.4f * ((Lado1 / 2) - (Dimension_Pedestal / 2) - (Zapata.H - r));
            return Vu;
        }

        private float CalculoEsfuerzoCortante(float Lado, float Vu, float r)
        {
            float EsfVu = Vu / (Lado * (Zapata.H - r));
            return EsfVu;
        }

        private float CalcPhiVc(float fc)
        {
            float PhiVc = 0.75f * 0.53f * (float)Math.Sqrt(fc) * 10f;
            return PhiVc;
        }

        private string Mensaje_PhiVc()
        {
            float eumax = Math.Max(Eux, Euy);
            if (eumax / 10 > PhiVc)
                return ("Esfuerzos cortante superan el esfuerzo maximo del concreto");
            else
                return ("Ok");
        }
    }
}