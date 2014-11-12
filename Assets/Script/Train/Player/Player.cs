using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public partial class Player : MonoBehaviour
{
    //PlayerID，不會有重複
    private static int playerID;
    public static int PlayerID
    {
        get { return playerID; }
        set
        {
            if (value < 0)
            {
                playerID = 0;
            }
            else { playerID = value; }
        }
    }

    //玩者資源
    public static string PlayerName;
    public static int Gold;
    public static int Dimand;
    public static int CurEnergy;
    public static int MaxEnergy;

    //monsterResource
    public static Dictionary<int, PlayerMonster> MonsterDic { get; set; }
    public static PlayerResource OwnResource = new PlayerResource();//玩者資源
    private static GameObject monsterObj;
    public static List<GameObject> MonsterObjList;
    private static int monsterNum;
    public static int MonsterNum
    {
        get { return Player.monsterNum; }
        set { Player.monsterNum = value; }
    }
    private static Dictionary<int, PlayerTeam> teamDic;
    public static Dictionary<int, PlayerTeam> TeamDic
    {
        get { return Player.teamDic; }
        set { Player.teamDic = value; }
    }
    //編輯中的隊伍ID
    public static int ChangeTeamID { get; set; }
    //要被替換怪物在隊伍中的位置
    public static int ChangeMemberRank { get; set; }
    private static List<int> noTeamMonsterIDList;
    public static List<int> NoTeamMonsterIDList
    {
        get { return Player.noTeamMonsterIDList; }
        set { Player.noTeamMonsterIDList = value; }
    }
    private static List<int> teamMonsterIDList;
    public static List<int> TeamMonsterIDList
    {
        get { return Player.teamMonsterIDList; }
        set { Player.teamMonsterIDList = value; }
    }
    private static int noTeamMonsterNum;
    public static int NoTeamMonsterNum
    {
        get { return Player.noTeamMonsterNum; }
        set { Player.noTeamMonsterNum = value; }
    }
    private static int teamNum;
    public static int TeamNum
    {
        get { return Player.teamNum; }
        set { Player.teamNum = value; }
    }


    //cur Ctrl monster
    public static int curMonsterID;
    //目前選擇觀看編輯的隊伍ID
    private static int curTeamID;
    public static int CurTeamID
    {
        get { return Player.curTeamID; }
        set { Player.curTeamID = value; }
    }

    /// <summary>
    /// 依造傳入的擁有怪獸清單，建立怪獸,_monsterItemDic[propertyName]取得value
    /// </summary>
    public static void StartMonsterSet(List<Dictionary<string, string>> _monsterItemList)
    {
        //讀取資料庫中玩家擁有的怪物ID，並建立怪物物件
        CreateMonsters(_monsterItemList);
    }

    private static void CreateMonsters(List<Dictionary<string, string>> _monsterItemList)
    {
        //設定怪物建立位置
        Vector3 monsterPos = new Vector3(0, 0, 0);
        //設定怪物建立角度
        Quaternion monsterRot = Quaternion.Euler(0f, 0f, 0f);
        //設定讀取怪物物件
        monsterObj = Resources.Load<GameObject>("GameObject/Room/Monster");
        //建立怪物字典
        MonsterDic = new Dictionary<int, PlayerMonster>();
        //建立怪物GameObject列表
        MonsterObjList = new List<GameObject>();
        //設定怪物計數器
        //以迴圈建立怪獸
        for (int i = 0; i < _monsterItemList.Count; i++)
        {
            //建立怪物GameObject                
            MonsterObjList.Add(Instantiate(monsterObj, monsterPos, monsterRot) as GameObject);
            //在怪物GameObject裡新增Component
            MonsterObjList[i].AddComponent<PlayerMonster>();
            //將此component命名為monsterClass
            PlayerMonster monsterClass = MonsterObjList[i].GetComponent<PlayerMonster>();
            //依照怪物ID設定怪物屬性
            monsterClass.SetAttribute(_monsterItemList[i]);
            //把怪物放進房間子物件裡
            MonsterObjList[i].transform.parent = RoomDic[monsterClass.InRoomID].transform;
            //設定怪獸物件名稱
            MonsterObjList[i].name = string.Format("Monster{0}", monsterClass.MonsterID);
            //設定怪物位置
            MonsterObjList[i].transform.localPosition = monsterPos;
        }
    }
    public static void AddMonsterToDictionary(PlayerMonster _monster)//將怪物加入擁有怪物字典
    {
        try
        {
            MonsterDic.Add(_monster.MonsterID, _monster);
        }
        catch (Exception ex)
        {
            Debug.LogWarning(ex);
        }
    }

    static void CheckAllMonsterEvo()
    {
        Dictionary<int, PlayerMonster>.KeyCollection keys = MonsterDic.Keys;
        foreach (int key in keys)
        {
            //檢查是否可進化
            MonsterDic[key].CheckEvolution();
        }
    }


    //讀取玩家擁有隊伍
    public static void StartTeamSet(List<Dictionary<string, string>> _teamItemList)
    {
        //讀取資料庫中玩家擁有的房間數量，並建立房間
        CreateTeams(_teamItemList);
    }
    private static void CreateTeams(List<Dictionary<string, string>> _monsterItemList)
    {
        //建立隊伍字典
        TeamDic = new Dictionary<int, PlayerTeam>();
        //以迴圈搜尋此玩家擁有的隊伍，迴圈長度取決抓取到的node
        for (int i = 0; i < _monsterItemList.Count; i++)
        {
            int TeamID = int.Parse(_monsterItemList[i]["TeamID"]);
            string TeamName = _monsterItemList[i]["TeamName"];
            int[] Member = new int[3];
            Member[0] = int.Parse(_monsterItemList[i]["Member1ID"]);
            Member[1] = int.Parse(_monsterItemList[i]["Member2ID"]);
            Member[2] = int.Parse(_monsterItemList[i]["Member3ID"]);
            TeamDic.Add(TeamID, new PlayerTeam(TeamID, TeamName, Member));
        }
        //讀出擁有多少隊伍
        TeamNum = TeamDic.Count;
    }
    public static void ChangeTeamMonster(int _value)//隊伍怪獸變動
    {
        if (_value == -1)
        {
            TeamDic[ChangeTeamID].Member[ChangeMemberRank] = 0;
        }
        else
        {
            TeamDic[ChangeTeamID].Member[ChangeMemberRank] = _value;
        }
        UpdateNoTeamMonster();
        Debug.Log("隊員已更換");
    }
    public static void ChangeMonsterSkill(int _monsterID, int _spellID)//怪獸的技能變動，[要更動技能的怪獸ID],[要更動第幾個技能],[更動後的技能ID]
    {
        MonsterDic[_monsterID].ChangeMonsterSkill(_spellID);
    }


    //讀取玩家沒有隊伍的怪獸
    private static void SettingNoTeamMonster()
    {
        NoTeamMonsterIDList = new List<int>();
        TeamMonsterIDList = new List<int>();
        UpdateNoTeamMonster();
    }
    public static void UpdateNoTeamMonster()
    {
        NoTeamMonsterIDList.Clear();
        TeamMonsterIDList.Clear();

        Dictionary<int, PlayerTeam>.KeyCollection TeamDickeys = TeamDic.Keys;
        foreach (int key in TeamDickeys)
        {
            for (int j = 0; j < 3; j++)
            {
                TeamMonsterIDList.Add(TeamDic[key].Member[j]);
            }
        }
        Dictionary<int, PlayerMonster>.KeyCollection keys = MonsterDic.Keys;
        foreach (int key in keys)
        {
            if (!TeamMonsterIDList.Contains(key))
            {
                NoTeamMonsterIDList.Add(key);
            }
        }
        NoTeamMonsterNum = NoTeamMonsterIDList.Count;
    }


    public static void ChangeScene()//切換場景
    {
        if (Application.loadedLevelName == "TrainScene")
        {
            CheckAllMonsterEvo();
            PlayerMonster.AfterFightTriggerEvent();//戰鬥後機率觸發事件
        }
    }


    //玩家起始設置
    public static void PlayerSourceSetting(int _ownPlayerID)
    {
        //更新玩者資源介面
        ResourceUI.UpdateResourceUI();
        PlayerID = _ownPlayerID;
    }


    // Use this for initialization
    public static void StartSet()
    {
        LoadingScene.ChangeSceneFunction += ChangeScene;
        //PlayerSourceSetting(1);
        //讀取玩家擁有房間資料及數量，在創造出房間
        //StartRoomSet();
        //讀取玩家擁有怪物資料及數量，在創造出怪物
        //StartMonsterSet();
        ShowRewardText.StartSetShowRewardText();//起始設定怪物獨立事件回饋
        //加入房間擁有的資源物件
        StartSetRoomResource();
        //隊伍設定
        //StartTeamSet();
        //讀取玩家已達成任務成就
        //StartQuestAchievementSet();

        SettingNoTeamMonster();
        CtrlPower.StartSetting();
        PlayerCtrl.StartSetting();
        RoomCtrl.StartSetting();
        RoomCtrl.OnlyActiveSeeRoom();//只顯示看到的房間
    }
}
