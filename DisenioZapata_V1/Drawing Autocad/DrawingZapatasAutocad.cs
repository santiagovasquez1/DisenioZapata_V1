using DisenioZapata_V1.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace DisenioZapata_V1.Drawing_Autocad
{
    public class DrawingZapatasAutocad : NotificationObject
    {
        private DrawZapata drawZapata;

        public DrawZapata DrawZapata
        {
            get { return drawZapata; }
            set
            {
                drawZapata = value; 
                DibujarZapatasCommand.ReevaluateCanExecute();
                CuadroZapatasCommand.ReevaluateCanExecute(); 
                OnPropertyChanged();
            }
        }

        private Datos_Zapatas datos_Zapatas;

        public Datos_Zapatas Datos_Zapatas
        {
            get { return datos_Zapatas; }
            set { datos_Zapatas = value; OnPropertyChanged(); }
        }

        public MiComando AbrirAutocadCommand { get; set; }
        public MiComando DibujarZapatasCommand { get; set; }
        public MiComando CuadroZapatasCommand { get; set; }
        private double[] xy;

        public double[] XY
        {
            get { return xy; }
            set { xy = value; }
        }

        private ObservableCollection<string> layers;

        public ObservableCollection<string> Layers
        {
            get { return layers; }
            set { layers = value; OnPropertyChanged(); }
        }

        private string layerzapata;

        public string LayerZapata
        {
            get { return layerzapata; }
            set { layerzapata = value; OnPropertyChanged(); }
        }

        private string layerpedestal;

        public string LayerPedestal
        {
            get { return layerpedestal; }
            set { layerpedestal = value; OnPropertyChanged(); }
        }

        private string layercotas;

        public string LayerCotas
        {
            get { return layercotas; }
            set { layercotas = value; OnPropertyChanged(); }
        }

        private string layerejes;

        public string LayerEjes
        {
            get { return layerejes; }
            set { layerejes = value; OnPropertyChanged(); }
        }

        public DrawingZapatasAutocad()
        {
            Datos_Zapatas = GetResources.Get_DatosZapatas();
            AbrirAutocadCommand = new MiComando(AbrirAutocadCommandExecute);
            DibujarZapatasCommand = new MiComando(DibujarZapatasCommandExecute, DibujarZapatasCommandcanExecute);
            CuadroZapatasCommand = new MiComando(CuadroZapatasCommandExecute, DibujarZapatasCommandcanExecute);
        }

        private void CuadroZapatasCommandExecute()
        {
            // throw new NotImplementedException();
        }

        private void DibujarZapatasCommandExecute()
        {
            FunctionsAutoCAD.FunctionsAutoCAD.GetPoint(ref xy);
            foreach (var zapatai in Datos_Zapatas.Zapatas)
            {
                DrawZapata = new DrawZapata(zapatai, XY, LayerZapata, LayerPedestal);
            }
        }

        private void GetLayers()
        {
            Layers = new ObservableCollection<string>();

            var LayersName = FunctionsAutoCAD.FunctionsAutoCAD.GetLayers();
            foreach (var layername in LayersName)
            {
                Layers.Add(layername);
            }
        }

        private bool DibujarZapatasCommandcanExecute()
        {
            if (Datos_Zapatas.Zapatas != null)
                return true;
            else
                return false;
        }

        private void AbrirAutocadCommandExecute()
        {
            FunctionsAutoCAD.FunctionsAutoCAD.OpenAutoCAD();
            FunctionsAutoCAD.FunctionsAutoCAD.SetScale("1:75");
            GetLayers();
        }
    }
}