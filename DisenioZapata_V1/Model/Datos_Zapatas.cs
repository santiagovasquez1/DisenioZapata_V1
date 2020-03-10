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
            get { return zapataSeleccionada; SetPresiones(); }
            set { zapataSeleccionada = value; OnPropertyChanged(); SetPresiones(); }
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

        public Datos_Zapatas()
        {
            NuevoProyectoCommand = new MiComando(NuevoProyectoCommandExecute);
            FuerzasProyectoCommand = new MiComando(FuerzasProyectoCommandExecute);
            PropiedadesProyectoCommand = new MiComando(PropiedadesProyectoCommandExecute);
            DatosPresionesCommand = new MiComando(DatosPresionesCommandExecute);
        }

        private void SetPresiones()
        {

            StartCalculo(Zapata_Seleccionada, "Dimensionamiento");
            Dimensionamiento Datos_Dim = Zapata_Seleccionada.ReturnDimensionamiento();
            Presiones = new ObservableCollection<DatosPresiones>();

            for (int i = 0; i < Datos_Dim.Ex.Count; i++)
            {
                DatosPresiones datoi = new DatosPresiones(Datos_Dim.QmaxX[i], Datos_Dim.QmaxY[i], Datos_Dim.Ex[i], Datos_Dim.Ey[i]);
                Presiones.Add(datoi);
            }
        }
        private void StartCalculo(Zapata zapata, string NameCalculo)
        {
            foreach (var Calculo in zapata.Calculos)
            {
                var Type = Calculo.GetType().Name;
                if (Type == NameCalculo)
                {
                    Calculo.Calculo_Clase();
                }
            }
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

        private void SetSuelo()
        {
            //Suelo = new Suelo("D", 12f);
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

        private float qx;

        public float Qx
        {
            get { return qx; }
            set { qx = value; OnPropertyChanged(); }
        }

        private float qy;

        public float Qy
        {
            get { return qy; }
            set { qy = value; OnPropertyChanged(); }
        }

        public DatosPresiones(float qx, float qy, float ex, float ey)
        {
            Ex = ex;
            Ey = ey;
            Qx = qx;
            Qy = qy;
        }
    }
}