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
        Bitmap[] playerAutos = new Bitmap[2];
        Cars cars;

        private SoundPlayer _soundPlayer;

        public Form1(Cars cars)
        {
            InitializeComponent();
            this.cars = cars;
            _soundPlayer = new SoundPlayer(Properties.Resources._8bit);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cars.SetAutos(playerAutos);
            _soundPlayer.Play();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Auto's
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    playerAutos[0] = Properties.Resources._61px_Jefferson_GTA2;
                    break;
                case 1:
                    playerAutos[0] = Properties.Resources._62px_AnistonBD4_GTA2;
                    break;
                case 2:
                    playerAutos[0] = Properties.Resources._62px_Arachnid_GTA2;
                    break;
                case 3:
                    playerAutos[0] = Properties.Resources._62px_Stinger_GTA2;
                    break;
                case 4:
                    playerAutos[0] = Properties.Resources._63px_A_Type_GTA2;
                    break;
                case 5:
                    playerAutos[0] = Properties.Resources._63px_Beamer_GTA2;
                    break;
                case 6:
                    playerAutos[0] = Properties.Resources._63px_FuroreGT_GTA2;
                    break;
                case 7:
                    playerAutos[0] = Properties.Resources._63px_MichelliRoadster_GTA2;
                    break;
                case 8:
                    playerAutos[0] = Properties.Resources._64px_Benson_GTA2;
                    break;
                case 9:
                    playerAutos[0] = Properties.Resources._64px_Hachura_GTA2;
                    break;
                case 10:
                    playerAutos[0] = Properties.Resources._64px_Rumbler_GTA2;
                    break;
                case 11:
                    playerAutos[0] = Properties.Resources._64px_TranceAM_GTA2;
                    break;
                case 12:
                    playerAutos[0] = Properties.Resources._64px_Wellard_GTA2;
                    break;
                case 13:
                    playerAutos[0] = Properties.Resources.ArmedLandRoamer_GTA2;
                    break;
                case 14:
                    playerAutos[0] = Properties.Resources.B_Type_GTA2;
                    break;
                case 15:
                    playerAutos[0] = Properties.Resources.BigBug_GTA2;
                    break;
                case 16:
                    playerAutos[0] = Properties.Resources.BoxTruck_GTA2;
                    break;
                case 17:
                    playerAutos[0] = Properties.Resources.Bug_GTA2;
                    break;
                case 18:
                    playerAutos[0] = Properties.Resources.Bulwark_GTA2;
                    break;
                case 19:
                    playerAutos[0] = Properties.Resources.Bus_GTA2;
                    break;
                case 20:
                    playerAutos[0] = Properties.Resources.CopCar_GTA2;
                    break;
                case 21:
                    playerAutos[0] = Properties.Resources.Dementia_GTA2;
                    break;
                case 22:
                    playerAutos[0] = Properties.Resources.DementiaLimousine_GTA2;
                    break;
                case 23:
                    playerAutos[0] = Properties.Resources.Eddy_GTA2;
                    break;
                case 24:
                    playerAutos[0] = Properties.Resources.FireTruck_GTA2;
                    break;
                case 25:
                    playerAutos[0] = Properties.Resources.G4BankVan_GTA2;
                    break;
                case 26:
                    playerAutos[0] = Properties.Resources.GarbageTruck_GTA2;
                    break;
                case 27:
                    playerAutos[0] = Properties.Resources.GT_A1_GTA2;
                    break;
                case 28:
                    playerAutos[0] = Properties.Resources.HotDogVan_GTA2;
                    break;
                case 29:
                    playerAutos[0] = Properties.Resources.Ice_CreamVan_GTA2;
                    break;
                case 30:
                    playerAutos[0] = Properties.Resources.JagularXK_GTA2;
                    break;
                case 31:
                    playerAutos[0] = Properties.Resources.KarmaBus_GTA2;
                    break;
                case 32:
                    playerAutos[0] = Properties.Resources.LandRoamer_GTA2;
                    break;
                case 33:
                    playerAutos[0] = Properties.Resources.Maurice_GTA2;
                    break;
                case 34:
                    playerAutos[0] = Properties.Resources.Medicar_GTA2;
                    break;
                case 35:
                    playerAutos[0] = Properties.Resources.Meteor_GTA2;
                    break;
                case 36:
                    playerAutos[0] = Properties.Resources.Miara_GTA2;
                    break;
                case 37:
                    playerAutos[0] = Properties.Resources.Minx_GTA2;
                    break;
                case 38:
                    playerAutos[0] = Properties.Resources.Morton_GTA2;
                    break;
                case 39:
                    playerAutos[0] = Properties.Resources.Pacifier_GTA2;
                    break;
                case 40:
                    playerAutos[0] = Properties.Resources.Panto_GTA2;
                    break;
                case 41:
                    playerAutos[0] = Properties.Resources.Pickup_GTA2;
                    break;
                case 42:
                    playerAutos[0] = Properties.Resources.Pickup_GTA2_gang;
                    break;
                case 43:
                    playerAutos[0] = Properties.Resources.Romero_GTA2;
                    break;
                case 44:
                    playerAutos[0] = Properties.Resources.Schmidt_GTA2;
                    break;
                case 45:
                    playerAutos[0] = Properties.Resources.Shark_GTA2;
                    break;
                case 46:
                    playerAutos[0] = Properties.Resources.SpecialAgentCar_GTA2;
                    break;
                case 47:
                    playerAutos[0] = Properties.Resources.SportsLimousine_GTA2;
                    break;
                case 48:
                    playerAutos[0] = Properties.Resources.Spritzer_GTA2;
                    break;
                case 49:
                    playerAutos[0] = Properties.Resources.StretchLimousine_GTA2;
                    break;
                case 50:
                    playerAutos[0] = Properties.Resources.SwatVan_GTA2;
                    break;
                case 51:
                    playerAutos[0] = Properties.Resources.T_Rex_GTA2;
                    break;
                case 52:
                    playerAutos[0] = Properties.Resources.Tank_GTA2;
                    break;
                case 53:
                    playerAutos[0] = Properties.Resources.Taxi_GTA2;
                    break;
                case 54:
                    playerAutos[0] = Properties.Resources.TaxiXpress_GTA2;
                    break;
                case 55:
                    playerAutos[0] = Properties.Resources.TowTruck_GTA2;
                    break;
                case 56:
                    playerAutos[0] = Properties.Resources.TruckCab_GTA2;
                    break;
                case 57:
                    playerAutos[0] = Properties.Resources.TruckCabSX_GTA2;
                    break;
                case 58:
                    playerAutos[0] = Properties.Resources.TVVan_GTA2;
                    break;
                case 59:
                    playerAutos[0] = Properties.Resources.U_JerkTruck_GTA2;
                    break;
                case 60:
                    playerAutos[0] = Properties.Resources.Van_GTA2;
                    break;
                case 61:
                    playerAutos[0] = Properties.Resources.Z_Type_GTA2;
                    break;
            }
#endregion
            if (comboBox2.SelectedIndex != -1) button1.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Auto's
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    playerAutos[1] = Properties.Resources._61px_Jefferson_GTA2;
                    break;
                case 1:
                    playerAutos[1] = Properties.Resources._62px_AnistonBD4_GTA2;
                    break;
                case 2:
                    playerAutos[1] = Properties.Resources._62px_Arachnid_GTA2;
                    break;
                case 3:
                    playerAutos[1] = Properties.Resources._62px_Stinger_GTA2;
                    break;
                case 4:
                    playerAutos[1] = Properties.Resources._63px_A_Type_GTA2;
                    break;
                case 5:
                    playerAutos[1] = Properties.Resources._63px_Beamer_GTA2;
                    break;
                case 6:
                    playerAutos[1] = Properties.Resources._63px_FuroreGT_GTA2;
                    break;
                case 7:
                    playerAutos[1] = Properties.Resources._63px_MichelliRoadster_GTA2;
                    break;
                case 8:
                    playerAutos[1] = Properties.Resources._64px_Benson_GTA2;
                    break;
                case 9:
                    playerAutos[1] = Properties.Resources._64px_Hachura_GTA2;
                    break;
                case 10:
                    playerAutos[1] = Properties.Resources._64px_Rumbler_GTA2;
                    break;
                case 11:
                    playerAutos[1] = Properties.Resources._64px_TranceAM_GTA2;
                    break;
                case 12:
                    playerAutos[1] = Properties.Resources._64px_Wellard_GTA2;
                    break;
                case 13:
                    playerAutos[1] = Properties.Resources.ArmedLandRoamer_GTA2;
                    break;
                case 14:
                    playerAutos[1] = Properties.Resources.B_Type_GTA2;
                    break;
                case 15:
                    playerAutos[1] = Properties.Resources.BigBug_GTA2;
                    break;
                case 16:
                    playerAutos[1] = Properties.Resources.BoxTruck_GTA2;
                    break;
                case 17:
                    playerAutos[1] = Properties.Resources.Bug_GTA2;
                    break;
                case 18:
                    playerAutos[1] = Properties.Resources.Bulwark_GTA2;
                    break;
                case 19:
                    playerAutos[1] = Properties.Resources.Bus_GTA2;
                    break;
                case 20:
                    playerAutos[1] = Properties.Resources.CopCar_GTA2;
                    break;
                case 21:
                    playerAutos[1] = Properties.Resources.Dementia_GTA2;
                    break;
                case 22:
                    playerAutos[1] = Properties.Resources.DementiaLimousine_GTA2;
                    break;
                case 23:
                    playerAutos[1] = Properties.Resources.Eddy_GTA2;
                    break;
                case 24:
                    playerAutos[1] = Properties.Resources.FireTruck_GTA2;
                    break;
                case 25:
                    playerAutos[1] = Properties.Resources.G4BankVan_GTA2;
                    break;
                case 26:
                    playerAutos[1] = Properties.Resources.GarbageTruck_GTA2;
                    break;
                case 27:
                    playerAutos[1] = Properties.Resources.GT_A1_GTA2;
                    break;
                case 28:
                    playerAutos[1] = Properties.Resources.HotDogVan_GTA2;
                    break;
                case 29:
                    playerAutos[1] = Properties.Resources.Ice_CreamVan_GTA2;
                    break;
                case 30:
                    playerAutos[1] = Properties.Resources.JagularXK_GTA2;
                    break;
                case 31:
                    playerAutos[1] = Properties.Resources.KarmaBus_GTA2;
                    break;
                case 32:
                    playerAutos[1] = Properties.Resources.LandRoamer_GTA2;
                    break;
                case 33:
                    playerAutos[1] = Properties.Resources.Maurice_GTA2;
                    break;
                case 34:
                    playerAutos[1] = Properties.Resources.Medicar_GTA2;
                    break;
                case 35:
                    playerAutos[1] = Properties.Resources.Meteor_GTA2;
                    break;
                case 36:
                    playerAutos[1] = Properties.Resources.Miara_GTA2;
                    break;
                case 37:
                    playerAutos[1] = Properties.Resources.Minx_GTA2;
                    break;
                case 38:
                    playerAutos[1] = Properties.Resources.Morton_GTA2;
                    break;
                case 39:
                    playerAutos[1] = Properties.Resources.Pacifier_GTA2;
                    break;
                case 40:
                    playerAutos[1] = Properties.Resources.Panto_GTA2;
                    break;
                case 41:
                    playerAutos[1] = Properties.Resources.Pickup_GTA2;
                    break;
                case 42:
                    playerAutos[1] = Properties.Resources.Pickup_GTA2_gang;
                    break;
                case 43:
                    playerAutos[1] = Properties.Resources.Romero_GTA2;
                    break;
                case 44:
                    playerAutos[1] = Properties.Resources.Schmidt_GTA2;
                    break;
                case 45:
                    playerAutos[1] = Properties.Resources.Shark_GTA2;
                    break;
                case 46:
                    playerAutos[1] = Properties.Resources.SpecialAgentCar_GTA2;
                    break;
                case 47:
                    playerAutos[1] = Properties.Resources.SportsLimousine_GTA2;
                    break;
                case 48:
                    playerAutos[1] = Properties.Resources.Spritzer_GTA2;
                    break;
                case 49:
                    playerAutos[1] = Properties.Resources.StretchLimousine_GTA2;
                    break;
                case 50:
                    playerAutos[1] = Properties.Resources.SwatVan_GTA2;
                    break;
                case 51:
                    playerAutos[1] = Properties.Resources.T_Rex_GTA2;
                    break;
                case 52:
                    playerAutos[1] = Properties.Resources.Tank_GTA2;
                    break;
                case 53:
                    playerAutos[1] = Properties.Resources.Taxi_GTA2;
                    break;
                case 54:
                    playerAutos[1] = Properties.Resources.TaxiXpress_GTA2;
                    break;
                case 55:
                    playerAutos[1] = Properties.Resources.TowTruck_GTA2;
                    break;
                case 56:
                    playerAutos[1] = Properties.Resources.TruckCab_GTA2;
                    break;
                case 57:
                    playerAutos[1] = Properties.Resources.TruckCabSX_GTA2;
                    break;
                case 58:
                    playerAutos[1] = Properties.Resources.TVVan_GTA2;
                    break;
                case 59:
                    playerAutos[1] = Properties.Resources.U_JerkTruck_GTA2;
                    break;
                case 60:
                    playerAutos[1] = Properties.Resources.Van_GTA2;
                    break;
                    case 61:
                    playerAutos[1] = Properties.Resources.Z_Type_GTA2;
                    break;
            }
#endregion
            if (comboBox1.SelectedIndex != -1) button1.Enabled = true;
        }
    }
}
