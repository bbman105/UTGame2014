using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerEnhanceResourceItemID;
    /// <summary>
    /// 向Server寫入玩者資源，傳入資料名稱
    /// </summary>
    public static void SetPlayerEnhanceResource(string _resourceName, string _resourceNum)//向Server寫入玩者資源
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerEnhanceResourceIGuid,
                PlayerEnhanceResourceItemID, _resourceName, _resourceNum, CallBack_SetPlayerEnhanceResourceInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerEnhanceResourceInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入怪獸強化素材成功");
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家資源資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者資源，傳入資料名稱
    /// </summary>
    public static void IniPlayerEnhanceResources(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者資源
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerEnhanceResourceIGuid,
                PlayerEnhanceResourceItemID, _resourceNameArray, _resourceNumArray, CallBack_SetPlayerEnhanceResourcesInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerEnhanceResourcesInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("EnhanceResource");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家資源資料時出錯 - Error:" + code);
            return;
        }
    }
}
