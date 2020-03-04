using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    public class Flexion:ICalculo
    {
        private Zapata zapata;

        public Zapata Zapata
        {
            get { return zapata; }
            set { zapata = value; }
        }

        public void Calculo_Clase()
        {
            throw new NotImplementedException();
        }

        public void Chequeos_Clase()
        {
            throw new NotImplementedException();
        }
    }
}
