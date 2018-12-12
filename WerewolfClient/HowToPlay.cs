using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WerewolfClient
{
    public partial class HowToPlay : Form
    {

        public int HTPstate = 1;
        public HowToPlay()
        {
            InitializeComponent();
            if (HTPstate == 1) BtnBack.Hide();
        }

        private void HTP_Close(object sender, EventArgs e)
        {
            StartMenu m = new StartMenu();
            m.Show();
            this.Close();
        }

        private void HTP_Next(object sender, EventArgs e)
        {
            HTPstate++;
            if (HTPstate >= 2) BtnBack.Show();
            switch (HTPstate)
            {
                case 2:
                    this.BackgroundImage = Properties.Resources.Night_time;
                    break;
                case 3:
                    this.BackgroundImage = Properties.Resources.Day_time;
                    break;
                case 4:
                    this.BackgroundImage = Properties.Resources.Icon_alpha_werewolf;
                    BtnNext.Hide();
                    break;
            }
        }

        private void HTP_Back(object sender, EventArgs e)
        {
            HTPstate--;
            if (HTPstate <= 2) BtnNext.Show();
            switch (HTPstate)
            {
                case 1:
                    this.BackgroundImage = Properties.Resources.Icon_seer;
                    BtnBack.Hide();
                    break;
                case 2:
                    this.BackgroundImage = Properties.Resources.Night_time;
                    break;
                case 3:
                    this.BackgroundImage = Properties.Resources.Day_time;
                    break;
            }
        }
    }
}
