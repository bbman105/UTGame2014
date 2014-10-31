using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerMonsterItemID;
    /// <summary>
    /// 向Server寫入玩者怪獸，傳入資料名稱，EX:Gold、Dimand..etc
    /// </summary>
    public static void SetPlayerMonster(string _resourceName, string _resourceNum)//向Server寫入玩者怪獸
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid,
                PlayerMonsterItemID, _resourceName, _resourceNum, CallBack_SetPlayerMonsterInstance, _resourceNum);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerMonsterInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家怪獸資料成功!");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家怪獸資料時出錯 - Error:" + code);
            return;
        }
    }
}
