using System;
using System.Collections.Generic;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    public class CortanteUnidireccional : ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }

        public List<float> VuX { get; set; }
        public List<float> VuY { get; set; }
        public List<float> euX { get; set; }
        public List<float> euY { get; set; }
        public float PhiVc { get; set; }
        public string ChequeoCortante { get; set; }
        public CortanteUnidireccional(Zapata zapata_I)
        {
            Zapata = zapata_I;
        }
        public void Calculo_Clase()
        {
            VuX = new List<float>();
            VuY = new List<float>();
            euX = new List<float>();
            euY = new List<float>();
            PhiVc = CalcPhiVc(Zapata.Fc);

            Dimensionamiento dimensionamiento = Zapata.Dimensionamiento;

            for (int i = 0; i < dimensionamiento.QmaxX.Count; i++)
            {
                float Qmax = new float[] { dimensionamiento.QmaxX[i], dimensionamiento.QminX[i], dimensionamiento.QmaxY[i], dimensionamiento.QminY[i] }.Max();
                VuX.Add(CalculoVu(Zapata.L1, Zapata.L2, Zapata.LcX, Qmax, Zapata.R));
                VuY.Add(CalculoVu(Zapata.L2, Zapata.L1, Zapata.LcY, Qmax, Zapata.R));
                euX.Add(CalculoEsfuerzoCortante(Zapata.L2, VuX.Last(), Zapata.R));
                euY.Add(CalculoEsfuerzoCortante(Zapata.L1, VuY.Last(), Zapata.R));
            }
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
            float PhiVc = 0.75f * 0.53f * (float)Math.Sqrt(fc);
            return PhiVc;
        }

        private string Mensaje_PhiVc()
        {
            float eumax = Math.Max(euX.Max(), euY.Max());
            if (eumax/10 > PhiVc)
                return ("Esfuerzos cortante superan el esfuerzo maximo del concreto");
            else
                return ("Ok");
        }
    }
}