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

        private Form _Login;
        public StartMenu(Form m)
        {
            InitializeComponent();
            _Login = m;
        }

        private void StartMenu_Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnHTP_Click(object sender, EventArgs e)
        {
            HowToPlay m = new HowToPlay();
            m.Show();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            //MainForm m = new MainForm();
            //m.Show();
            _Login.Visible = true;
            this.Visible = false;
        }
    }
}
