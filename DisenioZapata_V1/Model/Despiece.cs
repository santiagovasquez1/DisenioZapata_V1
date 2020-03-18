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
                AsTotalX = Set_AsTotal(BarraX, SepX,LadoX);
                LongX = Set_Long(BarraX, LadoX, GanchoX);
                ResumenX = Set_Resumen(LongX, BarraX, SepX, LadoX);
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
                AsTotalY = Set_AsTotal(BarraY, SepY,LadoY);
                LongY = Set_Long(BarraY, LadoY, GanchoY);
                ResumenY= Set_Resumen(LongY, BarraY, SepY, LadoY);
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
                AsTotalX = Set_AsTotal(BarraX, SepX,LadoX);
                ResumenX = Set_Resumen(LongX, BarraX, SepX, LadoX);
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
                AsTotalY = Set_AsTotal(BarraY, SepY,LadoY);
                ResumenY = Set_Resumen(LongY, BarraY, SepY, LadoY);
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
                LongX = Set_Long(BarraX, LadoX, GanchoX);
                ResumenX = Set_Resumen(LongX, BarraX, SepX, LadoX);
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
                LongY = Set_Long(BarraY, LadoY, GanchoY);
                ResumenY = Set_Resumen(LongY, BarraY, SepY, LadoY);
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
                LongX = Set_Long(BarraX, LadoX, GanchoX);
                ResumenX = Set_Resumen(LongX, BarraX, SepX, LadoX);
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
                LongY = Set_Long(BarraY, LadoY, GanchoY);
                ResumenY = Set_Resumen(LongY, BarraY, SepY, LadoY);
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
            GanchoX = true;
            GanchoY = true;
        }

        private Propiedades_Refuerzo Propiedades { get; set; }

        private float Set_AsTotal(string Asi, float sep,float lado)
        {
            if (Propiedades.As_refuerzo == null)
                Propiedades = new Propiedades_Refuerzo();

            int Cantidad = 0;

            if (Asi != null)
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
            if (Propiedades.Long_Gancho90 == null)
                Propiedades = new Propiedades_Refuerzo();

            if (Asi != null)
            {
                float longGancho;
                if (gancho == true)
                    longGancho = Propiedades.Long_Gancho90[Asi] ;
                else
                    longGancho = 0;
                float long_t = lado + 2 * longGancho - 2 * Recubrimiento;
                return long_t;
            }
            else
                return 0f;
        }
        private string Set_Resumen(float longitud, string barra,float sep,float lado)
        {
            int Cantidad = 0;

            if (sep > 0)
                Cantidad = (int)Math.Ceiling((lado - 2 * recubrimiento) / sep);

            return $"{Cantidad} {barra} longitud : {longitud} a {sep} m";
        }

    }
}