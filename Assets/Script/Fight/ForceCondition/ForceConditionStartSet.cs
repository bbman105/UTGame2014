using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class ForceCondition : MonoBehaviour
{
    static private ForceCondition staticCoroutine;
    public static UpdateForceStatusDelegate UpdateForceStatus;//勢力變化委託
    public static DeathDelegate UpdateNPCDeath;//NPC怪獸死亡委託
    public static DeathDelegate UpdatePlayerDeath;//玩家怪獸死亡委託
    public static DeathDelegate UpdateSombodyDeath;//怪獸死亡委託
    public static Dictionary<string, ForceStatus> ForceStatusDic { get; set; }//勢力狀態字典
    public static int PlayerTeamID { get; set; }//玩家隊伍
    public static int QuestID { get; set; }//此關卡任務ID
    public static int QuestLevel { get; set; }//此關卡任務是第幾個任務
    public static int MapID { get; set; }//此關卡地圖ID
    public static int PlanetID { get; set; }//此關卡的星球ID
    public static Dictionary<string, List<GameObject>> CharaDic { get; set; }//雙方腳色物件字典
    public static Dictionary<string, Status[]> CharaStatusDic { get; set; }//雙方腳色狀態字典
    public static float SpentTime;//戰鬥花費時間
    public static int Result;//戰鬥結果
    public static int BaseGold;//關卡基本取得金錢
    public static int BaseExp;//關卡基本取得經驗
    public static int HitGold { get; set; }//擊殺取得額外金錢獎勵
    public static Dictionary<int, int> KillLootDic;//擊殺取得的戰利品[物品ID][數量]
    public static int KillExp { get; set; }//擊殺取得額外經驗獎勵
    private static int gainExp;//關卡+擊殺經驗獎勵
    public static int GainExp//關卡+擊殺經驗獎勵
    {
        get { return ForceCondition.gainExp; }
        set
        {
            if (value > BaseExp + KillExp)
            {
                ForceCondition.gainExp = BaseExp + KillExp;
            }
            else
            {
                ForceCondition.gainExp = value;
            }
        }
    }
    private static int tmpGold;
    private static int TmpGold//暫時顯示用金錢
    {
        get { return ForceCondition.tmpGold; }
        set
        {
            if (value > FinalGold)
            {
                ForceCondition.tmpGold = FinalGold;
            }
            else
            {
                ForceCondition.tmpGold = value;
            }
        }
    }
    private static int tmpExp;
    private static int TmpExp//暫時顯示用經驗
    {
        get { return ForceCondition.tmpExp; }
        set
        {
            if (value > FinalExp)
            {
                ForceCondition.tmpExp = FinalExp;
            }
            else
            {
                ForceCondition.tmpExp = value;
            }
        }
    }
    private static float[] TmpMemberExp { get; set; }//初始化暫時經驗條顯示
    public static int[] TmpMemberNeedExp { get; set; }//初始化暫時需求經驗條顯示
    private static bool[] CanLvUp { get; set; }//可否升級
    private static int[] TmpMemberLv { get; set; }//暫時顯示等級
    private static int[] TmpMemberBrutal { get; set; }//暫時顯示殘暴
    private static int[] TmpMemberAggressive { get; set; }//暫時顯示侵略
    private static int[] TmpMemberResistance { get; set; }//暫時顯示抵抗
    private static int[] TmpMemberHealth { get; set; }//暫時顯示生命
    private static int[] AddMemberBrutal { get; set; }//增加的殘暴
    private static int[] AddMemberAggressive { get; set; }//增加的侵略
    private static int[] AddMemberResistance { get; set; }//增加的抵抗
    private static int[] AddMemberHealth { get; set; }//增加的生命

    private static int gainGold;
    public static int GainGold//關卡+擊殺金錢獎勵
    {
        get { return ForceCondition.gainGold; }
        set
        {
            if (value > BaseGold + HitGold)
            {
                ForceCondition.gainGold = BaseGold + HitGold;
            }
            else
            {
                ForceCondition.gainGold = value;
            }
        }
    }
    private static int RankGold { get; set; }//評價取得的金錢獎勵
    private static int RankExp { get; set; }//評價取得的經驗獎勵
    private static int FinalGold { get; set; }//最終取得金錢獎勵
    private static int FinalExp { get; set; }//最終取得經驗獎勵
    private static int PerFinalExp { get; set; }//分攤後每隻怪物實際到的經驗值
    public static int RankScore { get; set; }//玩者評價分數
    public static int Rank { get; set; }//評價1~5:s~d
    private static int rankTmpShow;//Rank亂數顯示動畫暫時評價
    public static int RankTmpShow
    {
        get { return ForceCondition.rankTmpShow; }
        set
        {
            if (value > 5)
            {
                ForceCondition.rankTmpShow = 1;
            }
            else if (value < 0)
            {
                ForceCondition.rankTmpShow = 5;
            }
            else
            {
                ForceCondition.rankTmpShow = value;
            }
        }
    }//Rank亂數顯示動畫暫時評價
    public static float RankExtraPercent;//評價獎勵額外獎勵倍數
    public static bool CallEnd;//是否呼叫結束戰鬥，呼叫後同時停止時間計算
    public static bool RewardShow;//是否展示獎勵結算介面
    public static bool ShowExperience { get; set; }//是否展示經驗值介面
    static float CalculateTime { get; set; }//播放計算數值動畫的時間
    static float RewardPerTime { get; set; }//每次播放計算獎勵間隔時間
    static float ExpPerTime { get; set; }//每次播放經驗條的間隔時間
    static bool CalculateTimeUp { get; set; }//當CalculateTimeUp等於true時，結束亂數顯示Rank
    static float ExperienceTime { get; set; }//播放計算經驗動畫的時間
    static bool ExperienceTimeUp { get; set; }//當ExperienceTimeUp等於true時，結束播放經驗動畫
    ///////////////////經驗介面//////////////////
    //怪物Status
    public static PlayerMonster CurMember;
    //隊員 gameobj LIst
    static GameObject[] memberCard;
    //腳色icon圖的atlas
    static private UIAtlas[] charaAtlas;
    static private UIAtlas[] iconCharaAtlas;
    //腳色icon圖的sprite
    static private UISprite[] charaSprite;
    static private UISprite[] iconCharaSprite;
    //腳色icon邊框圖的sprite
    static private UISprite[] bannerSprite;
    //腳色icon底圖的sprite
    static private UISprite[] bottomSprite;
    //腳色icon的死亡maskSprite
    static private UISprite[] deathMaskSprite;
    //腳色icon元素icon的sprite
    static private UISprite[] elementSprite;
    //腳色icon等級label
    static private UILabel[] lvLabel;
    //腳色icon死亡label
    static private UILabel[] deathLabel;
    //腳色各屬性數值UILabel 與 經驗UISprite
    static private ReRollLvPropertyBtn[] reRollProperty;
    static private UILabel[] nameLabel;
    static private UILabel[] lifeLabel;
    static private UILabel[] brutalLabel;
    static private UILabel[] aggressiveLabel;
    static private UILabel[] resistanceLabel;
    static private UILabel[] addLifeLabel;
    static private UILabel[] addBrutalLabel;
    static private UILabel[] addAggressiveLabel;
    static private UILabel[] addResistanceLabel;
    static private UISprite[] lifeArrow;
    static private UISprite[] brutalArrow;
    static private UISprite[] resistanceArrow;
    static private UISprite[] aggressiveArrow;
    static private UISprite[] expSprite;
    static private Animator[] levelUpSprite;
    //指揮介面
    static private Transform FightMemberController;
    static public MemberArrow[] MemberArrowBtn;
    //static private MemberSelector[] StayFightMemberBtn;
    //static private MemberSelector[] FightMemberBtn;
    //KO用元件
    static GameObject KoObj;//最後一擊顯示的的KO物件
    static Animator KoAni;

    void Start()//初始設定
    {
        if (LoadingScene.ChangeSceneFunction != null)
            LoadingScene.ChangeSceneFunction();
        if (MapID != 0)//有選擇地圖
        {
            staticCoroutine = this;
            StartDictionarySet();//字典初始化設定
            StartPlayerMonsterSetting();//玩家怪物生出
            StartNPCMonsterSetting();//NPC怪物生出
            StartSetScene();//設置地圖場景
            StartSetBattleEndUI();//抓取BattleEnd介面物件
            //BattleLogPlayer.BattleLogStartSet();//初始化設定BattleLog
        }
        StartForceStatusSet();//初始化雙方勢力
        MemberArrow.MemberOnFightSet();//初始設定預設怪物出戰
        StartSetFightMemberController();//初始化指揮介面物件，及按鈕對應場上怪獸
        StartSetSpawner();//初始化Spawner
        SetNpcFightMember();//設定NPC出戰怪物
        StartSetKo();//起始設置KO物件
        CountDown.StartCount();//戰鬥開始倒數計時
    }
    private static void SetNpcFightMember()//設定NPC出戰怪物
    {
        StageControler.SetRemainMember();
    }
    private static void StartSetSpawner()//初始化Spawner
    {
        RetreatNumberSpawner.StartSet();
        FightNumberSpawner.StartSet();
    }
    private static void StartSetKo()//起始設置KO物件
    {
        //KoObj = GameObject.FindGameObjectWithTag("KO");
        //KoAni = KoObj.GetComponent<TimeServer>();
    }
    private static void StartSetFightMemberController()//設定出指揮介面物件
    {
        //初始化準備出戰怪物按鈕
        MemberArrowBtn = new MemberArrow[3];
        int tmpCount = 0;
        //抓取腳色準備出戰按鈕物件，並設置初始值
        FightMemberController = GameObject.FindGameObjectWithTag("FightMemberController").transform;
        for (int i = 0; i < 3; i++)
        {
            if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
            {
                MemberArrowBtn[i] = FightMemberController.FindChild(string.Format("Arrow{0}", i)).GetComponent<MemberArrow>();
                MemberArrowBtn[i].SetMember(tmpCount);//設置按鈕對應到隊伍第幾隻怪物
                tmpCount++;
            }
            else
            {
                MemberArrowBtn[i] = FightMemberController.FindChild(string.Format("Arrow{0}", i)).GetComponent<MemberArrow>();
                MemberArrowBtn[i].gameObject.SetActive(false);
            }
        }
    }

    private static void StartDictionarySet()//字典初始化設定
    {
        CharaDic = new Dictionary<string, List<GameObject>>();
        ForceStatusDic = new Dictionary<string, ForceStatus>();
        CharaStatusDic = new Dictionary<string, Status[]>();//腳色字典
        KillLootDic = new Dictionary<int, int>();//取得的獎勵物品字典
    }

    private void StartNPCMonsterSetting()//NPC怪物生出//
    {
        if (QuestID != 0)
        {
            gameObject.AddComponent<NPCMonsterSpawner>();
            NPCMonsterSpawner npcSpawner = gameObject.GetComponent<NPCMonsterSpawner>();
            npcSpawner.NPCMonsterSpawnerSet(QuestID);
            npcSpawner.Spawn();
        }
    }


    public void StartPlayerMonsterSetting()//玩家怪物生出//
    {
        if (PlayerTeamID != 0)
        {
            gameObject.AddComponent<PlayerMonsterSpawner>();
            PlayerMonsterSpawner playerSpawner = gameObject.GetComponent<PlayerMonsterSpawner>();
            playerSpawner.PlayerMonsterSpawnerSet(PlayerTeamID, true);//true=leftSide
            playerSpawner.Spawn();
        }
        else
        {
            Debug.Log("無選擇隊伍");
        }
    }
    public void StartSetScene()//設置戰鬥場景
    {
        if (MapID != 0)
        {
            SceneSet newScene = new SceneSet(MapID);
            newScene.SetScene(MapID);
        }
        else
        {
            MapID = 1;
            Debug.Log("MapID為空值");
        }

    }

    public static void StartForceStatusSet()//初始化設定
    {
        Status[] status;//宣告腳色狀態
        Dictionary<string, List<GameObject>>.KeyCollection CharaDicKeyCol = CharaDic.Keys;//宣告勢力字典索引
        //從勢力字典中抓取索引來設定各勢力的狀態
        foreach (string key in CharaDicKeyCol)
        {
            ForceStatus fs = new ForceStatus();//宣告一個勢力狀態
            ForceStatusDic.Add(key, fs);//加入字典
            status = new Status[CharaDic[key].Count];//宣告此勢力的腳色狀態陣列
            //設定此勢力的所有屬性
            fs.MaxCharaNum = CharaDic[key].Count;
            fs.CurCharaNum = CharaDic[key].Count;
            fs.DeathCharaNum = 0;
            fs.Hit = 0;
            fs.ElementHit = 0;
            fs.ElementHitRate = 0;
            fs.Score = 0;
            for (int i = 0; i < CharaDic[key].Count; i++)
            {
                status[i] = CharaDic[key][i].GetComponent<Status>();
                fs.CurHealth += status[i].CurHealth;
                fs.MaxHealth += status[i].CurHealth;
            }
            fs.CurHealthPercent = 100 * (fs.CurHealth / fs.MaxHealth);
            CharaStatusDic.Add(key, status);//加入腳色字典
        }
        TimerUI.StartTimer();//開始戰鬥計時器
        Result = 0;//初始化戰鬥結果
        HitGold = 0;//初始化取得額外金錢
        KillExp = 0;//初始化取得額外經驗
        TmpExp = 0;//初始化暫時經驗顯示
        TmpGold = 0;//初始化暫時金錢顯示
        TmpMemberExp = new float[3];//初始化暫時經驗條顯示
        TmpMemberNeedExp = new int[3];//初始化暫時需求經驗條顯示
        CanLvUp = new bool[3];//初始化可否升級
        for (int i = 0; i < CanLvUp.Length; i++)
        {
            CanLvUp[i] = true;
        }
        TmpMemberLv = new int[3];//初始化暫時等級顯示
        TmpMemberBrutal = new int[3];//初始化暫時殘暴顯示
        TmpMemberAggressive = new int[3];//初始化暫時侵略顯示
        TmpMemberResistance = new int[3];//初始化暫時抵抗顯示
        TmpMemberHealth = new int[3];//初始化暫時生命顯示
        AddMemberBrutal = new int[3];//初始化增加的殘暴
        AddMemberAggressive = new int[3];//初始化增加的侵略
        AddMemberResistance = new int[3];//初始化增加的抵抗
        AddMemberHealth = new int[3];//初始化增加的生命
        GainGold = 0;//初始化金錢顯示
        GainExp = 0;//初始化經驗顯示
        FinalGold = 0;//初始化最終取得金錢
        FinalExp = 0;//初始化最終取得經驗
        PerFinalExp = 0;//初始化分攤後的經驗值
        CalculateTime = 1;//播放計算Rank動畫的時間
        RewardPerTime = 0.05f;//播放計算數值動畫每次時間
        CalculateTimeUp = false;//當CalculateTimeUp等於true時，結束亂數顯示Rank
        ExperienceTime = 1f;//播放計算經驗數值動畫的時間
        ExpPerTime = 0.02f;//每次更新經驗條的間隔
        ExperienceTimeUp = false;//當CalculateTimeUp等於true時，結束經驗動畫
        RankScore = 0;//初始化玩者評價分數
        RankTmpShow = 1;
        RankExtraPercent = 0;//評價獎勵倍數
        RewardShow = false;
        CallEnd = false;
        SetCharaStatus();
        //將UpdateForceCondition方法加入委派
        UpdateForceStatus += UpdateForceCondition;
    }

}
