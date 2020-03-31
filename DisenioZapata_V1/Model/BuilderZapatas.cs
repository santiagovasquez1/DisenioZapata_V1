using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using System;
using System.Collections;
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
        private Propiedades_Refuerzo Propiedades_Refuerzo { get; set; }

        public void BuildZapatas(List<Fuerzas_Modelo> fuerzas, ETipoZapata tipoZapata, Modelo_Etabs modelo)
        {
            Propiedades_Refuerzo = new Propiedades_Refuerzo();
            zapatas = new ObservableCollection<Zapata>();
            Zapata zapatai = null;
            var PuntosI = fuerzas.Select(x => x.PointLabel).Distinct().ToList();
            PuntosLimite(PuntosI, modelo);

            foreach (string Label in PuntosI)
            {
                var FuerzasLabel = fuerzas.FindAll(x => x.PointLabel == Label).ToList();
                var punto = modelo.Points.Find(x => x.Name == Label);
                var Section = modelo.Frames.Find(x => x.p1.Name == Label & x.Story.StoryName == modelo.Stories[1].StoryName).Section;
                var VariablesModelo = GetVbles();

                if (tipoZapata == ETipoZapata.Zapata_Aislada)
                {
                    zapatai = new Zapata_Aislada(Label, punto, FuerzasLabel, VariablesModelo.Suelo)
                    {
                        Fc = 210f,
                        Fy = 4220,
                        R = 0.07f,
                        H = 0.25f,
                        LcX = Section.B + VariablesModelo.DeltaX,
                        LcY = Section.H + VariablesModelo.DeltaY
                    };
                    zapatai.SetCalculos();
                    zapatai.Presiones(zapatai.L1, zapatai.L2, zapatai.H);
                    zapatai.SetCortanteUnidireccional();
                    zapatai.SetCortanteBidireccional();
                    zapatai.SetFlexion();
                    zapatai.Despiece = new Despiece(Propiedades_Refuerzo, zapatai.L1, zapatai.L2, zapatai.R);
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

        private VariablesModelo GetVbles()
        {
            var recursos = App.Current.Resources;
            int cont = 0;
            VariablesModelo variables = null;

            foreach (DictionaryEntry i in recursos)
            {
                if (i.Key.ToString() == "VariablesModelo")
                {
                    variables = (VariablesModelo)i.Value;
                    return variables;
                }

                cont++;
            }

            return variables;
        }
    }
}