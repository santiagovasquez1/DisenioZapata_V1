﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model
{
    [Serializable]
   public  class Fuerzas_Modelo:NotificationObject
    {
        public string Story { get; set; }
        public string PointLabel { get; set; }
        public string Load { get; set; }
        public double Fx { get; set; }
        public double Fy { get; set; }
        public double Fz { get; set; }
        public double Mx { get; set; }
        public double My { get; set; }
        public double Mz { get; set; }

        public override string ToString()
        {
            return $"{PointLabel} {Load}";
        }
    }
}
