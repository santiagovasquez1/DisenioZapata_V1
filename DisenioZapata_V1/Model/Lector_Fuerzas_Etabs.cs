﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace DisenioZapata_V1.Model
{
    public class Lector_Fuerzas_Etabs : ILectorFuerzas
    {
        private List<Fuerzas_Modelo> Fuerzas { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }

        public Lector_Fuerzas_Etabs(string Ruta)
        {
            Fuerzas = new List<Fuerzas_Modelo>();
            LectorArchivo(Ruta);
        }

        public void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "csv |*.csv";
            openFileDialog.Title = "Reacciones modelo";
            openFileDialog.ShowDialog();
            string Ruta = openFileDialog.FileName;
            LectorArchivo(Ruta);
        }

        public void LectorArchivo(string RutaArchivo)
        {
            int cont = 0;
            string sline;
            var Reader = new StreamReader(RutaArchivo);
            Fuerzas_Modelo fuerza = null;
            do
            {
                sline = Reader.ReadLine();

                if (cont > 0 && sline != null)
                {
                    var Datos = sline.Split(';');
                    fuerza = new Fuerzas_Modelo()
                    {
                        Story = Datos[0],
                        PointLabel = Datos[1],
                        Load = Datos[2],
                        Fx = Math.Abs(float.Parse(Datos[3])),
                        Fy = Math.Abs(float.Parse(Datos[4])),
                        Fz = Math.Abs(float.Parse(Datos[5])),
                        My = Math.Abs(float.Parse(Datos[6])),
                        Mx = Math.Abs(float.Parse(Datos[7])),
                        Mz = Math.Abs(float.Parse(Datos[8])),
                    };
                    Fuerzas.Add(fuerza);
                }
                cont++;
            } while (!(sline == null));

            Reader.Close();
        }

        public List<Fuerzas_Modelo> Get_Fuerzas()
        {
            return Fuerzas;
        }
    }
}