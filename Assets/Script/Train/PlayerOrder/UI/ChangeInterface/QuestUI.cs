using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class QuestUI : TrainSceneInterfaceUI
{
    private static int curMapPage;
    public static int CurMapPage
    {
        get { return QuestUI.curMapPage; }
        set { QuestUI.curMapPage = value; }
    }
    private static int curMapID;
    public static int CurMapID
    {
        get { return QuestUI.curMapID; }
        set { QuestUI.curMapID = value; }
    }


    public static List<Quest> QuestList;

    //讀入的prefab物件
    static GameObject QuestObj;
    //任務 gameobj LIst
    static GameObject[] questCard;
    static Transform Grid;
    //任務數量
    static int questNum;
    static int maxQuestNum;
    //每個任務卡顯示的的位置
    static Vector3[] cardPos;
    //任務卡物件的基本座標
    static Vector3 baseCardPos;
    //任務卡物件的Y軸位移座標
    static Vector3 addCardPosY;
    //任務卡物件的基本縮放
    static Vector3 baseCardScale;
    //任務卡物件的基本旋轉
    static Quaternion baseCardRot;
    //任務名稱label
    static private UILabel[] questNameLabel;
    static private UILabel mapNameLabel;
    static private UISprite mapSprite;
    //任務成就物件
    static private UISprite[] completeSprite;//完成成就
    static private UISprite[] exploreSprite;//探索成就
    static private UISprite[] rankSSprite;//S評價成就
    static private UISprite[] huntingSprite;//獵殺成就


    public void StartSetting()
    {
        ///////////////////////初始化型別///////////////////////////
        QuestList = new List<Quest>();
        Grid = questChoiceUI.transform.FindChild("ScrollView").FindChild("Grid");
        //介面數值初始化
        questCard = new GameObject[10];//最多10個任務
        cardPos = new Vector3[10];//最多10個任務
        questNum = new int();
        baseCardPos = new Vector3(0, 190, 0);
        addCardPosY = new Vector3(0, -190, 0);
        baseCardScale = new Vector3(1, 1, 1);
        baseCardRot = Quaternion.Euler(0, 0, 0);
        CurMapID = 1;
        CurMapPage = 1;
        //初始化任務卡上的ICon各屬性數值的物件
        questNameLabel = new UILabel[10];//最多10個任務
        completeSprite = new UISprite[10];
        exploreSprite = new UISprite[10];
        rankSSprite = new UISprite[10];
        huntingSprite = new UISprite[10];
        QuestObj = Resources.Load<GameObject>("GameObject/RoomUI/Map/quest");

        //讀取任務資料
        TextAsset XMLFlie_Map = Resources.Load<TextAsset>("String/Map");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Map.text);
        XmlNode root = doc.DocumentElement;
        //讀取xml文件中欲抓取的MonsterID標籤
        XmlNode CurMapNode = root.SelectSingleNode(string.Format("Map{0}{1}", CurMapPage, CurMapID));
        for (int i = 0; i < 10; i++)//最多10個任務
        {
            if (int.Parse(CurMapNode.SelectSingleNode(string.Format("Quest{0}ID", i + 1)).InnerText) != 0)
            {
                QuestList.Add(new Quest(int.Parse(CurMapNode.SelectSingleNode(string.Format("Quest{0}ID", i + 1)).InnerText)));
            }
        }
        
        questNum = QuestList.Count;
        
        for (int i = 0; i < questNum; i++)
        {
            //////////////////////////////創建任務卡片，並抓取任務卡及各屬性物件////////////////////////////////
            //設定任務卡位置
            cardPos[i] = baseCardPos + (addCardPosY * i);
            //創建任務卡
            questCard[i] = Instantiate(QuestObj, cardPos[i], baseCardRot) as GameObject;
            //把任務丟到MapChoice底下
            questCard[i].transform.parent = Grid;
            //設定任務卡到對的位置上
            questCard[i].transform.localPosition = cardPos[i];
            //設定任務卡縮放
            questCard[i].transform.localScale = baseCardScale;
            //設定任務卡名稱
            questCard[i].name = i.ToString();
            //抓取formation上任務卡的ICon各屬性數值的物件
            completeSprite[i]= questCard[i].transform.FindChild("complete").GetComponent<UISprite>();
            exploreSprite[i] = questCard[i].transform.FindChild("explore").GetComponent<UISprite>();
            rankSSprite[i] = questCard[i].transform.FindChild("rankS").GetComponent<UISprite>();
            huntingSprite[i] = questCard[i].transform.FindChild("hunting").GetComponent<UISprite>();
            questNameLabel[i] = Grid.FindChild(i.ToString()).FindChild("questname").GetComponent<UILabel>();
            questNameLabel[i].text = QuestList[i].QuestName;
        }
        maxQuestNum = questNum;

        mapNameLabel = questChoiceUI.transform.FindChild("map").FindChild("mapName").GetComponent<UILabel>();
        mapSprite = questChoiceUI.transform.FindChild("map").FindChild("mapAssign").GetComponent<UISprite>();
    }


    public static void LoadQuestData()
    {
        QuestList.Clear();
        //讀取任務資料
        TextAsset XMLFlie_Map = Resources.Load<TextAsset>("String/Map");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Map.text);
        XmlNode root = doc.DocumentElement;
        XmlNode CurMapNode = root.SelectSingleNode(string.Format("Map{0}{1}", CurMapPage, CurMapID));
        for (int i = 0; i < Player.UnlockQuestDic[CurMapPage].MapQuestLevel[CurMapID]; i++)//最多10個任務
        {
            if (int.Parse(CurMapNode.SelectSingleNode(string.Format("Quest{0}ID", i + 1)).InnerText) != 0)
            {
                QuestList.Add(new Quest(int.Parse(CurMapNode.SelectSingleNode(string.Format("Quest{0}ID", i + 1)).InnerText)));
            }
        }
        questNum = QuestList.Count;

        for (int j = 0; j < completeSprite.Length; j++)//將所有任務成就清空
        {
            completeSprite[j].alpha = 0;
            exploreSprite[j].alpha = 0;
            rankSSprite[j].alpha = 0;
            huntingSprite[j].alpha = 0;
        }
        for (int i = 0; i < maxQuestNum; i++)
        {
            if (questNum < (i + 1))
            {
                questCard[i].SetActive(false);
            }
            else
            {
                questCard[i].SetActive(true);
                //////////////////////////////創建任務卡片，並抓取任務卡及各屬性物件////////////////////////////////
                //設定任務卡位置
                cardPos[i] = baseCardPos + (addCardPosY * i);
                //設定任務卡到對的位置上
                questCard[i].transform.localPosition = cardPos[i];
                //設定任務卡縮放
                questCard[i].transform.localScale = baseCardScale;
                //設定任務卡名稱
                questCard[i].name = (questNum - (i + 1)).ToString();
                //抓取任務卡的屬性數值的物件
                questNameLabel[i] = Grid.FindChild((questNum - (i + 1)).ToString()).FindChild("questname").GetComponent<UILabel>();
                questNameLabel[i].text = QuestList[(questNum - (i + 1))].QuestName;
                //給予任務ID判斷選取了哪個任務
                questCard[i].transform.FindChild("data").GetComponent<GoQuestData>().QuestID = QuestList[(questNum - (i + 1))].QuestID;
                questCard[i].transform.FindChild("start").GetComponent<GoReady>().QuestID = QuestList[(questNum - (i + 1))].QuestID;
                questCard[i].transform.FindChild("data").GetComponent<GoQuestData>().QuestLevel = questNum - i;
                questCard[i].transform.FindChild("start").GetComponent<GoReady>().QuestLevel = questNum - i;


                if (Player.QuestAchievementDic.ContainsKey(QuestList[(questNum - (i + 1))].QuestID))//將有達成的成就顯示出來
                {
                    if (Player.QuestAchievementDic[QuestList[(questNum - (i + 1))].QuestID].Achieve[1])
                        completeSprite[i].alpha = 255;
                    if (Player.QuestAchievementDic[QuestList[(questNum - (i + 1))].QuestID].Achieve[2])
                        exploreSprite[i].alpha = 255;
                    if (Player.QuestAchievementDic[QuestList[(questNum - (i + 1))].QuestID].Achieve[3])
                        rankSSprite[i].alpha = 255;
                    if (Player.QuestAchievementDic[QuestList[(questNum - (i + 1))].QuestID].Achieve[4])
                        huntingSprite[i].alpha = 255;
                }
            }
        }
        if (questNum > maxQuestNum) maxQuestNum = questNum;
        mapNameLabel.text = GameDictionary.MapDic[int.Parse(CurMapPage.ToString() + CurMapID.ToString())].MapName;
        mapSprite.spriteName = "map" + CurMapPage.ToString() + CurMapID.ToString();
    }
}
