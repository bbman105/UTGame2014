using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{

    public List<Feature> OwnFeatureList;//擁有特徵清單

    public void SetOwnFeature()//取得擁有的特徵
    {
        OwnFeatureList.Clear();
        //讀取資料庫中玩家擁有的房間數量，並建立房間
        TextAsset XMLFlie_Feature = Resources.Load<TextAsset>("StringData/MonsterFeatureData");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Feature.text);
        XmlNodeList ownPlayerNode = doc.GetElementsByTagName("OwnMonsterUID");
        //以迴圈搜尋此玩家擁有的房間，迴圈長度取決抓取到的node
        for (int i = 0; i < ownPlayerNode.Count; i++)
        {
            //判斷屬於此玩家的房間，若為此玩家擁有，則建立房間
            if (ownPlayerNode[i].InnerText == MonsterID.ToString())
            {
                int talentID = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("FeatureID").InnerText);
                bool isDominant = bool.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("IsDominant").InnerText);
                if(GameDictionary.TalentDic.ContainsKey(talentID))
                {
                    Feature feature = new Feature(talentID, isDominant);
                    OwnFeatureList.Add(feature);
                }
            }
        }
        SetFeatureAddProperty();//設定特徵增加的屬性
        SetProperty();//設定最終屬性
    }
    void SetFeatureAddProperty()//設定特徵增加的屬性
    {
        FeatureHealthPlus = 0;
        FeatureHealthTimes = 0;
        FeatureBrutalPlus = 0;
        FeatureBrutalTimes = 0;
        FeatureResistancePlus = 0;
        FeatureResistanceTimes = 0;
        FeatureAggressivePlus = 0;
        FeatureAggressiveTimes = 0;
        FeatureSizePlus = 0;
        for (int i = 0; i < OwnFeatureList.Count; i++)
        {
            FeatureHealthPlus += OwnFeatureList[i].OwnTalent.TalentHealthPlus;
            FeatureHealthTimes += OwnFeatureList[i].OwnTalent.TalentHealthTimes;
            FeatureBrutalPlus += OwnFeatureList[i].OwnTalent.TalentBrutalPlus;
            FeatureBrutalTimes += OwnFeatureList[i].OwnTalent.TalentBrutalTimes;
            FeatureResistancePlus += OwnFeatureList[i].OwnTalent.TalentResistancePlus;
            FeatureResistanceTimes += OwnFeatureList[i].OwnTalent.TalentResistanceTimes;
            FeatureAggressivePlus += OwnFeatureList[i].OwnTalent.TalentAggressivePlus;
            FeatureAggressiveTimes += OwnFeatureList[i].OwnTalent.TalentAggressiveTimes;
            FeatureSizePlus += OwnFeatureList[i].OwnTalent.TalentSizePlus;
        }
    }

}
