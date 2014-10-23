using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace collision_map
{
    public partial class Form2 : Form
    {
        Map map;

        public Form2()
        {
            InitializeComponent();
            map = new Map(10); 
        }
    }
}
