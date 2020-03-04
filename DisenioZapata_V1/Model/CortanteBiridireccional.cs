using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public class CortanteBiridireccional : ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }
        public ObservableCollection<float> Vu{get;set;}
        public float Limite1 { get; set; }
        public float Limite2 { get; set; }
        public float SigmaCortante { get; set; }
        public void Calculo_Clase()
        {
            Vu = new ObservableCollection<float>();
            Dimensionamiento dimensionamiento = Zapata.ReturnDimensionamiento();

            for(int i = 0; i < dimensionamiento.QmaxX.Count; i++)
            {
                var Qmax = new float[] { dimensionamiento.QmaxX[i], dimensionamiento.QmaxY[i], dimensionamiento.QminX[i], dimensionamiento.QminY[i] }.Max();
                Vu.Add(CalcVu(Qmax));
            }
        }

        public void Chequeos_Clase()
        {
            throw new NotImplementedException();
        }
        private float CalcVu(float Qmax)
        {
            float a, b, c, Vu;
            a = zapata.L1 * zapata.L2;
            b = (Zapata.LcX + Zapata.H - Zapata.R);
            c = (Zapata.LcY + Zapata.H - Zapata.R);
            Vu = Qmax * 1.4f * (a - (b * c));
            return Vu;
        }
    }
}
