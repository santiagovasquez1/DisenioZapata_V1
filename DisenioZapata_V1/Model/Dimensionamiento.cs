using System;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    public class Dimensionamiento : NotificationObject, ICalculo
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

        public float My
        {
            get { return my; }
            set { my = value; OnPropertyChanged(); }
        }

        private float ex;

        public float Ex
        {
            get { return ex; }
            set { ex = value; OnPropertyChanged(); }
        }

        private float ey;

        public float Ey
        {
            get { return ey; }
            set { ey = value; OnPropertyChanged(); }
        }

        private float qmaxx;

        public float QmaxX
        {
            get { return qmaxx; }
            set { qmaxx = value; OnPropertyChanged(); }
        }

        private float qmaxy;

        public float QmaxY
        {
            get { return qmaxy; }
            set { qmaxy = value; OnPropertyChanged(); }
        }

        private float qminx;

        public float QminX
        {
            get { return qminx; }
            set { qminx = value; OnPropertyChanged(); }
        }

        private float qminy;

        public float QminY
        {
            get { return qminy; }
            set { qminy = value; OnPropertyChanged(); }
        }

        private float qmax;

        public float Qmax
        {
            get { return qmax; }
            set { qmax = value; }
        }

        private string chequeo_ex;

        public string Chequeo_ex
        {
            get { return chequeo_ex; }
            set { chequeo_ex = value; }
        }

        private string chequeo_ey;

        public string Chequeo_ey
        {
            get { return chequeo_ey; }
            set { chequeo_ey = value; }
        }

        private string chequeo_q;

        public string Chequeo_Q
        {
            get { return chequeo_q; }
            set { chequeo_q = value; }
        }

        public Dimensionamiento(Zapata zapata_i)
        {
            Zapata = zapata_i;
        }

        public void Calculo_Clase(Fuerzas_Modelo fuerza,int indice)
        {
            Load = fuerza.Load;
            Fz = (float)fuerza.Fz;
            Mx = (float)fuerza.Mx;
            My = (float)fuerza.My;
            Ex = (Calculo_Excentricidad((float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.Mx));
            Ey = (Calculo_Excentricidad((float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.My));
            QmaxX = (CalcQMax(Ex, (float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.Mx, Zapata.L1, Zapata.L2));
            QminX = (CalcQMin(Ex, (float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.Mx, Zapata.L1, Zapata.L2));
            QmaxY = (CalcQMax(Ey, (float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.My, Zapata.L2, Zapata.L1));
            QminY = (CalcQMin(Ey, (float)fuerza.Fz + Zapata.PesoPropio, (float)fuerza.My, Zapata.L2, Zapata.L1));
            Qmax = new float[] { QmaxX, QmaxY, QminX, QminY }.Max();
        }

        /// <summary>
        /// Calculo de excentricidad de la carga en la zapata
        /// </summary>
        /// <param Carga Axial="Fz"></param>
        /// <param Dimencion="Lado"></param>
        /// <returns>Excentricidad en un lado especifico de la zapata</returns>
        private float Calculo_Excentricidad(float Fz, float M)
        {
            return M / Fz;
        }

        private float CalcQMax(float excentricidad, float Fz, float M, float Lado1, float Lado2)
        {
            float Qmax = 0f;
            float sigma_Axial = 0;
            float sigma_flexion = 0;

            sigma_Axial = Fz / Zapata.Area;
            sigma_flexion = (6f * M) / (Lado2 * (float)Math.Pow(Lado1, 2));

            if (excentricidad < Lado1 / 6)
                Qmax = sigma_Axial + sigma_flexion;
            else if (excentricidad < Lado1 / 2 && excentricidad > Lado1 / 6)
            {
                var b = Lado2 - 2 * (M / Fz);
                Qmax = (4 * Fz) / (3 * Lado1 * b);
            }
            else
                Qmax = 0;

            return Qmax;
        }

        private float CalcQMin(float excentricidad, float Fz, float M, float Lado1, float Lado2)
        {
            float Qmin = 0f;
            float sigma_Axial = 0;
            float sigma_flexion = 0;

            sigma_Axial = Fz / Zapata.Area;
            sigma_flexion = (6f * M) / (Lado2 * (float)Math.Pow(Lado1, 2));

            if (excentricidad < Lado1 / 6)
                Qmin = sigma_Axial - sigma_flexion;

            return Qmin;
        }

        public void Chequeos_Clase()
        {
            Chequeo_ex = (ChequeoExcentricidad(Ex, Zapata.L1));
            Chequeo_ey = (ChequeoExcentricidad(Ey, Zapata.L2));
            var Q = new float[] { QmaxX, QminX, QmaxY, QminY };
            Chequeo_Q = (ChequeoEsfuerzos(Q, Zapata.Suelo.SigmaAdmi));
        }

        private string ChequeoExcentricidad(float excentricidad, float Lado)
        {
            if (excentricidad <= Lado / 6)
                return ("e<L/6");
            else if (excentricidad > Lado / 6 & excentricidad <= Lado / 2)
                return ("L/6<e<L/2");
            else
                return ("e>L2");
        }

        private string ChequeoEsfuerzos(float[] Q, float Sadmi)
        {
            if (Q.Max() > Sadmi)
                return ("Los esfuerzos superan el esfuerzo admisible");
            else
                return ("Ok");
        }
    }
}