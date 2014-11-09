using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckEnhanceResourceInstance()//向Server取得玩者資源
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerEnhanceResourceIGuid, CallBack_CheckEnhanceResourceInstance, null);
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
    static void CallBack_CheckEnhanceResourceInstance(int code, object data, object token)
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
                Debug.Log("尚未實例化玩家資源物件");
                NewEnhanceResourceInstance();//實例化一個新物品
                return;
            }
            //取得物品實例ID
            PlayerEnhanceResourceItemID = int.Parse(list[0]["id"].ToString());
            GetEnhanceResource();//向Server取得玩者所有資源
            Debug.Log(string.Format("取得PlayerEnhanceResource實例化物件，物件ID:{0}", PlayerEnhanceResourceItemID));
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
    public static void NewEnhanceResourceInstance()//實例化一個新物品
    {
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerEnhanceResourceIGuid, CallBack_NewEnhanceResourceInstance, null);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewEnhanceResourceInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerEnhanceResourceItemID))
            {
                InitializeEnhanceResource();//初始化玩者設定
            }
            else
            {
                Debug.Log(string.Format("取得PlayerEnhanceResource實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerEnhanceResource實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// //向Server取得玩者強化怪獸資源
    /// </summary>
    static void GetEnhanceResource()//向Server取得玩者強化怪獸資源
    {
        List<String> propertyList = new List<string>();
        propertyList.Add("FireSilver");
        propertyList.Add("FireGold");
        propertyList.Add("FireDimand");
        propertyList.Add("FireCrystal");
        propertyList.Add("WoodSilver");
        propertyList.Add("WoodGold");
        propertyList.Add("WoodDimand");
        propertyList.Add("WoodCrystal");
        propertyList.Add("WaterSilver");
        propertyList.Add("WaterGold");
        propertyList.Add("WaterDimand");
        propertyList.Add("WaterCrystal");
        IODataFromArcalet.GetPlayerEnhanceResource(propertyList);
    }
    /// <summary>
    /// 初始化玩者強化怪獸資源
    /// </summary>
    static void InitializeEnhanceResource()//初始化玩者設定
    {
        string[] attrNameArray = new string[12] { "FireSilver","FireGold","FireDimand","FireCrystal","WoodSilver","WoodGold","WoodDimand","WoodCrystal",
        "WaterSilver","WaterGold","WaterDimand","WaterCrystal"};
        string[] attrValueArray = new string[12] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        IODataFromArcalet.IniPlayerEnhanceResources(attrNameArray, attrValueArray);
    }
}
