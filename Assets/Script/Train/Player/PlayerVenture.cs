using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public partial class Player : MonoBehaviour
{

    //玩家解鎖任務進度字典[星球ID]QuestUnlock
    public static Dictionary<int, UnlockQuest> UnlockQuestDic { get; set; }
    static int CurUnlockPlanetID;
    static int CurUnlockMapID;
    static int CurUnlockQuestLevel;
    static int CurVentureID;
    static int UnLockQuestType = 0;//1=解鎖同地圖下一個任務,2=解鎖下個地圖,3=解鎖下個星球
    /// <summary>
    /// 依造傳入的擁有解鎖任務清單，建立解鎖進度字典
    /// </summary>
    public static void StartVentureSet(List<Dictionary<string, string>> _ventureItemList)
    {
        //讀取資料庫中玩家擁有的怪物ID，並建立怪物物件
        CreateUnlockQuestDic(_ventureItemList);
    }
    static void CreateUnlockQuestDic(List<Dictionary<string, string>> _ventureItemList)
    {
        //讀取xml文件
        try
        {
            //建立解鎖任務字典
            UnlockQuestDic = new Dictionary<int, UnlockQuest>();
            //以迴圈搜尋此玩家擁有的隊伍，迴圈長度取決抓取到的node
            for (int i = 0; i < _ventureItemList.Count; i++)
            {
                int VentureID = int.Parse(_ventureItemList[i]["VentureID"]);//解所冒險的ItemID
                int UnlockPlanetID = int.Parse(_ventureItemList[i]["UnlockPlanet"]);//解鎖星球
                if (UnlockPlanetID == 0)//如果此星球還沒解鎖
                    continue;
                int UnlockMapID = int.Parse(_ventureItemList[i]["UnlockMap"]);//解鎖地圖
                if (UnlockMapID == 0)//如果此地圖還沒解鎖
                    continue;
                int UnlockQuestID = int.Parse(_ventureItemList[i]["UnlockQuest"]);//解鎖任務關卡
                UnlockQuest UnlockQuest = new UnlockQuest(VentureID, UnlockPlanetID, UnlockMapID, UnlockQuestID);
                if (!UnlockQuestDic.ContainsKey(UnlockPlanetID))
                    UnlockQuestDic.Add(UnlockPlanetID, UnlockQuest);
                else
                {
                    UnlockQuestDic[UnlockPlanetID].MapQuestLevel.Add(UnlockMapID, UnlockQuestID);
                    UnlockQuestDic[UnlockPlanetID].MapQuesItem.Add(UnlockMapID, VentureID);
                }
            }
        }
        catch
        {
            Debug.LogError("讀入解鎖任務資料(UnlockQuestData.xml)時，發生錯誤");
        }
    }
    /// <summary>
    /// 解鎖新任務
    /// </summary>
    public static void UnlockNewQuest(int _planetID, int _mapID,int _questLevel)
    {
        UnLockQuestType = 1;
        CurUnlockPlanetID = _planetID;
        CurUnlockMapID = _mapID;
        CurUnlockQuestLevel = _questLevel + 1;
        string attrName = "UnlockQuest";
        string attrValue = CurUnlockQuestLevel.ToString();
        IODataFromArcalet.SetVenture(attrName, attrValue, Player.UnlockQuestDic[CurUnlockPlanetID].MapQuesItem[CurUnlockMapID]);
    }
    /// <summary>
    /// 解鎖新地圖
    /// </summary>
    public static void UnlockNewMap(int _planetID, int _mapID)
    {
        UnLockQuestType = 2;
        CurUnlockPlanetID = _planetID;
        CurUnlockMapID = _mapID + 1;
        string[] attrValueArray = new string[3] { CurUnlockPlanetID.ToString(), CurUnlockMapID.ToString(), "1" };
        IODataFromArcalet.NewVentureInstance(attrValueArray);
    }

    /// <summary>
    /// Server新增新解鎖地圖後回傳設定Client
    /// </summary>
    public static void CallBack_UnlockQuest(int _ventureID)
    {
        switch (UnLockQuestType)
        {
            case 1://解鎖同地圖下一個任務
                UnlockQuestDic[CurUnlockPlanetID].MapQuestLevel[CurUnlockMapID] = CurUnlockQuestLevel;
                break;
            case 2://解鎖下個地圖
                if (UnlockQuestDic.ContainsKey(CurUnlockPlanetID))
                {
                    if (!UnlockQuestDic[CurUnlockPlanetID].MapQuestLevel.ContainsKey(CurUnlockMapID))
                    {
                        UnlockQuestDic[CurUnlockPlanetID].MapQuestLevel.Add(CurUnlockMapID, 1);
                    }
                    else
                    {
                        Debug.LogWarning("新增解鎖的地圖ID已存在，重複解鎖此地圖");
                    }
                    if (!UnlockQuestDic[CurUnlockPlanetID].MapQuesItem.ContainsKey(CurUnlockMapID))
                    {
                        UnlockQuestDic[CurUnlockPlanetID].MapQuesItem.Add(CurUnlockMapID, _ventureID);
                    }
                    else
                    {
                        Debug.LogWarning("新增解鎖的地圖ID已存在，重複解鎖此地圖");
                    }
                }
                else
                {
                    Debug.LogWarning("嘗試解鎖尚未解鎖的星球上的地圖");
                }
                break;
            case 3://解鎖下個星球
                break;
            default:
                Debug.LogWarning("解鎖關卡類型無法辨識");
                break;
        }

    }
}
