using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int PlayerSettingItemID;
    /// <summary>
    /// 向Server寫入玩者設定，傳入資料名稱
    /// </summary>
    public static void SetPlayerSetting(string _resourceName, string _resourceNum)//向Server寫入玩者設定
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerSettingIGuid,
                PlayerSettingItemID, _resourceName, _resourceNum, CallBack_SetPlayerSettingInstance, _resourceName);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerSettingInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            Debug.Log("寫入玩家設定資料成功");
            if (token.ToString() == "StartSet")
                ArcaletSetter.InitializeDataProgress("Initialize");//標示為已向Server取得玩家設定

        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家設定資料時出錯 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server寫入玩者設定，傳入資料名稱
    /// </summary>
    public static void IniPlayerSettings(string[] _resourceNameArray, string[] _resourceNumArray)//向Server寫入玩者設定
    {
        try
        {
            ArcaletItem.SetItemInstanceAttribute(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerSettingIGuid,
                PlayerSettingItemID, _resourceNameArray, _resourceNumArray, CallBack_SetPlayerSettingsInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server寫入資料時發生不明錯誤");
        }
    }
    static void CallBack_SetPlayerSettingsInstance(int code, object token)
    {
        //Code為0表示寫入資料成功
        if (code == 0)
        {
            ArcaletSetter.InitializeDataProgress("PlayerSetting");//標示為已向Server取得玩家設定
        }
        //Code非0表示寫入資料失敗
        else
        {
            Debug.LogWarning("寫入玩家設定資料時出錯 - Error:" + code);
            return;
        }
    }
}
