using System;
using B_Lectura_E2K.Entidades;
using DisenioZapata_V1;
using DisenioZapata_V1.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PruebasUnitarias
{
    [TestClass]
    public class UnitTest1
    {
        public ILectorFuerzas Lector { get; set; }
        public Modelo_Etabs modelo_proyecto { get; set; }
        [TestMethod]
        public void TestMethod1()
        {
            string RutaArchivo = "D:\\Desarrollo_Software\\DisenioZapata_V1\\Modelo Portico_SINpilas_V5.3_COMBO.e2k";
            string RutaFuerzas = "D:\\Desarrollo_Software\\DisenioZapata_V1\\Fuerzas_Etabs.csv";

            modelo_proyecto = new Modelo_Estructura(RutaArchivo).Modelo;
            Lector = new Lector_Fuerzas_Etabs(RutaFuerzas);

            BuilderZapatas builder = new BuilderZapatas();
            builder.BuildZapatas(Lector.Get_Fuerzas(), ETipoZapata.Zapata_Aislada, modelo_proyecto);

            string Palabra = "dabalearrozalazorraelabad";
            var prueba = Palindromo(Palabra);

            Zapata zapataTest = builder.Zapatas[0];
            zapataTest.L1 = 2.0f;
            zapataTest.L2 = 2.0f;
            zapataTest.LcX = 0.50f;
            zapataTest.LcY = 0.50f;
            zapataTest.H = 0.30f;
            zapataTest.R = 0.07f;
            zapataTest.Fc = 210f;
            zapataTest.Fy = 4220f;
            zapataTest.GammaConcreto = 2.4f;
            zapataTest.CalcArea();
            zapataTest.CalcPesoPropio();
            zapataTest.SetCalculos();
            zapataTest.Presiones(zapataTest.L1, zapataTest.L2, zapataTest.H);
            zapataTest.SetCortanteUnidireccional();
            zapataTest.SetCortanteBidireccional();
            zapataTest.SetFlexion();
        }

        public bool Palindromo(string palabra)
        {
            string Temp = "";
            for(int i = palabra.Length-1; i >= 0; i--)
            {
                Temp += palabra.Substring(i, 1);
            }
            if (Temp == palabra)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
