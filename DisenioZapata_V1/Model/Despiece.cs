using System;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class Despiece : NotificationObject
    {
        private string barrax;

        public string BarraX
        {
            get { return barrax; }
            set
            {
                barrax = value;
                astotalx = Set_AsTotal(barrax, sepx,ladox);
                OnPropertyChanged();
            }
        }

        private string barray;

        public string BarraY
        {
            get { return barray; }
            set
            {
                barray = value;
                astotaly = Set_AsTotal(barray, sepy,ladoy);
                OnPropertyChanged();
            }
        }

        private float sepx;

        public float SepX
        {
            get { return sepx; }
            set
            {
                sepx = value;
                astotalx = Set_AsTotal(barrax, sepx,ladox);
                OnPropertyChanged();
            }
        }

        private float sepy;

        public float SepY
        {
            get { return sepy; }
            set
            {
                sepy = value;
                astotaly = Set_AsTotal(barray, sepy,ladoy);
                OnPropertyChanged();
            }
        }

        private float astotalx;

        public float AsTotalX
        {
            get { return astotalx; }
            set { astotalx = value; OnPropertyChanged(); }
        }

        private float astotaly;

        public float AsTotalY
        {
            get { return astotaly; }
            set { astotaly = value; OnPropertyChanged(); }
        }

        private float ladox;

        public float LadoX
        {
            get { return ladox; }
            set
            {
                ladox = value;
                longx = Set_Long(barrax, ladox, ganchox);
                OnPropertyChanged();
            }
        }

        private float ladoy;

        public float LadoY
        {
            get { return ladoy; }
            set
            {
                ladoy = value;
                longy = Set_Long(barray, ladoy, ganchoy);
                OnPropertyChanged();
            }
        }

        private float longx;

        public float LongX
        {
            get { return longx; }
            set { longx = value; OnPropertyChanged(); }
        }

        private float longy;

        public float LongY
        {
            get { return longy; }
            set { longy = value; OnPropertyChanged(); }
        }

        private float recubrimiento;

        public float Recubrimiento
        {
            get { return recubrimiento; }
            set { recubrimiento = value; OnPropertyChanged(); }
        }

        private bool ganchox;

        public bool GanchoX
        {
            get { return ganchox; }
            set
            {
                ganchox = value;
                longx = Set_Long(barrax, ladox, ganchox);
                OnPropertyChanged();
            }
        }

        private bool ganchoy;

        public bool GanchoY
        {
            get { return ganchoy; }
            set
            {
                ganchoy = value;
                longy = Set_Long(barray, ladoy, ganchoy);
                OnPropertyChanged();
            }
        }

        private string resumenx;

        public string ResumenX
        {
            get { return resumenx; }
            set { resumenx = value; OnPropertyChanged(); }
        }

        private string resumeny;

        public string ResumenY
        {
            get { return resumeny; }
            set { resumeny = value; OnPropertyChanged(); }
        }

        public Despiece(Propiedades_Refuerzo propiedades, float ladox, float ladoy, float r)
        {
            Propiedades = propiedades;
            LadoX = ladox;
            LadoY = ladoy;
            Recubrimiento = r;
        }

        private Propiedades_Refuerzo Propiedades { get; set; }

        private float Set_AsTotal(string Asi, float sep,float lado)
        {
            int Cantidad = 0;

            if (Asi != null | Asi != "")
            {
                float As_t = Propiedades.As_refuerzo[Asi];
                if (sep > 0)
                    Cantidad = (int)Math.Ceiling((lado - 2 * recubrimiento) / sep);

                return As_t * Cantidad;
            }
            else
                return 0f;
        }

        private float Set_Long(string Asi, float lado, bool gancho)
        {
            if (Asi != null | Asi != "")
            {
                float longGancho;
                if (gancho == true)
                    longGancho = Propiedades.Long_Gancho90[Asi] / 100f;
                else
                    longGancho = 0;
                float long_t = lado + 2 * longGancho - 2 * Recubrimiento;
                return long_t;
            }
            else
                return 0f;
        }
    }
}