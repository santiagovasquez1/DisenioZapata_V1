using B_Lectura_E2K.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    class Zapata_Concentrica:Zapata
    {
        public Zapata_Concentrica(string nombre, MPoint point, List<Fuerzas_Modelo>fuerzas)
        {
            Label = nombre;
            Point = point;
            Fuerzas = fuerzas;
        }
    }
}
