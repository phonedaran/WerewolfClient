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
            //ตอนนี้ไม่ได้ใช้
          
            this.Close();
        }

        private void HTP_Next(object sender, EventArgs e)
        {
            //เพิ่มรูปตรงตามเลขได้เลย
            HTPstate++;
            if (HTPstate >= 2) BtnBack.Show();
            switch (HTPstate)
            {
                case 2:
                    this.BackgroundImage = Properties.Resources.HTP2;
                    break;
                case 3:
                    this.BackgroundImage = Properties.Resources.HTP3;
                    break;
                case 4:
                    this.BackgroundImage = Properties.Resources.HTP4;
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
                    this.BackgroundImage = Properties.Resources.HTP1;
                    BtnBack.Hide();
                    break;
                case 2:
                    this.BackgroundImage = Properties.Resources.HTP2;
                    break;
                case 3:
                    this.BackgroundImage = Properties.Resources.HTP3;
                    break;
            }
        }
    }
}
