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
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    autos[1] = Properties.Resources._61px_Jefferson_GTA2;
                    break;
                case 1:
                    autos[2] = Properties.Resources._62px_AnistonBD4_GTA2;
                    break;
                case 2:
                    autos[3] = Properties.Resources._62px_Arachnid_GTA2;
                    break;
                case 3:
                    autos[4] = Properties.Resources._62px_Stinger_GTA2;
                    break;
                case 4:
                    autos[5] = Properties.Resources._63px_A_Type_GTA2;
                    break;
                case 5:
                    autos[6] = Properties.Resources._63px_Beamer_GTA2;
                    break;
                case 6:
                    autos[7] = Properties.Resources._63px_FuroreGT_GTA2;
                    break;
                case 7:
                    autos[8] = Properties.Resources._63px_MichelliRoadster_GTA2;
                    break;
                case 8:
                    autos[9] = Properties.Resources._64px_Benson_GTA2;
                    break;
                case 9:
                    autos[10] = Properties.Resources._64px_Hachura_GTA2;
                    break;
                case 10:
                    autos[11] = Properties.Resources._64px_Rumbler_GTA2;
                    break;
                case 11:
                    autos[12] = Properties.Resources._64px_TranceAM_GTA2;
                    break;
                case 12:
                    autos[13] = Properties.Resources._64px_Wellard_GTA2;
                    break;
                case 13:
                    autos[14] = Properties.Resources.ArmedLandRoamer_GTA2;
                    break;
                case 14:
                    autos[15] = Properties.Resources.B_Type_GTA2;
                    break;
                case 15:
                    autos[16] = Properties.Resources.BigBug_GTA2;
                    break;
                case 16:
                    autos[17] = Properties.Resources.BoxTruck_GTA2;
                    break;
                case 17:
                    autos[18] = Properties.Resources.Bug_GTA2;
                    break;
                case 18:
                    autos[19] = Properties.Resources.Bulwark_GTA2;
                    break;
                case 19:
                    autos[20] = Properties.Resources.Bus_GTA2;
                    break;
                case 20:
                    autos[21] = Properties.Resources.CopCar_GTA2;
                    break;
                case 21:
                    autos[22] = Properties.Resources.Dementia_GTA2;
                    break;
                case 22:
                    autos[23] = Properties.Resources.DementiaLimousine_GTA2;
                    break;
                case 23:
                    autos[24] = Properties.Resources.Eddy_GTA2;
                    break;
                case 24:
                    autos[25] = Properties.Resources.FireTruck_GTA2;
                    break;
                case 25:
                    autos[26] = Properties.Resources.G4BankVan_GTA2;
                    break;
                case 26:
                    autos[27] = Properties.Resources.GarbageTruck_GTA2;
                    break;
                case 27:
                    autos[28] = Properties.Resources.GT_A1_GTA2;
                    break;
                case 28:
                    autos[29] = Properties.Resources.HotDogVan_GTA2;
                    break;
                case 29:
                    autos[30] = Properties.Resources.Ice_CreamVan_GTA2;
                    break;
                case 30:
                    autos[31] = Properties.Resources.JagularXK_GTA2;
                    break;
                case 31:
                    autos[32] = Properties.Resources.KarmaBus_GTA2;
                    break;
                case 32:
                    autos[33] = Properties.Resources.LandRoamer_GTA2;
                    break;
                case 33:
                    autos[34] = Properties.Resources.Maurice_GTA2;
                    break;
                case 34:
                    autos[35] = Properties.Resources.Medicar_GTA2;
                    break;
                case 35:
                    autos[36] = Properties.Resources.Meteor_GTA2;
                    break;
                case 36:
                    autos[37] = Properties.Resources.Miara_GTA2;
                    break;
                case 37:
                    autos[38] = Properties.Resources.Minx_GTA2;
                    break;
                case 38:
                    autos[39] = Properties.Resources.Morton_GTA2;
                    break;
                case 39:
                    autos[40] = Properties.Resources.Pacifier_GTA2;
                    break;
                case 40:
                    autos[41] = Properties.Resources.Panto_GTA2;
                    break;
                case 41:
                    autos[42] = Properties.Resources.Pickup_GTA2;
                    break;
                case 42:
                    autos[43] = Properties.Resources.Pickup_GTA2_gang;
                    break;
                case 43:
                    autos[44] = Properties.Resources.Romero_GTA2;
                    break;
                case 44:
                    autos[45] = Properties.Resources.Schmidt_GTA2;
                    break;
                case 45:
                    autos[46] = Properties.Resources.Shark_GTA2;
                    break;
                case 46:
                    autos[47] = Properties.Resources.SpecialAgentCar_GTA2;
                    break;
                case 47:
                    autos[48] = Properties.Resources.SportsLimousine_GTA2;
                    break;
                case 48:
                    autos[49] = Properties.Resources.Spritzer_GTA2;
                    break;
                case 49:
                    autos[50] = Properties.Resources.StretchLimousine_GTA2;
                    break;
                case 50:
                    autos[51] = Properties.Resources.SwatVan_GTA2;
                    break;
                case 51:
                    autos[52] = Properties.Resources.T_Rex_GTA2;
                    break;
                case 52:
                    autos[53] = Properties.Resources.Tank_GTA2;
                    break;
                case 53:
                    autos[54] = Properties.Resources.Taxi_GTA2;
                    break;
                case 54:
                    autos[55] = Properties.Resources.TaxiXpress_GTA2;
                    break;
                case 55:
                    autos[56] = Properties.Resources.TowTruck_GTA2;
                    break;
                case 56:
                    autos[57] = Properties.Resources.TruckCab_GTA2;
                    break;
                case 57:
                    autos[58] = Properties.Resources.TruckCabSX_GTA2;
                    break;
                case 58:
                    autos[59] = Properties.Resources.TVVan_GTA2;
                    break;
                case 59:
                    autos[60] = Properties.Resources.U_JerkTruck_GTA2;
                    break;
                case 60:
                    autos[61] = Properties.Resources.Van_GTA2;
                    break;
                case 61:
                    autos[62] = Properties.Resources.Z_Type_GTA2;
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    autos[1] = Properties.Resources._61px_Jefferson_GTA2;
                    break;
                case 1:
                    autos[2] = Properties.Resources._62px_AnistonBD4_GTA2;
                    break;
                case 2:
                    autos[3] = Properties.Resources._62px_Arachnid_GTA2;
                    break;
                case 3:
                    autos[4] = Properties.Resources._62px_Stinger_GTA2;
                    break;
                case 4:
                    autos[5] = Properties.Resources._63px_A_Type_GTA2;
                    break;
                case 5:
                    autos[6] = Properties.Resources._63px_Beamer_GTA2;
                    break;
                case 6:
                    autos[7] = Properties.Resources._63px_FuroreGT_GTA2;
                    break;
                case 7:
                    autos[8] = Properties.Resources._63px_MichelliRoadster_GTA2;
                    break;
                case 8:
                    autos[9] = Properties.Resources._64px_Benson_GTA2;
                    break;
                case 9:
                    autos[10] = Properties.Resources._64px_Hachura_GTA2;
                    break;
                case 10:
                    autos[11] = Properties.Resources._64px_Rumbler_GTA2;
                    break;
                case 11:
                    autos[12] = Properties.Resources._64px_TranceAM_GTA2;
                    break;
                case 12:
                    autos[13] = Properties.Resources._64px_Wellard_GTA2;
                    break;
                case 13:
                    autos[14] = Properties.Resources.ArmedLandRoamer_GTA2;
                    break;
                case 14:
                    autos[15] = Properties.Resources.B_Type_GTA2;
                    break;
                case 15:
                    autos[16] = Properties.Resources.BigBug_GTA2;
                    break;
                case 16:
                    autos[17] = Properties.Resources.BoxTruck_GTA2;
                    break;
                case 17:
                    autos[18] = Properties.Resources.Bug_GTA2;
                    break;
                case 18:
                    autos[19] = Properties.Resources.Bulwark_GTA2;
                    break;
                case 19:
                    autos[20] = Properties.Resources.Bus_GTA2;
                    break;
                case 20:
                    autos[21] = Properties.Resources.CopCar_GTA2;
                    break;
                case 21:
                    autos[22] = Properties.Resources.Dementia_GTA2;
                    break;
                case 22:
                    autos[23] = Properties.Resources.DementiaLimousine_GTA2;
                    break;
                case 23:
                    autos[24] = Properties.Resources.Eddy_GTA2;
                    break;
                case 24:
                    autos[25] = Properties.Resources.FireTruck_GTA2;
                    break;
                case 25:
                    autos[26] = Properties.Resources.G4BankVan_GTA2;
                    break;
                case 26:
                    autos[27] = Properties.Resources.GarbageTruck_GTA2;
                    break;
                case 27:
                    autos[28] = Properties.Resources.GT_A1_GTA2;
                    break;
                case 28:
                    autos[29] = Properties.Resources.HotDogVan_GTA2;
                    break;
                case 29:
                    autos[30] = Properties.Resources.Ice_CreamVan_GTA2;
                    break;
                case 30:
                    autos[31] = Properties.Resources.JagularXK_GTA2;
                    break;
                case 31:
                    autos[32] = Properties.Resources.KarmaBus_GTA2;
                    break;
                case 32:
                    autos[33] = Properties.Resources.LandRoamer_GTA2;
                    break;
                case 33:
                    autos[34] = Properties.Resources.Maurice_GTA2;
                    break;
                case 34:
                    autos[35] = Properties.Resources.Medicar_GTA2;
                    break;
                case 35:
                    autos[36] = Properties.Resources.Meteor_GTA2;
                    break;
                case 36:
                    autos[37] = Properties.Resources.Miara_GTA2;
                    break;
                case 37:
                    autos[38] = Properties.Resources.Minx_GTA2;
                    break;
                case 38:
                    autos[39] = Properties.Resources.Morton_GTA2;
                    break;
                case 39:
                    autos[40] = Properties.Resources.Pacifier_GTA2;
                    break;
                case 40:
                    autos[41] = Properties.Resources.Panto_GTA2;
                    break;
                case 41:
                    autos[42] = Properties.Resources.Pickup_GTA2;
                    break;
                case 42:
                    autos[43] = Properties.Resources.Pickup_GTA2_gang;
                    break;
                case 43:
                    autos[44] = Properties.Resources.Romero_GTA2;
                    break;
                case 44:
                    autos[45] = Properties.Resources.Schmidt_GTA2;
                    break;
                case 45:
                    autos[46] = Properties.Resources.Shark_GTA2;
                    break;
                case 46:
                    autos[47] = Properties.Resources.SpecialAgentCar_GTA2;
                    break;
                case 47:
                    autos[48] = Properties.Resources.SportsLimousine_GTA2;
                    break;
                case 48:
                    autos[49] = Properties.Resources.Spritzer_GTA2;
                    break;
                case 49:
                    autos[50] = Properties.Resources.StretchLimousine_GTA2;
                    break;
                case 50:
                    autos[51] = Properties.Resources.SwatVan_GTA2;
                    break;
                case 51:
                    autos[52] = Properties.Resources.T_Rex_GTA2;
                    break;
                case 52:
                    autos[53] = Properties.Resources.Tank_GTA2;
                    break;
                case 53:
                    autos[54] = Properties.Resources.Taxi_GTA2;
                    break;
                case 54:
                    autos[55] = Properties.Resources.TaxiXpress_GTA2;
                    break;
                case 55:
                    autos[56] = Properties.Resources.TowTruck_GTA2;
                    break;
                case 56:
                    autos[57] = Properties.Resources.TruckCab_GTA2;
                    break;
                case 57:
                    autos[58] = Properties.Resources.TruckCabSX_GTA2;
                    break;
                case 58:
                    autos[59] = Properties.Resources.TVVan_GTA2;
                    break;
                case 59:
                    autos[60] = Properties.Resources.U_JerkTruck_GTA2;
                    break;
                case 60:
                    autos[61] = Properties.Resources.Van_GTA2;
                    break;
                    case 61:
                    autos[62] = Properties.Resources.Z_Type_GTA2;
                    break;
            }
        }
    }
}
