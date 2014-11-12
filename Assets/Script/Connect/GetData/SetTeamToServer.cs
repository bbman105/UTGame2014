using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerTeamItemID;
    /// <summary>
    /// 向Server寫入玩者隊伍，傳入資料名稱
    /// </summary>
    public static void SetTeam(string _resourceName, string _resourceNum)//向Server寫入玩者隊伍
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid,
                PlayerTeamItemID, _resourceName, _resourceNum, CallBack_SetTeamInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetTeamInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家隊伍成功");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家隊伍資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者隊伍，傳入資料名稱
    /// </summary>
    public static void IniTeams(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者隊伍
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerTeamIGuid,
                PlayerTeamItemID, _resourceNameArray, _resourceNumArray, CallBack_SetTeamsInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetTeamsInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerTeam");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家隊伍資料時出錯 - Error:" + code);
            return;
        }
    }
}
