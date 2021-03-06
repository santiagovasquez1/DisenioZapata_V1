﻿using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using DisenioZapata_V1.Model.UserModel;
using DisenioZapata_V1.View;
using DisenioZapata_V1.View.ViewsPlantaZapatas;
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
        public MainWindow()
        {
            InitializeComponent();
            DataBase dataBase = new DataBase();

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
            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToFlexion", (a) =>
            {
                Main.Content = new FlexionPage();
            });
            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToResumen", (a) =>
            {
                Main.Content = new ResumenPage();
            });
            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToDespiece", (a) =>
            {
                Main.Content = new Despiece_Refuerzo();
            });

            MessagingCenter.Subscribe<Datos_Zapatas>(this, "GoToVbles", (a) =>
            {
                var vblesinicio = new VariablesProyectoPage();
                vblesinicio.ShowDialog();
            });
        }


        private void MainWindowLoad(object sender, RoutedEventArgs e)
        {
            var User = GetResources.GetUser();
            DataBase data = new DataBase();

            data.InsertOperation(User);
        }

        private void Acerca_Click(object sender, RoutedEventArgs e)
        {
            var acercade = new AcercadePage();
            acercade.ShowDialog();
        }

        private void PlantaZapata_Click(object sender, RoutedEventArgs e)
        {
            var ZapataPlantas = new PlantaZapatas();
            ZapataPlantas.Show();
        }

        private void PlantaZapatas_Click(object sender, RoutedEventArgs e)
        {
            var VariablesDibujo = new VariablesDibujoPage();
            VariablesDibujo.ShowDialog();
        }
    }
}
