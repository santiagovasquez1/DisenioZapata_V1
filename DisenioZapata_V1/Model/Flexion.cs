using System;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class Flexion : NotificationObject, ICalculo
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

        private float mux;

        public float Mux
        {
            get { return mux; }
            set { mux = value; OnPropertyChanged(); }
        }

        private float muy;

        public float Muy
        {
            get
            { return muy; }
            set { muy = value; OnPropertyChanged(); }
        }

        private float asmin;

        public float Asmin
        {
            get { return asmin; }
            set { asmin = value; OnPropertyChanged(); }
        }

        private float asReqx;

        public float AsreqX
        {
            get { return asReqx; }
            set { asReqx = value; OnPropertyChanged(); }
        }

        private float asReqy;

        public float AsreqY
        {
            get { return asReqy; }
            set { asReqy = value; OnPropertyChanged(); }
        }

        private float qmax;

        public float Qmax
        {
            get { return qmax; }
            set { qmax = value; OnPropertyChanged(); }
        }

        public Flexion(Zapata zapata_i)
        {
            Zapata = zapata_i;
        }

        public void Calculo_Clase(Fuerzas_Modelo fuerza, int indice)
        {
            Dimensionamiento dimensionamiento = Zapata.Dimensionamientos[indice];

            load = fuerza.Load;
            Qmax = new float[] { dimensionamiento.QmaxX, dimensionamiento.QmaxY, dimensionamiento.QminX, dimensionamiento.QminY }.Max();
            Mux = (CalcMu(Zapata.L2, Zapata.L1, Zapata.LcX, Qmax));
            Muy = (CalcMu(Zapata.L1, Zapata.L2, Zapata.LcY, Qmax));

            Asmin = CalcAsMin();
            AsreqX = CalcAsReq(Mux);
             AsreqY = CalcAsReq(Muy);
        }

        public void Chequeos_Clase()
        {
            //throw new NotImplementedException();
        }

        private float CalcMu(float Lado1, float Lado2, float Lc, float Qmax)
        {
            float a, b;
            float Mu = 0;

            a = (Lado1 - Lc) / 2;
            b = (Lado1 - Lc) / 4;
            Mu = Lado2 * a * b * Qmax * 1.4f;
            return Mu;
        }

        private float CalcAsMin()
        {
            float Asmin = 0.0018f * Zapata.H * 100f * 100f;
            return Asmin;
        }

        private float CalcAsReq(float Mu)
        {
            float d = (Zapata.H - Zapata.R) * 100f;
            float bef = 100f; //cm
            float a = 0.90f * (float)Math.Pow(Zapata.Fy, 2) / (2f * 0.85f * Zapata.Fc * bef);
            float b = -Zapata.Fy * d * 0.90f;
            float c = (Mu * 1000f * 100f);
            float raiz = (float)Math.Sqrt((float)Math.Pow(b, 2) - 4 * a * c);
            float As1 = (-b + raiz) / (2 * a);
            float As2 = (-b - raiz) / (2 * a);

            if (As1 < 0)
            {
                As1 = (float)Math.Pow(10, 10);
            }
            if (As2 < 0)
            {
                As2 = (float)Math.Pow(10, 10);
            }

            float AsDef = new float[] { As1, As2 }.Min();
            return AsDef;
        }
    }
}