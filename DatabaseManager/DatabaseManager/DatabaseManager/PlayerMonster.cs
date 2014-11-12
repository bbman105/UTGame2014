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
    public partial class PlayerMonster : UserControl
    {
        DataSet DSPlayerMonster;
        DataTable DTPlayerMonster;
        public PlayerMonster()
        {
            InitializeComponent();
            StartSetPlayerMosnterData();
        }
        /// <summary>
        /// 讀取怪獸資料
        /// </summary>
        void StartSetPlayerMosnterData()
        {
            DSPlayerMonster = new DataSet("PlayerMonster");
            DTPlayerMonster = DSPlayerMonster.Tables.Add("PlayerMonster");
            //怪獸編號
            DataColumn colMonsterID = new DataColumn("MonsterID");
            colMonsterID.DataType = System.Type.GetType("System.String");
            colMonsterID.MaxLength = 20;
            colMonsterID.AllowDBNull = false;
            //擁有玩家ID
            DataColumn colOwnPlayerID = new DataColumn("OwnPlayerID");
            colMonsterID.DataType = System.Type.GetType("System.String");
            colMonsterID.MaxLength = 20;
            colMonsterID.AllowDBNull = false;
            //怪獸名稱
            DataColumn colMonsterName = new DataColumn("MonsterName");
            colMonsterName.DataType = System.Type.GetType("System.String");
            colMonsterName.MaxLength = 20;
            colMonsterName.AllowDBNull = false;
            //物種
            DataColumn colSpecies = new DataColumn("Species");
            colSpecies.DataType = System.Type.GetType("System.Int32");
            colSpecies.DefaultValue = 0;
            colSpecies.AllowDBNull = false;
            //物階
            DataColumn colSpeciesLevel = new DataColumn("SpeciesLevel");
            colSpeciesLevel.DataType = System.Type.GetType("System.Int32");
            colSpeciesLevel.DefaultValue = 0;
            colSpeciesLevel.AllowDBNull = false;
            //品質
            DataColumn colRare = new DataColumn("Rare");
            colRare.DataType = System.Type.GetType("System.String");
            colRare.MaxLength = 5;
            colRare.AllowDBNull = false;
            //等級
            DataColumn colLv = new DataColumn("LV");
            colLv.DataType = System.Type.GetType("System.String");
            colLv.MaxLength = 5;
            colLv.AllowDBNull = false;
            //升級加點生命
            DataColumn colLvHealth = new DataColumn("LvHealth");
            colLvHealth.DataType = System.Type.GetType("System.String");
            colLvHealth.MaxLength = 5;
            colLvHealth.AllowDBNull = false;
            //升級加點殘暴
            DataColumn colLvBrutal = new DataColumn("LvBrutal");
            colLvBrutal.DataType = System.Type.GetType("System.String");
            colLvBrutal.MaxLength = 5;
            colLvBrutal.AllowDBNull = false;
            //升級加點抵抗
            DataColumn colLvResistance = new DataColumn("LvResistance");
            colLvResistance.DataType = System.Type.GetType("System.String");
            colLvResistance.MaxLength = 5;
            colLvResistance.AllowDBNull = false;
            //升級加點侵略
            DataColumn colLvAggressive = new DataColumn("LvAggressive");
            colLvAggressive.DataType = System.Type.GetType("System.String");
            colLvAggressive.MaxLength = 5;
            colLvAggressive.AllowDBNull = false;
            //原生
            DataColumn colNatural = new DataColumn("Natural");
            colNatural.DataType = System.Type.GetType("System.String");
            colNatural.MaxLength = 5;
            colNatural.AllowDBNull = false;
            //突變
            DataColumn colMutation = new DataColumn("Mutation");
            colMutation.DataType = System.Type.GetType("System.String");
            colMutation.MaxLength = 5;
            colMutation.AllowDBNull = false;
            //野性
            DataColumn colWild = new DataColumn("Wild");
            colWild.DataType = System.Type.GetType("System.String");
            colWild.MaxLength = 5;
            colWild.AllowDBNull = false;
            //快樂
            DataColumn colHappy = new DataColumn("Happy");
            colHappy.DataType = System.Type.GetType("System.String");
            colHappy.MaxLength = 5;
            colHappy.AllowDBNull = false;
            //屬性
            DataColumn colElement = new DataColumn("Element");
            colElement.DataType = System.Type.GetType("System.String");
            colElement.MaxLength = 5;
            colElement.AllowDBNull = false;
            //經驗
            DataColumn colEXP = new DataColumn("EXP");
            colEXP.DataType = System.Type.GetType("System.String");
            colEXP.MaxLength = 5;
            colEXP.AllowDBNull = false;
            //個性
            DataColumn colPersonality = new DataColumn("Personality");
            colPersonality.DataType = System.Type.GetType("System.String");
            colPersonality.MaxLength = 5;
            colPersonality.AllowDBNull = false;
            //選擇技能
            DataColumn colSelectedSkill = new DataColumn("SelectedSkill");
            colSelectedSkill.DataType = System.Type.GetType("System.String");
            colSelectedSkill.MaxLength = 5;
            colSelectedSkill.AllowDBNull = false;
            //二階選擇的物種
            DataColumn colLV2Species = new DataColumn("LV2Species");
            colLV2Species.DataType = System.Type.GetType("System.String");
            colLV2Species.MaxLength = 5;
            colLV2Species.AllowDBNull = false;
            //三階選擇的物種
            DataColumn colLV3Species = new DataColumn("LV3Species");
            colLV3Species.DataType = System.Type.GetType("System.String");
            colLV3Species.MaxLength = 5;
            colLV3Species.AllowDBNull = false;
            //所在房間
            DataColumn colInRoomID = new DataColumn("InRoomID");
            colInRoomID.DataType = System.Type.GetType("System.String");
            colInRoomID.MaxLength = 5;
            colInRoomID.AllowDBNull = false;
            //將欄位加入資料表
            DTPlayerMonster.Columns.Add(colMonsterID);
            DTPlayerMonster.Columns.Add(colOwnPlayerID);
            DTPlayerMonster.Columns.Add(colMonsterName);
            DTPlayerMonster.Columns.Add(colSpecies);
            DTPlayerMonster.Columns.Add(colSpeciesLevel);
            DTPlayerMonster.Columns.Add(colRare);
            DTPlayerMonster.Columns.Add(colLv);
            DTPlayerMonster.Columns.Add(colLvHealth);
            DTPlayerMonster.Columns.Add(colLvBrutal);
            DTPlayerMonster.Columns.Add(colLvResistance);
            DTPlayerMonster.Columns.Add(colLvAggressive);
            DTPlayerMonster.Columns.Add(colNatural);
            DTPlayerMonster.Columns.Add(colMutation);
            DTPlayerMonster.Columns.Add(colWild);
            DTPlayerMonster.Columns.Add(colHappy);
            DTPlayerMonster.Columns.Add(colElement);
            DTPlayerMonster.Columns.Add(colEXP);
            DTPlayerMonster.Columns.Add(colPersonality);
            DTPlayerMonster.Columns.Add(colSelectedSkill);
            DTPlayerMonster.Columns.Add(colLV2Species);
            DTPlayerMonster.Columns.Add(colLV3Species);
            DTPlayerMonster.Columns.Add(colInRoomID);
            //設定DataGridView控制項顯示PlayerMonsterData資料集裡的Product資料表
            DataGirdView.DataSource = DSPlayerMonster;
            DataGirdView.DataMember = "PlayerMonster";
            //新增DataRow方式
            //DTPlayerMonster.Rows.Add(new Object[] { "P2297", "怪獸A", 580, 20 });
            //加入主索引鍵
            DTPlayerMonster.PrimaryKey = new DataColumn[] { DTPlayerMonster.Columns["MonsterID"] };
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
