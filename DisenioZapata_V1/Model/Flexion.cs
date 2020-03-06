using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public class Flexion:ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }

        public List<float> Mux { get; set; }
        public List<float> Muy { get; set; }
        public Flexion(Zapata zapata_i)
        {
            Zapata = zapata_i;
        }
        public void Calculo_Clase()
        {
            Dimensionamiento dimensionamiento = Zapata.ReturnDimensionamiento();
            Mux = new List<float>();
            Muy = new List<float>();
            for (int i = 0; i < dimensionamiento.QmaxX.Count; i++)
            {
                var Qmax = new float[] { dimensionamiento.QmaxX[i], dimensionamiento.QmaxY[i], dimensionamiento.QminX[i], dimensionamiento.QminY[i] }.Min();
                Mux.Add(CalcMu(Zapata.L2, Zapata.L1, Zapata.LcX, Qmax));
                Muy.Add(CalcMu(Zapata.L1, Zapata.L2, Zapata.LcY, Qmax));
            }

        }

        public void Chequeos_Clase()
        {
            //throw new NotImplementedException();
        }
        private float CalcMu(float Lado1,float Lado2,float Lc,float Qmax)
        {
            float a, b;
            float Mu = 0;

            a = (Lado1 - Lc) / 2;
            b= (Lado1 - Lc) / 4;
            Mu = Lado2 * a * b * Qmax * 1.4f;
            return Mu;
        }
    }
}
