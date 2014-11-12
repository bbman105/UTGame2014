using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DatabaseManager
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
        }
        public static ArcaletSetter ArcaletSetterCom;

        private void Form1_Load(object sender, EventArgs e)
        {
            ArcaletSetterCom = new ArcaletSetter(this);
            ArcaletSetterCom.StartDPLaunch();//DP登入
            StartSetUserControl();//起始設定主題Panel字典
        }



        private void Btn_PlayerSetting_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_PlayerResource_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_PlayerMonster_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_PlayerRoom_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_MonsterTeam_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_MonsterFeature_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

        private void Btn_VentureProgress_Click(object sender, EventArgs e)
        {
            DisableThemeUserControl();//不啟用ThemePanel控制項
            HideThemeUserControl();//隱藏ThemePanel控制項
        }

    }
}
