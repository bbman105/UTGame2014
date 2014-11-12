using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerRoomItemID;
    /// <summary>
    /// 向Server寫入玩者房間，傳入資料名稱
    /// </summary>
    public static void SetRoom(string _resourceName, string _resourceNum)//向Server寫入玩者房間
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid,
                PlayerRoomItemID, _resourceName, _resourceNum, CallBack_SetRoomInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetRoomInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家房間成功");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家房間資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者房間，傳入資料名稱
    /// </summary>
    public static void IniRooms(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者房間
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerRoomIGuid,
                PlayerRoomItemID, _resourceNameArray, _resourceNumArray, CallBack_SetRoomsInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetRoomsInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerRoom");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家房間資料時出錯 - Error:" + code);
            return;
        }
    }
}
