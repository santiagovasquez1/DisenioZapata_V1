using B_Lectura_E2K.Entidades;
using System.Collections.Generic;

namespace DisenioZapata_V1.Model
{
    internal class Zapata_Aislada : Zapata
    {
        public Zapata_Aislada(string nombre, MPoint point, List<Fuerzas_Modelo> fuerzas)
        {
            Label = nombre;
            Point = point;
            Fuerzas = fuerzas;
            Calculos = new List<ICalculo>();
        }

        public override void SetCalculos()
        {
            Dimensionamiento dimensionamiento = new Dimensionamiento(this);
            CortanteBiridireccional cortanteBiridireccional = new CortanteBiridireccional(this);
            CortanteUnidireccional cortanteUnidireccional = new CortanteUnidireccional(this);
            Flexion flexion = new Flexion(this);

            Calculos.AddRange(new ICalculo[] { dimensionamiento, cortanteUnidireccional, cortanteBiridireccional, cortanteUnidireccional });
        }
    }
}