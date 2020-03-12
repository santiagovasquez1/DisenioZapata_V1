using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model.DatosTablas;
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
        public MiComando DatosFlexionCommand { get; set; }
        public Datos_Zapatas()
        {
            NuevoProyectoCommand = new MiComando(NuevoProyectoCommandExecute);
            FuerzasProyectoCommand = new MiComando(FuerzasProyectoCommandExecute);
            PropiedadesProyectoCommand = new MiComando(PropiedadesProyectoCommandExecute);
            DatosPresionesCommand = new MiComando(DatosPresionesCommandExecute);
            DatosCortantesCommand = new MiComando(DatosCortantesCommandExecute);
            DatosFlexionCommand = new MiComando(DatosFlexionCommandExecute);
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
        private void DatosFlexionCommandExecute()
        {
            MessagingCenter.Send(this, "GoToFlexion");
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
}