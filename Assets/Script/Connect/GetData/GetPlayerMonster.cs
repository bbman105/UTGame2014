using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server取得玩者怪獸，傳入資料名稱List
    /// </summary>
    public static void GetPlayerMonster(List<string> _propertyNameList)//向Server取得玩者怪獸
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerEnhanceResourceIGuid, CallBack_GetPlayerEnhanceResource, _propertyNameList);
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
    static void CallBack_GetPlayerMonster(int code, object data, object token)
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
                    int attributeValue = 0;
                    //取得物品實例ID
                    PlayerMonsterItemID = int.Parse(list[0]["id"].ToString());
                    //設定Client端怪獸
                    Player.OwnResource.SetEnhanceMaterial(propertyName, attributeValue);
                }
                else
                {
                    Debug.LogWarning(string.Format("PlayerEnhanceResource資料表中的屬性名稱{0}不在欲取得的屬性名稱陣列中", propertyName));
                }
            }
        }
        //Code非0表示取得資料失敗
        else
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
        ArcaletSetter.SetDataProgressCheck(2);//設定資料進度，標示為已向Server取得怪獸強化素材
        Debug.Log("取得玩家怪獸資料成功!");
    }
}
