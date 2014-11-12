using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseManager
{
    public partial class ControlMenu : UserControl
    {
        private ManagerForm managerForm;
        public ControlMenu(Form _form)
        {
            InitializeComponent();
            managerForm = (ManagerForm)_form;
        }

        private void Btn_OnlinePlayer_Click(object sender, EventArgs e)
        {
            managerForm.DisableThemeUserControl();//不啟用ThemePanel控制項
            managerForm.HideThemeUserControl();//隱藏ThemePanel控制項
            managerForm.EnableThemeUserControl("OnlinePlayer");//啟用ThemePanel控制項
            managerForm.ShowThemeUserControl("OnlinePlayer");//顯示ThemePanel控制項
        }

        private void Btn_PlayerMonster_Click(object sender, EventArgs e)
        {
            managerForm.DisableThemeUserControl();//不啟用ThemePanel控制項
            managerForm.HideThemeUserControl();//隱藏ThemePanel控制項
            managerForm.EnableThemeUserControl("PlayerMonster");//啟用ThemePanel控制項
            managerForm.ShowThemeUserControl("PlayerMonster");//顯示ThemePanel控制項
        }
    }
}
