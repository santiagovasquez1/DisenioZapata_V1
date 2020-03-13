using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xamarin.Forms;

namespace DisenioZapata_V1
{
    public class DatosZapatas:NotificationObject
    {
        private ObservableCollection<Zapata> zapatas;

        public ObservableCollection<Zapata> Zapatas
        {
            get { return zapatas; }
            set { zapatas = value; OnPropertyChanged(); }
        }
        private Zapata zapata_seleccionada;

        public Zapata Zapata_Seleccionada
        {
            get { return zapata_seleccionada; }
            set { zapata_seleccionada = value; OnPropertyChanged(); }
        }
        public Modelo_Etabs modelo_proyecto { get; set; }
        public ILectorFuerzas Lector { get; set; }
        public MyCommand NuevoProyectoCommand { get; set; }
        public MyCommand FuerzasProyectoCommand { get; set; }
        public DatosZapatas()
        {
            NuevoProyectoCommand = new MyCommand(NuevoProyectoCommandExecute);
            FuerzasProyectoCommand = new MyCommand(FuerzasProyectoCommandExecute);
        }

        private void NuevoProyectoCommandExecute()
        {
            OpenModel();
            OpenForces();
            Builder();
        }
        private void FuerzasProyectoCommandExecute()
        {
            MessagingCenter.Send(this , "GoToFuerzas");
        }

        private void OpenModel()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Modelo Estructural";
            openFileDialog.ShowDialog();
            string Ruta = openFileDialog.FileName;
            modelo_proyecto = new Modelo_Estructura(Ruta).Modelo;
        }

        private void OpenForces()
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
            builder.BuildZapatas(Lector.Get_Fuerzas() , ETipoZapata.Zapata_Aislada , modelo_proyecto);
            Zapatas = builder.Zapatas;
        }
    }
}
