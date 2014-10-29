using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Bitmap[] autos = new Bitmap[2];

        private SoundPlayer _soundPlayer;

        public Form1()
        {
            InitializeComponent();

            _soundPlayer = new SoundPlayer(Properties.Resources._8bit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _soundPlayer.Play();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 1:
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    autos[0] = Properties.Resources._61px_Jefferson_GTA2;
                    break;
            }
        }
    }
}
