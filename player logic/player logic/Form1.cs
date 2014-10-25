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

        public Form1()
        {
            InitializeComponent();

            stopWatch = new Stopwatch();
            stopHorloge = new Stopwatch();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            stopWatch.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stopWatch.Stop();
            textBox1.Text = string.Format("{0}", stopWatch.Elapsed);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stopHorloge.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stopHorloge.Stop();
            textBox2.Text = string.Format("{0}", stopHorloge.Elapsed);
        }
    }
}
