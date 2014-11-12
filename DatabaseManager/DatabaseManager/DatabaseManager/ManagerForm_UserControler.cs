using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseManager
{
    public partial class ManagerForm
    {
        Dictionary<string, List<UserControl>> ThemeUserControlDic;//主題UserControl字典
        Dictionary<string, List<UserControl>> ControlMenuBtnDic;//主題控制按鈕字典
        List<string> DicBuff;
        /// <summary>
        /// 起始設定主題UserControl字典
        /// </summary>
        void StartSetUserControl()//起始設定UserControl字典
        {
            ThemeUserControlDic = new Dictionary<string, List<UserControl>>();
            ControlMenuBtnDic = new Dictionary<string, List<UserControl>>();
            StartSetControlMenu();//初始化設定ControlMenu
            StartSetOnlinePlayer();//初始化設定OnlinePlayer
            StartSetPlayerMonster();//初始化設定PlayerMonster
        }
        /// <summary>
        /// 啟用ControlMenu控制項，傳入要啟用的控制項名稱，若沒傳入就是所有ControlMenu的控制項
        /// </summary>
        public void EnableMenuControl()//啟用ControlMenu控制項
        {
            DicBuff = new List<string>(ControlMenuBtnDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ControlMenuBtnDic[key].Count; i++)
                {
                    ControlMenuBtnDic[key][i].Enabled = true;
                }
            }
        }
        /// <summary>
        /// 不啟用ControlMenu控制項，傳入要啟用的控制項名稱，若沒傳入就是所有ControlMenu的控制項
        /// </summary>
        public void DisableMenuControl()//不啟用ControlMenu控制項
        {
            DicBuff = new List<string>(ControlMenuBtnDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ControlMenuBtnDic[key].Count; i++)
                {
                    ControlMenuBtnDic[key][i].Enabled = false;
                }
            }
        }
        public void DisableMenuControl(string _controlName)//不啟用ControlMenu控制項
        {
            if (ControlMenuBtnDic.ContainsKey(_controlName))
            {
                for (int i = 0; i < ControlMenuBtnDic[_controlName].Count; i++)
                {
                    ControlMenuBtnDic[_controlName][i].Enabled = false;
                }
            }
        }

        /// <summary>
        /// 啟用ThemePanel控制項，傳入要啟用的控制項名稱，若沒傳入就是所有ThemePanel的控制項
        /// </summary>
        public void EnableThemeUserControl()//啟用ThemePanel控制項
        {
            DicBuff = new List<string>(ThemeUserControlDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ThemeUserControlDic[key].Count; i++)
                {
                    ThemeUserControlDic[key][i].Enabled = true;
                }
            }
        }
        public void EnableThemeUserControl(string _controlName)//啟用ThemePanel控制項
        {
            if (ThemeUserControlDic.ContainsKey(_controlName))
            {
                for (int i = 0; i < ThemeUserControlDic[_controlName].Count; i++)
                {
                    ThemeUserControlDic[_controlName][i].Enabled = true;
                }
            }
        }
        /// <summary>
        /// 不啟用ThemePanel控制項，傳入要取消啟用的控制項名稱，若沒傳入就是所有ThemePanel的控制項
        /// </summary>
        public void DisableThemeUserControl()//不啟用ThemePanel控制項
        {
            DicBuff = new List<string>(ThemeUserControlDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ThemeUserControlDic[key].Count; i++)
                {
                    ThemeUserControlDic[key][i].Enabled = false;
                }
            }
        }
        public void DisableThemeUserControl(string _controlName)//不啟用ThemePanel控制項
        {
            if (ThemeUserControlDic.ContainsKey(_controlName))
            {
                for (int i = 0; i < ThemeUserControlDic[_controlName].Count; i++)
                {
                    ThemeUserControlDic[_controlName][i].Enabled = false;
                }
            }
        }
        /// <summary>
        /// 顯示ThemePanel控制項，傳入要顯示的控制項名稱，若沒傳入就是所有ThemePanel的控制項
        /// </summary>
        public void ShowThemeUserControl()//顯示ThemePanel控制項
        {
            DicBuff = new List<string>(ThemeUserControlDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ThemeUserControlDic[key].Count; i++)
                {
                    ThemeUserControlDic[key][i].Show();
                }
            }
        }
        public void ShowThemeUserControl(string _controlName)//顯示ThemePanel控制項
        {
            if (ThemeUserControlDic.ContainsKey(_controlName))
            {
                for (int i = 0; i < ThemeUserControlDic[_controlName].Count; i++)
                {
                    ThemeUserControlDic[_controlName][i].Show();
                }
            }
        }
        /// <summary>
        /// 隱藏ThemePanel控制項，傳入要隱藏的控制項名稱，若沒傳入就是所有ThemePanel的控制項
        /// </summary>
        public void HideThemeUserControl()//隱藏ThemePanel控制項
        {
            DicBuff = new List<string>(ThemeUserControlDic.Keys);
            foreach (string key in DicBuff)
            {
                for (int i = 0; i < ThemeUserControlDic[key].Count; i++)
                {
                    ThemeUserControlDic[key][i].Hide();
                }
            }
        }
        public void HideThemeUserControl(string _controlName)//隱藏ThemePanel控制項
        {
            if (ThemeUserControlDic.ContainsKey(_controlName))
            {
                for (int i = 0; i < ThemeUserControlDic[_controlName].Count; i++)
                {
                    ThemeUserControlDic[_controlName][i].Hide();
                }
            }
        }
    }
}
