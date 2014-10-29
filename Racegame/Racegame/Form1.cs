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
        private SoundPlayer _soundPlayer;

        public Form1()
        {
            InitializeComponent();

            _soundPlayer = new SoundPlayer(@"C:\Users\Rutger\Documents\GitHub\racegame\Racegame\Racegame\Resources\8bit.wav");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            _soundPlayer.Play();
        }
    }
}
