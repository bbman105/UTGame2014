using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int curInTeam;//初始化放置第幾個隊伍
    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckTeamInstance()//向Server取得玩者隊伍
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerTeamIGuid, CallBack_CheckTeamInstance, null);
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
    static void CallBack_CheckTeamInstance(int code, object data, object token)
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
                Debug.LogWarning("尚未實例化玩家隊伍物件");
                IniTeamInstance();//實例化一個新物品
                return;
            }
            ArcaletSetter.GetTeamMaxProgress = list.Count;
            GetTeam();//向Server取得玩者所有隊伍
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
    public static void IniTeamInstance()//實例化新物品
    {
        int createTeamNum = 3;//起始創造3個隊伍給玩家
        ArcaletSetter.IniTeamMaxProgress = createTeamNum;
        for (int i = 0; i < createTeamNum; i++)
        {
            ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerTeamIGuid, CallBack_NewTeamInstance, null);
        }
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewTeamInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerTeamItemID))
            {
                InitializeTeamAttribute();//初始化玩家隊伍屬性
            }
            else
            {
                Debug.Log(string.Format("取得PlayerTeam實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerTeam實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者隊伍屬性
    /// </summary>
    static void GetTeam()//向Server取得玩者隊伍屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("TeamID");
        propertyList.Add("TeamName");
        propertyList.Add("Member1ID");
        propertyList.Add("Member2ID");
        propertyList.Add("Member3ID");
        IODataFromArcalet.GetTeam(propertyList);//向Server取得玩者隊伍
    }
    /// <summary>
    /// 初始化玩家隊伍屬性
    /// </summary>
    static void InitializeTeamAttribute()//初始化玩家隊伍屬性
    {
        curInTeam++;
        string[] attrNameArray = new string[5] { "TeamID", "TeamName", "Member1ID", "Member2ID", "Member3ID" };
        string[] attrValueArray = new string[5];
        attrValueArray[0] = PlayerTeamItemID.ToString();
        attrValueArray[1] = string.Format("隊伍{0}", curInTeam);
        if (curInTeam == 1)
        {
            attrValueArray[2] = DefaultMonsterIDArray[0].ToString();
            attrValueArray[3] = DefaultMonsterIDArray[1].ToString();
            attrValueArray[4] = DefaultMonsterIDArray[2].ToString();
        }
        else
        {
            attrValueArray[2] = "0";
            attrValueArray[3] = "0";
            attrValueArray[4] = "0";
        }
        IODataFromArcalet.IniTeams(attrNameArray, attrValueArray);
    }
}
