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
        public ControlMenu MyControlMenu;
        /// <summary>
        /// 初始化設定ControlMenu
        /// </summary>
        void StartSetControlMenu()//初始化設定ControlMenu
        {
            MyControlMenu = new ControlMenu(this);
            MenuControlPanel.Controls.Add(MyControlMenu);//將ControlMenu加入至BodyPanel底下
            List<UserControl> panelList = new List<UserControl>();
            panelList.Add(MyControlMenu);
            ControlMenuBtnDic.Add("ControlMenu", panelList);//加入ControlMenu
            DisableMenuControl("ControlMenu");//不啟用ControlMenu控制項
        }
    }
}
