using B_Lectura_E2K.Entidades;
using DisenioZapata_V1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DisenioZapata_V1
{
    public class BuilderZapatas:NotificationObject
    {
        private List<Zapata> zapatas;

        public List<Zapata> Zapatas
        {
            get { return zapatas; }
            set { zapatas = value; OnPropertyChanged(); }
        }

        public void BuildZapatas(List<Fuerzas_Modelo> fuerzas,ETipoZapata tipoZapata,Modelo_Etabs modelo)
        {
            zapatas = new List<Zapata>();
            Zapata zapatai = null;
            var PuntosI = fuerzas.Select(x => x.PointLabel).Distinct().ToList();

            foreach (string Label in PuntosI)
            {
                var FuerzasLabel = fuerzas.FindAll(x => x.PointLabel == Label).ToList();
                var punto = modelo.Points.Find(x => x.Name == Label);
                if(tipoZapata == ETipoZapata.Zapata_Aislada)
                {
                    zapatai = new Zapata_Concentrica(Label , punto , FuerzasLabel);
                }
                Zapatas.Add(zapatai);
            }

        }
    }
}