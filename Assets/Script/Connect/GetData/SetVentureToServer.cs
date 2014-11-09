using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerVentureItemID;
    /// <summary>
    /// 向Server寫入玩者冒險進度，傳入資料名稱
    /// </summary>
    public static void SetVenture(string _resourceName, string _resourceNum, int _playerVentureItemID)//向Server寫入玩者冒險進度
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid,
                _playerVentureItemID, _resourceName, _resourceNum, CallBack_SetVentureInstance, _playerVentureItemID);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    /// <summary>
    /// 向Server寫入玩者冒險進度，傳入資料名稱
    /// </summary>
    public static void SetVenture(string[] _resourceNameArray, string[] _resourceNumArray, int _playerVentureItemID)//向Server寫入玩者冒險進度
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid,
                _playerVentureItemID, _resourceNameArray, _resourceNumArray, CallBack_SetVentureInstance, _playerVentureItemID);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetVentureInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家冒險進度成功");
            Player.CallBack_UnlockQuest(int.Parse(token.ToString()));//設定Client玩家解鎖任務
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家冒險進度資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者冒險進度，傳入資料名稱
    /// </summary>
    public static void IniVentures(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者冒險進度
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid,
                PlayerVentureItemID, _resourceNameArray, _resourceNumArray, CallBack_SetVenturesInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetVenturesInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerVenture");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家冒險進度資料時出錯 - Error:" + code);
            return;
        }
    }
}
