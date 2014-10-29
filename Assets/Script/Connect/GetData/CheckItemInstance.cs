using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckPlayerItemInstance()//向Server取得玩者資源
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_CheckPlayerItemInstance, null);
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
    static void CallBack_CheckPlayerItemInstance(int code, object data, object token)
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
                NewItemInstance(ArcaletSetter.PlayerResourceIGuid);//實例化一個新玩家資料物品，傳入PlayerResourceIGuid
                Debug.Log("尚未實例化玩家資源物件，進行資料實例化");
                return;
            }
            //取得物品實例ID
            PlayerresourceItemID = int.Parse(list[0]["id"].ToString());
            GetPlayerAllResource();//向Server取得玩者所有資源
            Debug.Log(string.Format("取得實例化玩家資源物件，物件ID:{0}", PlayerresourceItemID));
        }
        else//Code非0表示取得資料失敗
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 向Server取得玩者所有資源
    /// </summary>
    static void GetPlayerAllResource()//向Server取得玩者所有資源
    {
        IODataFromArcalet.GetPlayerResource("Gold");//向Server取得玩者金幣
        IODataFromArcalet.GetPlayerResource("CurEnergy");//向Server取得玩者目前能量
        IODataFromArcalet.GetPlayerResource("MaxEnergy");//向Server取得玩者最大能量
        IODataFromArcalet.GetPlayerResource("Dimand");//向Server取得玩者鑽石
    }
}
