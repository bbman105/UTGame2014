using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server取得玩者資源，傳入資料名稱，EX:Gold、Dimand..etc
    /// </summary>
    public static void GetPlayerResource(string _resourceName)//向Server取得玩者資源
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_GetPlayerResourceInstance, _resourceName);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Debug.LogError("向Server取得資料時發生不明錯誤");
        }
    }
    /*******************************************************************
    * 取得的資料型態為 List<HashTable>
    * 需要資料格式的轉換 
    *******************************************************************/
    static void CallBack_GetPlayerResourceInstance(int code, object data, object token)
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
                Debug.LogError("尚未實例化物件");
                return;
            }
            string resourceName = token as string;
            if (resourceName == null)
            {
                Debug.LogError("傳入資料名稱時的資料型態不為字串，導致錯誤發生");
                return;
            }
            //長度大於1則永遠只取一個Hashtable
            List<Hashtable> hashAttributeList = list[0]["attr"] as List<Hashtable>;
            bool IsGetData = false;
            foreach (Hashtable hashAttribute in hashAttributeList)
            {
                if (hashAttribute["name"].ToString() == resourceName)
                {
                    IsGetData = true;
                    int attributeValue;
                    if (int.TryParse(hashAttribute["value"].ToString(), out attributeValue))
                    {
                        //取得物品實例ID
                        PlayerresourceItemID = int.Parse(list[0]["id"].ToString());
                        //設定Client端資源
                        Player.OwnResource.SetResource(resourceName, attributeValue);
                    }
                    else
                    {
                        Debug.LogError(string.Format("讀取玩家資料，名為{0}的資料內容無法轉換為int", resourceName));
                        return;
                    }
                }
            }
            if (!IsGetData)
            {
                Debug.LogError(string.Format("玩家資料庫中不存在名為{0}的資料", resourceName));
                return;
            }
        }
        //Code非0表示取得資料失敗
        else
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
        Debug.Log("取得玩家資源資料成功!");
    }
}
