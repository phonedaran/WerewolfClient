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
        

        public GameOver()
        {
            InitializeComponent();
        }

        private void WinnerName(WerewolfModel wm)
        {
            foreach(Player player in wm.Players)
            {
                string role;
                role = player.Role.Name;

                //TEST
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
                        //BUG Villager ไม่มี
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

                }


                /*
                if(_gameOut.Outcome == Game.OutcomeEnum.Werewolfwin)
                {
                    if (role == WerewolfModel.ROLE_ALPHA_WEREWOLF) richTextBox1.Text += player.Name;
                    if (role == WerewolfModel.ROLE_WEREWOLF) richTextBox1.Text += player.Name;
                    if (role == WerewolfModel.ROLE_WEREWOLF_SEER) richTextBox1.Text += player.Name;
                    if (role == WerewolfModel.ROLE_ALPHA_WEREWOLF) richTextBox1.Text += player.Name;
                }
                */
                if (_gameOut.Outcome == Game.OutcomeEnum.Serialkillerwin)
                {
                    if (role == WerewolfModel.ROLE_ALPHA_WEREWOLF) richTextBox1.Text += player.Name;

                }



            }
        }
    }
}
