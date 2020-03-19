using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    internal class Zapata_Aislada : Zapata
    {
        public Zapata_Aislada(string nombre, MPoint point, List<Fuerzas_Modelo> fuerzas, Suelo sueloi)
        {            
            Suelo = sueloi;
            Label = nombre;
            Point = point;
            Fuerzas = fuerzas;
            CalcArea();
            GammaConcreto = 2.4f;       
            CalcPesoPropio();          
            ResumenZapata = new ResumenZapata(this);
        }

        public override void SetCalculos()
        {
            Dimensionamientos = new ObservableCollection<Dimensionamiento>();
            CortanteUnidireccional = new ObservableCollection<CortanteUnidireccional>();
            CortanteBiridireccional = new ObservableCollection<CortanteBiridireccional>();
            Flexion = new ObservableCollection<Flexion>();

           for(int i=0;i<Fuerzas.Count;i++)
            {
                Dimensionamientos.Add(new Dimensionamiento(this));
                CortanteUnidireccional.Add(new CortanteUnidireccional(this));
                CortanteBiridireccional.Add(new CortanteBiridireccional(this));
                Flexion.Add(new Flexion(this));
            }
        }
    }
}