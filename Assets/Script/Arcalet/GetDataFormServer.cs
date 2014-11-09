using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{
    //取得資料
    static bool IsGetPlayerSetting;//是否已從Server取得玩家基本設定
    static int GetPlayerSettingCurProgress;//取得玩家設定目前進度
    static int GetPlayerSettingMaxProgress;//取得玩家設定最終進度


    static bool IsGetPlayerResourceData;//是否已從Server取得玩家資源
    static int GetPlayerResourceCurProgress;//取得玩家資源目前進度
    static int GetPlayerResourceMaxProgress;//取得玩家資源最終進度

    static bool IsGetEnhanceResourceData;//是否已從Server取得怪獸強化素材
    static int GetEnhanceResourceCurProgress;//取得怪獸強化素材目前進度
    static int GetEnhanceResourceMaxProgress;//取得怪獸強化素材最終進度

    static bool IsGetRoomData;//是否已從Server取得玩家房間
    static int GetPlayerRoomCurProgress;//取得玩家房間目前進度
    public static int GetPlayerRoomMaxProgress;//取得玩家房間最終進度

    static bool IsGetMonsterData;//是否已從Server取得玩家怪獸
    static int GetMonsterCurProgress;//取得玩家怪獸目前進度
    public static int GetMonsterMaxProgress;//取得玩家怪獸最終進度

    static bool IsGetTeamData;//是否已從Server取得玩家房間
    static int GetTeamCurProgress;//取得玩家房間目前進度
    public static int GetTeamMaxProgress;//取得玩家房間最終進度

    static bool IsGetVentureData;//是否已從Server取得玩家冒險
    static int GetVentureCurProgress;//取得玩家冒險目前進度
    public static int GetVentureMaxProgress;//取得玩家冒現最終進度

    static bool IsGetQuestAchievementData;//是否已從Server取得玩家任務成就
    static int GetQuestAchievementCurProgress;//取得玩家任務成就目前進度
    public static int GetQuestAchievementMaxProgress;//取得玩家任務成就最終進度

    //初始化
    static bool IsIniPlayerSetting;//是否已初始化玩者基本設定
    static int IniPlayerSettingCurProgress;//玩者設定初始化目前進度 
    static int IniPlayerSettingMaxProgress;//玩者設定初始化最終進度 

    static bool IsIniPlayerResourceData;//是否已初始化玩者資源
    static int IniPlayerResourceCurProgress;//玩者資源初始化目前進度 
    static int IniPlayerResourceMaxProgress;//玩者資源初始化最終進度 

    static bool IsIniEnhanceResourceData;//是否已初始化怪獸資源
    static int IniEnhanceResourceCurProgress;//怪獸資源初始化目前進度 
    static int IniEnhanceResourceMaxProgress;//怪獸資源初始化最終進度 

    static bool IsIniRoomData;//是否已初始化玩者房間
    static int IniRoomCurProgress;//玩者房間初始化目前進度 
    public static int IniRoomMaxProgress;//玩者房間初始化最終進度 

    static bool IsIniMonsterData;//是否已初始化玩者怪獸資料
    static int IniMonsterCurProgress;//怪獸初始化目前進度
    public static int IniMonsterMaxProgress;//怪獸初始化最終進度

    static bool IsIniTeamData;//是否已從Server取得玩家房間
    static int IniTeamCurProgress;//取得玩家房間目前進度
    public static int IniTeamMaxProgress;//取得玩家房間最終進度

    static bool IsIniVentureData;//是否已從Server取得玩家冒險
    static int IniVentureCurProgress;//取得玩家冒險目前進度
    public static int IniVentureMaxProgress;//取得玩家冒險最終進度

    static bool IsIniQuestAchievementData;//是否已從Server取得玩家任務成就
    static int IniQuestAchievementCurProgress;//取得玩家任務成就目前進度
    public static int IniQuestAchievementMaxProgress;//取得玩家任務成就最終進度

    //場景轉換
    static SceneController scaneController;
    public static void StartSetGetDataFromServer()//初始設定GetDataFormServer
    {
        IsGetPlayerSetting = false;
        //取得資料
        GetPlayerSettingCurProgress = 0;
        GetPlayerSettingMaxProgress = 1;

        IsGetPlayerResourceData = false;
        GetPlayerResourceCurProgress = 0;
        GetPlayerResourceMaxProgress = 1;

        IsGetEnhanceResourceData = false;
        GetEnhanceResourceCurProgress = 0;
        GetEnhanceResourceMaxProgress = 1;

        IsGetRoomData = false;
        GetPlayerRoomCurProgress = 0;

        IsGetMonsterData = false;
        GetMonsterCurProgress = 0;

        IsGetTeamData = false;
        GetTeamCurProgress = 0;

        IsGetVentureData = false;
        GetVentureCurProgress = 0;

        IsGetQuestAchievementData = false;
        GetQuestAchievementCurProgress = 0;

        //初始化
        IsIniPlayerSetting = false;
        IniPlayerSettingCurProgress = 0;
        IniPlayerSettingMaxProgress = 1;

        IsIniPlayerResourceData = false;
        IniPlayerResourceCurProgress = 0;
        IniPlayerResourceMaxProgress = 1;

        IsIniEnhanceResourceData = false;
        IniEnhanceResourceCurProgress = 0;
        IniEnhanceResourceMaxProgress = 1;

        IsIniRoomData = false;
        IniRoomCurProgress = 0;

        IsIniMonsterData = false;
        IniMonsterCurProgress = 0;

        IsIniTeamData = false;
        IniTeamCurProgress = 0;

        IsIniVentureData = false;
        IniVentureCurProgress = 0;

        IsIniQuestAchievementData = false;
        IniQuestAchievementCurProgress = 0;
        //場景轉換物件
        scaneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }
    /// <summary>
    /// 起始向Server取得所有資料
    /// </summary>
    void GetGameDataFormArcaletServer()//起始向Server取得所有資料
    {
        //向Server取得並設定玩家基本設定
        IODataFromArcalet.CheckPlayerSettingInstance();//向Server取得玩者基本設定
    }
    /// <summary>
    /// 讀取資料進度，傳入讀取的資料種類名稱
    /// </summary>
    public static void GetDataProgress(string _dataType)//讀取資料進度
    {
        switch (_dataType)
        {
            case "PlayerSetting"://玩家基本設定
                GetPlayerSettingCurProgress++;
                if (GetPlayerSettingCurProgress >= GetPlayerSettingMaxProgress)
                {
                    IsGetPlayerSetting = true;
                    ArcaletSetter.ConnectStatusText.text = "取得玩家資料...";
                    Debug.Log("取得玩家設定成功!");
                    //向Server取得基本資源
                    IODataFromArcalet.CheckResourceInstance();//向Server檢查實例化玩者資源物件
                }
                break;
            case "Resource"://玩家資源
                GetPlayerResourceCurProgress++;
                if (GetPlayerResourceCurProgress >= GetPlayerResourceMaxProgress)
                {
                    IsGetPlayerResourceData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得玩家資料...";
                    Debug.Log("取得玩家資源成功!");
                    //向Server取得怪獸強化素材
                    IODataFromArcalet.CheckEnhanceResourceInstance();//向Server檢查實例化怪獸強化物件
                }
                break;
            case "EnhanceResource"://怪物強化素材
                GetEnhanceResourceCurProgress++;
                if (GetEnhanceResourceCurProgress >= GetEnhanceResourceMaxProgress)
                {
                    IsGetEnhanceResourceData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得房間資料...";
                    Debug.Log("取得怪獸強化素材成功!");
                    //向Server取得房間資料
                    IODataFromArcalet.CheckRoomInstance();//向Server檢查實例化玩者房間物件
                }
                break;
            case "PlayerRoom"://玩家房間
                GetPlayerRoomCurProgress++;
                if (GetPlayerRoomCurProgress >= GetPlayerRoomMaxProgress)
                {
                    IsGetRoomData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得怪獸資料...";
                    Debug.Log("取得房間成功!");
                    //向server取得怪獸資料
                    IODataFromArcalet.CheckMonsterInstance();//向Server取得玩者怪獸
                }
                break;
            case "PlayerMonster"://玩家怪獸
                GetMonsterCurProgress++;
                if (GetMonsterCurProgress >= GetMonsterMaxProgress)
                {
                    IsGetMonsterData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得隊伍資料...";
                    Debug.Log("取得怪獸成功!");
                    //向server取得隊伍資料
                    IODataFromArcalet.CheckTeamInstance();//向Server取得隊伍
                }
                break;
            case "PlayerTeam"://玩家隊伍
                GetTeamCurProgress++;
                if (GetTeamCurProgress >= GetTeamMaxProgress)
                {
                    IsGetTeamData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得冒險資料...";
                    Debug.Log("取得隊伍成功!");
                    //向server取得冒險資料
                    IODataFromArcalet.CheckVentureInstance();//向Server取得冒險
                }
                break;
            case "PlayerVenture"://玩家冒險
                GetVentureCurProgress++;
                if (GetVentureCurProgress >= GetVentureMaxProgress)
                {
                    Debug.Log("取得冒險成功!");
                    IsGetVentureData = true;
                    //向server取得任務成就資料
                    IODataFromArcalet.CheckQuestAchievementInstance();//向Server取得任務成就
                }
                break;
            case "PlayerQuestAchievement"://玩家任務成就
                GetQuestAchievementCurProgress++;
                if (GetQuestAchievementCurProgress >= GetQuestAchievementMaxProgress)
                {
                    Debug.Log("取得任務成就成功!");
                    IsGetQuestAchievementData = true;
                    CheckGetProgress();//檢查進度，若已經讀取完畢進入遊戲
                }
                break;
            default:
                Debug.Log("設定資料讀取進度的類型無法辨識");
                break;
        }
    }
    /// <summary>
    /// 初始化資料進度，傳入初始化的資料種類名稱
    /// </summary>
    public static void InitializeDataProgress(string _dataType)//初始化資料進度
    {
        switch (_dataType)
        {
            case "PlayerSetting"://玩家基本設定
                IniPlayerSettingCurProgress++;
                if (IniPlayerSettingCurProgress >= IniPlayerSettingMaxProgress)
                {
                    IsIniPlayerSetting = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化玩家設定...";
                    Debug.Log("初始化玩家設定成功!");
                    //向Server新增玩家資源物品
                    IODataFromArcalet.NewResourceInstance();//向Server初始化玩家資源
                }
                break;
            case "Resource"://玩家資源
                IniPlayerResourceCurProgress++;
                if (IniPlayerResourceCurProgress >= IniPlayerResourceMaxProgress)
                {
                    IsIniPlayerResourceData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化玩家設定...";
                    Debug.Log("初始化玩家資源成功!");
                    //向Server新增怪獸強化素材物品
                    IODataFromArcalet.NewEnhanceResourceInstance();//向Server初始化怪獸強化素材
                }
                break;
            case "EnhanceResource"://怪物強化素材
                IniEnhanceResourceCurProgress++;
                if (IniEnhanceResourceCurProgress >= IniEnhanceResourceMaxProgress)
                {
                    IsIniEnhanceResourceData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化房間資料...";
                    Debug.Log("初始化怪獸強化素材成功!");
                    //向Server新增房間資料
                    IODataFromArcalet.IniRoomInstance();//向Server初始化房間資料
                }
                break;
            case "PlayerRoom"://玩家房間
                IniRoomCurProgress++;
                if (IniRoomCurProgress >= IniRoomMaxProgress)//進度達100%時
                {
                    IsIniRoomData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化怪獸資料...";
                    Debug.Log("初始化房間資料成功!");
                    //向server新增怪獸資料
                    IODataFromArcalet.IniMonsterInstance();//向Server初始化玩者怪獸
                }
                break;
            case "PlayerMonster"://玩家怪獸
                IniMonsterCurProgress++;
                if (IniMonsterCurProgress >= IniMonsterMaxProgress)
                {
                    IsIniMonsterData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化隊伍...";
                    Debug.Log("初始化怪獸資料成功!");
                    //向server新增隊伍資料
                    IODataFromArcalet.IniTeamInstance();//向Server初始化玩者隊伍
                }
                break;
            case "PlayerTeam"://玩家隊伍
                IniTeamCurProgress++;
                if (IniTeamCurProgress >= IniTeamMaxProgress)
                {
                    IsIniTeamData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化玩家冒險進度...";
                    Debug.Log("初始化隊伍資料成功!");
                    //向server新增冒險資料
                    IODataFromArcalet.IniVentureInstance();//向Server初始化冒險進度
                }
                break;
            case "PlayerVenture"://玩家冒險
                IniVentureCurProgress++;
                if (IniVentureCurProgress >= IniVentureMaxProgress)
                {
                    IsIniVentureData = true;
                    ArcaletSetter.ConnectStatusText.text = "初始化玩家任務成就...";
                    Debug.Log("初始化冒險資料成功!");
                    //向server新增任務成就資料
                    IODataFromArcalet.IniQuestAchievementInstance();//向Server初始化任務成就
                }
                break;
            case "PlayerQuestAchievement"://玩家冒險
                IniQuestAchievementCurProgress++;
                if (IniQuestAchievementCurProgress >= IniQuestAchievementMaxProgress)
                {
                    IsIniQuestAchievementData = true;
                    ArcaletSetter.ConnectStatusText.text = "取得玩家資料...";
                    Debug.Log("初始化任務成就資料成功!");
                    IODataFromArcalet.SetPlayerSetting("StartSet", "1");//向Server寫入玩者設定
                }
                break;
            case "Initialize"://初始化
                CheckIniProgress();//檢查進度，若已經讀取完畢進入遊戲
                break;
            default:
                Debug.Log("設定資料讀取進度的類型無法辨識");
                break;
        }
    }
    static void CheckGetProgress()//檢查進度，若已經讀取完畢進入遊戲
    {
        if (IsGetPlayerSetting && IsGetPlayerResourceData && IsGetEnhanceResourceData && IsGetRoomData && IsGetMonsterData && IsGetTeamData && IsGetVentureData && IsGetQuestAchievementData)//已從Server取得資料
        {
            Debug.Log("讀取培養場景");
            scaneController.GoTrainScene();//進入培養場景
        }
    }
    static void CheckIniProgress()//檢查進度，若已經初始化完畢進入遊戲
    {
        if (IsIniPlayerSetting && IsIniEnhanceResourceData && IsIniPlayerResourceData && IsIniRoomData && IsIniMonsterData && IsIniTeamData && IsIniVentureData && IsIniQuestAchievementData)//已從Server取得資料
        {
            Debug.Log("讀取培養場景");
            IODataFromArcalet.CheckPlayerSettingInstance();//向Server取得玩者基本設定
        }
    }
}