using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public partial class Player : MonoBehaviour
{
    //暫時用
    static Dictionary<int, PlayerMonster> TmpPlayerMonsterDic;//暫存怪物字典
    static Dictionary<int, PlayerOwnQuestAchievement> TmpQuestAchievementDic;//暫存玩家擁有任務成就字典
    static Dictionary<int, PlayerTeam> TmpTeamDic;//暫存玩家隊伍
    static PlayerResource TmpOwnResource;//暫存玩家資源




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
    public static List<GameObject> monster;
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

    public static Dictionary<int, PlayerOwnQuestAchievement> QuestAchievementDic { get; set; }//玩家擁有任務成就字典，給予任務編號找到擁有成就清單，[0]=完成成就，[1]=探索成就....


    //讀取玩家達成的任務成就
    private static void StartQuestAchievementSet()
    {
        //讀取資料庫中玩家擁有的怪物ID，並建立怪物物件
        TextAsset XMLFlie_Player = Resources.Load<TextAsset>("StringData/QuestAchievementData");
        LoadOwnQuestAchievement(XMLFlie_Player, PlayerID);
    }
    //建立玩家擁有的成就字典
    private static void LoadOwnQuestAchievement(TextAsset XMLFile, int ownPlayerID)
    {
        try
        {
            //暫時用
            TmpQuestAchievementDic = new Dictionary<int, PlayerOwnQuestAchievement>();
            if (TrainScene.StartSet)//暫時用
            {
                TmpQuestAchievementDic = QuestAchievementDic;
            }
            //建立擁有任務成就字典
            QuestAchievementDic = new Dictionary<int, PlayerOwnQuestAchievement>();
            if (TrainScene.StartSet)//暫時用
            {
                QuestAchievementDic = TmpQuestAchievementDic;
            }
            else
            {
                //讀取XML文件
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLFile.text);
                XmlNodeList ownPlayerNode = doc.GetElementsByTagName("AchievePlayerUID");
                //以迴圈搜尋此玩家擁有的成就種類ID，迴圈長度取決抓取到的node
                for (int i = 0; i < ownPlayerNode.Count; i++)
                {
                    //判斷屬於此玩家的達成成就，若為此玩家擁有，則加入此玩家的達成成就字典中
                    if (ownPlayerNode[i].InnerText == ownPlayerID.ToString())
                    {
                        try
                        {
                            int questID = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("QuestID").InnerText);
                            int achievementTypeID = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("AchievementType").InnerText);
                            if (QuestAchievementDic.ContainsKey(questID))
                            {
                                QuestAchievementDic[questID].Achieve[achievementTypeID - 1] = true;
                            }
                            else
                            {
                                QuestAchievementDic.Add(questID, new PlayerOwnQuestAchievement(achievementTypeID, new bool[4]));
                            }
                        }
                        catch
                        {
                            Debug.Log("讀入玩家擁有任務成就時，發生錯誤");
                        }
                    }
                }
            }
        }
        catch
        {
            Debug.Log("讀入玩家擁有任務成就時，發生錯誤");
        }
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


    //讀取玩家擁有的怪物
    private static void StartMonsterSet()
    {
        //讀取資料庫中玩家擁有的怪物ID，並建立怪物物件
        TextAsset XMLFlie_Player = Resources.Load<TextAsset>("StringData/PlayerMonsterData");
        LoadOwnMonster(XMLFlie_Player, PlayerID);
    }
    //建立擁有的怪物
    private static void LoadOwnMonster(TextAsset XMLFile, int ownPlayerID)
    {
        //設定怪物建立位置
        Vector3 monsterPos = new Vector3(0, 0, 0);
        //設定怪物建立角度
        Quaternion monsterRot = Quaternion.Euler(0f, 0f, 0f);
        //設定讀取怪物物件
        monsterObj = Resources.Load<GameObject>("GameObject/Room/Monster");
        XmlDocument doc = new XmlDocument();
        XmlNodeList ownPlayerNode;
        TmpPlayerMonsterDic = new Dictionary<int, PlayerMonster>();//暫時用
        if (TrainScene.StartSet)//暫時用
        {
            TmpPlayerMonsterDic = MonsterDic;
        }
        //建立怪物字典
        MonsterDic = new Dictionary<int, PlayerMonster>();
        //建立怪物列表
        //MonsterList = new List<PlayerMonster>();
        //建立怪物GameObject列表
        monster = new List<GameObject>();
        //讀取XML文件            
        doc.LoadXml(XMLFile.text);
        ownPlayerNode = doc.GetElementsByTagName("OwnPlayerID");
        //設定怪物計數器
        int MCount = 0;
        //以迴圈搜尋此玩家擁有的怪物，迴圈長度取決抓取到的node
        for (int i = 0; i < ownPlayerNode.Count; i++)
        {
            //判斷屬於此玩家的怪物，若為此玩家擁有，則建立怪物
            if (ownPlayerNode[i].InnerText == ownPlayerID.ToString())
            {
                //建立怪物GameObject                
                monster.Add(Instantiate(monsterObj, monsterPos, monsterRot) as GameObject);
                //設定怪物編號，此編號不是資料庫中的怪物UID，只是這場景中的暫定編號
                monster[MCount].transform.GetComponent<RoomCharaStatus>().monsterID = MCount;
                //把怪物放進房間子物件裡
                monster[MCount].transform.parent = RoomObjList[MCount].transform;
                //設定怪物位置
                monster[MCount].transform.localPosition = monsterPos;
                //在怪物GameObject裡新增Component
                monster[MCount].AddComponent<PlayerMonster>();
                //將此component命名為monsterClass
                PlayerMonster monsterClass = (PlayerMonster)monster[MCount].GetComponent<PlayerMonster>();
                //讀取此怪物的ID                
                int monsterID = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("MonsterID").InnerText);
                //依照怪物ID設定怪物屬性
                monsterClass.StartSetMonsterAttribute(XMLFile, monsterID);
                //設定怪物計數器+1
                MCount++;
            }
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
            Debug.Log(ex);
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
    private static void StartTeamSet()
    {
        //讀取資料庫中玩家擁有的房間數量，並建立房間
        TextAsset XMLFlie_Team = Resources.Load<TextAsset>("StringData/PlayerTeamData");
        LoadOwnTeam(XMLFlie_Team, PlayerID);
    }
    //建立玩家擁有的隊伍
    private static void LoadOwnTeam(TextAsset XMLFile, int ownPlayerID)
    {
        if (TrainScene.StartSet)//暫時用
        {
            TmpTeamDic = TeamDic;
        }
        //建立隊伍字典
        TeamDic = new Dictionary<int, PlayerTeam>();
        TmpPlayerMonsterDic = new Dictionary<int, PlayerMonster>();//暫時用
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFile.text);
        XmlNodeList ownPlayerNode = doc.GetElementsByTagName("OwnPlayerID");
        //以迴圈搜尋此玩家擁有的隊伍，迴圈長度取決抓取到的node
        for (int i = 0; i < ownPlayerNode.Count; i++)
        {
            //判斷屬於此玩家的隊伍，若為此玩家擁有，則加入此玩家的隊伍清單
            if (ownPlayerNode[i].InnerText == ownPlayerID.ToString())
            {
                try
                {
                    int TeamID = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("TeamID").InnerText);
                    string TeamName = ownPlayerNode[i].ParentNode.SelectSingleNode("TeamName").InnerText;
                    int[] Member = new int[3];
                    Member[0] = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("Member1ID").InnerText);
                    Member[1] = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("Member2ID").InnerText);
                    Member[2] = int.Parse(ownPlayerNode[i].ParentNode.SelectSingleNode("Member3ID").InnerText);
                    //TeamList.Add(new PlayerTeam(TeamID, ownPlayerID, Member));
                    TeamDic.Add(TeamID, new PlayerTeam(TeamID, ownPlayerID, TeamName, Member));
                }
                catch
                {
                    Debug.Log("讀入玩家擁有隊伍時，發生錯誤");
                }
            }
        }
        //讀出擁有多少隊伍
        TeamNum = TeamDic.Count;

        if (TrainScene.StartSet)//暫時用
        {
            TeamDic = TmpTeamDic;
        }
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
        /*

        try
        {
            if (TrainScene.StartSet)//暫時用
            {
                TmpOwnResource = OwnResource;
            }
            //建立玩者資源
            OwnResource = new PlayerResource();
            //讀取XML文件
            TextAsset XMLFile = Resources.Load<TextAsset>("StringData/PlayerResourceData");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(XMLFile.text);
            XmlNodeList ownPlayerNode = doc.GetElementsByTagName("PlayerID");
            XmlNodeList GoldNode = doc.GetElementsByTagName("Gold");
            XmlNodeList DimandNode = doc.GetElementsByTagName("Dimand");
            XmlNodeList CurEnergyNode = doc.GetElementsByTagName("CurEnergy");
            XmlNodeList MaxEnergyNode = doc.GetElementsByTagName("MaxEnergy");
            XmlNodeList FireSilverNode = doc.GetElementsByTagName("FireSilver");
            XmlNodeList FireGoldNode = doc.GetElementsByTagName("FireGold");
            XmlNodeList FireDimandNode = doc.GetElementsByTagName("FireDimand");
            XmlNodeList FireCrystalNode = doc.GetElementsByTagName("FireCrystal");
            XmlNodeList WoodSilverNode = doc.GetElementsByTagName("WoodSilver");
            XmlNodeList WoodGoldNode = doc.GetElementsByTagName("WoodGold");
            XmlNodeList WoodDimandNode = doc.GetElementsByTagName("WoodDimand");
            XmlNodeList WoodCrystalNode = doc.GetElementsByTagName("WoodCrystal");
            XmlNodeList WaterSilverNode = doc.GetElementsByTagName("WaterSilver");
            XmlNodeList WaterGoldNode = doc.GetElementsByTagName("WaterGold");
            XmlNodeList WaterDimandNode = doc.GetElementsByTagName("WaterDimand");
            XmlNodeList WaterCrystalNode = doc.GetElementsByTagName("WaterCrystal");
            
            //以迴圈搜尋此玩家擁有的隊伍，迴圈長度取決抓取到的node
            for (int i = 0; i < ownPlayerNode.Count; i++)
            {
                //判斷屬於此玩家的隊伍，若為此玩家擁有，則加入此玩家的隊伍清單
                if (ownPlayerNode[i].InnerText == _ownPlayerID.ToString())
                {

                }
            }
            if (TrainScene.StartSet)//暫時用
            {
                OwnResource = TmpOwnResource;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            Debug.Log("讀取玩家資源資料時發生錯誤");
        }
        */
    }

    // Use this for initialization
    public static void StartSet()
    {
        if (!TrainScene.StartSet)//如果已經初始設置過就不再執行
        {
            TrainScene.ChangeSceneFunction += ChangeScene;
            //PlayerSourceSetting(1);
            //讀取玩家擁有房間資料及數量，在創造出房間
            StartRoomSet();
            //讀取玩家擁有怪物資料及數量，在創造出怪物
            StartMonsterSet();
            ShowRewardText.StartSetShowRewardText();//起始設定怪物獨立事件回饋
            //加入房間擁有的資源物件
            StartSetRoomResource();
            //隊伍設定
            StartTeamSet();
            //讀取玩家已達成任務成就
            StartQuestAchievementSet();

            SettingNoTeamMonster();
            CtrlPower.StartSetting();
            PlayerCtrl.StartSetting();
            RoomCtrl.StartSetting();
            RoomCtrl.OnlyActiveSeeRoom();//只顯示看到的房間
        }
    }

}
