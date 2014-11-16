using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace player_logic
{
    public partial class Form1 : Form
    {
        Stopwatch stopWatch;
        public System.TimeSpan[] Timesp1;
        public System.TimeSpan[] Timesp2;
        public bool Alpha;
        public bool Beta;
        public bool Gamma;
        public bool Delta;
        public bool Phi;
        public bool Omega;

        public Form1()
        {
            InitializeComponent();

            stopWatch = new Stopwatch();
            Timesp2 = new System.TimeSpan[3];
            Timesp1 = new System.TimeSpan[3];
            Alpha = false;
            Beta = false;
            Gamma = false;
            Delta = false;
            Phi = false;
            Omega = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopWatch.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool a = true;
            if (a)
            {
                Timesp1[2] = stopWatch.Elapsed;
                textBox1.Text = string.Format("{0}", Timesp1[2]);
                Alpha = true;
            }
            int i = Convert.ToInt32(stopHorloge.Elapsed);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool b = true;
            if (b)
            {
                Timesp2[2] = stopWatch.Elapsed;
                textBox2.Text = string.Format("{0}", Timesp2[2]);
                Beta = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool a = true;
            if (a == true)
            {
                Timesp1[0] = stopWatch.Elapsed;
                label2.Text = string.Format("{0}", Timesp1[0]);
                Gamma = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool b = true;
            if (b == true)
            {
                Timesp1[1] = stopWatch.Elapsed;
                label3.Text = string.Format("{0}", Timesp1[1]);
                Delta = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool c = true;
            if (c == true)
            {
                Timesp2[0] = stopWatch.Elapsed;
                label7.Text = string.Format("{0}", Timesp2[0]);
                Phi = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool d = true;
            if (d == true)
            {
                Timesp2[1] = stopWatch.Elapsed;
                label8.Text = string.Format("{0}", Timesp2[1]);
                Omega = true;
            }
        }
    } 
}
