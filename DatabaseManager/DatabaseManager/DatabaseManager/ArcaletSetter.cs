using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace DatabaseManager
{
    public class ArcaletSetter
    {
        public string dp_user = "scozirge"; //輸入超級使用者帳號
        public string dp_password = "2uuxuurx"; //輸入超級使用者密碼
        public int OnlinePlayerNumber = 0;

        private const string gguid = "caa6ccb9-bb3c-aa49-bbfc-6c2da84a757c";
        private const string sguid = "98a5b11d-b98e-864a-bd9a-cf3c75c6db7b";
        byte[] certificate = { 0xe9, 0xb4, 0xdb, 0x21, 0x6c, 0xa7, 0x25, 0x41, 0x87, 0x3b, 0xb2, 0x41, 0xe4, 0x27, 0x90, 0x8c, 0xce, 0x11, 0xd5, 0x92, 0x2, 0x40, 0x5a, 0x42, 0x85, 0x2a, 0x8a, 0xdd, 0xb5, 0x94, 0x50, 0x1a, 0xe9, 0x44, 0x35, 0xf4, 0xfb, 0x48, 0xb8, 0x49, 0x84, 0x22, 0x9f, 0x7d, 0xf7, 0x27, 0x6, 0x69, 0xfa, 0x33, 0x77, 0x76, 0x52, 0xf4, 0x78, 0x4f, 0xab, 0xa8, 0x4e, 0x3d, 0x50, 0xc, 0xf6, 0x21, 0xc1, 0x68, 0xd1, 0xa5, 0xd4, 0x21, 0x33, 0x42, 0x8e, 0xd1, 0x1d, 0x11, 0x8d, 0x8e, 0x80, 0xbf, 0x9d, 0xd2, 0x15, 0x3f, 0xf9, 0xe, 0xb0, 0x41, 0xb4, 0x27, 0xab, 0xb9, 0xff, 0xc9, 0xbc, 0x85, 0x82, 0xc9, 0x3a, 0x25, 0x39, 0xdb, 0x4b, 0x4a, 0xa2, 0xfd, 0xd5, 0xc7, 0x3b, 0x7c, 0xe6, 0x18, 0x6b, 0x5a, 0x18, 0xaf, 0x84, 0xde, 0x2a, 0x40, 0xa7, 0xb0, 0xd3, 0xa7, 0x7d, 0x81, 0xee, 0x16 };
        public ArcaletGame ag = null;
        private ManagerForm managerForm;
        public ArcaletSetter(Form _form)
        {
            managerForm = (ManagerForm)_form;
        }
        /// <summary>
        /// 使用超級使用者帳戶登入Arcalet
        /// </summary>
        public void StartDPLaunch()
        {
            //啟動WindowsForm自動執行登入動作
            ag = new ArcaletGame(dp_user, dp_password, gguid, sguid, certificate);
            ag.onStateChanged += OnStateChanged;
            ag.onMessageIn += MainMessageIn;
            ag.onCompletion += CB_Login;
            ag.Launch();
            //label1_look.Text = "On Login";
        }
        void CB_Login(int code, ArcaletGame game)
        {
            if (code == 0)
            {
                managerForm.EnableMenuControl();//啟用ControlMenu控制項
                //Code為0表示登入成功
                //label1_look.Text = "Login Successed";
            }
            else
            {
                MessageBox.Show("登入失敗Error Code：" + code);
                //Code非0表示登入失敗
                //label1_look.Text = "Login Failed，Error Code：" + code;
            }
        }
        void OnStateChanged(int state, int code, ArcaletGame game)
        {
            /*
            //DP斷線的報錯機制
            if (state >= 900) label1_look.Text = "Disconnection，State：" + state +
                " Error Code：" + code;
            */
        }

        void MainMessageIn(string msg, int delay, ArcaletGame game)
        {
            //使用Try Catch處理訊息可以避免收到錯誤訊息造成DP Exception
            try
            {
                string[] cmds = msg.Split(':'); //將收到的訊息以分號":" 區隔
                switch (cmds[0])
                {
                    case "new":
                        PlayerLogin(cmds[1]);
                        managerForm.MyOnlinePlayer.WritePlayerStateLog(string.Format("玩家{0}登入", cmds[2]));//寫入文字到玩家登入狀態面板中
                        break;
                    case "quit":
                        PlayerLogout();
                        managerForm.MyOnlinePlayer.WritePlayerStateLog(string.Format("玩家{0}登出", cmds[1]));//寫入文字到玩家登入狀態面板中
                        break;
                }
            }
            catch (Exception e) { }
        }

        void PlayerLogin(string msg)
        {
            //將收到的訊息轉為poid,poid,poid在密頻傳送訊息時是很重要的指標
            int poid = int.Parse(msg);
            //發送給玩家端確認DP已經連接,並且附上DP的poid
            ag.PrivacySend("dp_new:" + ag.poid, poid);
            //線上玩家數+1
            OnlinePlayerNumber++;
            managerForm.MyOnlinePlayer.UpdateOnlinePlayerHeadPanel();//更新OnlinePlayerUI
        }

        void PlayerLogout()
        {
            //線上玩家數-1
            OnlinePlayerNumber--;
            managerForm.MyOnlinePlayer.UpdateOnlinePlayerHeadPanel();//更新OnlinePlayerUI
        }
    }
}
