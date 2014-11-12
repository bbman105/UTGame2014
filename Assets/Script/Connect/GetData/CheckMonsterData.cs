using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{
    static int[] DefaultMonsterIDArray;//初始給予的怪獸IteamID
    static int curIniMonster;//初始化第幾隻怪獸
    /// <summary>
    /// 向Server檢查玩者資料實例
    /// </summary>
    public static void CheckMonsterInstance()//向Server取得玩者怪獸
    {
        try
        {
            ArcaletItem.GetItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerMonsterIGuid, CallBack_CheckMonsterInstance, null);
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
            if (list.Count == 0)//長度為0表示這個Item還沒有實例化過
            {
                IniMonsterInstance();//實例化一個新物品
                Debug.LogWarning("尚未實例化玩家怪獸物件");
                return;
            }
            ArcaletSetter.GetMonsterMaxProgress = list.Count;
            GetMonster();//向Server取得玩者所有怪獸
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
    public static void IniMonsterInstance()//實例化新物品
    {
        int createMonsterNum = 3;//起始創造3個房間給玩家
        //起始給玩家3隻預設怪獸
        DefaultMonsterIDArray = new int[createMonsterNum];
        int[] MonsterIDArray = new int[3] { 1, 2, 3 };
        ArcaletSetter.IniMonsterMaxProgress = createMonsterNum;
        for (int i = 0; i < createMonsterNum; i++)
        {
            ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, ArcaletSetter.PlayerMonsterIGuid, CallBack_IniMonsterInstance, MonsterIDArray[i]);
        }
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_IniMonsterInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerMonsterItemID))
            {
                DefaultMonsterIDArray[curIniMonster] = PlayerMonsterItemID;
                curIniMonster++;
                int defaultMonsterID = int.Parse(token.ToString());
                InitializeMonster(defaultMonsterID);//初始化玩家怪獸屬性
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
        propertyList.Add("MonsterID");
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
        propertyList.Add("LV2Species");
        propertyList.Add("LV3Species");
        propertyList.Add("InRoomID");
        IODataFromArcalet.GetPlayerMonster(propertyList);//向Server取得玩者金幣
    }
    /// <summary>
    /// 初始化玩家房間屬性
    /// </summary>
    static void InitializeMonster(int _defaultMonsterID)//初始化玩家怪獸屬性
    {
        string[] attrNameArray = new string[21] { "MonsterID","Name", "Species", "SpeciesLevel","Rare","LV","LvHealth","Wild","Mutation","Natural","Happy",
        "LvBrutal","LvAggressive","LvResistance","MainElement","EXP","Personality","SelectedSkill","LV2Species","LV3Species","InRoomID"};
        string[] attrValueArray = new string[21];
        attrValueArray[0] = PlayerMonsterItemID.ToString();
        attrValueArray[1] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].MonsterName;
        attrValueArray[2] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Species.ToString();
        attrValueArray[3] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].SpeciesLevel.ToString();
        attrValueArray[4] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Rare.ToString();
        attrValueArray[5] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].LV.ToString();
        attrValueArray[6] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].LvHealth.ToString();
        attrValueArray[7] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Wild.ToString();
        attrValueArray[8] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Mutation.ToString();
        attrValueArray[9] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Natural.ToString();
        attrValueArray[10] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Happy.ToString();
        attrValueArray[11] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].LvBrutal.ToString();
        attrValueArray[12] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].LvAggressive.ToString();
        attrValueArray[13] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].LvResistance.ToString();
        attrValueArray[14] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].MainElement.ToString();
        attrValueArray[15] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].EXP.ToString();
        attrValueArray[16] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Personality.ToString();
        attrValueArray[17] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].SelectedSkill.ToString();
        attrValueArray[18] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Level2Species.ToString();
        attrValueArray[19] = GameDictionary.DefaultMonsterDic[_defaultMonsterID].Level3Species.ToString();
        attrValueArray[20] = IniRoomID[curInRoomFloor].ToString();
        curInRoomFloor++;
        IODataFromArcalet.IniPlayerMonster(attrNameArray, attrValueArray);
    }
}
