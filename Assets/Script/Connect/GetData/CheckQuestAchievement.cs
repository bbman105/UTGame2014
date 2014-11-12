using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckQuestAchievementInstance()//向Server取得玩者冒險成就
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid, CallBack_CheckQuestAchievementInstance, null);
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
    static void CallBack_CheckQuestAchievementInstance(int code, object data, object token)
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
                Debug.LogWarning("尚未實例化玩家冒險成就物件");
                IniQuestAchievementInstance();//實例化一個新物品
                return;
            }
            ArcaletSetter.GetQuestAchievementMaxProgress = list.Count;
            GetQuestAchievement();//向Server取得玩者所有冒險成就
        }
        else//Code非0表示取得資料失敗
        {
            Debug.LogWarning("向Server取得資料失敗 - Error:" + code);
            return;
        }
    }
    /// <summary>
    /// 實例化一個新物品，傳入string[2]{""QuestID",""AchievementType"}的值
    /// </summary>
    public static void NewQuestAchievementInstance(string[] _attrValueArray)//實例化新物品
    {
        int tmpInt = 0;
        if (_attrValueArray.Length != 2)
        {
            Debug.LogWarning("傳入欲修改的屬性數值陣列長度不符合此Item擁有的屬性數量");
            return;
        }
        for (int i = 0; i < _attrValueArray.Length; i++)
        {
            if (int.TryParse(_attrValueArray[i], out tmpInt))
            {
            }
            else//如果無法轉成數值，代表傳進的資料錯誤
            {
                Debug.LogWarning("實例化任務成就物品時，傳入引數文字無法轉為數值");
                return;
            }
        }
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid, CallBack_NewQuestAchievementInstance, _attrValueArray);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewQuestAchievementInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out QuestAchievementItemID))
            {
                string[] attrNameArray = new string[3] { "QuestAchievementID", "QuestID", "AchievementType" };
                string[] tmpAttrValueArray = (string[])token;
                string[] attrValueArray = new string[3] { QuestAchievementItemID.ToString(), tmpAttrValueArray[0], tmpAttrValueArray[1] };
                SetQuestAchievement(attrNameArray, attrValueArray, QuestAchievementItemID);//寫入冒險成就資料到Server
            }
            else
            {
                Debug.Log(string.Format("取得PlayerQuestAchievement實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerQuestAchievement實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 實例化一個新物品
    /// </summary>
    public static void IniQuestAchievementInstance()//實例化新物品
    {
        int createQuestAchievementNum = 1;//起始創造1個冒險成就給玩家
        ArcaletSetter.IniQuestAchievementMaxProgress = createQuestAchievementNum;
        for (int i = 0; i < createQuestAchievementNum; i++)
        {
            ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerQuestAchievementIGuid, CallBack_IniQuestAchievementInstance, null);
        }
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_IniQuestAchievementInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out QuestAchievementItemID))
            {
                InitializeQuestAchievementAttribute();//初始化玩家冒險成就屬性
            }
            else
            {
                Debug.Log(string.Format("取得PlayerQuestAchievement實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("PlayerQuestAchievement實例化物品失敗 Failed - Error:" + code);
        }
    }
    /// <summary>
    /// 向Server取得玩者冒險成就屬性
    /// </summary>
    static void GetQuestAchievement()//向Server取得玩者冒險成就屬性
    {
        List<string> propertyList = new List<string>();
        propertyList.Add("QuestAchievementID");
        propertyList.Add("QuestID");
        propertyList.Add("AchievementType");
        IODataFromArcalet.GetQuestAchievement(propertyList);//向Server取得玩者冒險成就
    }
    /// <summary>
    /// 初始化玩家冒險成就屬性
    /// </summary>
    static void InitializeQuestAchievementAttribute()//初始化玩家冒險成就屬性
    {

        string[] attrNameArray = new string[3] { "QuestAchievementID", "QuestID", "AchievementType" };
        string[] attrValueArray = new string[3];
        attrValueArray[0] = QuestAchievementItemID.ToString();
        attrValueArray[1] = "0";
        attrValueArray[2] = "0";
        IODataFromArcalet.IniQuestAchievements(attrNameArray, attrValueArray);

    }
}
