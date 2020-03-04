using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
   public  class Dimensionamiento:NotificationObject,ICalculo
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
        private List<float> sigma;
        public List<float> Sigma
        {
            get { return sigma; }
            set { sigma = value; OnPropertyChanged(); }
        }
        public void Calculo_Clase()
        {
            Ex = new List<float>();
            Ey = new List<float>();
            QmaxX = new List<float>();
            QminX = new List<float>();
            QmaxY = new List<float>();

            foreach(var fuerza in zapata.Fuerzas)
            {
                Ex.Add(Calculo_Excentricidad((float)fuerza.Fz,Zapata.L1));
                Ey.Add(Calculo_Excentricidad((float)fuerza.Fz, Zapata.L2));
            }
        }
        /// <summary>
        /// Calculo de excentricidad de la carga en la zapata
        /// </summary>
        /// <param Carga Axial="Fz"></param>
        /// <param Dimencion="Lado"></param>
        /// <returns>Excentricidad en un lado especifico de la zapata</returns>
        private float Calculo_Excentricidad(float Fz,float Lado)
        {
            return Lado / Fz;
        }
        private float CalcQMax(float excentricidad,float Fz,float M,float Lado1,float Lado2)
        {
            float Qmax=0f;
            float sigma_Axial = 0;
            float sigma_flexion = 0;

            sigma_Axial = Fz / Zapata.Area;
            sigma_flexion = (6f * M) / (Lado2 * (float)Math.Pow(Lado1, 2));

            if (excentricidad < Lado1 / 6)
                Qmax = sigma_Axial + sigma_flexion;
            //if(excentricidad<Lado1/2 && excentricidad>Lado1/6)
            //    Qmax=
            return Qmax;
        }
    }
}
