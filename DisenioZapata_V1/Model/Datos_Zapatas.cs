using B_Lectura_E2K.Entidades;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model
{
    public class Datos_Zapatas : NotificationObject
    {
        public ILectorFuerzas Lector { get; set; }
        public Modelo_Etabs modelo_proyecto { get; set; }

        private ObservableCollection<Zapata> zapatas;

        public ObservableCollection<Zapata> Zapatas
        {
            get { return zapatas; }
            set { zapatas = value; OnPropertyChanged(); }
        }

        private ObservableCollection<DatosPresiones> presiones;

        public ObservableCollection<DatosPresiones> Presiones
        {
            get { return presiones; }
            set { presiones = value; OnPropertyChanged(); }
        }

        private Zapata zapataSeleccionada;

        public Zapata Zapata_Seleccionada
        {
            get { return zapataSeleccionada; }
            set
            {
                zapataSeleccionada = value;
                OnPropertyChanged();
            }
        }

        private Suelo suelo;

        public Suelo Suelo
        {
            get { return suelo; }
            set { suelo = value; OnPropertyChanged(); }
        }

        public MiComando NuevoProyectoCommand { get; set; }
        public MiComando FuerzasProyectoCommand { get; set; }
        public MiComando PropiedadesProyectoCommand { get; set; }
        public MiComando DatosPresionesCommand { get; set; }
        public MiComando DatosCortantesCommand { get; set; }
        public Datos_Zapatas()
        {
            NuevoProyectoCommand = new MiComando(NuevoProyectoCommandExecute);
            FuerzasProyectoCommand = new MiComando(FuerzasProyectoCommandExecute);
            PropiedadesProyectoCommand = new MiComando(PropiedadesProyectoCommandExecute);
            DatosPresionesCommand = new MiComando(DatosPresionesCommandExecute);
            DatosCortantesCommand = new MiComando(DatosCortantesCommandExecute);
        }
        private void NuevoProyectoCommandExecute()
        {
            OpenModel();
            AbrirFuerzas();
            Builder();
            PropiedadesProyectoCommandExecute();
        }

        private void FuerzasProyectoCommandExecute()
        {
            MessagingCenter.Send(this, "GoToFuerzas");
        }

        private void PropiedadesProyectoCommandExecute()
        {
            MessagingCenter.Send(this, "GoToDimensiones");
        }

        private void DatosPresionesCommandExecute()
        {
            MessagingCenter.Send(this, "GoToPresiones");
        }
        private void DatosCortantesCommandExecute()
        {
            MessagingCenter.Send(this, "GoToCortantes");
        }
        private void SetSuelo()
        {
            MessagingCenter.Send(this, "GoToPresiones");
        }

        private void OpenModel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Modelo Estructural";
            openFileDialog.ShowDialog();
            string Ruta = openFileDialog.FileName;
            modelo_proyecto = new Modelo_Estructura(Ruta).Modelo;
        }

        private void AbrirFuerzas()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv |*.csv";
            openFileDialog.Title = "Reacciones modelo";
            openFileDialog.ShowDialog();
            string Ruta = openFileDialog.FileName;
            Lector = new Lector_Fuerzas_Etabs(Ruta);
        }

        private void Builder()
        {
            BuilderZapatas builder = new BuilderZapatas();
            builder.BuildZapatas(Lector.Get_Fuerzas(), ETipoZapata.Zapata_Aislada, modelo_proyecto);
            Zapatas = builder.Zapatas;
        }
    }

    public class DatosPresiones : NotificationObject
    {
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
            set { mx = value; }
        }

        private float my;

        public float My
        {
            get { return my; }
            set { my = value; }
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

        public float Qmaxx
        {
            get { return qmaxx; }
            set { qmaxx = value; OnPropertyChanged(); }
        }

        private float qmaxy;

        public float Qmaxy
        {
            get { return qmaxy; }
            set { qmaxy = value; OnPropertyChanged(); }
        }

        private float qminx;

        public float Qminx
        {
            get { return qminx; }
            set { qminx = value; OnPropertyChanged(); }
        }

        private float qminy;

        public float Qminy
        {
            get { return qminy; }
            set { qminy = value; OnPropertyChanged(); }
        }

        private string Chequeoex;

        public string Chequeo_Ex
        {
            get { return Chequeoex; }
            set { Chequeoex = value; OnPropertyChanged(); }
        }

        private string Chequeoey;

        public string Chequeo_Ey
        {
            get { return Chequeoey; }
            set { Chequeoey = value; OnPropertyChanged(); }
        }

        public DatosPresiones(string load, float fz, float mx, float my, float qx, float qy, float qminx, float qminy, float ex, float ey, string checkEx, string checkEy)
        {
            Load = load;
            Fz = fz;
            Mx = mx;
            My = my;
            Ex = ex;
            Ey = ey;
            Qmaxx = qx;
            Qmaxy = qy;
            Qminx = qminx;
            Qminy = qminy;
            Chequeo_Ex = checkEx;
            Chequeo_Ey = checkEy;
        }
    }
}