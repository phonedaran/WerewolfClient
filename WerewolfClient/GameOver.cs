using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WerewolfAPI.Model;

namespace WerewolfClient
{
    public partial class GameOver : Form
    {
        List<Player> players = null;
        Game _gameOut = null;
        private bool _alreadyAdd = false;
        

        public GameOver()
        {
            InitializeComponent();
        }

        private void WinnerName(Model m)
        {
            if(m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;

                foreach(Player player in wm.Players)
                {
                    string role;
                    role = player.Role.Name;
                    if (!_alreadyAdd)//TEST ยังไม่เสร็จ
                    {
                        switch (_gameOut.Outcome)
                        {
                            case Game.OutcomeEnum.Werewolfwin:
                                if (role == WerewolfModel.ROLE_ALPHA_WEREWOLF) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_WEREWOLF) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_WEREWOLF_SEER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_ALPHA_WEREWOLF) richTextBox1.Text += player.Name;
                                break;
                            case Game.OutcomeEnum.Serialkillerwin:
                                if (role == WerewolfModel.ROLE_SERIAL_KILLER) richTextBox1.Text += player.Name;
                                break;
                            case Game.OutcomeEnum.Villagerwin:
                                //Villager ไม่มี << เพิ่มแล้ว
                                if (role == WerewolfModel.ROLE_VILLAGER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_SEER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_AURA_SEER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_BODYGUARD) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_DOCTOR) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_GUNNER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_JAILER) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_MEDIUM) richTextBox1.Text += player.Name;
                                if (role == WerewolfModel.ROLE_PRIEST) richTextBox1.Text += player.Name;
                                break;
                            case Game.OutcomeEnum.Headhunterwin:
                                if (role == WerewolfModel.ROLE_HEAD_HUNTER) richTextBox1.Text += player.Name;
                                break;
                            case Game.OutcomeEnum.Foolwin:
                                if (role == WerewolfModel.ROLE_FOOL) richTextBox1.Text += player.Name;
                                break;
                            case Game.OutcomeEnum.Nowin:
                                richTextBox1.Text += "No Winer!!!";
                                break;
                        }
                    } _alreadyAdd = true;
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WhoWin(Model m)
        {
            if(m is WerewolfModel)
            {
                switch (_gameOut.Outcome)
                {
                    case Game.OutcomeEnum.Werewolfwin:
                        label2.Text = "Werewolf win";
                        break;
                    case Game.OutcomeEnum.Serialkillerwin:
                        label2.Text = "Seriakiller win";
                        break;
                    case Game.OutcomeEnum.Villagerwin:
                        label2.Text = "Villager win";
                        break;
                    case Game.OutcomeEnum.Headhunterwin:
                        label2.Text = "Head Hunter win";
                        break;
                    case Game.OutcomeEnum.Foolwin:
                        label2.Text = "Fool win";
                        break;
                    case Game.OutcomeEnum.Nowin:
                        label2.Text = "No winner";
                        break;
                }
            }
        }
    }
}
