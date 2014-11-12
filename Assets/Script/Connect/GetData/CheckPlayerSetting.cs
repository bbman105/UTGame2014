using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 向Server檢查玩者設定實例
    /// </summary>
    public static void CheckPlayerSettingInstance()//向Server取得玩者基本設定
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerSettingIGuid, CallBack_CheckPlayerSettingInstance, null);
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
    static void CallBack_CheckPlayerSettingInstance(int code, object data, object token)
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
                NewPlayerSettingInstance();//實例化一個新物品，產生一個新玩者設定
                Debug.Log("尚未實例化玩家基本設定物件");
                return;
            }
            else
            {
                //長度大於1則永遠只取一個Hashtable
                List<Hashtable> hashAttributeList = list[0]["attr"] as List<Hashtable>;
                foreach (Hashtable hashAttribute in hashAttributeList)
                {
                    string propertyName = hashAttribute["name"].ToString();
                    if (propertyName == "StartSet")
                    {
                        if (int.Parse(hashAttribute["value"].ToString()) == 0)//如果為0代表沒設定玩家初始資料
                        {
                            //如果玩家的StartSet=0，代表尚未初始化玩家設定
                            InitializePlayerSetting();//初始化玩家設定
                        }
                    }
                }
                //取得物品實例ID
                PlayerSettingItemID = int.Parse(list[0]["id"].ToString());
                GetPlayerSetting();//向Server取得玩者所有基本設定
                Debug.Log(string.Format("取得PlayerSetting實例化物件，物件ID:{0}", PlayerSettingItemID));
            }
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
    static void NewPlayerSettingInstance()//實例化一個新物品
    {
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerSettingIGuid, CallBack_NewPlayerSettingInstance, null);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewPlayerSettingInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerSettingItemID))
            {
                InitializePlayerSetting();//初始化玩者設定
            }
            else
            {
                Debug.Log(string.Format("取得PlayerSetting實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerSetting實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者基本設定屬性
    /// </summary>
    static void GetPlayerSetting()//向Server取得玩者基本設定屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("StartSet");
        IODataFromArcalet.GetPlayerSetting(propertyList);//向Server取得玩者基本設定
    }
    /// <summary>
    /// 初始化玩者設定
    /// </summary>
    static void InitializePlayerSetting()//初始化玩者設定
    {
        string[] attrNameArray = new string[1] { "StartSet" };
        string[] attrValueArray = new string[1] { "0" };
        IODataFromArcalet.IniPlayerSettings(attrNameArray, attrValueArray);
    }
}
