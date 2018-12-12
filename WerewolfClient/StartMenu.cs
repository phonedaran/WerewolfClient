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
    public partial class StartMenu : Form
    {

        public StartMenu()
        {
            InitializeComponent();
        }

        private void StartMenu_Close(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BtnHTP_Click(object sender, EventArgs e)
        {
            HowToPlay m = new HowToPlay();
            m.Show();
            this.Hide();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            MainForm m = new MainForm();
            m.Show();
            this.Close();
        }
    }
}
