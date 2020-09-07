using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using B_Lectura_E2K.Entidades;
using BibliotecaE2K;
using BibliotecaE2K.Core;

namespace DisenioZapata_V1.Model
{
    [Serializable]
   public class Modelo_Estructura
    {
        [field:NonSerialized]
        public Modelo_Etabs Modelo { get; set; }
        public Modelo_Estructura(string RutaModelo)
        {
            //Regex.IsMatch(x, "SECTION")
            Modelo = new Etabs2018(RutaModelo).Modelo;
        }
    }
}
