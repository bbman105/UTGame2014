﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server取得玩者隊伍，傳入資料名稱List
    /// </summary>
    public static void GetTeam(List<string> _propertyNameList)//向Server取得玩者隊伍
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerTeamIGuid, CallBack_GetTeam, _propertyNameList);
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
    static void CallBack_GetTeam(int code, object data, object token)
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
            List<Dictionary<string, string>> TeamItemList = new List<Dictionary<string, string>>();//初始化物品清單

            for (int i = 0; i < list.Count; i++)
            {
                Dictionary<string, string> propertyDic = new Dictionary<string, string>();//初始化物品的屬性字典
                List<string> propertyNameList = token as List<string>;
                //取得該物品ID的屬性清單
                List<Hashtable> hashAttributeList = list[i]["attr"] as List<Hashtable>;
                foreach (Hashtable hashAttribute in hashAttributeList)
                {
                    string propertyName = hashAttribute["name"].ToString();
                    if (propertyNameList.Contains(propertyName))
                    {
                        propertyDic.Add(propertyName, hashAttribute["value"].ToString());
                    }
                    else
                    {
                        Debug.LogWarning(string.Format("PlayerTeamData資料表中的屬性名稱{0}不在欲取得的屬性名稱陣列中", propertyName));
                    }
                }
                TeamItemList.Add(propertyDic);//將此實例物品屬性的字典存入清單中
                ArcaletSetter.GetDataProgress("PlayerTeam");//設定資料進度，標示為已向Server取得隊伍強化素材
            }
            Player.StartTeamSet(TeamItemList);//傳入玩家擁有的隊伍資訊並創造隊伍
        }
        //Code非0表示取得資料失敗
        else
        {
            Debug.LogWarning("向Server取得PlayerTeamData失敗 - Error:" + code);
            return;
        }
    }
}