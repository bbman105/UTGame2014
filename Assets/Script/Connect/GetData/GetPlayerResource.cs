using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server取得玩者資源，傳入資料名稱List
    /// </summary>
    public static void GetPlayerResource(List<string> _propertyNameList)//向Server取得玩者資源
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerResourceIGuid, CallBack_GetPlayerResource, _propertyNameList);
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
    static void CallBack_GetPlayerResource(int code, object data, object token)
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
                Debug.LogWarning("尚未實例化物件");
                return;
            }
            List<string> propertyNameList = token as List<string>;
            //長度大於1則永遠只取一個Hashtable
            List<Hashtable> hashAttributeList = list[0]["attr"] as List<Hashtable>;
            foreach (Hashtable hashAttribute in hashAttributeList)
            {
                string propertyName = hashAttribute["name"].ToString();
                if (propertyNameList.Contains(propertyName))
                {
                    int attributeValue;
                    if (int.TryParse(hashAttribute["value"].ToString(), out attributeValue))
                    {
                        //取得物品實例ID
                        PlayerMonsterItemID = int.Parse(list[0]["id"].ToString());
                        //設定Client端資源
                        Player.OwnResource.SetResource(propertyName, attributeValue);
                    }
                    else
                    {
                        Debug.LogWarning(string.Format("讀取玩家資料，名為{0}的資料內容無法轉換為int", propertyName));
                        return;
                    }
                }
                else
                {
                    Debug.LogWarning(string.Format("PlayerResource資料表中的屬性名稱{0}不在欲取得的屬性名稱陣列中", propertyName));
                }
            }
        }
        //Code非0表示取得資料失敗
        else
        {
            Debug.LogWarning("向Server取得PlayerResourceData失敗 - Error:" + code);
            return;
        }
        ArcaletSetter.GetDataProgress("Resource");//標示為已向Server取得玩家資源
        Debug.Log("取得玩家資源資料成功!");
    }
}
