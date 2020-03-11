using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
   public interface ICalculo
    {
        Zapata Zapata { get; set; }
        void Calculo_Clase(Fuerzas_Modelo fuerza,int indice);
        void Chequeos_Clase();
    }
}
