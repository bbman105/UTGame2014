using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Talent
{

    public int TalentID { get; set; }//天賦ID
    public string TalentName { get; set; }//天賦名稱
    public string Description { get; set; }//天賦描述
    public int PermanentEffect { get; set; }//常駐法術ID
    //天賦加值屬性
    public int TalentHealthPlus;
    public float TalentHealthTimes;
    public int TalentBrutalPlus;
    public float TalentBrutalTimes;
    public int TalentResistancePlus;
    public float TalentResistanceTimes;
    public int TalentAggressivePlus;
    public float TalentAggressiveTimes;
    public float TalentSizePlus;

    public Talent(int _talentID)
    {
        TalentID = _talentID;
        LoadData();
    }
    public void LoadData()
    {
        if (TalentID != 0)//按邊號讀取技能
        {
            //依照法術編號讀取法術資料中的法術各屬性
            TextAsset XMLFlie = Resources.Load<TextAsset>("String/Talent");
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFlie.text);
            XmlNode root = doc.DocumentElement;

            try
            {
                //抓取此技能的node區段
                XmlNode TelentNode = root.SelectSingleNode("TID" + TalentID);
                TalentName = TelentNode.SelectSingleNode("Name").InnerText;
                Description = TelentNode.SelectSingleNode("Description").InnerText;
                PermanentEffect = int.Parse(TelentNode.SelectSingleNode("PermanentEffect").InnerText);
                TalentHealthPlus = int.Parse(TelentNode.SelectSingleNode("HealthPlus").InnerText);
                TalentHealthTimes = float.Parse(TelentNode.SelectSingleNode("HealthTimes").InnerText);
                TalentBrutalPlus = int.Parse(TelentNode.SelectSingleNode("BrutalPlus").InnerText);
                TalentBrutalTimes = int.Parse(TelentNode.SelectSingleNode("BrutalTimes").InnerText);
                TalentResistancePlus = int.Parse(TelentNode.SelectSingleNode("ResistancePlus").InnerText);
                TalentResistanceTimes = int.Parse(TelentNode.SelectSingleNode("ResistanceTimes").InnerText);
                TalentAggressivePlus = int.Parse(TelentNode.SelectSingleNode("AggressivePlus").InnerText);
                TalentAggressiveTimes = int.Parse(TelentNode.SelectSingleNode("AggressiveTimes").InnerText);
                TalentSizePlus = float.Parse(TelentNode.SelectSingleNode("SizePlus").InnerText);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                Debug.Log("讀取天賦資料時發生錯誤");
            }
        }
    }
}
