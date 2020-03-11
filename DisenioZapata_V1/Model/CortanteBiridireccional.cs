using System;
using System.Linq;

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

        private string load;

        public string Load
        {
            get { return load; }
            set { load = value; }
        }

        private float fz;

        public float Fz
        {
            get { return fz; }
            set { fz = value; }
        }

        private float mx;

        public float Mx
        {
            get { return mx; }
            set { mx = value; ; }
        }

        private float my;

        public float My
        {
            get { return my; }
            set { my = value; }
        }

        public float Vu { get; set; }
        public float PhiVc1 { get; set; }
        public float PhiVc2 { get; set; }
        public float PhiVc3 { get; set; }
        public float PhiVc { get; set; }
        public float Qmax { get; set; }
        public string ChequeoVu { get; set; }

        public CortanteBiridireccional(Zapata zapata_I)
        {
            Zapata = zapata_I;
        }

        public void Calculo_Clase(Fuerzas_Modelo fuerza, int indice)
        {
            Dimensionamiento dimensionamiento = Zapata.Dimensionamientos[indice];

            Load = fuerza.Load;
            Fz = (float)fuerza.Fz;
            Mx = (float)fuerza.Mx;
            My = (float)fuerza.My;
            PhiVc1 = CalculoPhiVc1();
            PhiVc2 = CalculoPhiVc2();
            PhiVc3 = CalculoPhiVc3();
            PhiVc = new float[] { PhiVc1, PhiVc2, PhiVc3 }.Min();
            Qmax = new float[] { dimensionamiento.QmaxX, dimensionamiento.QmaxY, dimensionamiento.QminX, dimensionamiento.QminY }.Max();
            Vu = (CalcVu(Qmax));
        }

        public void Chequeos_Clase()
        {
            if (Vu > PhiVc)
                ChequeoVu = "Cortante ultima mayor que PhiVc";
            else
                ChequeoVu = "Ok";
        }

        private float CalcVu(float Qmax)
        {
            float d, A1, A2, Ac, Vu;

            d = Zapata.H - Zapata.R;
            A1 = Zapata.L1 * Zapata.L2;
            A2 = (Zapata.LcX + d) * (Zapata.LcY + d);
            Ac = A1 - A2;
            Vu = 1.4f * Qmax * Ac;
            return Vu;
        }

        private float CalculoPhiVc1()
        {
            float phivc = 1.1f * 0.75f * (float)Math.Sqrt(Zapata.Fc);
            return phivc;
        }

        private float CalculoPhiVc2()
        {
            float beta, phivc;

            beta = Math.Max(Zapata.LcX, Zapata.LcY) / Math.Min(Zapata.LcX, Zapata.LcY);
            phivc = 0.75f * 0.53f * (1 + (2 / beta)) * (float)Math.Sqrt(Zapata.Fc);
            return phivc;
        }

        private float CalculoPhiVc3()
        {
            float asd, b0, phivc;
            float d = Zapata.H - Zapata.R;

            b0 = (2 * (Zapata.LcX + d) + 2 * (Zapata.LcY + d)) * 100;
            asd = 0;

            switch (Zapata.TipoColumna)
            {
                case ETipoColumnas.Interna:
                    asd = 40f;
                    break;

                case ETipoColumnas.Borde:
                    asd = 30f;
                    break;

                case ETipoColumnas.Esquinera:
                    asd = 20f;
                    break;
            }

            phivc = 0.75f * 0.27f * (2f + (asd * d * 100 / b0)) * (float)Math.Sqrt(Zapata.Fc);
            return phivc;
        }
    }
}