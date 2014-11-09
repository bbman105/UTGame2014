using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public partial class Player : MonoBehaviour
{
    //玩家解鎖任務成就進度
    public static Dictionary<int, PlayerOwnQuestAchievement> QuestAchievementDic { get; set; }//玩家擁有任務成就字典，給予任務編號找到擁有成就清單，[1]=完成成就，[2]=探索成就,[3]=評價成就,[4]=懸賞成就
    //讀取玩家達成的任務成就
    public static void StartQuestAchievementSet(List<Dictionary<string, string>> _QuestAchievementItemList)
    {
        //讀取資料庫中玩家擁有的怪物ID，並建立怪物物件
        CreateQuestAchievementDic(_QuestAchievementItemList);
    }
    //建立玩家擁有的成就字典
    static void CreateQuestAchievementDic(List<Dictionary<string, string>> _QuestAchievementItemList)
    {
        try
        {
            //建立擁有任務成就字典
            QuestAchievementDic = new Dictionary<int, PlayerOwnQuestAchievement>();
            //以迴圈搜尋此玩家擁有的隊伍，迴圈長度取決抓取到的node
            for (int i = 0; i < _QuestAchievementItemList.Count; i++)
            {
                int questID = int.Parse(_QuestAchievementItemList[i]["QuestID"]);
                if (questID == 0)
                    continue;
                int achievementTypeID = int.Parse(_QuestAchievementItemList[i]["AchievementType"]);
                if (achievementTypeID == 0)
                    continue;
                int achievementItemID = int.Parse(_QuestAchievementItemList[i]["QuestAchievementID"]);

                if (QuestAchievementDic.ContainsKey(questID))
                {
                    QuestAchievementDic[questID].AchievementItemID[achievementTypeID] = achievementItemID;
                    QuestAchievementDic[questID].Achieve[achievementTypeID] = true;
                }
                else
                {
                    QuestAchievementDic.Add(questID, new PlayerOwnQuestAchievement(achievementItemID, achievementTypeID));
                }
            }
        }
        catch
        {
            Debug.Log("讀入玩家擁有任務成就時，發生錯誤");
        }
    }
    /// <summary>
    /// 取得新任務成就，傳入任務ID與成就類型1=完成,2=探索,3=評價,4=懸賞
    /// </summary>
    public static void UnlockNewQuestAchievement(int _questID, int _achievementType)
    {
        Debug.Log("_achievementType=" + _achievementType);
        if (_achievementType == 0)
            return;
        string[] attrValueArray = new string[2] { _questID.ToString(), _achievementType.ToString() };
        IODataFromArcalet.NewQuestAchievementInstance(attrValueArray);
    }
    public static void CallBack_UnlockQuestAchievement(int _questAchievementItemID, int _questID, int _achievementType)
    {
        Debug.Log("_achievementType=" + _achievementType);
        if (QuestAchievementDic.ContainsKey(_questID))
        {
            QuestAchievementDic[_questID].AchievementItemID[_achievementType] = _questAchievementItemID;
            QuestAchievementDic[_questID].Achieve[_achievementType] = true;
        }
        else
        {
            QuestAchievementDic.Add(_questID, new PlayerOwnQuestAchievement(_questAchievementItemID, _achievementType));
        }
    }

}
