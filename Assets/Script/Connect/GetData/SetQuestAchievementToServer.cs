using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int QuestAchievementItemID;
    /// <summary>
    /// 向Server寫入玩者冒險成就，傳入資料名稱
    /// </summary>
    public static void SetQuestAchievement(string _resourceName, string _resourceNum, int _questVentureItemID)//向Server寫入玩者冒險成就
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid,
                _questVentureItemID, _resourceName, _resourceNum, CallBack_SetQuestAchievementInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    /// <summary>
    /// 向Server寫入玩者冒險成就，傳入資料名稱
    /// </summary>
    public static void SetQuestAchievement(string[] _resourceNameArray, string[] _resourceNumArray, int _questVentureItemID)//向Server寫入玩者冒險成就
    {
        try
        {
            if (_resourceNameArray.Length != _resourceNumArray.Length || _resourceNumArray.Length != 3)
            {
                Debug.LogWarning("要設定QuestAchievement的屬性名稱陣列的長度或屬性值陣列的長度錯誤");
                return;
            }
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid,
                _questVentureItemID, _resourceNameArray, _resourceNumArray, CallBack_SetQuestAchievementInstance, _resourceNumArray);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetQuestAchievementInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            if (token == null)
                return;
            string[] valueArray = (string[])token;
            Player.CallBack_UnlockQuestAchievement(int.Parse(valueArray[0]), int.Parse(valueArray[1]), int.Parse(valueArray[2]));//設定Client玩家解鎖任務成就
            Debug.Log("寫入玩家冒險成就成功");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家冒險成就資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者冒險成就，傳入資料名稱
    /// </summary>
    public static void IniQuestAchievements(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者冒險成就
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid,
                QuestAchievementItemID, _resourceNameArray, _resourceNumArray, CallBack_SetQuestAchievementsInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetQuestAchievementsInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerQuestAchievement");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家冒險成就資料時出錯 - Error:" + code);
            return;
        }
    }
}
