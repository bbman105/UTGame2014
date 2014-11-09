using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerCtrl : Player
{

    private static bool canCtrl;
    public static bool CanCtrl//由CtrlPower的StartCtrl控制
    {
        get { return PlayerCtrl.canCtrl; }
        set { PlayerCtrl.canCtrl = value; }
    }
    ////////////////////////////////觸碰點////////////////////////////////////
    static Camera trainSceneCamera;
    private static int curTounchCount;
    private sbyte dragDir;
    public static GameObject CurCtrlTarget;//目前觸碰的物件
    public static Collider2D[] CurTouchTargets;//觸碰的目標物件群
    bool DrapUp;//是否是向上拖曳
    static float LongPressTime;//長按需求時間
    static float BeginTouchTime;//開始觸碰的時間點
    Touch touch;
    static private bool FirstStationary;//起始長按
    /////////////////////////////////////拖曳房間///////////////////////////////////
    static List<float> StartRoomPosY;
    static float TouchDiffRoomPosY;//觸碰點與第一房間座標Y軸差
    static Vector2 DragRoomPos;//拖曳房間的的位置
    private bool InFitPos;//開始執行位置歸正
    private static int curFloor;
    public static int CurFloor//目前在第幾層樓
    {
        get { return PlayerCtrl.curFloor; }
        set
        {
            if (value < 0)
                PlayerCtrl.curFloor = 0;
            else
                PlayerCtrl.curFloor = value;
        }
    }
    private Vector2 DragRoomPos2;//房間拖曳的點
    bool AlreadyFirstInActiveDragRoom;//是否已經執行第一次拖曳方向顯示房間
    private float RoomMoveDistY;//實際房間移動的Y軸距離
    private int moveRoomDistNum;//物件拖曳時實際移動了多少半個房間的距離
    private static float perRoomDist;//一個房間的距離
    static private List<float> LastPosY;//在拖曳之前房間的位置
    static private List<float> LockPosY;//拖曳後房間該到的Y軸
    static private List<float> MaxDownPosY;//最低時，所有房間的位置
    static private List<float> MaxUpPosY;//最高時，所有房間位置
    static private float FirstRoomPosY;//目前第一個房間的Y軸
    static private float StartRestrictDownPosY;//起始第一個房間的Y軸，限制最低拖曳樓層位置
    static private float StartRestrictUpPosY;//限制最高拖曳樓層位置
    static private float RestrictDrag;//超出房間數量範圍時，減少的力道，設2代表拖曳力道=原始力道-原始力道/2
    static private List<Vector3> LockPos;//拖曳後房間該到的位置
    private Vector3 roomDragSpd;
    /////////////////////////////////////搬動怪獸///////////////////////////////////
    byte TouchCondition;//觸碰狀態
    Rigidbody2D CatchMonsterRigid;//現在抓取的怪獸Rigid
    CircleCollider2D CatchMonsterCollider;//現在抓取的怪獸Collider
    public static GameObject CurCtrlMonster;//目前抓取的怪獸物件
    Vector2 MonsterStartPos;//怪物起始位置
    Vector2 DragMonstePos;//拖曳怪獸的位置
    static bool MonveMonster;//是否已經搬移怪獸
    PlayerMonster CurMonsterClass;//目前抓取的怪獸class
    Room MoveToRoomClass;//要放置的房間class
    bool CanMoveTo;//判斷拖曳怪獸的點是否為合法的點

    void Awake()
    {
        DontDestroyOnLoad(gameObject);//切場景時不清除物件
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
    }
    public static void StartSetting()
    {
        RestrictDrag = 1.3f;
        BeginTouchTime = 0;
        LongPressTime = 0.5f;//按下後多久視為長按
        CurFloor = 0;
        MonsterActionSwitchOn = true;
        trainSceneCamera = Camera.main;
        EventUISetting();
        curTounchCount = 0;
        perRoomDist = 5.14f;
        StartRoomPosY = new List<float>();
        LockPosY = new List<float>();
        LockPos = new List<Vector3>();
        LastPosY = new List<float>();
        MaxDownPosY = new List<float>();
        MaxUpPosY = new List<float>();
        for (int i = 0; i < RoomNum; i++)
        {
            LastPosY.Add(RoomObjList[i].transform.localPosition.y);
            MaxDownPosY.Add(RoomObjList[i].transform.localPosition.y);
            MaxUpPosY.Add(RoomObjList[i].transform.localPosition.y - ((RoomNum - 2) * perRoomDist));
            if (i == 0)
                StartRestrictDownPosY = RoomObjList[i].transform.localPosition.y;
        }
        StartRestrictUpPosY = StartRestrictDownPosY - ((RoomNum - 2) * perRoomDist);
    }


    void Update()
    {
        curTounchCount = 0;
        while (curTounchCount < Input.touchCount)
        {
            touch = Input.GetTouch(curTounchCount);
            if (CanCtrl)
            {

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        BeginTouch();//開始觸碰時執行
                        break;
                    case TouchPhase.Stationary:
                        StationaryTouch();//長按時執行
                        break;
                    case TouchPhase.Moved:
                        if (!InFitPos)
                        {
                            DragTouch();//拖曳時執行
                        }
                        break;
                    case TouchPhase.Ended:
                        if (!InFitPos)
                        {
                            EndTouch();//放開觸碰時執行
                        }
                        break;
                }
            }
            ++curTounchCount;
        }
        if (TouchCondition == 1)
        {
            if (InFitPos)
            {
                for (int i = 0; i < RoomNum; i++)
                {
                    RoomObjList[i].transform.localPosition = Vector3.Lerp(RoomObjList[i].transform.localPosition, LockPos[i], 10 * Time.deltaTime);
                }
            }
        }
    }
    void UpdateDragActiveRoom()//只顯示拖曳方向看的到的房間
    {
        bool tmpDragUp = DrapUp;
        if (RoomMoveDistY > 0)
            DrapUp = true;
        else
            DrapUp = false;
        if (tmpDragUp != DrapUp || !AlreadyFirstInActiveDragRoom)
        {
            AlreadyFirstInActiveDragRoom = true;
            RoomCtrl.OnlyActiveDragSeeRoom(DrapUp);//只顯示拖曳方向會看到的房間
            RoomCtrl.DisplayActiveRoomHappyBar();//只顯示可看到房間的快樂度
        }
    }
    void BeginTouch()//開始觸碰時執行
    {
        FirstStationary = true;
        MonveMonster = false;
        TouchCondition = 0;//恢復沒觸碰狀態
        BeginTouchTime = Time.time;
        //EventTouch();//按下物件觸發事件
        RoomMoveDistY = 0;
        moveRoomDistNum = 0;
        StartRoomPosY.Clear();
        LockPosY.Clear();
        LockPos.Clear();
        InFitPos = false;
        AlreadyFirstInActiveDragRoom = false;

        //判斷觸碰點
        TouchWorldPoint = trainSceneCamera.ScreenToWorldPoint(Input.GetTouch(curTounchCount).position);
        if (Physics2D.OverlapPoint(TouchWorldPoint) != null)
        {
            CurTouchTargets = Physics2D.OverlapPointAll(TouchWorldPoint);
            for (int i = 0; i < CurTouchTargets.Length; i++)
            {
                switch (CurTouchTargets[i].tag)
                {
                    case "EventBubble"://點擊事件泡泡
                        TouchCondition = 3;//開始獨立事件
                        CurCtrlTarget = CurTouchTargets[i].gameObject;
                        ShowEventUI();
                        break;
                    case "RoomChara"://點擊怪獸
                        CurCtrlTarget = CurTouchTargets[i].gameObject;
                        CurCtrlMonster = CurCtrlTarget;
                        MonsterStartPos = CurCtrlMonster.transform.position;
                        CurMonsterClass = CurCtrlMonster.GetComponent<PlayerMonster>();
                        if (!CurMonsterClass.OnEvent)//如果怪獸沒有事件中點取
                        {
                            TouchCondition = 2;//搬移怪獸狀態
                            ShowTouchText(CurCtrlMonster);
                            StartCatchMonster();//開始抓取怪獸
                        }
                        else//如果怪獸正處於事件中點取
                        {
                            TouchCondition = 3;//開始獨立事件
                            CurCtrlTarget = CurCtrlTarget.transform.FindChild("eventBubble").gameObject;//將物件改為選取EventBubble
                            ShowEventUI();
                        }
                        break;
                    case "RoomResource"://點擊房間資源物件
                        CurTouchTargets[i].transform.GetComponent<RoomResource>().TakeResource();
                        break;
                    default:
                        break;
                }
                if (TouchCondition == 2 || TouchCondition == 3)//如果抓取到怪獸或是觸發獨立事件則跳出為圈，不繼續偵測觸碰到的物件種類
                {
                    break;
                }
            }
            if (TouchCondition != 2 && TouchCondition != 3)//不是處於事件或是抓取怪獸中就設定為拖曳狀態
            {
                TouchCondition = 1;//TouchCondition=1就是沒有抓取怪獸，則為拖曳房間狀態
            }
            for (int j = 0; j < RoomNum; j++)
            {
                if (j == 0)
                {
                    FirstRoomPosY = RoomObjList[j].transform.localPosition.y;
                    TouchDiffRoomPosY = TouchWorldPoint.y - FirstRoomPosY;//算出第一個房間跟觸碰點的Y軸差
                }

                StartRoomPosY.Add(RoomObjList[j].transform.localPosition.y);
                LockPosY.Add(StartRoomPosY[j]);
                LockPos.Add(Vector3.zero);
            }
        }
    }
    void StationaryTouch()//長按時執行
    {
        if (TouchCondition == 1)
        {
            if (FirstStationary)//如果還沒觸發長按
            {
                if (Time.time - BeginTouchTime > LongPressTime)
                {
                    FirstStationary = false;//觸發長按後設定為false
                    RoomCtrl.DisplayActiveRoomHappyBar();//只顯示可看到房間的快樂度
                }
            }
        }
        else if (TouchCondition == 2)
        {
            if (Time.time - BeginTouchTime > LongPressTime)
            {
                //判斷觸碰點
                TouchWorldPoint = trainSceneCamera.ScreenToWorldPoint(Input.GetTouch(curTounchCount).position);
                DragMonstePos = TouchWorldPoint;
                CurCtrlTarget.transform.position = DragMonstePos;
            }
        }
    }
    void DragTouch()//拖曳時執行
    {
        PlayerCtrl.SwitchAllMonsterAction(false);//暫停怪獸動
        //判斷觸碰點
        TouchWorldPoint = trainSceneCamera.ScreenToWorldPoint(Input.GetTouch(curTounchCount).position);
        if (TouchCondition == 2)
        {
            //怪獸移動到滑鼠拖曳位置
            DragMonstePos = TouchWorldPoint;
            CurCtrlTarget.transform.position = DragMonstePos;
            if (Physics2D.OverlapPoint(DragMonstePos) != null)
            {
                CanMoveTo = true;//設為怪獸可搬移
                CurTouchTargets = Physics2D.OverlapPointAll(DragMonstePos);
                for (int i = 0; i < CurTouchTargets.Length; i++)
                {
                    switch (CurTouchTargets[i].tag)
                    {
                        case "Wall"://拖曳觸碰的位置包含牆壁或地板
                            CanMoveTo = false;//設為不可搬移狀態
                            CurMonsterClass.SetMonsterSpriteToCanNotMoveColor();//設定怪獸貼圖顏色標示為不可搬移
                            break;
                        case "RoomTrigger"://拖曳觸碰的位置的房間
                            MoveToRoomClass = CurTouchTargets[i].transform.parent.GetComponent<Room>();
                            if (MoveToRoomClass.OwnMonsterDic.Count > 2)//如果房間怪獸人數已滿，就不可搬移
                            {
                                CanMoveTo = false;//設為不可搬移狀態
                                CurMonsterClass.SetMonsterSpriteToCanNotMoveColor();//設定怪獸貼圖顏色標示為不可搬移
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (CanMoveTo)//如果觸碰點沒有牆壁或地板，則怪獸設為可搬移狀態
                    CurMonsterClass.SetMonsterSpriteToCanMoveColor();//設定怪獸貼圖顏色標示為可搬移
            }
            else
            {
                CanMoveTo = false;//設為不可搬移狀態
                CurMonsterClass.SetMonsterSpriteToCanNotMoveColor();//設定怪獸貼圖顏色標示為不可搬移
            }
        }
        else if (TouchCondition == 1)
        {
            DragRoomPos = new Vector2(0, TouchWorldPoint.y - TouchDiffRoomPosY);
            RoomMoveDistY = DragRoomPos.y - FirstRoomPosY;
            UpdateDragActiveRoom();//只顯示拖曳方向看的到的房間
            //如果房間位置超出畫面
            if (DragRoomPos.y > StartRestrictDownPosY)
            {
                DragRoomPos = new Vector2(0, DragRoomPos.y - (DragRoomPos.y - StartRestrictDownPosY) / RestrictDrag);
            }
            else if (DragRoomPos.y < StartRestrictUpPosY)
            {
                DragRoomPos = new Vector2(0, DragRoomPos.y + (StartRestrictUpPosY - DragRoomPos.y) / RestrictDrag);
            }
            for (int i = 0; i < RoomNum; i++)
            {
                RoomObjList[i].transform.localPosition = new Vector2(RoomObjList[i].transform.localPosition.x, StartRoomPosY[i] + RoomMoveDistY);
                RoomObjList[i].transform.localPosition = new Vector2(RoomObjList[i].transform.localPosition.x, DragRoomPos.y + i * perRoomDist);
            }
            moveRoomDistNum = Mathf.RoundToInt(Mathf.Abs(RoomMoveDistY / (perRoomDist)));//計算物件拖曳時實際移動了多少半個房間的距離
            if (moveRoomDistNum > 2)
            {
                InFitPos = true;
                moveRoomDistNum = 2;
                EndTouch();//當拖曳距離太長時，強制執行觸碰時執行
            }
        }
    }
    public void StartCatchMonster()//開始抓取怪獸
    {
        CurMonsterClass.SetCatchSortingOrder();//設定怪受貼圖層級為正在抓取中
        DragMonstePos = CurCtrlMonster.transform.position;//設定目前拖曳點為怪獸座標
        CatchMonsterRigid = CurCtrlMonster.GetComponent<Rigidbody2D>();
        CatchMonsterCollider = CurCtrlMonster.GetComponent<CircleCollider2D>();
        CatchMonsterRigid.isKinematic = true;//關閉怪獸受到引力影響
        CatchMonsterCollider.enabled = false;//關閉怪獸的碰撞
    }
    public void EndCatchMonster()//結束抓取怪獸
    {
        CatchMonsterRigid.isKinematic = false;//開啟怪獸受到引力影響
        CatchMonsterCollider.enabled = true;//開啟怪獸的碰撞
        //判斷觸碰點
        TouchWorldPoint = trainSceneCamera.ScreenToWorldPoint(Input.GetTouch(curTounchCount).position);
        if (Physics2D.OverlapPoint(TouchWorldPoint) != null)
        {
            CurTouchTargets = Physics2D.OverlapPointAll(TouchWorldPoint);
            for (int i = 0; i < CurTouchTargets.Length; i++)
            {
                switch (CurTouchTargets[i].tag)
                {
                    case "RoomTrigger"://放開觸碰的位置的房間
                        if (CanMoveTo)
                        {
                            if (CurCtrlTarget.transform.parent != CurTouchTargets[i].transform.parent)//如果移到不同房間
                            {
                                MoveToRoomClass = CurTouchTargets[i].transform.parent.GetComponent<Room>();
                                if (MoveToRoomClass.OwnMonsterDic.Count < 3)//如果房間沒有客滿才可搬移怪獸
                                {
                                    CurMonsterClass.MoveMonsterToRoom(MoveToRoomClass, DragMonstePos);//將怪獸加入到新的房間
                                    MonveMonster = true;
                                }
                                else
                                {
                                    CurCtrlMonster.transform.position = MonsterStartPos;//恢復怪物位置
                                    CurMonsterClass.SetSpriteSortingOrder();//恢復怪獸貼圖層級
                                }
                            }
                            else
                            {
                                MonveMonster = true;
                                CurMonsterClass.SetSpriteSortingOrder();//恢復怪獸貼圖層級
                            }
                        }
                        else//如果怪獸搬移時在不可放置的狀態下釋放，則恢復怪獸位置
                        {
                            CurCtrlMonster.transform.position = MonsterStartPos;//恢復怪物位置
                            CurMonsterClass.SetSpriteSortingOrder();//恢復怪獸貼圖層級
                        }
                        break;
                    default:
                        break;
                }
                if (MonveMonster)//如果已搬移怪獸，不須要繼續搜尋觸碰到的物件，跳出迴圈
                    break;
            }
            if (!MonveMonster)//如果已搬移怪獸，不須要繼續搜尋觸碰到的物件，跳出迴圈
            {
                CurCtrlMonster.transform.position = MonsterStartPos;//恢復怪物位置
                CurMonsterClass.SetSpriteSortingOrder();//恢復怪獸貼圖層級
            }
        }
        else
        {
            CurCtrlMonster.transform.position = MonsterStartPos;//恢復怪物位置
            CurMonsterClass.SetSpriteSortingOrder();//恢復怪獸貼圖層級
        }
        CurMonsterClass.SetNotCatchSortingOrder();//設定怪獸貼圖顏色標示為不在搬移
    }
    void EndTouch()//放開觸碰時執行
    {
        if (TouchCondition == 2)//如果有觸碰到怪獸
        {
            EndCatchMonster();//結束抓取怪獸
            PlayerCtrl.SwitchAllMonsterAction(true);//恢復怪獸動
        }
        else if (TouchCondition == 1)
        {
            if (moveRoomDistNum > 0)
            {
                //找出第一個房間跟最後一個房間的位置                
                if (RoomMoveDistY > 0)
                {
                    CurFloor -= moveRoomDistNum;
                    //LockPosY[0] = LastPosY[0] + (moveRoomDistNum * perRoomDist);
                    LockPosY[LockPos.Count - 1] = LastPosY[LastPosY.Count - 1] + (moveRoomDistNum * perRoomDist);
                }
                else
                {
                    CurFloor += moveRoomDistNum;
                    //LockPosY[0] = LastPosY[0] - (moveRoomDistNum * perRoomDist);
                    LockPosY[LockPos.Count - 1] = LastPosY[LastPosY.Count - 1] - (moveRoomDistNum * perRoomDist);
                }

                //如果房間位置超出畫面
                if (DragRoomPos.y > StartRestrictDownPosY)
                {
                    CurFloor = 0;
                    for (int i = 0; i < RoomNum; i++)
                    {
                        LockPosY[i] = MaxDownPosY[i];
                        LockPos[i] = new Vector3(0, LockPosY[i], 0);
                        LastPosY[i] = LockPosY[i];
                    }
                }
                else if (DragRoomPos.y < StartRestrictUpPosY)
                {
                    CurFloor = RoomNum - 2;
                    for (int i = 0; i < RoomNum; i++)
                    {
                        LockPosY[i] = MaxUpPosY[i];
                        LockPos[i] = new Vector3(0, LockPosY[i], 0);
                        LastPosY[i] = LockPosY[i];
                    }
                }
                else
                {
                    for (int i = 0; i < RoomNum; i++)
                    {
                        if (RoomMoveDistY > 0)
                        {
                            LockPosY[i] = LastPosY[i] + (moveRoomDistNum * perRoomDist);
                            LastPosY[i] = LockPosY[i];
                        }
                        else
                        {
                            LockPosY[i] = LastPosY[i] - (moveRoomDistNum * perRoomDist);
                            LastPosY[i] = LockPosY[i];
                        }
                        LockPos[i] = new Vector3(0, LockPosY[i], 0);
                    }
                }
            }
            else
            {
                for (int i = 0; i < RoomNum; i++)
                {
                    LockPosY[i] = LastPosY[i];
                    LockPos[i] = new Vector3(0, LockPosY[i], 0);
                }
            }
            PlayerCtrl.SwitchAllMonsterAction(true);//恢復怪獸動
            InFitPos = true;
            RoomCtrl.HideActiveRoomHappyBar();//隱藏房間快樂度
        }
        RoomCtrl.OnlyActiveSeeRoom();//只顯示看到的房間
    }
}