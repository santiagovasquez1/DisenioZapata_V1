using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DisenioZapata_V1.Drawing
{
    public interface IShape
    {
        void Dibujar(DrawingContext dc);
    }
}
