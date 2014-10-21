using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MemberArrow : MonoBehaviour
{
    public static Dictionary<int, bool> MemberOnFight;
    protected static Dictionary<int, MemberArrow> MemberArrowDic;
    public static int ArrowAniCode;//判斷播放的animation
    protected static Dictionary<int, Status> MemberStatusDic;
    protected static Dictionary<int, bool> MemberIsDead;//腳色是否死亡
    protected static bool CanRetreatMember;//是否可以撤退隊員
    protected static bool CanAddMember;//是否還有可以出戰的隊員
    protected static int OnFightNum { get; set; }//場上出場的怪獸數量
    protected SpeciesModel speciesModel;
    protected int memberRank;//第幾隻怪獸
    protected Status status;//對應到的怪獸屬性
    protected GameObject myGameobject;//腳色箭頭物件
    protected Transform myTransform;//腳色箭頭Transform
    protected GameObject memberGameobject;//對應到的怪獸物件
    protected Transform memberTransform;//對應到的怪獸Transform
    SpriteRenderer[] btnSprites;//箭頭底下的所有SpriteRenderer
    //出戰冷卻
    protected float CDTime;//CD時間
    protected float CDTimer;//CD計時器
    public bool OnCD { get; set; }//是否在CD
    //腳色Icon冷卻Label
    private TextMesh cdTextMesh;
    private Renderer cdRenderer;
    private int showCdNumber;//頭像上顯示的CD值
    //腳色Tip顯示
    private static Animator tipTextAni;
    private static TextMesh tipTextMesh;
    private static Renderer tipRenderer;
    private static string tipText;//Tip顯示內容
    private static int tipCode;//判斷要顯示什麼Tip內容

    public static int RestrictCondition { get; set; }//0=可設置預出戰怪物，1=準備開始，2=戰鬥已開始，-1=關閉按鈕功能

    ////////////頭像欄位///////////
    //腳色icon圖的sprite
    private Sprite speciesSprite;
    private SpriteRenderer iconCharaSprite;
    //腳色死亡ICON的sprite
    private SpriteRenderer charaDeathSprite;
    //腳色icon元素icon的sprite
    private Sprite arrowSprite;
    private SpriteRenderer elementArrow;
    //腳色icon元素icon的sprite
    private Sprite bladeSprite;//出戰雙刀圖示
    private Sprite sandglassSprite;//撤退沙漏圖示
    private SpriteRenderer conditionArrow;
    //所有腳色icon血條Sprite
    static protected Dictionary<int, Transform> HealthTransformDic;
    //所有箭頭的Animator
    static protected Dictionary<int, Animator> ArrowAnimatorDic;

    public static void MonsterGoStay()//未設定為預設出戰的怪物取消顯示
    {
        for (int i = 0; i < MemberStatusDic.Count; i++)
        {
            if (!MemberOnFight[i])
            {
                MemberStatusDic[i].IsRetreated();//已經退場
            }
        }
    }
    public static void InActiveNullMemberArrow()//取消顯示空隊員的箭頭
    {
        for (int i = 0; i < ForceCondition.CharaStatusDic["PlayerMonster"].Length; i++)
        {
            if (!MemberArrowDic.ContainsKey(i))
            {
                ForceCondition.CharaStatusDic["PlayerMonster"][i].gameObject.SetActive(false);
            }
        }
    }
    public void SetMember(int _memberRank)//設定怪物按鈕對應到場上怪物
    {
        this.memberRank = _memberRank;
        ForceCondition.CharaStatusDic["PlayerMonster"][this.memberRank].MemberArrowRank = memberRank;
        CDTime = 0.5f;//CD設定為8秒
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
        SetBtnIcon();//設定按鈕圖像
        if (MemberOnFight[memberRank])
            ArrowAniCode = 1;
        else
            ArrowAniCode = 2;
        SetArrowAni();//依照是否出戰設定箭頭動畫
        MemberArrowDic.Add(memberRank, this);
        MemberStatusDic.Add(this.memberRank, status);//將此隊員狀態加入字典
        MemberIsDead.Add(memberRank, false);
        UpdateFightMember();
    }
    void SetArrowAni()//依照ArrowAniCode設定箭頭動畫
    {
        switch (ArrowAniCode)
        {
            case 1:
                ArrowAnimatorDic[memberRank].SetTrigger("Fight");
                speciesModel.SetSelectedColor(true);
                break;
            case 2:
                ArrowAnimatorDic[memberRank].SetTrigger("Retreat");
                speciesModel.SetSelectedColor(false);
                break;
            case 3:
                ArrowAnimatorDic[memberRank].SetTrigger("CanNotFight");
                break;
            case 4:
                ArrowAnimatorDic[memberRank].SetTrigger("CanNotRetreat");
                break;
            default:
                break;
        }
    }
    protected void SetBtnIcon()//設置怪獸按鈕Icon
    {
        //抓取腳色的ICon各屬性數值的物件
        iconCharaSprite = myTransform.FindChild("member").GetComponent<SpriteRenderer>();
        speciesSprite = Resources.Load<Sprite>(string.Format("Sprite/MonsterSprite/{0}", status.SpeciesKey));
        iconCharaSprite.sprite = speciesSprite;
        elementArrow = myTransform.FindChild("arrowBottom").GetComponent<SpriteRenderer>();
        arrowSprite = Resources.Load<Sprite>(string.Format("Sprite/Arrow/arrow{0}", status.MainElement));
        elementArrow.sprite = arrowSprite;
        conditionArrow = myTransform.FindChild("condition").GetComponent<SpriteRenderer>();
        bladeSprite = Resources.Load<Sprite>(string.Format("Sprite/Arrow/blade{0}", status.MainElement));
        sandglassSprite = Resources.Load<Sprite>(string.Format("Sprite/Arrow/sandglass{0}", status.MainElement));
        charaDeathSprite = myTransform.FindChild("death").GetComponent<SpriteRenderer>();
        if (MemberOnFight[memberRank])
        {
            conditionArrow.sprite = bladeSprite;//設定圖示為沙漏
        }
        else
        {
            conditionArrow.sprite = sandglassSprite;//設定圖示為沙漏
        }

        if (!HealthTransformDic.ContainsKey(memberRank))
        {
            HealthTransformDic.Add(memberRank, myTransform.FindChild("health").GetComponent<Transform>());
        }
        if (!ArrowAnimatorDic.ContainsKey(memberRank))
        {
            ArrowAnimatorDic.Add(memberRank, myTransform.GetComponent<Animator>());
        }
        //抓取CD文字
        cdTextMesh = myTransform.FindChild("cd").GetComponent<TextMesh>();
        cdRenderer = cdTextMesh.renderer;
        cdRenderer.sortingLayerName = "FightSceneBG";
        cdRenderer.sortingOrder = 10;
        cdRenderer.enabled = false;//隱藏CD秒數
    }
    static void StartSetTipText()//初始設置文字顯示
    {
        //抓取Tip顯示文字
        tipTextAni = GameObject.FindGameObjectWithTag("FightMemberController").transform.FindChild("Tip").GetComponent<Animator>();
        tipTextMesh = tipTextAni.GetComponentInChildren<TextMesh>();
        tipRenderer = tipTextMesh.renderer;
        tipRenderer.sortingLayerName = "FightSceneBG";
        tipRenderer.sortingOrder = 11;
        tipRenderer.enabled = false;
        tipCode = 0;
    }
    public static void MemberOnFightSet()//初始設定預設怪物出戰，最先被呼叫
    {
        MemberArrowDic = new Dictionary<int, MemberArrow>();
        MemberStatusDic = new Dictionary<int, Status>();
        MemberIsDead = new Dictionary<int, bool>();
        ForceCondition.UpdateForceStatus += UpdateFightMember;//"檢查隊員是否存方法"加到委託事件中
        RestrictCondition = 0;//控制權限設置
        OnFightNum = 0;
        MemberOnFight = new Dictionary<int, bool>();
        int canOnFightNum = 2;//起始預設出場兩隻怪物
        for (int i = 0; i < ForceCondition.CharaStatusDic["PlayerMonster"].Length; i++)
        {

            if (canOnFightNum > 0)
            {
                OnFightNum++;
                MemberOnFight.Add(i, true);
                canOnFightNum--;
            }
            else
            {
                MemberOnFight.Add(i, false);
            }
        }
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
        StartSetTipText();//初始設置文字顯示
    }
    public virtual void LockBtn()//設定出戰按鈕正在CD壓灰
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].color = Color.gray;
        }
        //if (OnCD && !MemberIsDead[memberRank])
        //cdRenderer.enabled = true;//顯示CD文字
    }
    public virtual void UnLockBtn()//設定出戰按鈕結束CD取消壓灰
    {
        for (int i = 0; i < btnSprites.Length; i++)
        {
            btnSprites[i].color = Color.white;
        }
        if (!OnCD)
            cdRenderer.enabled = false;//隱藏CD文字
    }
    private static void MemberDead(int _key)//腳色死亡
    {
        if (!MemberIsDead[_key])
        {
            OnFightNum--;
            MemberArrowDic[_key].charaDeathSprite.enabled = true;
            MemberArrowDic[_key].conditionArrow.sprite = MemberArrowDic[_key].sandglassSprite;//設定圖示為沙漏
            MemberIsDead[_key] = true;
            //死亡時面板自動掉落並標示為不可用
            MemberOnFight[_key] = false;
            ArrowAnimatorDic[_key].SetTrigger("Retreat");
            MemberArrowDic[_key].OnCD = false;
            MemberArrowDic[_key].LockBtn();
            ArrowAniCode = 2;//設定箭頭動畫為出戰失敗
            MemberArrowDic[_key].SetArrowAni();//依照是否出戰設定箭頭動畫
            ForceToFightCheck();//檢查如果場上沒有怪獸出場，則強制出場一隻怪獸
            CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
        }
    }
    public static void UpdateFightMember()//更新腳色狀態，更新血量，若怪物死亡，改變已出場的怪物數量
    {
        Dictionary<int, Status>.KeyCollection keys = MemberStatusDic.Keys;
        foreach (int key in keys)
        {
            if (!MemberStatusDic[key].Alive)
            {
                MemberDead(key);
            }
            float tmpHealthBarLength = (16.5f * ((float)MemberStatusDic[key].CurHealth / (float)MemberStatusDic[key].MaxHealth));
            HealthTransformDic[key].localScale = new Vector3(tmpHealthBarLength, 1, 1);//更新腳色ICON血條
        }
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }
    private static void ForceToFightCheck()//檢查如果場上沒有怪獸出場，則強制出場一隻怪獸
    {
        if (OnFightNum <= 0)//如果場上沒有怪獸出場，則強制出場一隻怪獸
        {
            for (int i = 0; i < MemberOnFight.Count; i++)
            {
                if (!MemberOnFight[i] && !MemberIsDead[i])//未出戰，且存活的怪獸強制出場一隻
                {
                    MemberArrowDic[i].ToFight();//怪獸出場
                    MemberArrowDic[i].SetArrowAni();//依照是否出戰設定箭頭動畫
                    MemberOnFight[i] = true;
                    Debug.Log("OnFightNum=" + OnFightNum);
                    break;
                }
            }
        }
    }
    protected static void CheckAssignMember()//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    {
        switch (OnFightNum)
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
        btnSprites = myTransform.GetComponentsInChildren<SpriteRenderer>();
        HealthTransformDic = new Dictionary<int, Transform>();
        ArrowAnimatorDic = new Dictionary<int, Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ToFightCDFunction();
    }

    void OnMouseDown()
    {
        if (RestrictCondition == 0)//CountDown.EndCount(倒數結束)為false時，可設定預設出戰怪物
        {
            if (MemberOnFight[memberRank])
            {
                if (CanRetreatMember)
                {
                    OnFightNum--;
                    MemberOnFight[memberRank] = false;//設定此怪獸退場
                    conditionArrow.sprite = sandglassSprite;//設定圖示為沙漏
                    speciesModel.SetSelectedColor(false);//此怪獸顏色壓灰
                    CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
                    ArrowAniCode = 2;//設定箭頭動畫為撤退
                }
                else
                {
                    ArrowAniCode = 4;//設定箭頭動畫為撤退失敗
                    tipCode = 1;
                    ShowTip();//依照tipCode顯示tip說明
                }
            }
            else
            {
                if (CanAddMember)
                {
                    OnFightNum++;
                    MemberOnFight[memberRank] = true;//設定此怪獸出場
                    conditionArrow.sprite = bladeSprite;//設定圖示為雙刀
                    speciesModel.SetSelectedColor(true);//設定此怪獸顏色恢復正常
                    CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
                    ArrowAniCode = 1;//設定箭頭動畫為出戰
                }
                else
                {
                    ArrowAniCode = 3;//設定箭頭動畫為出戰失敗
                    tipCode = 2;
                    ShowTip();//依照tipCode顯示tip說明
                }
            }
            SetArrowAni();//依照是否出戰設定箭頭動畫
        }
        else if (RestrictCondition == 2)//戰鬥中
        {
            if (MemberOnFight[memberRank])
            {
                if (CanRetreatMember)
                {
                    if (!MemberIsDead[memberRank])
                        ToStay();//隊員退場
                }
                else
                {
                    ArrowAniCode = 4;//設定箭頭動畫為撤退失敗
                    tipCode = 1;
                    ShowTip();//依照tipCode顯示tip說明
                }
            }
            else
            {
                if (CanAddMember)
                {
                    if (!MemberIsDead[memberRank])
                    {
                        if (!OnCD)
                            ToFight();//隊員出戰
                    }
                    else
                    {
                        ArrowAniCode = 3;//設定箭頭動畫為出戰失敗
                        tipCode = 3;
                        ShowTip();//依照tipCode顯示tip說明
                    }
                }
                else
                {
                    ArrowAniCode = 3;//設定箭頭動畫為出戰失敗
                    tipCode = 2;
                    ShowTip();//依照tipCode顯示tip說明
                }
            }
            SetArrowAni();//依照是否出戰設定箭頭動畫
        }
    }

    void ToStay()//退場
    {
        OnFightNum--;
        MemberOnFight[memberRank] = false;//設定此怪獸退場
        ArrowAniCode = 2;//設定箭頭動畫為撤退
        MemberArrowDic[memberRank].OnCD = true;//開始計時CD8秒
        MemberArrowDic[memberRank].LockBtn();//設定按鈕為灰色代表在CD無法招出
        conditionArrow.sprite = sandglassSprite;//設定圖示為沙漏
        status.StartRetreat();//怪物退場
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }
    void ToFight()//隊員出戰
    {
        OnFightNum++;
        MemberOnFight[memberRank] = true;//設定此怪獸出場
        ArrowAniCode = 1;//設定箭頭動畫為出場
        conditionArrow.sprite = bladeSprite;//設定圖示為雙刀
        status.Fight();//怪物出戰
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }

    void ToFightCDFunction()
    {
        if (OnCD)
        {
            if (CDTimer > 0)
            {
                if (showCdNumber != (int)CDTimer)
                {
                    showCdNumber = (int)CDTimer;
                    cdTextMesh.text = showCdNumber.ToString();
                }
                CDTimer -= Time.deltaTime;
            }
            else
            {
                CDTimer = CDTime;
                OnCD = false;
                UnLockBtn();//設定按鈕為原來顏色，代表CD結束可以再次出戰
            }
        }
    }
    static void ShowTip()//依照tipCode顯示tip說明
    {

        switch (tipCode)
        {
            case 0:
                break;
            case 1:
                tipTextMesh.text = "最少出戰一隻怪獸";
                tipTextAni.SetTrigger("ShowTip");//設定動畫
                break;
            case 2:
                tipTextMesh.text = "最多出戰兩隻怪獸";
                tipTextAni.SetTrigger("ShowTip");//設定動畫
                break;
            case 3:
                tipTextMesh.text = "死亡怪獸無法出戰";
                tipTextAni.SetTrigger("ShowTip");//設定動畫
                break;
            default:
                Debug.Log("無此tipCode代碼");
                break;
        }
    }
}
