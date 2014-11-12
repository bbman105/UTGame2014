﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster, IMonsterSize, IComparable
{

    /////////////////////////怪物屬性////////////////////////////


    //擁有此怪物玩家的編號
    private int ownPlayerID;
    //怪物的進化點數上限
    const int limitEvoPoint = 60;
    //怪物野性值
    private int wild;
    public int Wild
    {
        get { return wild; }
        set
        {
            if (value < 0)
            {
                wild = 0;
            }
            else if (value > limitEvoPoint)
            {
                wild = limitEvoPoint;
            }
            else
            {
                wild = value;
            }
        }
    }
    //怪物突變值
    private int mutation;
    public int Mutation
    {
        get { return mutation; }
        set
        {
            if (value < 0)
            {
                mutation = 0;
            }
            else if (value > limitEvoPoint)
            {
                mutation = limitEvoPoint;
            }
            else
            {
                mutation = value;
            }
        }
    }
    //怪物原生值
    private int natural;
    public int Natural
    {
        get { return natural; }
        set
        {
            if (value < 0)
            {
                natural = 0;
            }
            else if (value > limitEvoPoint)
            {
                natural = limitEvoPoint;
            }
            else
            {
                natural = value;
            }
        }
    }

    //目前經驗
    private int curExp;
    public int CurExp
    {
        get { return curExp; }
        set { curExp = value; }
    }
    //升級所需經驗
    private int needExp;
    public int NeedExp
    {
        get { return needExp; }
        set { needExp = value; }
    }


    public int EveryEvoLevel = 10;//給個階級最多升10等級
    //個性
    private int personality;
    public int Personality
    {
        get { return personality; }
        set { personality = value; }
    }
    public int SelectedSkillID { get; set; }//選擇的技能ID
    //第二階選擇的物種
    private int level2Species;
    public int Level2Species
    {
        get { return level2Species; }
        set
        {
            if (value == 1 || value == 2 || value == 3)
            {
                level2Species = value;
            }
            else
            {
                level2Species = 0;
            }
        }
    }

    //第三階選擇的物種
    private int level3Species;
    public int Level3Species
    {
        get { return level3Species; }
        set
        {
            if (value == 4 || value == 5 || value == 6)
            {
                level3Species = value;
            }
            else
            {
                level3Species = 0;
            }
        }
    }
    public static int SortType { get; set; }//排序方法
    public static bool SmallToBig { get; set; }//由小到大
    //怪獸狀態
    public RoomCharaStatus CharaStatus;//此怪獸的狀態
    //經驗取得控制器
    ExperienceGettor ExpGettor;
    //進化點數取得控制器
    EvolutionPointGettor EvoPointGettor;
    //更名控制器
    MonsterNameGettor NameGettor;
    //怪物大小控制器
    MonsterSizeGettor SizeGettor;
    //怪獸事件泡泡框相關物件
    public GameObject EventBubble { get; set; }
    Transform EventBubbleTransform { get; set; }
    public GameObject EventIcon { get; set; }
    public CircleCollider2D EventCollider { get; set; }
    //怪獸進化控制器
    EvolutionGettor EvoGettor;
    Transform myTransform;
    bool IsStartSet;//是否已啟動功能，未啟動時不觸發update裡的內容

    void Update()
    {
        if (!IsStartSet)
            return;
        EventTimerFunction();//獨立事件觸發
        DailyEventTimerFunction();//日常事件的計時觸發
    }
    public void GetPlayerMonsterTool()//取得玩家怪物需要的控制器工具
    {
        ExpGettor = new ExperienceGettor(this);//設定經驗取得控制器
        EvoPointGettor = new EvolutionPointGettor(this);//設定進化點數取得控制器
        NameGettor = new MonsterNameGettor(this);//設定更名控制器
        SizeGettor = new MonsterSizeGettor(this);//怪獸大小控制器
        EvoGettor = new EvolutionGettor(this);//怪獸進化控制器
    }
    //初始設定怪獸
    public void StartSetMonster()//初始設定怪獸
    {
        SetCachedObj();//抓取物件
        GetPlayerMonsterTool();//取得各種控制器
        //SetAttribute(_propertyDic);//讀取怪獸資料
        StartSetMonsterProperty();//初始化怪獸起始數值
        StartSetMonsterResourceQuality();//起始設定怪獸生產資源量
        StartSetMonsterSkill();//讀入怪物擁有技能
        EvoGettor.GetEvoPhase();//設定怪獸階級
        StartSetEvent();//事件起始設定
        StartSetDailyEvent();//日常事件起始設置
        StartSetTouch();//觸摸怪獸事件
        SetSize(Size);//依怪物體型設定圖像縮放大小及碰撞範圍
        CharaStatus.MonsterID = MonsterID;
        CharaStatus.StartSetRoomCharaStatus();//起始設至怪獸狀態
        SetSprite();//依怪物種類設定物件圖像
        SetSpriteSortingOrder();//設定怪獸貼圖層級
        SetMonsterHappyInRoom(Player.RoomDic[InRoomID]);//設定此怪獸所在的房間及相關資訊
        SpeciesName = GameDictionary.SpeciesNameDic[SpeciesKey];//設定怪物物種名稱
        IsStartSet = true;
    }
    /// <summary>
    /// 讀取怪獸資料
    /// </summary>
    public void SetAttribute(Dictionary<string, string> _propertyDic)//讀取怪獸資料
    {
        MonsterID = int.Parse(_propertyDic["MonsterID"]);
        MonsterName = _propertyDic["Name"];
        Species = int.Parse(_propertyDic["Species"]);
        SpeciesLevel = int.Parse(_propertyDic["SpeciesLevel"]);
        SpeciesKey = string.Format("{0}-{1}", Species, SpeciesLevel);
        Rare = int.Parse(_propertyDic["Rare"]);
        Lv = int.Parse(_propertyDic["LV"]);
        CurExp = int.Parse(_propertyDic["EXP"]);
        NeedExp = GameDictionary.MonsterExperienceDic[Lv][RareToString(Rare)];
        LvHealthPlus = int.Parse(_propertyDic["LvHealth"]);
        Wild = int.Parse(_propertyDic["Wild"]);
        Mutation = int.Parse(_propertyDic["Mutation"]);
        Natural = int.Parse(_propertyDic["Natural"]);
        LvBrutalPlus = int.Parse(_propertyDic["LvBrutal"]);
        LvAggressivePlus = int.Parse(_propertyDic["LvAggressive"]);
        LvResistancePlus = int.Parse(_propertyDic["LvResistance"]);
        MainElement = int.Parse(_propertyDic["MainElement"]);
        Personality = int.Parse(_propertyDic["Personality"]);
        SelectedSkillID = int.Parse(_propertyDic["SelectedSkill"]);
        Level2Species = int.Parse(_propertyDic["LV2Species"]);
        Level3Species = int.Parse(_propertyDic["LV3Species"]);
        Player.AddMonsterToDictionary(this);//將此怪物加入玩者擁有怪物字典中
        //放置怪物到房間並設定所在房間的資訊
        InRoomID = int.Parse(_propertyDic["InRoomID"]);
        StartSetHappy(int.Parse(_propertyDic["Happy"]));//設定快樂度   
        StartSetMonsterToRoom();//起始放置怪物到房間
    }

    void StartSetMonsterToRoom()//起始放置怪物到房間
    {
        try
        {
            if (InRoomID == 0)//如果InRoomID為0，代表初始得到此怪獸
            {
                List<int> dicBuffer = new List<int>(Player.RoomDic.Keys);
                foreach (int key in dicBuffer)
                {
                    if (Player.RoomDic[key].OwnMonsterDic.Count < Player.RoomDic[key].RoomCapacity)//如果房間還沒客滿
                    {
                        Player.RoomDic[key].AddMonster(MonsterID);//加入到新房間
                        InRoomID = Player.RoomDic[key].RoomID;//設定此怪獸所在的房間ID
                    }
                }
            }
            else
            {
                if (Player.RoomDic.ContainsKey(InRoomID))//如果怪獸所在的房間存在
                {
                    if (Player.RoomDic[InRoomID].OwnMonsterDic.Count < Player.RoomDic[InRoomID].RoomCapacity)//如果房間還沒客滿
                    {
                        Player.RoomDic[InRoomID].AddMonster(MonsterID);//加入到新房間
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning(string.Format("起始放置怪物{0}到房間時發生錯誤", MonsterID));
            Debug.Log(ex);
        }
    }
    public void SetCatchSortingOrder()//設定怪受貼圖層級為正在抓取中
    {
        //怪獸圖像層級
        MonsterSprite.sortingOrder = 1000;
    }
    public void SetNotCatchSortingOrder()//設定怪獸貼圖顏色標示為不在搬移
    {
        MonsterSprite.color = new Color32(255, 255, 255, 255);
    }
    public void SetMonsterSpriteToCanMoveColor()//設定怪獸貼圖顏色標示為可搬移
    {
        MonsterSprite.color = new Color32(255, 255, 255, 140);
    }
    public void SetMonsterSpriteToCanNotMoveColor()//設定怪獸貼圖顏色標示為不可搬移
    {
        MonsterSprite.color = new Color32(255, 60, 60, 140);
    }

    public void SetSpriteSortingOrder()//設定怪獸貼圖層級
    {
        //怪獸圖像層級
        MonsterSprite.sortingOrder = 10 + MyRoom.Floor * 50;
        //泡泡事件層級
        EventBubbleSprite.sortingOrder = 35 + MyRoom.Floor * 50;
        EventIconSprite.sortingOrder = 36 + MyRoom.Floor * 50;
        //事件回饋ICON層級
        RewardIcon1.sortingOrder = 37 + MyRoom.Floor * 50;
        RewardIcon2.sortingOrder = 38 + MyRoom.Floor * 50;
        RewardIcon3.sortingOrder = 39 + MyRoom.Floor * 50;
        //對話文字層級
        for (int i = 0; i < MonsterTextMesh.Length; i++)
        {
            MonsterTextMesh[i].sortingLayerName = "Room";
            MonsterTextMesh[i].sortingOrder = 41 + MyRoom.Floor * 50;
        }
        TextBottom.sortingOrder = 40 + MyRoom.Floor * 50;
    }
    public void MoveMonsterToRoom(Room _moveToRoom, Vector2 _pos)//將怪物從某房間移到某房間
    {
        try
        {
            if (Player.RoomDic.ContainsKey(InRoomID))
            {
                if (Player.RoomDic[InRoomID].OwnMonsterDic.ContainsKey(MonsterID))
                {
                    if (_moveToRoom.OwnMonsterDic.Count < 3)//目標房間怪獸數量還沒達到上限
                    {
                        MyRoom.RemoveMonster(MonsterID);//從原本房間移除
                        MyRoom = _moveToRoom;
                        InRoomID = _moveToRoom.RoomID;
                        IODataFromArcalet.SetPlayerMonster("InRoomID", InRoomID.ToString(), MonsterID);//向Server寫入玩者怪獸
                        MyRoom.AddMonster(MonsterID);//加入到新房間
                        myTransform.parent = MyRoom.transform;
                        myTransform.position = _pos;
                        SetSpriteSortingOrder();//設定怪獸貼圖層級
                    }
                    else
                    {
                        Debug.LogWarning("目標房間已經客滿了");
                    }
                }
                else
                {
                    Debug.LogWarning("原本房間不存在此怪物");
                }
            }
            else
            {
                Debug.LogWarning("此怪物原本房間ID不存在房間字典中");
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }
    private void SetCachedObj()//抓取怪獸Component
    {
        myTransform = transform;//將transform暫存自快取中
        //怪物圖像物件
        MonsterSprite = myTransform.FindChild("status").FindChild("sprite").GetComponent<SpriteRenderer>();
        CharaStatus = myTransform.GetComponent<RoomCharaStatus>();//取得此怪獸的狀態
    }
    //設定sprite的方法
    public void SetSprite()
    {
        MonsterSprite.sprite = Resources.Load<Sprite>(string.Format("Sprite/MonsterSprite/{0}", SpeciesKey));
    }



    public void SetSize(float _size)//設定怪獸大小
    {
        SizeGettor.SetSize(_size);
    }

    public void SetName(string _value)//更改怪獸名稱
    {
        NameGettor.GetName(_value);
    }

    public void GetEvolutionPoint(int _type, int _value)//取得進化點數
    {
        EvoPointGettor.GetEvolutionPoint(_type, _value);
    }

    public void GetExp(int _value)//取得經驗
    {
        ExpGettor.GetExperience(_value);
    }
    public void CheckEvolution()//檢查進化，若符合條件就進化
    {
        EvolutionLoad.CheckEvolutionSchedule(EvoGettor);//進化條件檢查，若符合條件就進化
    }



    int IComparable.CompareTo(System.Object obj)//依Rank做排序
    {
        int result = 0;
        PlayerMonster spell = (PlayerMonster)obj;
        switch (SortType)
        {
            case 0:
                if (SmallToBig)
                    result = this.Rare - spell.Rare;
                else
                    result = spell.Rare - this.Rare;
                break;
            case 1:
                if (SmallToBig)
                    result = this.Lv - spell.Lv;
                else
                    result = spell.Lv - this.Lv;
                break;
            case 2:
                if (SmallToBig)
                    result = this.MainElement - spell.MainElement;
                else
                    result = spell.MainElement - this.MainElement;
                break;
            default:
                break;
        }
        return result;
    }
}
