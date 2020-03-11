using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisenioZapata_V1.Model.DatosTablas
{
    public class DatosPresiones : NotificationObject
    {
        private string load;

        public string Load
        {
            get { return load; }
            set { load = value; }
        }

        private float fz;

        public float Fz
        {
            get { return fz; }
            set { fz = value; }
        }

        private float mx;

        public float Mx
        {
            get { return mx; }
            set { mx = value; }
        }

        private float my;

        public float My
        {
            get { return my; }
            set { my = value; }
        }

        private float ex;

        public float Ex
        {
            get { return ex; }
            set { ex = value; OnPropertyChanged(); }
        }

        private float ey;

        public float Ey
        {
            get { return ey; }
            set { ey = value; OnPropertyChanged(); }
        }

        private float qmaxx;

        public float Qmaxx
        {
            get { return qmaxx; }
            set { qmaxx = value; OnPropertyChanged(); }
        }

        private float qmaxy;

        public float Qmaxy
        {
            get { return qmaxy; }
            set { qmaxy = value; OnPropertyChanged(); }
        }

        private float qminx;

        public float Qminx
        {
            get { return qminx; }
            set { qminx = value; OnPropertyChanged(); }
        }

        private float qminy;

        public float Qminy
        {
            get { return qminy; }
            set { qminy = value; OnPropertyChanged(); }
        }

        private string Chequeoex;

        public string Chequeo_Ex
        {
            get { return Chequeoex; }
            set { Chequeoex = value; OnPropertyChanged(); }
        }

        private string Chequeoey;

        public string Chequeo_Ey
        {
            get { return Chequeoey; }
            set { Chequeoey = value; OnPropertyChanged(); }
        }

        public DatosPresiones(string load, float fz, float mx, float my, float qx, float qy, float qminx, float qminy, float ex, float ey, string checkEx, string checkEy)
        {
            Load = load;
            Fz = fz;
            Mx = mx;
            My = my;
            Ex = ex;
            Ey = ey;
            Qmaxx = qx;
            Qmaxy = qy;
            Qminx = qminx;
            Qminy = qminy;
            Chequeo_Ex = checkEx;
            Chequeo_Ey = checkEy;
        }
    }
}
