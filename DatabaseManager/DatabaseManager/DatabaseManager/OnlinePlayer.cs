using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace DatabaseManager
{
    public partial class OnlinePlayer : UserControl
    {
        public OnlinePlayer()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 更新OnlinePlayerUI
        /// </summary>
        public void UpdateOnlinePlayerHeadPanel()//更新OnlinePlayerUI
        {
            Label_PlayerNumber.Text = ManagerForm.ArcaletSetterCom.OnlinePlayerNumber.ToString();
        }
        string PlayerState_FileAddress = @"..\..\Logs\PlayerState";
        string PlayerState_FileName = @"\PlayerState.txt";
        /// <summary>
        /// 寫入文字到玩家登入狀態面板中
        /// </summary>
        public void WritePlayerStateLog(string _log)//寫入文字到玩家登入狀態面板中
        {
            string timeStr = DateTime.Now.ToString();
            string cutLineStr = "----------";
            string thisLogStr = cutLineStr + _log + cutLineStr + timeStr + "\r\n";
            RichBox.Text += thisLogStr;
            FileStream fs = new FileStream(PlayerState_FileAddress + PlayerState_FileName, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(thisLogStr);
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 從本基端讀取玩家登入狀態面板文字
        /// </summary>
        public void LoadPlayerStateLog()//從本基端讀取玩家登入狀態面板文字
        {
            if (!File.Exists(PlayerState_FileAddress + PlayerState_FileName))
            {
                MessageBox.Show(string.Format("找不到{0}{1}", PlayerState_FileAddress, PlayerState_FileName), "找不到檔案", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileStream fs = File.OpenRead(PlayerState_FileAddress + PlayerState_FileName);
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);
            while (fs.Read(b, 0, b.Length) > 0)
            {
                RichBox.Text += temp.GetString(b);
            }
            fs.Close();
        }
        /// <summary>
        /// 清空玩家登入狀態面板文字
        /// </summary>
        public void ClearPlayerStateLog()//清空玩家登入狀態面板文字
        {
            RichBox.Text = "";
        }
    }
}
