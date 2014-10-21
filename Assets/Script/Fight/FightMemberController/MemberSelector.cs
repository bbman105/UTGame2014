using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MemberSelector : MonoBehaviour
{
    public static bool[] MemberOnFight;
    protected static Dictionary<int, StayMemberSelector> StayMemberSelectors;
    protected static Dictionary<int, FightMemberSelector> FightMemberSelectors;
    protected static Dictionary<int, Status> MemberStatusDic;
    protected static bool CanRetreatMember;//是否可以撤退隊員
    protected static bool CanAddMember;//是否還有可以出戰的隊員
    protected SpeciesModel speciesModel;
    protected int memberRank;
    protected Status status;
    protected GameObject myGameobject;
    protected Transform myTransform;
    protected GameObject memberGameobject;
    protected Transform memberTransform;
    UISprite[] btnSprites;
    //出戰冷卻
    protected float CDTime;//CD時間
    protected float CDTimer;//CD計時器
    public bool OnCD { get; set; }//是否在CD
    public static int CountCondition { get; set; }//0=可設置預出戰怪物，1=準備開始，2=戰鬥已開始
    ////////////頭像欄位///////////
    //腳色icon圖的sprite
    private UISprite charaSprite;
    private UISprite iconCharaSprite;
    //腳色icon邊框圖的sprite
    private UISprite bannerSprite;
    //腳色icon底圖的sprite
    private UISprite bottomSprite;
    //腳色icon元素icon的sprite
    private UISprite elementSprite;
    //腳色icon等級label
    private UILabel lvLabel;
    //腳色icon血條Sprite
    static protected Dictionary<int, UISprite> StayHealthSpriteDic;
    static protected Dictionary<int, UISprite> FightHealthSpriteDic;

    public static void MonsterGoStay()//未設定為預設出戰的怪物取消顯示
    {
        for (int i = 0; i < ForceCondition.CharaStatusDic["PlayerMonster"].Length; i++)
        {
            if (!MemberOnFight[i])
            {
                ForceCondition.CharaStatusDic["PlayerMonster"][i].gameObject.SetActive(false);
            }
        }
    }
    public virtual void SetMember(int _memberRank)//設定怪物按鈕對應到場上怪物
    {
        this.memberRank = _memberRank;
        CDTime = 8;//CD設定為8秒
        CDTimer = CDTime;
        OnCD = false;
        if (ForceCondition.CharaStatusDic["PlayerMonster"][this.memberRank] != null)
        {
            status = ForceCondition.CharaStatusDic["PlayerMonster"][this.memberRank];
            memberGameobject = status.gameObject;
            memberTransform = status.transform;
            speciesModel = new SpeciesModel(
                status.transform,
                status.GetComponent<Status>().CharaSpriteRenderer,
                status.GetComponent<Status>().SpeciesKey,
                status.GetComponent<Status>().Size);
        }
    }
    protected virtual void SetBtnIcon()//設置怪獸按鈕Icon
    {
        //抓取腳色的ICon各屬性數值的物件
        iconCharaSprite = myTransform.FindChild("sprite").GetComponent<UISprite>();
        bannerSprite = myTransform.FindChild("banner").GetComponent<UISprite>();
        bottomSprite = myTransform.FindChild("bg").GetComponent<UISprite>();
        elementSprite = myTransform.FindChild("element").GetComponent<UISprite>();
        elementSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        bannerSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        bottomSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");

        ///////////////////////讀取目前選取腳色的資料/////////////////////////////
        //設置腳色Icon的各種資料(怪物、元素、邊框、底圖)
        //選擇對的物種頭像
        if (status.Species < 8)
        {
            iconCharaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        }
        else
        {
            iconCharaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
        }
        iconCharaSprite.spriteName = status.SpeciesKey;
        iconCharaSprite.MakePixelPerfect();
        elementSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        elementSprite.spriteName = "element" + status.MainElement.ToString();
        elementSprite.MakePixelPerfect();
        bannerSprite.spriteName = "frame" + status.Rare.ToString();
        bannerSprite.MakePixelPerfect();
        bottomSprite.spriteName = "bottom" + status.Rare.ToString();
        bottomSprite.MakePixelPerfect();
    }
    public static void MemberOnFightSet()//初始設定預設怪物出戰，最先被呼叫
    {
        StayMemberSelectors = new Dictionary<int, StayMemberSelector>();
        FightMemberSelectors = new Dictionary<int, FightMemberSelector>();
        MemberStatusDic = new Dictionary<int, Status>();
        ForceCondition.UpdateForceStatus += UpdateFightMember;//"檢查隊員是否存方法"加到委託事件中
        CountCondition = 0;//控制權限設置
        MemberOnFight = new bool[3];
        int canOnFightNum = 2;//起始預設出場兩隻怪物
        for (int i = 0; i < ForceCondition.CharaStatusDic["PlayerMonster"].Length; i++)
        {
            if (canOnFightNum > 0)
            {
                if (ForceCondition.CharaStatusDic["PlayerMonster"][i] != null)
                {
                    MemberOnFight[i] = true;
                    canOnFightNum--;
                }
                else
                {
                    MemberOnFight[i] = false;
                }
            }
            else
            {
                MemberOnFight[i] = false;
            }
        }
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }
    public static void BtnShower(bool _toFight, int _memberRank)
    {
        if (_toFight)
        {
            StayMemberSelectors[_memberRank].HideBtn();
            FightMemberSelectors[_memberRank].ShowBtn();
        }
        else
        {
            StayMemberSelectors[_memberRank].ShowBtn();
            FightMemberSelectors[_memberRank].HideBtn();
        }
    }
    protected void HideBtn()
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].enabled = false;
        }
    }
    protected void ShowBtn()
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].enabled = true;
        }
    }
    public virtual void LockBtn()//設定出戰按鈕正在CD壓灰
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].color = Color.gray;
        }
    }
    public virtual void UnLockBtn()//設定出戰按鈕結束CD取消壓灰
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].color = Color.white;
        }
    }
    public static void UpdateFightMember()//更新腳色狀態，更新血量，若怪物死亡，改變已出場的怪物數量
    {
        Dictionary<int, Status>.KeyCollection keys = MemberStatusDic.Keys;
        foreach (int key in keys)
        {
            if (!MemberStatusDic[key].Alive)
            {
                MemberOnFight[key] = false;
            }
            int tmpHealthBarLength = 68 + (int)(132 * (MemberStatusDic[key].CurHealth / MemberStatusDic[key].MaxHealth));
            StayHealthSpriteDic[key].width = tmpHealthBarLength;//更新未出戰腳色ICON血條
            FightHealthSpriteDic[key].width = tmpHealthBarLength;//更新已出戰腳色ICON血條
        }
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }
    protected static void CheckAssignMember()//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    {
        int onFightNum = 0;
        for (int i = 0; i < MemberOnFight.Length; i++)
        {
            if (MemberOnFight[i])
                onFightNum++;
        }
        switch (onFightNum)
        {
            case 0:
                break;
            case 1:
                CanRetreatMember = false;
                CanAddMember = true;
                break;
            case 2:
                CanRetreatMember = true;
                CanAddMember = false;
                break;
            default:
                Debug.Log("已出戰隊員數量異常");
                break;
        }


    }

    // Use this for initialization
    void Awake()
    {
        myGameobject = gameObject;
        myTransform = transform;
        btnSprites = myTransform.GetComponentsInChildren<UISprite>();
        
        StayHealthSpriteDic = new Dictionary<int, UISprite>();
        FightHealthSpriteDic = new Dictionary<int, UISprite>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
