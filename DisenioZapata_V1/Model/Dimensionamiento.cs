using System;
using System.Collections.Generic;
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

        private List<float> ex;

        public List<float> Ex
        {
            get { return ex; }
            set { ex = value; OnPropertyChanged(); }
        }

        private List<float> ey;

        public List<float> Ey
        {
            get { return ey; }
            set { ey = value; OnPropertyChanged(); }
        }

        private List<float> qmaxx;

        public List<float> QmaxX
        {
            get { return qmaxx; }
            set { qmaxx = value; OnPropertyChanged(); }
        }

        private List<float> qmaxy;

        public List<float> QmaxY
        {
            get { return qmaxy; }
            set { qmaxy = value; OnPropertyChanged(); }
        }

        private List<float> qminx;

        public List<float> QminX
        {
            get { return qminx; }
            set { qminx = value; OnPropertyChanged(); }
        }

        private List<float> qminy;

        public List<float> QminY
        {
            get { return qminy; }
            set { qminy = value; OnPropertyChanged(); }
        }

        private float sigma;

        public float Sigma
        {
            get { return sigma; }
            set { sigma = value; OnPropertyChanged(); }
        }

        private List<string> chequeo_ex;

        public List<string> Chequeo_ex
        {
            get { return chequeo_ex; }
            set { chequeo_ex = value; }
        }

        private List<string> chequeo_ey;

        public List<string> Chequeo_ey
        {
            get { return chequeo_ey; }
            set { chequeo_ey = value; }
        }

        private List<string> chequeo_q;

        public List<string> Chequeo_Q
        {
            get { return chequeo_q; }
            set { chequeo_q = value; }
        }

        public void Calculo_Clase()
        {
            Ex = new List<float>();
            Ey = new List<float>();
            QmaxX = new List<float>();
            QminX = new List<float>();
            QmaxY = new List<float>();

            foreach (var fuerza in zapata.Fuerzas)
            {
                Ex.Add(Calculo_Excentricidad((float)fuerza.Fz , Zapata.L1));
                Ey.Add(Calculo_Excentricidad((float)fuerza.Fz , Zapata.L2));
                QmaxX.Add(CalcQMax(Ex.Last() , (float)fuerza.Fz , (float)fuerza.Mx , Zapata.L1 , Zapata.L2));
                QminX.Add(CalcQMin(Ex.Last() , (float)fuerza.Fz , (float)fuerza.Mx , Zapata.L1 , Zapata.L2));
                QmaxY.Add(CalcQMax(Ey.Last() , (float)fuerza.Fz , (float)fuerza.My , Zapata.L2 , Zapata.L1));
                QminY.Add(CalcQMin(Ey.Last() , (float)fuerza.Fz , (float)fuerza.My , Zapata.L2 , Zapata.L1));
            }
        }

        /// <summary>
        /// Calculo de excentricidad de la carga en la zapata
        /// </summary>
        /// <param Carga Axial="Fz"></param>
        /// <param Dimencion="Lado"></param>
        /// <returns>Excentricidad en un lado especifico de la zapata</returns>
        private float Calculo_Excentricidad(float Fz , float Lado)
        {
            return Lado / Fz;
        }

        private float CalcQMax(float excentricidad , float Fz , float M , float Lado1 , float Lado2)
        {
            float Qmax = 0f;
            float sigma_Axial = 0;
            float sigma_flexion = 0;

            sigma_Axial = Fz / Zapata.Area;
            sigma_flexion = (6f * M) / (Lado2 * (float)Math.Pow(Lado1 , 2));

            if (excentricidad < Lado1 / 6)
                Qmax = sigma_Axial + sigma_flexion;
            if (excentricidad < Lado1 / 2 && excentricidad > Lado1 / 6)
                Qmax = 4 * Fz / (3 * Lado2 - 2 * (M / Fz));
            else
                Qmax = 0;

            return Qmax;
        }

        private float CalcQMin(float excentricidad , float Fz , float M , float Lado1 , float Lado2)
        {
            float Qmin = 0f;
            float sigma_Axial = 0;
            float sigma_flexion = 0;

            sigma_Axial = Fz / Zapata.Area;
            sigma_flexion = (6f * M) / (Lado2 * (float)Math.Pow(Lado1 , 2));

            if (excentricidad < Lado1 / 6)
                Qmin = sigma_Axial - sigma_flexion;

            return Qmin;
        }

        public void Chequeos_Clase()
        {
            Chequeo_ex = new List<string>();
            Chequeo_ey = new List<string>();
            Chequeo_Q = new List<string>();

            for (int i = 0; i < Ex.Count; i++)
            {
                Chequeo_ex.Add(ChequeoExcentricidad(Ex[i] , Zapata.L1));
                Chequeo_ey.Add(ChequeoExcentricidad(Ey[i] , Zapata.L2));
                var Q = new float[] { QmaxX[i] , QminX[i] , QmaxY[i] , QminY[i] };
                Chequeo_Q.Add(ChequeoEsfuerzos(Q , Sigma));
            }
        }

        private string ChequeoExcentricidad(float excentricidad , float Lado)
        {
            if (excentricidad <= Lado / 6)
                return ("e<L/6");
            else if (excentricidad > Lado / 6 & excentricidad >= Lado / 2)
                return ("L/6<e<L/2");
            else
                return ("e>L2");
        }

        private string ChequeoEsfuerzos(float[] Q , float Sadmi)
        {
            if (Q.Max() > Sadmi)
                return ("Los esfuerzos superan el esfuerzo admisible");
            else
                return ("Ok");
        }
    }
}