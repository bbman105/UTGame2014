﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckResourceInstance()//向Server取得玩者資源
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_CheckResourceInstance, null);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
            Debug.LogWarning("向Server取得資料時發生不明錯誤");
        }
    }
    /*******************************************************************
    * 取得的資料型態為 List<HashTable>
    * 需要資料格式的轉換 
    *******************************************************************/
    static void CallBack_CheckResourceInstance(int code, object data, object token)
    {
        //Code為0表示取得資料成功
        if (code == 0)
        {
            /*******************************************************************
            * 取得的資料型態為 List<HashTable>
            * 需要資料格式的轉換
            * 玩家初登入遊戲時不會有任何Instant Item,所以 List<Hashtable>.Count長度必為0
            *******************************************************************/
            List<Hashtable> list = data as List<Hashtable>;
            if (list.Count == 0)//長度為0表示這個Item還沒有實例化過
            {
                NewResourceInstance();//實例化一個新物品
                Debug.LogWarning("尚未實例化玩家資源物件");
                return;
            }
            //取得物品實例ID
            PlayerResourceItemID = int.Parse(list[0]["id"].ToString());
            GetResource();//向Server取得玩者資源
            Debug.Log(string.Format("取得PlayerResource實例化物件，物件ID:{0}", PlayerResourceItemID));
        }
        else//Code非0表示取得資料失敗
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 實例化一個新物品
    /// </summary>
    public static void NewResourceInstance()//實例化一個新物品
    {
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_NewResourceInstance, null);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewResourceInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerResourceItemID))
            {
                InitializeResource();//初始化玩家資源屬性
            }
            else
            {
                Debug.Log(string.Format("取得PlayerResource實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerResource實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者資源屬性
    /// </summary>
    static void GetResource()//向Server取得玩者資源屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("Gold");
        propertyList.Add("CurEnergy");
        propertyList.Add("MaxEnergy");
        propertyList.Add("Dimand");
        IODataFromArcalet.GetPlayerResource(propertyList);//向Server取得玩者金幣
    }
    /// <summary>
    /// 初始化玩家資源屬性
    /// </summary>
    static void InitializeResource()//初始化玩家資源屬性
    {
        string[] attrNameArray = new string[4] { "Gold", "CurEnergy", "MaxEnergy", "Dimand" };
        string[] attrValueArray = new string[4] { "100", "10", "10", "0" };
        IODataFromArcalet.IniPlayerResources(attrNameArray, attrValueArray);
    }
}
