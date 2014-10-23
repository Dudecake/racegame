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
    public partial class Form1 : Form
    {
        //private BufferedGraphics bfgfx;
        
        public Form1()
        {
            InitializeComponent();
            button1.BackColor = Color.Red;
            Form2 frm2 = new Form2();
            frm2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Visible = true;
        }
    }
}
