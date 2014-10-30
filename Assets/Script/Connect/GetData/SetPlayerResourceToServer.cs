using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerResourceItemID;
    /// <summary>
    /// 向Server寫入玩者資源，傳入資料名稱，EX:Gold、Dimand..etc
    /// </summary>
    public static void SetPlayerResource(string _resourceName, string _resourceNum)//向Server寫入玩者資源
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid,
                PlayerResourceItemID, _resourceName, _resourceNum, CallBack_SetPlayerResourceInstance, _resourceNum);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerResourceInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家資源資料成功!");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家資源資料時出錯 - Error:" + code);
            return;
        }
    }
}
