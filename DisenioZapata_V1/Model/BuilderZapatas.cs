using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DisenioZapata_V1
{
    public class BuilderZapatas : NotificationObject
    {
        private ObservableCollection<Zapata> zapatas;

        public ObservableCollection<Zapata> Zapatas
        {
            get { return zapatas; }
            set { zapatas = value; OnPropertyChanged(); }
        }

        private double Xmax { get; set; }
        private double Ymax { get; set; }
        private double Xmin { get; set; }
        private double Ymin { get; set; }

        public void BuildZapatas(List<Fuerzas_Modelo> fuerzas, ETipoZapata tipoZapata, Modelo_Etabs modelo)
        {
            zapatas = new ObservableCollection<Zapata>();
            Zapata zapatai = null;
            var PuntosI = fuerzas.Select(x => x.PointLabel).Distinct().ToList();
            PuntosLimite(PuntosI, modelo);

            foreach (string Label in PuntosI)
            {
                var FuerzasLabel = fuerzas.FindAll(x => x.PointLabel == Label).ToList();
                var punto = modelo.Points.Find(x => x.Name == Label);

                if (tipoZapata == ETipoZapata.Zapata_Aislada)
                {
                    zapatai = new Zapata_Aislada(Label, punto, FuerzasLabel, new Suelo("TipoD", 15f));
                    zapatai.Fc = 210f;
                    zapatai.Fy = 4220;
                    zapatai.R = 0.07f;
                    zapatai.SetCalculos();
                    zapatai.Presiones(zapatai.L1, zapatai.L2, zapatai.H);
                    zapatai.SetCortanteUnidireccional();
                    zapatai.SetCortanteBidireccional();
                    zapatai.SetFlexion();
                }

                DeterminarTipoColumna(zapatai);
                Zapatas.Add(zapatai);
            }
        }

        private void DeterminarTipoColumna(Zapata zapatai)
        {
            if (Math.Round(zapatai.Point.X, 3) == Math.Round(Xmax, 3) && Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymax, 3) ||
                Math.Round(zapatai.Point.X, 3) == Math.Round(Xmin, 3) && Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymin, 3) ||
                Math.Round(zapatai.Point.X, 3) == Math.Round(Xmin, 3) && Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymax, 3) ||
                Math.Round(zapatai.Point.X, 3) == Math.Round(Xmax, 3) && Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymin, 3)) 
            {
                zapatai.TipoColumna = ETipoColumnas.Esquinera;
            }
            else if (Math.Round(zapatai.Point.X, 3) == Math.Round(Xmax, 3) | Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymax, 3) | Math.Round(zapatai.Point.X, 3) == Math.Round(Xmin, 3) | Math.Round(zapatai.Point.Y, 3) == Math.Round(Ymin, 3))
            {
                zapatai.TipoColumna = ETipoColumnas.Borde;
            }
            else
            {
                zapatai.TipoColumna = ETipoColumnas.Interna;
            }
        }

        private void PuntosLimite(List<string> Puntos, Modelo_Etabs modelo)
        {
            Xmax = (from pi in Puntos
                    select modelo.Points.Find(x => x.Name == pi).X).Max();
            Ymax = (from pi in Puntos
                    select modelo.Points.Find(x => x.Name == pi).Y).Max();
            Xmin = (from pi in Puntos
                    select modelo.Points.Find(x => x.Name == pi).X).Min();
            Ymin = (from pi in Puntos
                    select modelo.Points.Find(x => x.Name == pi).Y).Min();
        }
    }
}