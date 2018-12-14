using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventEnum = WerewolfClient.WerewolfModel.EventEnum;
using CommandEnum = WerewolfClient.WerewolfCommand.CommandEnum;
using WerewolfAPI.Model;
using Role = WerewolfAPI.Model.Role;

using System.Collections;


namespace WerewolfClient
{
    public partial class MainForm : Form, View
    {
        private Timer _updateTimer;
        private WerewolfController controller;
        private Game.PeriodEnum _currentPeriod;
        private int _currentDay;
        private int _currentTime;
        private bool _voteActivated;
        private bool _actionActivated;
        private string _myRole;
        private bool _isDead;
        public List<Player> players = null;
        //Add
        private bool _isEnd = false;
        private bool EmoShow = true;
       
        private int BulletCount = 0;


        public MainForm()
        {
            InitializeComponent();

            Emo_hide();
            foreach (int i in Enumerable.Range(0, 16))
            {
                this.Controls["GBPlayers"].Controls["BtnPlayer" + i].Click += new System.EventHandler(this.BtnPlayerX_Click);
                this.Controls["GBPlayers"].Controls["BtnPlayer" + i].Tag = i;
            }

            _updateTimer = new Timer();
            _voteActivated = false;
            _actionActivated = false;
            EnableButton(BtnJoin, true);
            EnableButton(BtnAction, false);
            EnableButton(BtnVote, false);
            _myRole = null;
            _isDead = false;
        }

        Hashtable emotions;
        void CreateEmotions()
        {
            //Hash รูป
            emotions = new Hashtable(12);
            emotions.Add("[:1]", Properties.Resources.ST1);
            emotions.Add("[:2]", Properties.Resources.ST2);
            emotions.Add("[:3]", Properties.Resources.ST3);
            emotions.Add("[:4]", Properties.Resources.ST4);
            emotions.Add("[:5]", Properties.Resources.ST5);
            emotions.Add("[:6]", Properties.Resources.ST6);
            emotions.Add("[:7]", Properties.Resources.ST7);
            emotions.Add("[:8]", Properties.Resources.ST8);
            emotions.Add("[:9]", Properties.Resources.ST9);
            emotions.Add("[:10]", Properties.Resources.ST10);
            emotions.Add("[:11]", Properties.Resources.ST11);
            emotions.Add("[:12]", Properties.Resources.ST12);
        }

        void AddEmotions()
        {
            //ตัวสร้างรูปในแชท
            foreach (string emote in emotions.Keys)
            {
                while (TbChatBox.Text.Contains(emote))
                {
                    int ind = TbChatBox.Text.IndexOf(emote);
                    TbChatBox.Select(ind, emote.Length);
                    Clipboard.SetImage((Image)emotions[emote]);
                    TbChatBox.Paste();
                    Clipboard.Clear();
                }
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.RequestUpdate;
            controller.ActionPerformed(wcmd);
        }

        public void AddChatMessage(string str)
        {
            TbChatBox.AppendText(str + Environment.NewLine);

        }

        public void EnableButton(Button btn, bool state)
        {
            btn.Visible = btn.Enabled = state;
        }

        private void UpdateAvatar(WerewolfModel wm)
        {
            int i = 0;
            foreach (Player player in wm.Players)
            {
                Controls["GBPlayers"].Controls["BtnPlayer" + i].Text = player.Name;

                Image img = Properties.Resources.Icon_villager;
                if (player.Name == wm.Player.Name || player.Status != Player.StatusEnum.Alive)
                {
                    // FIXME, need to optimize this
                    
                    string role;
                    if (player.Name == wm.Player.Name)
                    {
                        role = _myRole;
                    }
                    else if (player.Role != null)
                    {
                        role = player.Role.Name;
                    }
                    else
                    {
                        continue;
                    }
                    switch (role)
                    {
                        case WerewolfModel.ROLE_SEER:
                            img = Properties.Resources.Icon_seer;
                            break;
                        case WerewolfModel.ROLE_AURA_SEER:
                            img = Properties.Resources.Icon_aura_seer;
                            break;
                        case WerewolfModel.ROLE_PRIEST:
                            img = Properties.Resources.Icon_priest;
                            break;
                        case WerewolfModel.ROLE_DOCTOR:
                            img = Properties.Resources.Icon_doctor;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF:
                            img = Properties.Resources.Icon_werewolf;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SEER:
                            img = Properties.Resources.Icon_wolf_seer;
                            break;
                        case WerewolfModel.ROLE_ALPHA_WEREWOLF:
                            img = Properties.Resources.Icon_alpha_werewolf;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                            img = Properties.Resources.Icon_wolf_shaman;
                            break;
                        case WerewolfModel.ROLE_MEDIUM:
                            img = Properties.Resources.Icon_medium;
                            break;
                        case WerewolfModel.ROLE_BODYGUARD:
                            img = Properties.Resources.Icon_bodyguard;
                            break;
                        case WerewolfModel.ROLE_JAILER:
                            img = Properties.Resources.Icon_jailer;
                            break;
                        case WerewolfModel.ROLE_FOOL:
                            img = Properties.Resources.Icon_fool;
                            break;
                        case WerewolfModel.ROLE_HEAD_HUNTER:
                            img = Properties.Resources.Icon_head_hunter;
                            break;
                        case WerewolfModel.ROLE_SERIAL_KILLER:
                            img = Properties.Resources.Icon_serial_killer;
                            break;
                        case WerewolfModel.ROLE_GUNNER:
                            img = Properties.Resources.Icon_gunner;
                            break;
                    }
                    ((Button)Controls["GBPlayers"].Controls["BtnPlayer" + i]).Image = img;
                }
                //เปลี่ยนรูปตอนตาย
                
                if (player.Status == Player.StatusEnum.Votedead)
                    img = Properties.Resources.RIP2;
                if (player.Status == Player.StatusEnum.Shotdead)
                    img = Properties.Resources.RIP2;
                if (player.Status == Player.StatusEnum.Holydead)
                    img = Properties.Resources.RIP2;
                if (player.Status == Player.StatusEnum.Jaildead)
                    img = Properties.Resources.RIP2;
                if (player.Status == Player.StatusEnum.Killdead)
                    img = Properties.Resources.RIP2;
                    
                ((Button)Controls["GBPlayers"].Controls["BtnPlayer" + i]).Image = img;
                i++;
            }
        }



        public void Notify(Model m)
        {
            CreateEmotions();
            AddEmotions();

            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;

                switch (wm.Event)
                {
                    case EventEnum.JoinGame:

                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            BtnJoin.Visible = false;
                            AddChatMessage("You're joing the game #" + wm.EventPayloads["Game.Id"] + ", please wait for game start.");
                            _updateTimer.Interval = 1000;
                            _updateTimer.Tick += new EventHandler(OnTimerEvent);
                            _updateTimer.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("You can't join the game, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        Emo_hide();
                        break;
                    case EventEnum.GameStopped:
                        AddChatMessage("Game is finished, outcome is " + wm.EventPayloads["Game.Outcome"]);
                        GameOver gov = new GameOver();
                        gov.Show();
                        _updateTimer.Enabled = false;
                        break;

                    case EventEnum.GameStarted:
                        players = wm.Players;
                        _myRole = wm.EventPayloads["Player.Role.Name"];
                        AddChatMessage("Your role is " + _myRole + ".");
                        _currentPeriod = Game.PeriodEnum.Night;

                        NightTime();

                        EnableButton(BtnAction, true);
                        switch (_myRole)
                        {
                            case WerewolfModel.ROLE_PRIEST:
                                BtnAction.Text = WerewolfModel.ACTION_HOLYWATER;
                                break;
                            case WerewolfModel.ROLE_GUNNER:
                                //เพิ่มจำกัดการยิง ยัTEST <<< ใช้ไม่ได้ตรงนี้แค่เปลี่ยนตัวTextปุ่มตอนแรก
                                if(_currentPeriod == Game.PeriodEnum.Day)
                                {
                                    if(BulletCount >= 2)
                                    {
                                        break;
                                    }
                                }
                                BulletCount++;
                                
                                BtnAction.Text = WerewolfModel.ACTION_SHOOT;
                                break;
                            case WerewolfModel.ROLE_JAILER:
                                BtnAction.Text = WerewolfModel.ACTION_JAIL;
                                break;
                            case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                                BtnAction.Text = WerewolfModel.ACTION_ENCHANT;
                                break;
                            case WerewolfModel.ROLE_BODYGUARD:
                                BtnAction.Text = WerewolfModel.ACTION_GUARD;
                                break;
                            case WerewolfModel.ROLE_DOCTOR:
                                BtnAction.Text = WerewolfModel.ACTION_HEAL;
                                break;
                            case WerewolfModel.ROLE_SERIAL_KILLER:
                                BtnAction.Text = WerewolfModel.ACTION_KILL;
                                break;
                            case WerewolfModel.ROLE_SEER:
                            case WerewolfModel.ROLE_WEREWOLF_SEER:
                                BtnAction.Text = WerewolfModel.ACTION_REVEAL;
                                break;
                            case WerewolfModel.ROLE_AURA_SEER:
                                BtnAction.Text = WerewolfModel.ACTION_AURA;
                                break;
                            case WerewolfModel.ROLE_MEDIUM:
                                BtnAction.Text = WerewolfModel.ACTION_REVIVE;
                                break;
                            default:
                                EnableButton(BtnAction, false);
                                break;
                        }
                        EnableButton(BtnVote, true);
                        EnableButton(BtnJoin, false);
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.SwitchToDayTime:
                        //รูปตอนเช้า
                        DayTime();
                        AddChatMessage("Switch to day time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Day;
                        LBPeriod.Text = "Day time of";
                        break;
                    case EventEnum.SwitchToNightTime:
                        //รูปตอนมืด
                        NightTime();
                        AddChatMessage("Switch to night time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Night;
                        LBPeriod.Text = "Night time of";
                        break;
                    case EventEnum.UpdateDay:
                        // TODO  catch parse exception here
                        string tempDay = wm.EventPayloads["Game.Current.Day"];
                        _currentDay = int.Parse(tempDay);
                        LBDay.Text = tempDay;
                        break;
                    case EventEnum.UpdateTime:
                        string tempTime = wm.EventPayloads["Game.Current.Time"];
                        _currentTime = int.Parse(tempTime);
                        LBTime.Text = tempTime;
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.Vote:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your vote is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't vote now.");
                        }
                        break;
                    case EventEnum.Action:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your action is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't perform action now.");
                        }
                        break;
                    case EventEnum.YouShotDead:
                        AddChatMessage("You're shot dead by gunner.");
                        _isDead = true;
                        break;
                    case EventEnum.OtherShotDead:
                        AddChatMessage(wm.EventPayloads["Game.Target.Name"] + " was shot dead by gunner.");
                        break;
                    case EventEnum.Alive:
                        AddChatMessage(wm.EventPayloads["Game.Target.Name"] + " has been revived by medium.");
                        if (wm.EventPayloads["Game.Target.Id"] == null)
                        {
                            _isDead = false;
                        }
                        break;
                    case EventEnum.ChatMessage:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage(wm.EventPayloads["Game.Chatter"] + ":" + wm.EventPayloads["Game.ChatMessage"]);
                        }
                        break;
                    case EventEnum.Chat:
                        if (wm.EventPayloads["Success"] == WerewolfModel.FALSE)
                        {
                            switch (wm.EventPayloads["Error"])
                            {
                                case "403":
                                    AddChatMessage("You're not alive, can't talk now.");
                                    break;
                                case "404":
                                    AddChatMessage("You're not existed, can't talk now.");
                                    break;
                                case "405":
                                    AddChatMessage("You're not in a game, can't talk now.");
                                    break;
                                case "406":
                                    AddChatMessage("You're not allow to talk now, go to sleep.");
                                    break;
                            }
                        }
                        break;
                }
                // need to reset event
                wm.Event = EventEnum.NOP;
            }
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.JoinGame;
            controller.ActionPerformed(wcmd);
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnVote.BackColor = Color.Red;
            }
            _voteActivated = !_voteActivated;
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
                _actionActivated = false;
            }
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (_isDead)
            {
                AddChatMessage("You're dead!!");
                return;
            }
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnAction.BackColor = Color.Red;
            }
            _actionActivated = !_actionActivated;
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
                _voteActivated = false;
            }
        }

        private void BtnPlayerX_Click(object sender, EventArgs e)
        {
            Button btnPlayer = (Button)sender;
            int index = (int)btnPlayer.Tag;
            if (players == null)
            {
                // Nothing to do here;
                return;
            }
            if (_actionActivated)
            {
                _actionActivated = false;
                BtnAction.BackColor = Button.DefaultBackColor;
                AddChatMessage("You perform [" + BtnAction.Text + "] on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Action;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
            if (_voteActivated)
            {
                _voteActivated = false;
                BtnVote.BackColor = Button.DefaultBackColor;
                AddChatMessage("You vote on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Vote;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TbChatInput_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && TbChatInput.Text != "")
            {
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Chat;
                wcmd.Payloads = new Dictionary<string, string>() { { "Message", TbChatInput.Text } };
                TbChatInput.Text = "";
                controller.ActionPerformed(wcmd);
            }
        }

        private void DayTime()
        {
            this.BackgroundImage = Properties.Resources.Day_Time;
            Emo_hide();
        }

        private void NightTime()
        {
            this.BackgroundImage = Properties.Resources.Night_Time;
            Emo_hide();
        }

        private void Emo_Click(object sender, EventArgs e)
        {
            TbChatInput.Text += ((Button)sender).Text;
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.Chat;
            wcmd.Payloads = new Dictionary<string, string>() { { "Message", TbChatInput.Text } };
            TbChatInput.Text = "";
            controller.ActionPerformed(wcmd);
            Emo_hide();
        }

        private void Emo_hide()
        {
            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            button5.Hide();
            button6.Hide();
            button9.Hide();
            button10.Hide();
            button11.Hide();
            button12.Hide();
            button13.Hide();
            button14.Hide();
            EmoShow = false;
        }

        private void Emo_Show()
        {
            button1.Show();
            button2.Show();
            button3.Show();
            button4.Show();
            button5.Show();
            button6.Show();
            button9.Show();
            button10.Show();
            button11.Show();
            button12.Show();
            button13.Show();
            button14.Show();
            EmoShow = true;

        }

        private void Emo_hide_show(object sender, EventArgs e)
        {
            if (EmoShow == true)
            {
                Emo_hide();
            }
            else
            {
                Emo_Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {       
            //TEST BUTTON
            _isEnd = true;
            if (_isEnd)
            {
                GameOver n = new GameOver();
                n.Show();
            }
            _updateTimer.Enabled = false;
        }

        private void DeadPlayer(WerewolfModel wm)
        {
            if (_isDead)
            {
                int i = 0;
                foreach (Player player in wm.Players)
                {
                    Image img = Properties.Resources.Icon_villager;
                    ((Button)Controls["GBPlayers"].Controls["BtnPlayer" + i]).Image = img;
                    i++;
                }
            }
        }


    }
}