using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ballon
{
    public partial class Form1 : Form
    {
        private Balloon ballon;
        private Graphics tekenGebied;

        public Form1()
        {
            InitializeComponent();

            ballon = new Balloon();
            tekenGebied = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ballon.MoveRight(20);
            tekenGebied.Clear(Color.White);
            ballon.Display(tekenGebied);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ballon.ChangeSize(20);
            tekenGebied.Clear(Color.White);
            ballon.Display(tekenGebied);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ballon.MoveLeft(20);
            tekenGebied.Clear(Color.White);
            ballon.Display(tekenGebied);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ballon.MoveUp(20);
            tekenGebied.Clear(Color.White);
            ballon.Display(tekenGebied);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ballon.MoveDown(20);
            tekenGebied.Clear(Color.White);
            ballon.Display(tekenGebied);
        }
    }
}
