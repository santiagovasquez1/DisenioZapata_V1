using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using B_Lectura_E2K.Entidades;
using BibliotecaE2K;
using BibliotecaE2K.Core;

namespace DisenioZapata_V1.Model
{
   public class Modelo_Estructura
    {
        public Modelo_Etabs Modelo { get; set; }
        public Modelo_Estructura(string RutaModelo)
        {
            Modelo = new Etabs95(RutaModelo).Modelo;
        }
    }
}
