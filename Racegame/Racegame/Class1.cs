using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    class Autos
    {
        Bitmap[] autos = new Bitmap[2];

        public Bitmap[] GetAutos()
        {
            return autos;
        }
        public void SetAutos(Bitmap[] auto)
        {
            autos = auto;
        }
    }
}
