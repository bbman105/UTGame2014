using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckMonsterInstance()//向Server取得玩者怪獸
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_CheckMonsterInstance, null);
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
    static void CallBack_CheckMonsterInstance(int code, object data, object token)
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
            if (list.Count == 0)//長度為0表示這個Item還沒有實例化過，進行實例化
            {
                NewMonsterInstance();//實例化一個新玩家怪獸Item
                Debug.Log("尚未實例化玩家怪獸物件，進行資料實例化");
                return;
            }
            //取得物品實例ID
            PlayerMonsterItemID = int.Parse(list[0]["id"].ToString());
            GetMonster();//向Server取得玩者所有怪獸
            Debug.Log(string.Format("取得PlayerResource實例化物件，物件ID:{0}", PlayerMonsterItemID));
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
    /// <param name="_iGuid"></param>
    static void NewMonsterInstance()//實例化一個新物品
    {
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_NewMonsterInstance, null);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewMonsterInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerMonsterItemID))
            {
                GetMonster();//向Server取得玩者所有怪獸
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
    /// 向Server取得玩者怪獸屬性
    /// </summary>
    static void GetMonster()//向Server取得玩者怪獸屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("Name");
        propertyList.Add("Species");
        propertyList.Add("SpeciesLevel");
        propertyList.Add("Rare");
        propertyList.Add("LV");
        propertyList.Add("LvHealth");
        propertyList.Add("Wild");
        propertyList.Add("Mutation");
        propertyList.Add("Natural");
        propertyList.Add("Happy");
        propertyList.Add("LvBrutal");
        propertyList.Add("LvAggressive");
        propertyList.Add("LvResistance");
        propertyList.Add("MainElement");
        propertyList.Add("EXP");
        propertyList.Add("Personality");
        propertyList.Add("SelectedSkill");
        propertyList.Add("Level2Species");
        propertyList.Add("Level3Species");
        propertyList.Add("InRoomID");
        IODataFromArcalet.GetPlayerResource(propertyList);//向Server取得玩者金幣
    }
}
