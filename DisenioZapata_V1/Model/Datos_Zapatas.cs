using B_Lectura_E2K.Entidades;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Xamarin.Forms;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class Datos_Zapatas : NotificationObject
    {
        [NonSerialized]
        private ILectorFuerzas lector;

        public ILectorFuerzas Lector { get => lector; set => lector = value; }

        [NonSerialized]
        private Modelo_Etabs modelo_proyecto1;

        public Modelo_Etabs modelo_proyecto { get => modelo_proyecto1; set => modelo_proyecto1 = value; }

        private ObservableCollection<Zapata> zapatas;

        public ObservableCollection<Zapata> Zapatas
        {
            get { return zapatas; }
            set { zapatas = value; OnPropertyChanged(); }
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

        private string ruta;

        public string Ruta
        {
            get { return ruta; }
            set { ruta = value; }
        }

        private string rutan;

        public string Rutan
        {
            get { return rutan; }
            set { rutan = value; }
        }

        [NonSerialized]
        private Propiedades_Refuerzo propiedades_Refuerzo;

        public Propiedades_Refuerzo Propiedades_Refuerzo
        {
            get { return propiedades_Refuerzo; }
            set { propiedades_Refuerzo = value; OnPropertyChanged(); }
        }

        [field: NonSerialized]
        public MiComando Variablescommand { get; set; }
        [field: NonSerialized]
        public MiComando NuevoProyectoCommand { get; set; }

        [field: NonSerialized]
        public MiComando FuerzasProyectoCommand { get; set; }

        [field: NonSerialized]
        public MiComando PropiedadesProyectoCommand { get; set; }

        [field: NonSerialized]
        public MiComando DatosPresionesCommand { get; set; }

        [field: NonSerialized]
        public MiComando DatosCortantesCommand { get; set; }

        [field: NonSerialized]
        public MiComando DatosFlexionCommand { get; set; }

        [field: NonSerialized]
        public MiComando ResumenCommand { get; set; }

        [field: NonSerialized]
        public MiComando GuardarComoCommand { get; set; }

        [field: NonSerialized]
        public MiComando GuardarCommand { get; set; }

        [field: NonSerialized]
        public MiComando AbrirCommand { get; set; }
        [field: NonSerialized]
        public MiComando Despiececommand { get; set; }
        
        public Datos_Zapatas()
        {            
            Propiedades_Refuerzo = new Propiedades_Refuerzo();
            Variablescommand = new MiComando(VariablescommandExecute);
            NuevoProyectoCommand = new MiComando(NuevoProyectoCommandExecute);
            FuerzasProyectoCommand = new MiComando(FuerzasProyectoCommandExecute);
            PropiedadesProyectoCommand = new MiComando(PropiedadesProyectoCommandExecute);
            DatosPresionesCommand = new MiComando(DatosPresionesCommandExecute);
            DatosCortantesCommand = new MiComando(DatosCortantesCommandExecute);
            DatosFlexionCommand = new MiComando(DatosFlexionCommandExecute);
            ResumenCommand = new MiComando(ResumenCommandExecute);
            GuardarComoCommand = new MiComando(GuardarComoCommandExecute);
            GuardarCommand = new MiComando(GuardarCommandExecuta);
            AbrirCommand = new MiComando(AbrirCommandExecuta);
            Despiececommand = new MiComando(DespiececommandExecute);
        }

        private void VariablescommandExecute()
        {
            MessagingCenter.Send(this, "GoToVbles");
        }

        private void DespiececommandExecute()
        {
            if (Zapata_Seleccionada.Despiece == null)
            {
                zapataSeleccionada.Despiece = new Despiece(Propiedades_Refuerzo,Zapata_Seleccionada.L1,Zapata_Seleccionada.L2,Zapata_Seleccionada.R);
            }

            MessagingCenter.Send(this, "GoToDespiece");
        }

        private void AbrirCommandExecuta()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Modelo Estructural";
            openFileDialog.Filter = "zap|*.zap";
            openFileDialog.ShowDialog();
            Ruta = openFileDialog.FileName;

            var temp = SerializacionObjetos.Deserealizar(Ruta);

            Zapatas = temp.Zapatas;
            Zapata_Seleccionada = temp.Zapata_Seleccionada;
            modelo_proyecto = temp.modelo_proyecto;
            PropiedadesProyectoCommandExecute();
            ResumenCommandExecute();
        }

        private void GuardarCommandExecuta()
        {
            if (Ruta != null)
            {
                SerializacionObjetos.Serializar(Ruta, this);
            }
            else
                GuardarComoCommandExecute();
        }

        private void GuardarComoCommandExecute()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "zap|*.zap";
            saveFileDialog.Title = "Guardar proyecto";
            saveFileDialog.ShowDialog();
            Ruta = saveFileDialog.FileName;
            SerializacionObjetos.Serializar(Ruta, this);
        }

        private void NuevoProyectoCommandExecute()
        {
            OpenModel();
            AbrirFuerzas();
            VariablescommandExecute();
            Builder();
            Zapata_Seleccionada = Zapatas.FirstOrDefault();
            PropiedadesProyectoCommandExecute();
            DatosPresionesCommandExecute();
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

        private void ResumenCommandExecute()
        {
            MessagingCenter.Send(this, "GoToResumen");
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
            Rutan = openFileDialog.FileName;
            Lector = new Lector_Fuerzas_Etabs(Rutan);
        }

        private void Builder()
        {
            BuilderZapatas builder = new BuilderZapatas();
            builder.BuildZapatas(Lector.Get_Fuerzas(), ETipoZapata.Zapata_Aislada, modelo_proyecto);
            Zapatas = builder.Zapatas;
        }
    }
}