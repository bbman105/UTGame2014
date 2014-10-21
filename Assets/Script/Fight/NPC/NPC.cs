using UnityEngine;
using System.Collections;
using System.Xml;

public class NPC
{

    public int MonsterID { get; set; }//怪物編號
    public string MonsterName { get; set; }//怪物名稱
    public int Species { get; set; }//物種
    public int SpeciesLevel { get; set; }//物種階級
    public int Rare { get; set; }//品質
    public int LV { get; set; }//等級
    public int Health { get; set; }//生命
    public float Size { get; set; }//體型
    public int Brutal { get; set; }//殘暴
    public int Resistance { get; set; }//抵抗
    public int Aggressive { get; set; }//侵略
    public int MainElement { get; set; }//屬性
    public int EXP { get; set; }//經驗
    public int Gold { get; set; }//金錢
    public string NPCType { get; set; }//NPC種類
    public int AIID { get; set; }//AI編號
    public int Loot { get; set; }//戰利品ID
    public float DropPercent { get; set; }//掉落機率

    public NPC(int _monsterID)
    {
        MonsterID = _monsterID;
        //讀取地圖資料
        TextAsset XMLFlie_NPC = Resources.Load<TextAsset>("String/NPCMonster");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_NPC.text);
        XmlNode root = doc.DocumentElement;
        //讀取xml文件中欲抓取的MonsterID標籤
        XmlNode CurMonsterNode = root.SelectSingleNode(string.Format("MID{0}", MonsterID));
        MonsterName = CurMonsterNode.SelectSingleNode("Name").InnerText;//怪物名稱
        Species = int.Parse(CurMonsterNode.SelectSingleNode("Species").InnerText);//物種
        SpeciesLevel = int.Parse(CurMonsterNode.SelectSingleNode("SpeciesLevel").InnerText);//物種階級
        Rare = int.Parse(CurMonsterNode.SelectSingleNode("Rare").InnerText);//品質
        LV = int.Parse(CurMonsterNode.SelectSingleNode("LV").InnerText);//等級
        Health = int.Parse(CurMonsterNode.SelectSingleNode("Health").InnerText);//生命
        Size = float.Parse(CurMonsterNode.SelectSingleNode("Size").InnerText);//體型
        Brutal = int.Parse(CurMonsterNode.SelectSingleNode("Brutal").InnerText);//殘暴
        Resistance = int.Parse(CurMonsterNode.SelectSingleNode("Resistance").InnerText);//抵抗
        Aggressive = int.Parse(CurMonsterNode.SelectSingleNode("Aggressive").InnerText);//侵略
        MainElement = int.Parse(CurMonsterNode.SelectSingleNode("MainElement").InnerText);//屬性
        EXP = int.Parse(CurMonsterNode.SelectSingleNode("EXP").InnerText);//經驗
        Gold = int.Parse(CurMonsterNode.SelectSingleNode("Gold").InnerText);//金錢
        NPCType = CurMonsterNode.SelectSingleNode("SpeciesLevel").InnerText;//種類
        AIID = int.Parse(CurMonsterNode.SelectSingleNode("AIID").InnerText);//AI編號
        Loot = int.Parse(CurMonsterNode.SelectSingleNode("Loot").InnerText);//戰利品ID
        DropPercent = float.Parse(CurMonsterNode.SelectSingleNode("DropPercent").InnerText);//戰利品掉落機率
    }

}
