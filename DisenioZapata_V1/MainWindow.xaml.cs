using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using DisenioZapata_V1.View;
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
using Xamarin.Forms;

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
            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToFuerzas", (a) =>
            {
                Main.Content = new Page1();               
            });

            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToDimensiones", (a) =>
            {
                MainProp.Content = new PropiedadesPage();
            });

            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToPresiones", (a) =>
            {
                Main.Content = new DatosPresionesPage();
            });
            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToCortantes", (a) =>
            {
                Main.Content = new Cortantes();
            });
        }

        private void PresionesOnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
