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
    public partial class Login : Form, View
    {
        private WerewolfController controller;
        private Form _mainForm;
        public Login(Form MainForm)
        {
            InitializeComponent();
            _mainForm = MainForm;
        }

        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case WerewolfModel.EventEnum.SignIn:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            _mainForm.Visible = true;
                            this.Visible = false;
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case WerewolfModel.EventEnum.SignUp:
                        if (wm.EventPayloads["Success"] == "True")
                        {
                            MessageBox.Show("Sign up successfuly, please login", "Success", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        }
                        else
                        {
                            MessageBox.Show("Login or password incorrect, please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                }
            }
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignIn;
            wcmd.Payloads = new Dictionary<string, string>() { { "Login", TbLogin.Text }, { "Password", TbPassword.Text }, { "Server", TBServer.Text } };
            controller.ActionPerformed(wcmd);
        }

        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = WerewolfCommand.CommandEnum.SignUp;
            wcmd.Payloads = new Dictionary<string, string>() { { "Login", TbLogin.Text}, { "Password",TbPassword.Text}, { "Server", TBServer.Text } };
            controller.ActionPerformed(wcmd);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            //TEST BUTTON

            StartMenu m = new StartMenu();
            m.Show();
            //this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.YellowGreen;
            button3.BackColor = Button.DefaultBackColor;
            button4.BackColor = Button.DefaultBackColor;
            TBServer.Text = "http://project-ile.net:2342/werewolf/";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.BackColor = Button.DefaultBackColor;
            button3.BackColor = Color.YellowGreen;
            button4.BackColor = Button.DefaultBackColor;
            TBServer.Text = "http://project-ile.net:2344/werewolf/";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button2.BackColor = Button.DefaultBackColor;
            button3.BackColor = Button.DefaultBackColor;
            button4.BackColor = Color.YellowGreen;
            TBServer.Text = "http://project-ile.net:23416/werewolf/";
        }
    }
}
