using System;
using System.Collections.Generic;

namespace DisenioZapata_V1.Model
{
    [Serializable]
    public class Propiedades_Refuerzo:NotificationObject
    {
        [NonSerialized]
        private Dictionary<string, float> as_refuerzo;

        public Dictionary<string, float> As_refuerzo
        {
            get { return as_refuerzo; }
            set { as_refuerzo = value; OnPropertyChanged(); }
        }

        [field: NonSerialized]
        private Dictionary<string,float> long_gancho90;

        public Dictionary<string,float> Long_Gancho90
        {
            get { return long_gancho90; }
            set { long_gancho90 = value;OnPropertyChanged(); }
        }
       
        public Propiedades_Refuerzo()
        {
            Set_As_Refuerzo();
            Set_G90();
        }

        private void Set_As_Refuerzo()
        {
            As_refuerzo = new Dictionary<string, float>
            {
                { "#4", 1.29f },
                { "#5", 1.99f },
                { "#6", 2.84f },
                { "#7", 3.87f },
                { "#8", 5.10f }
            };
        }

        private void Set_G90()
        {
            Long_Gancho90 = new Dictionary<string, float>()
            {
                {"#3", 0.14f },
                {"#4", 0.167f },
                {"#5", 0.192f },
                {"#6", 0.228f },
                {"#7", 0.266f },
                {"#8", 0.305f },
            };
        }
    }
}