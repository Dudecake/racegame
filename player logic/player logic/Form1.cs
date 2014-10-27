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
        Stopwatch stopHorloge;
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
            stopHorloge = new Stopwatch();
            Timesp1 = new System.TimeSpan[3];
            Timesp2 = new System.TimeSpan[3];
            Alpha = false;
            Beta = false;
            Gamma = false;
            Delta = false;
            Phi = false;
            Omega = false;
            label11.Visible = false;
            label11.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopWatch.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopWatch.Stop();
            bool a = true;
            if(a)
            {
                Timesp1[2] = stopWatch.Elapsed;
                textBox1.Text = string.Format("{0}", stopWatch.Elapsed);
                Alpha = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stopHorloge.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stopHorloge.Stop();
            bool b = true;
            if(b)
            {
                Timesp2[2] = stopHorloge.Elapsed;
                textBox2.Text = string.Format("{0}", stopHorloge.Elapsed);
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
                label2.Text = string.Format("{0}", stopWatch.Elapsed);
                Timesp1[0] = stopWatch.Elapsed;
                stopWatch.Restart();
                Gamma = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool b = true;
            if(b == true)
            {
                label3.Text = string.Format("{0}", stopWatch.Elapsed);
                Timesp1[1] = stopWatch.Elapsed;
                Delta = true;
                stopWatch.Restart();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool c = true;
            if(c == true)
            {
                label7.Text = string.Format("{0}", stopHorloge.Elapsed);
                Timesp2[0] = stopHorloge.Elapsed;
                Phi = true;
                stopHorloge.Restart();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool d = true;
            if(d == true)
            {
                label8.Text = string.Format("{0}", stopHorloge.Elapsed);
                Timesp2[1] = stopHorloge.Elapsed;
                Omega = true;
                stopHorloge.Restart();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Timesp1.Min();
            label9.Text = String.Format("{0}", Timesp1.Min());
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Timesp2.Min();
            label10.Text = String.Format("{0}", Timesp2.Min());
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(Alpha && Beta && Gamma && Delta && Phi && Omega)
            {
                if(Timesp1.Min() < Timesp2.Min())
                {
                    label11.Visible = true;
                    label11.Text = "Player 1 has got the best time";
                }
                else if(Timesp2.Min() < Timesp1.Min())
                {
                    label11.Visible = true;
                    label11.Text = "Player 2 has got the best time";
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            stopWatch.Reset();
            stopHorloge.Reset();
            textBox1.Clear();
            textBox2.Clear();
            label2.ResetText();
            label3.ResetText();
            label7.ResetText();
            label8.ResetText();
            label9.ResetText();
            label10.ResetText();
            label11.ResetText();
        }
    }
}
