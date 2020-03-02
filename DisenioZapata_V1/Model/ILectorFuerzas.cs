using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public interface ILectorFuerzas
    {
        void LectorArchivo(string RutaArchivo);
        List<Fuerzas_Modelo> Get_Fuerzas();
    }
}
