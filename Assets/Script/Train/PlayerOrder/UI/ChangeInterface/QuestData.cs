using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class QuestData : TrainSceneInterfaceUI
{
    static int questID;
    public static int QuestID
    {
        get { return QuestData.questID; }
        set { QuestData.questID = value; }
    }
    static public int QuestLevel { get; set; }
    //怪物icon gameobj LIst
    static GameObject[] chara;
    static GameObject[] typeIcon;
    static int monsterNum;
    //怪物icon圖的sprite
    static private UISprite[] charaSprite;
    //問號圖的sprite
    static private UISprite[] unKnownSprite;
    //邊框圖的sprite
    static private UISprite[] bannerSprite;
    //底圖的sprite
    static private UISprite[] bottomSprite;
    //元素icon的sprite
    static private UISprite[] elementSprite;
    //種類icon的sprite
    static private UISprite[] typeSprite;
    //Name label
    static private UILabel[] nameLabel;


    public void StartSetting()
    {
        chara = new GameObject[6];
        typeIcon = new GameObject[6];
        charaSprite = new UISprite[6];
        unKnownSprite = new UISprite[6];
        bannerSprite = new UISprite[6];
        bottomSprite = new UISprite[6];
        elementSprite = new UISprite[6];
        typeSprite = new UISprite[6];
        nameLabel = new UILabel[6];
        monsterNum = new int();
        //建立怪物頭像icon物件
        for (int i = 0; i < 6; i++)
        {
            //抓取隊伍卡上腳色
            chara[i] = questDataUI.transform.FindChild(string.Format("chara{0}", (i + 1))).gameObject;
            typeIcon[i] = chara[i].transform.FindChild("type").gameObject;
            //抓取隊伍卡上腳色ICON的物件
            charaSprite[i] = chara[i].transform.FindChild("sprite").GetComponent<UISprite>();
            unKnownSprite[i] = chara[i].transform.FindChild("unKnown").GetComponent<UISprite>();
            bannerSprite[i] = chara[i].transform.FindChild("banner").GetComponent<UISprite>();
            bottomSprite[i] = chara[i].transform.FindChild("bg").GetComponent<UISprite>();
            elementSprite[i] = chara[i].transform.FindChild("element").GetComponent<UISprite>();
            nameLabel[i] = chara[i].transform.FindChild("lv").GetComponent<UILabel>();
            typeSprite[i] = chara[i].transform.FindChild("type").GetComponent<UISprite>();
        }
    }

    public static void LoadMonsterSprite()
    {
        //讀取任務資料
        TextAsset XMLFlie_Quest = Resources.Load<TextAsset>("String/Quest");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Quest.text);
        XmlNode root = doc.DocumentElement;
        XmlNode QuestNode = root.SelectSingleNode(string.Format("QID{0}", QuestID));
        XmlNode OwnMonsterNode = QuestNode.SelectSingleNode("OwnMonsterID");
        string[] npcID = OwnMonsterNode.InnerText.Split(',');
        monsterNum = npcID.Length;


        //讀取怪物資料
        TextAsset XMLFlie_NPCMonster = Resources.Load<TextAsset>("String/NPCMonster");
        doc.LoadXml(XMLFlie_NPCMonster.text);
        root = doc.DocumentElement;
        XmlNode NPCMonsterNode;

        //給予目前的任務ID
        questDataUI.transform.FindChild("start").GetComponent<GoReady>().QuestID = QuestID;
        questDataUI.transform.FindChild("start").GetComponent<GoReady>().QuestLevel = QuestLevel;

        try
        {
            for (int i = 0; i < 6; i++)
            {
                if (i < monsterNum)
                {
                    if (int.Parse(npcID[i]) != 0)
                    {
                        chara[i].SetActive(true);
                        //讀取當下NPC怪物資料
                        NPCMonsterNode = root.SelectSingleNode(string.Format("MID{0}", npcID[i]));
                        if (NPCMonsterNode == null)//如果不存在此NPCID
                        {
                            Debug.LogWarning(string.Format("找不到NPCMosnterID{0}", npcID[i]));
                            return;
                        }

                        /////////////設定怪物頭像//////////////

                        //設置怪物等級label
                        nameLabel[i].text = NPCMonsterNode.SelectSingleNode("Name").InnerText;
                        //設置怪物首領ICON
                        if (NPCMonsterNode.SelectSingleNode("NPCType").InnerText == "Underling")
                        {
                            typeIcon[i].SetActive(false);
                            unKnownSprite[i].alpha = 0;
                            charaSprite[i].color = Color.white;
                        }
                        else if (NPCMonsterNode.SelectSingleNode("NPCType").InnerText == "HideBoss")
                        {
                            if (!Player.QuestAchievementDic.ContainsKey(QuestID))
                            {
                                unKnownSprite[i].alpha = 255;
                                charaSprite[i].color = Color.black;
                            }
                            else
                            {
                                if (Player.QuestAchievementDic[QuestID].Achieve[2])
                                {
                                    charaSprite[i].color = Color.white;
                                    unKnownSprite[i].alpha = 0;
                                }
                                else
                                {
                                    charaSprite[i].color = Color.black;
                                    unKnownSprite[i].alpha = 255;
                                }
                            }
                            typeIcon[i].SetActive(true);
                            typeIcon[i].transform.GetComponent<UISprite>().spriteName = string.Format("NpcType_{0}", NPCMonsterNode.SelectSingleNode("NPCType").InnerText);
                        }
                        else if (NPCMonsterNode.SelectSingleNode("NPCType").InnerText == "Boss")
                        {
                            unKnownSprite[i].alpha = 0;
                            charaSprite[i].color = Color.white;
                            typeIcon[i].SetActive(true);
                            typeIcon[i].transform.GetComponent<UISprite>().spriteName = string.Format("NpcType_{0}", NPCMonsterNode.SelectSingleNode("NPCType").InnerText);
                        }
                        else
                        {
                            Debug.LogWarning("NPCMonster的NPCType無法辨識");
                        }
                        //選擇對的物種頭像             
                        if (int.Parse(NPCMonsterNode.SelectSingleNode("Species").InnerText) < 8)
                        {
                            charaSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                        }
                        else
                        {
                            charaSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                        }
                        charaSprite[i].spriteName = string.Format("{0}-{1}", NPCMonsterNode.SelectSingleNode("Species").InnerText, NPCMonsterNode.SelectSingleNode("SpeciesLevel").InnerText);
                        //歸正縮放比例
                        charaSprite[i].MakePixelPerfect();
                        bannerSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                        bannerSprite[i].spriteName = "frame" + NPCMonsterNode.SelectSingleNode("MainElement").InnerText;
                        bannerSprite[i].MakePixelPerfect();
                        bottomSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                        bottomSprite[i].spriteName = "bottom" + NPCMonsterNode.SelectSingleNode("MainElement").InnerText;//NPCMonsterNode.SelectSingleNode("Rare").InnerText;
                        bottomSprite[i].MakePixelPerfect();
                        elementSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                        elementSprite[i].spriteName = "element" + NPCMonsterNode.SelectSingleNode("MainElement").InnerText;
                        elementSprite[i].MakePixelPerfect();
                    }
                    else
                    {
                        chara[i].SetActive(false);
                    }
                }
                else
                {
                    chara[i].SetActive(false);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }


    }
}
