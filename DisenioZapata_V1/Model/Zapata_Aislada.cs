using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace DisenioZapata_V1.Model
{
    internal class Zapata_Aislada : Zapata
    {
        public Zapata_Aislada(string nombre, MPoint point, List<Fuerzas_Modelo> fuerzas, Suelo sueloi)
        {
            Suelo = sueloi;
            Label = nombre;
            Point = point;
            Fuerzas = fuerzas;
            H = 0.25f;
            CalcArea();
            CalcPesoPropio();
            GammaConcreto = 2.4f;
        }

        public override void SetCalculos()
        {
            Dimensionamiento = new Dimensionamiento(this);
            CortanteBiridireccional = new CortanteBiridireccional(this);
            CortanteUnidireccional = new CortanteUnidireccional(this);
            Flexion = new Flexion(this);
        }
    }
}