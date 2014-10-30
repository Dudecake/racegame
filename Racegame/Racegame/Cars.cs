using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class Cars
    {
        Bitmap[] autos;
 
        public Bitmap[] GetAutos()
        {
            return autos;
        }
        public void SetAutos(Bitmap[] autos)
        {
            this.autos = autos;
        }
    }
}
