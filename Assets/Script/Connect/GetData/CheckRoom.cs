using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int[] IniRoomID;//初始化的房間ID
    static int curInRoomFloor;//初始怪獸放置到第幾個房間
    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckRoomInstance()//向Server取得玩者房間
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerRoomIGuid, CallBack_CheckRoomInstance, null);
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
    static void CallBack_CheckRoomInstance(int code, object data, object token)
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
                Debug.LogWarning("尚未實例化玩家房間物件");
                IniRoomInstance();//實例化一個新物品
                return;
            }
            ArcaletSetter.GetPlayerRoomMaxProgress = list.Count;
            GetRoom();//向Server取得玩者所有房間
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
    public static void IniRoomInstance()//實例化新物品
    {
        int createRoomNum = 3;//起始創造3個房間給玩家
        IniRoomID = new int[createRoomNum];
        ArcaletSetter.IniRoomMaxProgress = createRoomNum;
        for (int i = 0; i < createRoomNum;i++ )
        {
            ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerRoomIGuid, CallBack_NewRoomInstance, i);
        }
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewRoomInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerRoomItemID))
            {
                IniRoomID[int.Parse(token.ToString())] = PlayerRoomItemID;
                InitializeRoomAttribute();//初始化玩家房間屬性
            }
            else
            {
                Debug.Log(string.Format("取得PlayerRoom實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerRoom實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者房間屬性
    /// </summary>
    static void GetRoom()//向Server取得玩者房間屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("RoomID");
        propertyList.Add("LV");
        propertyList.Add("Name");
        propertyList.Add("Style");
        IODataFromArcalet.GetRoom(propertyList);//向Server取得玩者房間
    }
    /// <summary>
    /// 初始化玩家房間屬性
    /// </summary>
    static void InitializeRoomAttribute()//初始化玩家房間屬性
    {
        string[] attrNameArray = new string[4] { "RoomID", "LV", "Name", "Style" };
        string[] attrValueArray = new string[4];
        attrValueArray[0] = PlayerRoomItemID.ToString();
        attrValueArray[1] = "1";
        attrValueArray[2] = string.Format("Room{0}", PlayerRoomItemID);
        attrValueArray[3] = "1";
        IODataFromArcalet.IniRooms(attrNameArray, attrValueArray);
    }
}
