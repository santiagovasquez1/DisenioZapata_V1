using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DisenioZapata_V1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ILectorFuerzas Lector { get; set; }
        public Modelo_Etabs modelo_proyecto { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }
        private void AbrirClick(object sender , RoutedEventArgs e)
        {
            OpenModel();
            AbrirFuerzas();
            Builder();
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
            builder.BuildZapatas(Lector.Get_Fuerzas(), ETipoZapata.Zapata_Concentrica, modelo_proyecto);
        }
    }
}
