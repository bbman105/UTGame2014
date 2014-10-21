using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class MapUI : TrainSceneInterfaceUI
{
    //目前選擇的地圖分頁
    private static int curPlanetRank;
    public static int CurPlanetRank
    {
        get { return MapUI.curPlanetRank; }
        set
        {
            if (value < 1)
            {
                value = 1;
            }
            else if (value > PlanetNum)
            {
                value = PlanetNum;
            }
            else { MapUI.curPlanetRank = value; }
        }
    }
    //暫存解鎖地圖清單
    public static int PlanetNum;
    //地圖的sprite
    static private UISprite[] mapSprite;
    static private UISprite[] mapLock;
    //地圖名稱label
    static private UILabel[] mapNameLabel;
    //星球名稱
    static private UILabel planetNameLabel;
    //星球sprite
    //static private List<UISprite> planetSprite;


    public void StartSetting()
    {
        ///////////////////////初始化型別///////////////////////////
        //介面數值初始化
        int count = 0;//計數器
        Dictionary<int,Map>.KeyCollection keys=GameDictionary.MapDic.Keys;
        foreach (int key in keys)//計算有多少顆星球
        {
            if (GameDictionary.MapDic[key].PlanetID > count)
            {
                count++;
            }
        }
        PlanetNum = count;
        CurPlanetRank = 1;//目前選取在第幾個星球
        //初始化地圖卡上的ICon各屬性數值的物件
        mapSprite = new UISprite[6];
        mapLock = new UISprite[6];
        mapNameLabel = new UILabel[6];
        //planetSprite = new List<UISprite>();

        for (int i = 0; i < 6; i++)//一顆星球上有6個地圖
        {
            //抓取MapChoice上星球地圖的物件
            mapSprite[i] = mapChoiceUI.transform.FindChild("planet").FindChild(string.Format("map{0}", i+1)).GetComponent<UISprite>();
            mapLock[i] = mapSprite[i].transform.FindChild("lock").GetComponent<UISprite>();
            mapNameLabel[i] = mapChoiceUI.transform.FindChild("planet").FindChild(string.Format("mapName{0}", i + 1)).GetComponent<UILabel>();
            mapSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/TrainScene/Map/MapChoice");
        }
        planetNameLabel = mapChoiceUI.transform.FindChild("planetName").GetComponent<UILabel>();
    }


    public static void LoadMapData()
    {
        //設定星球名稱
        planetNameLabel.text = GameDictionary.MapDic[int.Parse(CurPlanetRank.ToString() + 1.ToString())].PlanetName;
        for (int i = 0; i < 6; i++)//一顆星球上有6個地圖
        {
            mapNameLabel[i].text = GameDictionary.MapDic[int.Parse(CurPlanetRank.ToString() + (i+1).ToString())].MapName;
            if (GameDictionary.UnlockQuestDic[CurPlanetRank].MapQuestLevel.ContainsKey(i+1))
            {
                mapSprite[i].color = Color.white;
                mapLock[i].alpha = 0;
                mapSprite[i].GetComponent<GoQuest>().MapID = i + 1;
                mapSprite[i].GetComponent<GoQuest>().PlanetID = CurPlanetRank;
            }
            else
            {
                mapSprite[i].color = Color.gray;
                mapLock[i].alpha = 255;
                mapSprite[i].GetComponent<GoQuest>().MapID = 0;
                mapSprite[i].GetComponent<GoQuest>().PlanetID = CurPlanetRank;
            }
        }
    }

}
