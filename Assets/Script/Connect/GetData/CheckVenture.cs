using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckVentureInstance()//向Server取得玩者冒險進度
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid, CallBack_CheckVentureInstance, null);
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
    static void CallBack_CheckVentureInstance(int code, object data, object token)
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
                Debug.LogWarning("尚未實例化玩家冒險進度物件");
                IniVentureInstance();//實例化一個新物品
                return;
            }
            ArcaletSetter.GetVentureMaxProgress = list.Count;
            GetVenture();//向Server取得玩者所有冒險進度
        }
        else//Code非0表示取得資料失敗
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
    }

    /// <summary>
    /// 實例化一個新物品，傳入string[3]{""UnlockPlanet",""UnlockMap","UnlockQuest"}的值
    /// </summary>
    public static void NewVentureInstance(string[] _attrValueArray)//實例化新物品
    {
        int tmpInt = 0;
        for (int i = 0; i < _attrValueArray.Length;i++ )
        {
            if (int.TryParse(_attrValueArray[i], out tmpInt))
            {
                if (_attrValueArray.Length != 3)
                {
                    Debug.LogWarning("傳入欲修改的屬性數值陣列長度不符合此Item擁有的屬性數量");
                    return;
                }
            }
            else//如果無法轉成數值，代表傳進的資料錯誤
            {
                Debug.LogWarning("實例化解鎖任務物品時，傳入引數文字無法轉為數值");
                return;
            }
        }
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid, CallBack_NewVentureInstance, _attrValueArray);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewVentureInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            PlayerVentureItemID = int.Parse(data.ToString());
            string[] attrNameArray = new string[4] { "VentureID", "UnlockPlanet", "UnlockMap", "UnlockQuest" };
            string[] tmpAttrValueArray = (string[])token;
            string[] attrValueArray = new string[4] { PlayerVentureItemID.ToString(), tmpAttrValueArray[0], tmpAttrValueArray[1], tmpAttrValueArray[2] };
            SetVenture(attrNameArray, attrValueArray, PlayerVentureItemID);//寫入冒險資料到Server
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerVenture實例化物品失敗 Failed - Error:" + code);
        }
    }

    /// <summary>
    /// 實例化一個新物品
    /// </summary>
    public static void IniVentureInstance()//實例化新物品
    {
        int createVentureNum = 1;//起始創造1個冒險進度給玩家
        ArcaletSetter.IniVentureMaxProgress = createVentureNum;
        for (int i = 0; i < createVentureNum; i++)
        {
            ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerVentureIGuid, CallBack_IniVentureInstance, null);
        }
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_IniVentureInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerVentureItemID))
            {
                InitializeVentureAttribute();//初始化玩家冒險進度屬性
            }
            else
            {
                Debug.Log(string.Format("取得PlayerVenture實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerVenture實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者冒險進度屬性
    /// </summary>
    static void GetVenture()//向Server取得玩者冒險進度屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("VentureID");
        propertyList.Add("UnlockPlanet");
        propertyList.Add("UnlockMap");
        propertyList.Add("UnlockQuest");
        IODataFromArcalet.GetVenture(propertyList);//向Server取得玩者冒險進度
    }
    /// <summary>
    /// 初始化玩家冒險進度屬性
    /// </summary>
    static void InitializeVentureAttribute()//初始化玩家冒險進度屬性
    {
        string[] attrNameArray = new string[4] { "VentureID", "UnlockPlanet", "UnlockMap", "UnlockQuest" };
        string[] attrValueArray = new string[4];
        attrValueArray[0] = PlayerVentureItemID.ToString();
        attrValueArray[1] = "1";
        attrValueArray[2] = "1";
        attrValueArray[3] = "9";
        IODataFromArcalet.IniVentures(attrNameArray, attrValueArray);
    }
}
