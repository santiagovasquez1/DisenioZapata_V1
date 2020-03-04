using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public class CortanteUnidireccional:ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }
        public List<float> VuX { get; set; }
        public List<float> VuY { get; set; }
        public List<float> euX { get; set; }
        public List<float> euY { get; set; }
        public List<float> Fvc { get; set; }
        public void Calculo_Clase()
        {
            VuX=new List<float>();
            VuY=new List<float>();
            euX=new List<float>();
            euY=new List<float>();
            Fvc = new List<float>();

            foreach (var fuerza in zapata.Fuerzas)
            {
                
            }
        }

        public void Chequeos_Clase()
        {
            throw new NotImplementedException();
        }
    }
}
