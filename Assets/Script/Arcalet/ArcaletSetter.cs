﻿using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{

    //Arcalet宣告
    #region Variables
    //在此輸入開發者後台顯示的gguid
    private const string gguid = "caa6ccb9-bb3c-aa49-bbfc-6c2da84a757c";
    //在此輸入開發者後台顯示的sguid
    private const string sguid = "98a5b11d-b98e-864a-bd9a-cf3c75c6db7b";
    //PlayerSetting的iguid
    public const string PlayerSettingIGuid = "17903ce6-cece-844d-8108-f8d2515cd34d";
    //PlayerResourceData的iguid
    public const string PlayerResourceIGuid = "aeb7d8d3-4fea-cb49-8e3f-399907ade5b6";
    //PlayerEnhanceResourceData的iguid
    public const string PlayerEnhanceResourceIGuid = "c59d031f-e871-cb47-afc3-e37576df3763";
    //PlayerRoomData的iguid
    public const string PlayerRoomIGuid = "7b5be6b9-f783-424b-889a-684977bc32e5";
    //PlayerMonsterData的iguid
    public const string PlayerMonsterIGuid = "8b9f9ea5-14f8-1b48-a6d5-85d4b2facac9";
    //PlayerTeamData的iguid
    public const string PlayerTeamIGuid = "c70c2d26-b360-0243-a25c-f7a65f87814c";
    //PlayerVentureData的iguid
    public const string PlayerVentureIGuid = "981d48c7-629d-544a-a5de-934fad153308";
    //PlayerQuestAchievementData的iguid
    public const string PlayerQuestAchievementIGuid = "494a834e-00ea-f948-b27e-7a65a1a83133";
    //在此輸入開發者後台顯示的憑證
    //EX: byte[] certificate = {0x60,0x1f,0xef,0xc5,0x..,0x..,0x..,0x..};
    byte[] certificate = { 0xe9, 0xb4, 0xdb, 0x21, 0x6c, 0xa7, 0x25, 0x41, 0x87, 0x3b, 0xb2, 0x41, 0xe4, 0x27, 0x90, 0x8c, 0xce, 0x11, 0xd5, 0x92, 0x2, 0x40, 0x5a, 0x42, 0x85, 0x2a, 0x8a, 0xdd, 0xb5, 0x94, 0x50, 0x1a, 0xe9, 0x44, 0x35, 0xf4, 0xfb, 0x48, 0xb8, 0x49, 0x84, 0x22, 0x9f, 0x7d, 0xf7, 0x27, 0x6, 0x69, 0xfa, 0x33, 0x77, 0x76, 0x52, 0xf4, 0x78, 0x4f, 0xab, 0xa8, 0x4e, 0x3d, 0x50, 0xc, 0xf6, 0x21, 0xc1, 0x68, 0xd1, 0xa5, 0xd4, 0x21, 0x33, 0x42, 0x8e, 0xd1, 0x1d, 0x11, 0x8d, 0x8e, 0x80, 0xbf, 0x9d, 0xd2, 0x15, 0x3f, 0xf9, 0xe, 0xb0, 0x41, 0xb4, 0x27, 0xab, 0xb9, 0xff, 0xc9, 0xbc, 0x85, 0x82, 0xc9, 0x3a, 0x25, 0x39, 0xdb, 0x4b, 0x4a, 0xa2, 0xfd, 0xd5, 0xc7, 0x3b, 0x7c, 0xe6, 0x18, 0x6b, 0x5a, 0x18, 0xaf, 0x84, 0xde, 0x2a, 0x40, 0xa7, 0xb0, 0xd3, 0xa7, 0x7d, 0x81, 0xee, 0x16 };
    public static ArcaletGame arcaletGame = null;//宣告ArcaletGame, 此Class將負責與arcalet Server的通訊
    #endregion
    //連線狀態文字物件
    public static GameObject ConnectStatusTextObt;
    public static UILabel ConnectStatusText;
    void Awake()
    {
        //切換場景時不移除此物件
        DontDestroyOnLoad(gameObject);
        //抓取連線狀態文字物件
        ConnectStatusTextObt = GameObject.FindGameObjectWithTag("ConnectText").gameObject;
        ConnectStatusText = ConnectStatusTextObt.transform.FindChild("text").GetComponent<UILabel>();
    }

    /// <summary>
    /// 起始設定Arcalet
    /// </summary>
    public static void StartSetArcaletSetter()//起始設定Arcalet
    {
        StartSetPlayerLogin();//起始設定PlayerLogin
        StartSetGetDataFromServer();//初始設定GetDataFormServer
        ArcaletSystem.UnityEnvironment();
    }

    /// <summary>
    /// 當程式關閉時使用者帳號並不會登出,我們可以此加入自動登出的機制
    /// </summary>
    void OnApplicationQuit()
    {
        //當程式關閉時使用者帳號並不會登出,我們可以此加入自動登出的機制
        if (arcaletGame != null)
            Logout();
    }
}
