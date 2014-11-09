using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerMonsterItemID;
    /// <summary>
    /// 向Server寫入玩者怪獸資料
    /// </summary>
    public static void SetPlayerMonster(string _resourceName, string _resourceNum, int _playerMonsterItemID)//向Server寫入玩者怪獸
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerMonsterIGuid,
                _playerMonsterItemID, _resourceName, _resourceNum, CallBack_SetPlayerMonsterInstance, _resourceNum);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    /// <summary>
    /// 向Server寫入玩者怪獸資料
    /// </summary>
    public static void SetPlayerMonster(string[] _resourceNameArray, string[] _resourceNumArray, int _playerMonsterItemID)//向Server寫入玩者怪獸
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerMonsterIGuid,
                _playerMonsterItemID, _resourceNameArray, _resourceNumArray, CallBack_SetPlayerMonsterInstance, null);
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
            Debug.Log("寫入怪獸資料成功");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家怪獸資料時出錯 - Error:" + code);
            return;
        }
    }

    /// <summary>
    /// 向Server寫入玩者怪獸，傳入資料名稱
    /// </summary>
    public static void IniPlayerMonster(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者怪獸
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerMonsterIGuid,
                PlayerMonsterItemID, _resourceNameArray, _resourceNumArray, CallBack_IniPlayerMonstersInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_IniPlayerMonstersInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerMonster");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家怪獸資料時出錯 - Error:" + code);
            return;
        }
    }
}
