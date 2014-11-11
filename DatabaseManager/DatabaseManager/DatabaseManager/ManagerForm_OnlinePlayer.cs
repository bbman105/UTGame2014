using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
namespace DatabaseManager
{
    public partial class ManagerForm
    {
        public OnlinePlayer MyOnlinePlayer;
        /// <summary>
        /// 初始化設定OnlinePlayer
        /// </summary>
        void StartSetOnlinePlayer()//初始化設定OnlinePlayer
        {
            MyOnlinePlayer = new OnlinePlayer();
            BodyPanel.Controls.Add(MyOnlinePlayer);//將OnlinePlayer加入至BodyPanel底下
            List<UserControl> panelList = new List<UserControl>();
            panelList.Add(MyOnlinePlayer);
            ThemeUserControlDic.Add("OnlinePlayer", panelList);//加入OnlinePlayer
            MyOnlinePlayer.ClearPlayerStateLog();//清空玩家登入狀態面板文字
            MyOnlinePlayer.LoadPlayerStateLog();//從本基端讀取玩家登入狀態面板文字
            HideThemeUserControl("OnlinePlayer");//隱藏ThemePanel控制項
            DisableThemeUserControl("OnlinePlayer");//不啟用ThemePanel控制項
        }
    }
}
