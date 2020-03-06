using B_Lectura_E2K.Entidades;
using DisenioZapata_V1;
using DisenioZapata_V1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasUnitarias
{
    [TestClass]
    class ZapataTest
    {
        public ILectorFuerzas Lector { get; set; }
        public Modelo_Etabs modelo_proyecto { get; set; }

        [TestMethod]
        public void PruebaCortanteBidireccional()
        {
            string RutaArchivo = "D:\\Desarrollo_Software\\DisenioZapata_V2\\Modelo Portico_SINpilas_V5.3_COMBO.e2k";
            string RutaFuerzas = "D:\\Desarrollo_Software\\DisenioZapata_V1\\Fuerzas_Etabs.csv";

            modelo_proyecto = new Modelo_Estructura(RutaArchivo).Modelo;
            Lector = new Lector_Fuerzas_Etabs(RutaFuerzas);

            BuilderZapatas builder = new BuilderZapatas();
            builder.BuildZapatas(Lector.Get_Fuerzas(), ETipoZapata.Zapata_Aislada, modelo_proyecto);

            Zapata zapataTest = builder.Zapatas[0];
            zapataTest.SetCalculos();


        }
    }
}
