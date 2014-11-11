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
        public PlayerMonster MyPlayerMonster;
        /// <summary>
        /// 初始化設定PlayerMonster
        /// </summary>
        void StartSetPlayerMonster()//初始化設定PlayerMonster
        {
            MyPlayerMonster = new PlayerMonster();
            BodyPanel.Controls.Add(MyPlayerMonster);//將PlayerMonster加入至BodyPanel底下
            List<UserControl> panelList = new List<UserControl>();
            panelList.Add(MyPlayerMonster);
            ThemeUserControlDic.Add("PlayerMonster", panelList);//加入PlayerMonster
            HideThemeUserControl("PlayerMonster");//隱藏ThemePanel控制項
            DisableThemeUserControl("PlayerMonster");//不啟用ThemePanel控制項
        }
    }
}
