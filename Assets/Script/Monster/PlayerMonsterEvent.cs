using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{

    ////////////////////////////事件屬性///////////////////////////
    //多久觸發一次事件
    float EventMaxTime { get; set; }
    float EventMinTime { get; set; }
    private float eventTimer;
    public float EventTimer
    {
        get { return eventTimer; }
        set
        {
            if (value < 0)
            {
                eventTimer = 0;
            }
            else { eventTimer = value; }
        }
    }
    //事件是否正在觸發
    public bool OnEvent { get; set; }
    //事件持續多久
    private float eventKeepTime;
    public float EventKeepTime
    {
        get { return eventKeepTime; }
        set { eventKeepTime = value; }
    }
    private float eventKeepTimer;
    public float EventKeepTimer
    {
        get { return eventKeepTimer; }
        set { eventKeepTimer = value; }
    }
    //事件ID
    private int eventID;
    public int EventID
    {
        get { return eventID; }
        set
        {
            if (value < 0)
            {
                eventID = 0;
            }
            else { eventID = value; }
        }
    }

    private int eventAction;
    public int EventAction
    {
        get { return eventAction; }
        set { eventAction = value; }
    }
    RoomCharaStatus StatusCom;
    SpriteRenderer EventBubbleSprite { get; set; }//泡泡底的圖像
    SpriteRenderer EventIconSprite { get; set; }//泡泡ICON的圖像
    SpriteRenderer RewardIcon1 { get; set; }//回復ICON圖像
    SpriteRenderer RewardIcon2 { get; set; }//回復ICON圖像
    SpriteRenderer RewardIcon3 { get; set; }//回復ICON圖像
    MeshRenderer[] MonsterTextMesh;    //怪獸說話文字
    SpriteRenderer TextBottom { get; set; }//怪獸說話文字底框


    private int eventNum;
    public int EventNum
    {
        get { return eventNum; }
        set { eventNum = value; }
    }
    float eventBubbleDir;
    float eventBubblePos;
    IShowText showFeedBackText;
    IShowText showRewardText;
    //設定事件起始設定
    private void StartSetEvent()//事件起始設定
    {
        //怪物狀態
        StatusCom = myTransform.GetComponent<RoomCharaStatus>();
        EventBubbleTransform = myTransform.FindChild("eventBubble");
        ShowTextList = new List<IShowText>();
        showFeedBackText = new ShowFeedBackText(EventBubbleTransform);
        ShowTextList.Add(showFeedBackText);
        showRewardText = new ShowRewardText(EventBubbleTransform);
        ShowTextList.Add(showRewardText);
        showRewardTime = 1;
        //eventBubble物件
        EventBubble = EventBubbleTransform.gameObject;
        EventIcon = EventBubble.transform.FindChild("eventIcon").gameObject;
        EventCollider = EventBubbleTransform.GetComponent<CircleCollider2D>();
        //怪物事件ICON圖像
        EventIconSprite = EventIcon.GetComponent<SpriteRenderer>();
        EventBubbleSprite = EventBubbleTransform.FindChild("eventBubble").GetComponent<SpriteRenderer>();
        RewardIcon1 = myTransform.FindChild("RewardText").FindChild("rewardText1").FindChild("eventIcon").GetComponent<SpriteRenderer>();
        RewardIcon2 = myTransform.FindChild("RewardText").FindChild("rewardText2").FindChild("eventIcon").GetComponent<SpriteRenderer>();
        RewardIcon3 = myTransform.FindChild("RewardText").FindChild("rewardText3").FindChild("eventIcon").GetComponent<SpriteRenderer>();
        MonsterTextMesh = myTransform.GetComponentsInChildren<MeshRenderer>();
        TextBottom = myTransform.FindChild("EventText").FindChild("EventText").FindChild("textBottom").GetComponent<SpriteRenderer>();
        //event Timer
        EventID = 0;
        EventBubble.SetActive(false);
        EventIcon.SetActive(true);
        EventMaxTime = 5;//5
        EventMinTime = 20;//20
        SetEventTimer();//重新設定事件觸發的時間
        EventKeepTime = 30;
        EventKeepTimer = EventKeepTime;
        OnEvent = false;
    }
    public void SetEventTimer()//重新設定事件觸發的時間
    {
        EventTimer = Random.Range(EventMinTime, EventMaxTime);
    }
    private void EventTimerFunction()
    {
        if (OnEvent == false)
        {
            if (EventTimer > 0)
            {
                EventTimer -= Time.deltaTime;
            }
            else
            {
                int rndTriiger = Random.Range(1, 6);
                if (rndTriiger == 1)//5分支1機率觸發
                {
                    GetEvent();//觸發亂數事件
                }
                SetEventTimer();//重新設定事件觸發的時間
            }
        }
        else
        {
            if (eventKeepTimer > 0)
            {
                EventKeepTimer -= Time.deltaTime;
            }
            else
            {
                RemoveEvent();//移除事件
            }
        }
    }
    public void RemoveEvent()//移除事件
    {
        EventKeepTimer = EventKeepTime;
        OnEvent = false;
        EventBubble.SetActive(false);
        StatusCom.Action(EventAction, 0);//1是開始執行,0是結束
    }
    void GetEvent()//觸發事件
    {
        OnEvent = true;
        EventBubble.SetActive(true);
        AdjustEventBubblePosition(StatusCom.FaceDir);//歸正ICON位置
        UnityEngine.Random.seed = System.Guid.NewGuid().GetHashCode();
        int rnd = UnityEngine.Random.Range(0, TrainScene.EventNum);
        EventID = int.Parse(TrainScene.EventList[rnd].id);
        EventIconSprite.sprite = Resources.Load<Sprite>(string.Format("Sprite/Room/Event/{0}", TrainScene.EventDic[EventID].dialogueIcon));
        EventAction = TrainScene.EventDic[EventID].action;
        StatusCom.Action(EventAction, 1);//1是開始執行,0是結束
    }
    public void RandTriggerEvent()//亂數觸發事件
    {
        int rndTriiger = Random.Range(1, 11);
        if (rndTriiger == 1)//10分支1機率觸發
        {
            GetEvent();//觸發事件
        }
    }
    public static void AfterFightTriggerEvent()//戰鬥之後抓出沒有事件的怪物觸發事件
    {
        List<int> monsterKeyBuffer = new List<int>(Player.MonsterDic.Keys);
        foreach (int key in monsterKeyBuffer)
        {
            if (!Player.MonsterDic[key].OnEvent)//此怪獸沒有事件
            {
                Player.MonsterDic[key].RandTriggerEvent();//亂數觸發事件
            }
        }
    }
    public void SwitchMonsterAction(bool _on)//怪獸動作開關
    {
        StatusCom.SwitchCharaAction(_on);
        if (_on)
            StatusCom.enabled = true;
        else
            StatusCom.enabled = false;
        this.enabled = _on;
    }
    /// <summary>
    /// 傳入怪獸面向，歸正ICON位置
    /// </summary>
    public void AdjustEventBubblePosition(int _faceDir)//歸正ICON位置
    {
        eventBubbleDir = _faceDir * 1;
        eventBubblePos = _faceDir * -1;
        EventBubbleTransform.localScale = new Vector3(eventBubbleDir, 1, 1);
        EventBubbleTransform.localPosition = new Vector3(eventBubblePos, 1, 1);
    }
    public void ShowFeedBackText(int _choiceCase)
    {
        //呼叫回饋事件文字
        showFeedBackText.ShowText(_choiceCase);
        StartCoroutine(WaitForShowRewardText(showRewardTime, _choiceCase));
    }
    IEnumerator WaitForShowRewardText(float _waitTime, int _choiceCase)//等待0.5秒
    {
        yield return new WaitForSeconds(_waitTime);
        for (int i = 0; i < 3; i++)//抓取影響的3個屬性
        {
            if (TrainScene.EventFeedBackDic[string.Format("{0}-{1}", Personality, TrainScene.CurEventID)].ansFeedback[_choiceCase][i] != 0)
            {
                //呼叫回饋個性數值
                showRewardText.ShowText(_choiceCase, i);
                yield return new WaitForSeconds(_waitTime);
            }
        }
    }
}
