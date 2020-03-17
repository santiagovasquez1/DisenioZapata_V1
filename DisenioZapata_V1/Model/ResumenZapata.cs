using System;
using System.Linq;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class ResumenZapata : NotificationObject
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }

        #region Presiones

        private string loadpresionesmax;

        public string Loadpresionesmax
        {
            get { return loadpresionesmax; }
            set { loadpresionesmax = value; OnPropertyChanged(); }
        }

        private float qmax;

        public float Qmax
        {
            get { return qmax; }
            set { qmax = value; OnPropertyChanged(); }
        }

        private bool cumplepresiones;

        public bool CumplePresiones
        {
            get { return cumplepresiones; }
            set { cumplepresiones = value; OnPropertyChanged(); }
        }

        #endregion Presiones

        #region Cortante unidireccional

        private string loadvux;

        public string LoadVux
        {
            get { return loadvux; }
            set { loadvux = value; OnPropertyChanged(); }
        }

        private string loadvuy;

        public string LoadVuY
        {
            get { return loadvuy; }
            set { loadvuy = value; OnPropertyChanged(); }
        }

        private float vux_max;

        public float VuX_Max
        {
            get { return vux_max; }
            set { vux_max = value; OnPropertyChanged(); }
        }

        private float vuy_max;

        public float Vuy_Max
        {
            get { return vuy_max; }
            set { vuy_max = value; OnPropertyChanged(); }
        }

        private float phivc;

        public float PhiVc
        {
            get { return phivc; }
            set { phivc = value; OnPropertyChanged(); }
        }

        private bool cumpleV1;

        public bool CumpleV1
        {
            get { return cumpleV1; }
            set { cumpleV1 = value; OnPropertyChanged(); }
        }

        #endregion Cortante unidireccional

        #region Cortante bidireccional

        private string loadbidireccionalmax;

        public string Loadbidireccionalmax
        {
            get { return loadbidireccionalmax; }
            set { loadbidireccionalmax = value; OnPropertyChanged(); }
        }

        private float vu_Max;

        public float Vu_Max
        {
            get { return vu_Max; }
            set { vu_Max = value; OnPropertyChanged(); }
        }

        private float phivc1;

        public float PhiVc1
        {
            get { return phivc1; }
            set { phivc1 = value; OnPropertyChanged(); }
        }

        private float phivc2;

        public float PhiVc2
        {
            get { return phivc2; }
            set { phivc2 = value; OnPropertyChanged(); }
        }

        private float phivc3;

        public float PhiVc3
        {
            get { return phivc3; }
            set { phivc3 = value; OnPropertyChanged(); }
        }

        private bool cumpleV2;

        public bool CumpleV2
        {
            get { return cumpleV2; }
            set { cumpleV2 = value; OnPropertyChanged(); }
        }

        #endregion Cortante bidireccional

        #region Flexion

        private string loadmux;

        public string LoadMux
        {
            get { return loadmux; }
            set { loadmux = value; OnPropertyChanged(); }
        }

        private string loadmuy;

        public string LoadMuY
        {
            get { return loadmuy; }
            set { loadmuy = value; OnPropertyChanged(); }
        }

        private float mux_max;

        public float MuX_Max
        {
            get { return mux_max; }
            set { mux_max = value; OnPropertyChanged(); }
        }

        private float muy_max;

        public float Muy_Max
        {
            get { return muy_max; }
            set { muy_max = value; OnPropertyChanged(); }
        }

        private float asx;

        public float Asx
        {
            get { return asx; }
            set { asx = value; OnPropertyChanged(); }
        }

        private float asy;

        public float Asy
        {
            get { return asy; }
            set { asy = value; OnPropertyChanged(); }
        }

        #endregion Flexion

        public ResumenZapata(Zapata zapata)
        {
            Zapata = zapata;
        }

        public void SetResumenPresiones()
        {
            var prueba = Zapata.Dimensionamientos.Select(x => x.Qmax).FirstOrDefault();
            int indice = 0;

            if (prueba != 0)
            {
                Qmax = Zapata.Dimensionamientos.Select(x => x.Qmax).Max();
                indice = Zapata.Dimensionamientos.Select(x => x.Qmax).ToList().FindIndex(x => x == Qmax);
                Loadpresionesmax = Zapata.Dimensionamientos.Select(x => x.Load).ToList()[indice];

                if (Qmax < Zapata.Suelo.SigmaAdmi)
                    CumplePresiones = true;
                else
                    CumplePresiones = false;
            }
        }

        public void SetResumenUnidireccional()
        {
            var prueba = Zapata.CortanteUnidireccional.Select(x => x.Qmax).FirstOrDefault();
            int indiceX, indiceY;

            if (prueba != 0)
            {
                VuX_Max = Zapata.CortanteUnidireccional.Select(x => x.Eux).Max();
                Vuy_Max = Zapata.CortanteUnidireccional.Select(x => x.Euy).Max();

                indiceX = Zapata.CortanteUnidireccional.Select(x => x.Eux).ToList().FindIndex(x => x == VuX_Max);
                indiceY = Zapata.CortanteUnidireccional.Select(x => x.Euy).ToList().FindIndex(x => x == Vuy_Max);

                LoadVux = Zapata.CortanteUnidireccional.Select(x => x.Load).ToList()[indiceX];
                LoadVuY = Zapata.CortanteUnidireccional.Select(x => x.Load).ToList()[indiceY];

                PhiVc = Zapata.CortanteUnidireccional.Select(x => x.PhiVc).FirstOrDefault();

                if (VuX_Max <= phivc && Vuy_Max <= phivc)
                    CumpleV1 = true;
                else
                    CumpleV1 = false;
            }
        }

        public void SetResumenBidireccional()
        {
            var prueba = Zapata.CortanteBiridireccional.Select(x => x.Qmax).FirstOrDefault();
            int indice;

            if (prueba != 0)
            {
                Vu_Max = Zapata.CortanteBiridireccional.Select(x => x.Qmax).Max() * 1.4f;
                indice = Zapata.CortanteBiridireccional.Select(x => x.Qmax).ToList().FindIndex(x => x == Zapata.CortanteBiridireccional.Select(x1 => x1.Qmax).Max());
                loadbidireccionalmax = Zapata.CortanteBiridireccional.Select(x => x.Load).ToList()[indice];
                PhiVc1 = Zapata.CortanteBiridireccional.Select(x => x.PhiVc1).FirstOrDefault();
                PhiVc2 = Zapata.CortanteBiridireccional.Select(x => x.PhiVc2).FirstOrDefault();
                PhiVc3 = Zapata.CortanteBiridireccional.Select(x => x.PhiVc3).FirstOrDefault();

                float phivc_def = new float[] { PhiVc1 , PhiVc2 , PhiVc3 }.Min();

                if (VuX_Max < phivc_def)
                    CumpleV2 = true;
                else
                    CumpleV2 = false;
            }
        }

        public void SetResumenFlexion()
        {
            var prueba = Zapata.Flexion.Select(x => x.AsreqX).FirstOrDefault();
            int indiceX, indiceY;

            if (prueba != 0)
            {
                MuX_Max = Zapata.Flexion.Select(x => x.Mux).Max();
                Muy_Max = Zapata.Flexion.Select(x => x.Muy).Max();

                indiceX = Zapata.Flexion.Select(x => x.Mux).ToList().FindIndex(x => x == MuX_Max);
                indiceY = Zapata.Flexion.Select(x => x.Muy).ToList().FindIndex(x => x == Muy_Max);

                Asx = Zapata.Flexion.Select(x => x.AsreqX).ToList()[indiceX];
                Asy = Zapata.Flexion.Select(x => x.AsreqY).ToList()[indiceX];

                LoadMux = Zapata.Flexion.Select(x => x.Load).ToList()[indiceX];
                LoadMuY = Zapata.Flexion.Select(x => x.Load).ToList()[indiceY];
            }
        }
    }
}