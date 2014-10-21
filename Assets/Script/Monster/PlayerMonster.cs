using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster, IMonsterSize, IComparable
{

    /////////////////////////怪物屬性////////////////////////////


    //擁有此怪物玩家的編號
    private int ownPlayerID;
    public int OwnPlayerID
    {
        get { return ownPlayerID; }
        set
        {
            if (value < 0)
            {
                ownPlayerID = 0;
            }
            else { ownPlayerID = value; }
        }
    }

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
    //怪獸資料
    TextAsset PlayerMonsterXml { get; set; }
    XmlNode MIDXml;
    Transform myTransform;

    void Update()
    {
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

    /// <summary>
    /// 從XML讀取怪獸資料
    /// </summary>
    void LoadMonsterAttributeFromXml()//從XML讀取怪獸資料
    {
        MonsterID = int.Parse(MIDXml.SelectSingleNode("MonsterID").InnerText);
        OwnPlayerID = int.Parse(MIDXml.SelectSingleNode("OwnPlayerID").InnerText);
        MonsterName = MIDXml.SelectSingleNode("Name").InnerText;
        Species = int.Parse(MIDXml.SelectSingleNode("Species").InnerText);
        SpeciesLevel = int.Parse(MIDXml.SelectSingleNode("SpeciesLevel").InnerText);
        SpeciesKey = string.Format("{0}-{1}", Species, SpeciesLevel);
        Rare = int.Parse(MIDXml.SelectSingleNode("Rare").InnerText);
        Lv = int.Parse(MIDXml.SelectSingleNode("LV").InnerText);
        CurExp = int.Parse(MIDXml.SelectSingleNode("EXP").InnerText);
        NeedExp = GameDictionary.MonsterExperienceDic[Lv][RareToString(Rare)];
        LvHealthPlus = int.Parse(MIDXml.SelectSingleNode("Health").InnerText);
        Wild = int.Parse(MIDXml.SelectSingleNode("Wild").InnerText);
        Mutation = int.Parse(MIDXml.SelectSingleNode("Mutation").InnerText);
        Natural = int.Parse(MIDXml.SelectSingleNode("Natural").InnerText);
        Player.AddMonsterToDictionary(this);//將此怪物加入玩者擁有怪物字典中
        LvBrutalPlus = int.Parse(MIDXml.SelectSingleNode("Brutal").InnerText);
        LvAggressivePlus = int.Parse(MIDXml.SelectSingleNode("Aggressive").InnerText);
        LvResistancePlus = int.Parse(MIDXml.SelectSingleNode("Resistance").InnerText);
        MainElement = int.Parse(MIDXml.SelectSingleNode("MainElement").InnerText);
        Personality = int.Parse(MIDXml.SelectSingleNode("Personality").InnerText);
        SelectedSkillID = int.Parse(MIDXml.SelectSingleNode("SelectedSkill").InnerText);
        Level2Species = int.Parse(MIDXml.SelectSingleNode("Level2Species").InnerText);
        Level3Species = int.Parse(MIDXml.SelectSingleNode("Level3Species").InnerText);
        SpeciesName = GameDictionary.SpeciesNameDic[SpeciesKey];
        //放置怪物到房間並設定所在房間的資訊
        InRoomID = int.Parse(MIDXml.SelectSingleNode("InRoomID").InnerText);
        StartSetMonsterToRoom();//起始放置怪物到房間
        SetMonsterHappyInRoom(Player.RoomDic[InRoomID]);//設定此怪獸所在的房間及相關資訊
        StartSetHappy(int.Parse(MIDXml.SelectSingleNode("Happy").InnerText));//設定快樂度     
    }
    public void StartSetMonsterAttribute(TextAsset XMLFile, int MID)
    {
        try
        {
            GetPlayerMonsterTool();//取得各種控制器
            //讀取XML文件
            XmlDocument doc = new XmlDocument();
            PlayerMonsterXml = XMLFile;
            doc.LoadXml(PlayerMonsterXml.text);
            //讀取xml文件中欲抓取的MonsterID標籤
            MIDXml = doc.SelectSingleNode(string.Format("root/MID{0}", MID));
            LoadMonsterAttributeFromXml();//從XML讀取怪獸資料
        }
        catch (Exception ex)
        {
            Debug.Log("讀取玩家怪物數值文件時發生錯誤，文件名稱PlayerMonster.xml");
            throw ex;
        }
        StartSetMonsterProperty();//初始化怪獸起始數值
        StartSetMonsterResourceQuality();//起始設定怪獸生產資源量
        SetCachedObj();//抓取物件
        StartSetMonsterSkill();//讀入怪物擁有技能
        EvoGettor.GetEvoPhase();//設定怪獸階級
        StartSetEvent();//事件起始設定
        StartSetDailyEvent();//日常事件起始設置
        StartSetTouch();//觸摸怪獸事件
        SetSize(Size);//依怪物體型設定圖像縮放大小及碰撞範圍
        CharaStatus = myTransform.GetComponent<RoomCharaStatus>();//取得此怪獸的狀態
        CharaStatus.StartSetRoomCharaStatus();//起始設至怪獸狀態
        SetSprite();//依怪物種類設定物件圖像
        SetSpriteSortingOrder();//設定怪獸貼圖層級
    }
    void StartSetMonsterToRoom()//起始放置怪物到房間
    {
        try
        {
            Player.RoomDic[InRoomID].AddMonster(MonsterID);//加入到新房間
        }
        catch (Exception ex)
        {
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
                        MyRoom.AddMonster(MonsterID);//加入到新房間
                        myTransform.parent = MyRoom.transform;
                        myTransform.position = _pos;
                        SetSpriteSortingOrder();//設定怪獸貼圖層級
                    }
                    else
                    {
                        Debug.Log("目標房間已經客滿了");
                    }
                }
                else
                {
                    Debug.Log("原本房間不存在此怪物");
                }
            }
            else
            {
                Debug.Log("此怪物原本房間ID不存在房間字典中");
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
