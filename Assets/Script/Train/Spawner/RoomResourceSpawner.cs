using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomResourceSpawner : MonoBehaviour
{
    static byte eventType;
    public static byte EventType//事件種類0=大便,1=肚子餓,2=感冒,3=鬧鬼
    {
        get { return RoomResourceSpawner.eventType; }
        set { RoomResourceSpawner.eventType = value; }
    }
    static byte resourceType;
    static public byte ResourceType//獎勵類型 0=金幣 1=能量 2=鑽石
    {
        get { return resourceType; }
        set { resourceType = value; }
    }
    static int resourceNum;
    static public int ResourceNum//獎勵數量
    {
        get { return resourceNum; }
        set { resourceNum = value; }
    }
    static int ResourceID;//資源物件ID，用來辨認是場上哪個物件
    static Transform roomTransform;//目標怪獸的房間
    static PlayerMonster targetMonster;
    static Quaternion rot;
    static Vector3 pos;
    static Vector3 scale;
    static GameObject ResourceObjResource;//資源物件的位置
    static GameObject CurResourceObj;//目前的資源物件
    static RoomResource curRoomResource;
    static Sprite[] EventSprite;//事件貼圖
    static SpriteRenderer curItemSprite;//目前物件的貼圖
    static SpriteRenderer curResourceSprite;//目前資源物件的貼圖
    public static Dictionary<int, GameObject> ResourceObjDic = new Dictionary<int, GameObject>();
    public static Dictionary<int, RoomResource> RoomResourceDic = new Dictionary<int, RoomResource>();
    public static Dictionary<int, SpriteRenderer> ItemSpriteDic = new Dictionary<int, SpriteRenderer>();
    public static Dictionary<int, SpriteRenderer> ResourceSpriteDic = new Dictionary<int, SpriteRenderer>();


    void Awake()
    {
        DontDestroyOnLoad(gameObject);//切場景時不清除物件
    }
    public static void StartSetRoomResourceSpawner()//起始設定房間資源物件產生器
    {
        ResourceObjResource = Resources.Load<GameObject>("GameObject/RoomCom/ResourceItem");//位置物件
        EventSprite = new Sprite[4];//位置物件
        EventSprite[0] = Resources.Load<Sprite>("Sprite/ResourceItem/shit");
        EventSprite[1] = Resources.Load<Sprite>("Sprite/ResourceItem/meat");
        EventSprite[2] = Resources.Load<Sprite>("Sprite/ResourceItem/virus");
        EventSprite[3] = Resources.Load<Sprite>("Sprite/ResourceItem/ghost");
        rot = Quaternion.Euler(0f, 0f, 0f);
        pos = Vector3.zero;
    }
    static void SpawnNewResource()//產生新的資源物件
    {
        ResourceID++;//字典中的資源編號+1
        CurResourceObj = GameObject.Instantiate(ResourceObjResource, pos, rot) as GameObject;
        curRoomResource = CurResourceObj.transform.FindChild("trigger").GetComponent<RoomResource>();
        curItemSprite = CurResourceObj.transform.FindChild("item").GetComponent<SpriteRenderer>();
        curResourceSprite = CurResourceObj.transform.FindChild("resource").GetComponent<SpriteRenderer>();
        CurResourceObj.transform.parent = roomTransform;//放置到目標怪獸房間底下
        Player.RoomDic[targetMonster.InRoomID].AddRoomOwnResource(targetMonster, ResourceID, EventType, ResourceNum, pos, scale);//將該房間加入資源物件
        curRoomResource.SetResource(targetMonster, ResourceID, ResourceType, ResourceNum, EventType, targetMonster.InRoomID, pos, scale);//設定資源
        ResourceObjDic.Add(ResourceID, CurResourceObj);//加入資源物件字典清單
        RoomResourceDic.Add(ResourceID, curRoomResource);
        ItemSpriteDic.Add(ResourceID, curItemSprite);
        ResourceSpriteDic.Add(ResourceID, curResourceSprite);
    }
    static void SetSpriteSorting()//設定貼圖層級
    {
        curItemSprite.sortingOrder = Player.RoomDic[targetMonster.InRoomID].Floor * 50 + 30;//設定貼圖層級
        curResourceSprite.sortingOrder = Player.RoomDic[targetMonster.InRoomID].Floor * 50 + 31;//設定貼圖層級
        //Player.RoomDic[targetMonster.InRoomID].AddRoomOwnResource(targetMonster, curRoomResource.ResourceID, EventType, ResourceNum, pos, scale);//將該房間加入資源物件
        //curRoomResource.SetResource(targetMonster, curRoomResource.ResourceID, ResourceType, ResourceNum, EventType, targetMonster.InRoomID, pos, scale);//設定資源
    }
    static void SetResourceObjTransform()//設定資源物件大小及位置
    {
        switch (EventType)
        {
            case 0://大便
                pos = targetMonster.transform.localPosition;
                scale = new Vector3(targetMonster.Size / 1.5f, targetMonster.Size / 1.5f, 1);//設定物件大小
                break;
            case 1://生病
                float rndVirusPosX = (float)(Random.Range(-25f, 25f) / 10);
                float rndVirusPosY = (float)(Random.Range(-15f, 10f) / 10);
                pos = new Vector3(rndVirusPosX, rndVirusPosY, 0);
                scale = new Vector3(0.4f, 0.4f, 0.4f);
                break;
            case 2://鬧鬼
                float rndGhostPosX = (float)(Random.Range(-15f, 15f) / 10);
                float rndGhostPosY = (float)(Random.Range(-15f, 10f) / 10);
                pos = new Vector3(rndGhostPosX, rndGhostPosY, 0);
                scale = new Vector3(0.7f, 0.7f, 0.7f);
                break;
            default:
                Debug.Log("事件種類無法辨識");
                break;
        }
    }
    public static void SpawnResource(PlayerMonster _target, byte _eventType, int _resourceNum)//怪獸產出資源物件
    {
        bool aleadySpawnResource = false;
        targetMonster = _target;
        EventType = _eventType;
        ResourceNum = _resourceNum;
        roomTransform = targetMonster.transform.parent;

        SetResourceObjTransform();//設定資源物件大小
        List<int> TransformBuffer = new List<int>(RoomResourceDic.Keys);
        foreach (int key in TransformBuffer)
        {
            if (!RoomResourceDic[key].InUsing)
            {
                CurResourceObj = ResourceObjDic[key];
                CurResourceObj.transform.parent = roomTransform;//放置到目標怪獸房間底下
                CurResourceObj.gameObject.SetActive(true);//開啟功能
                curRoomResource = RoomResourceDic[key];
                curItemSprite = ItemSpriteDic[key];
                Player.RoomDic[targetMonster.InRoomID].AddRoomOwnResource(targetMonster, curRoomResource.ResourceID, EventType, ResourceNum, pos, scale);//將該房間加入資源物件
                curRoomResource.SetResource(targetMonster, curRoomResource.ResourceID, ResourceType, ResourceNum, EventType, targetMonster.InRoomID, pos, scale);//設定資源
                SetSpriteSorting();//傳入資源種類與數量及事件種類
                aleadySpawnResource = true;
                break;
            }
        }
        if (!aleadySpawnResource)
        {
            SpawnNewResource();//產生新的資源物件
            SetSpriteSorting();//傳入資源種類與數量及事件種類
        }
    }

    public static void SpawnResourceFromRoom(PlayerMonster _target, Room _room, byte _eventType, int _resourceNum, Vector3 _pos, Vector3 _scale)//起始依照房間資訊產生新的資源物件
    {
        targetMonster = _target;
        EventType = _eventType;
        ResourceNum = _resourceNum;
        pos = _pos;
        scale = _scale;
        roomTransform = _room.transform;
        //創造房間物件
        ResourceID++;//字典中的資源編號+1
        CurResourceObj = GameObject.Instantiate(ResourceObjResource, pos, rot) as GameObject;
        curRoomResource = CurResourceObj.transform.FindChild("trigger").GetComponent<RoomResource>();
        curItemSprite = CurResourceObj.transform.FindChild("item").GetComponent<SpriteRenderer>();
        curResourceSprite = CurResourceObj.transform.FindChild("resource").GetComponent<SpriteRenderer>();
        CurResourceObj.transform.parent = roomTransform;//放置到目標怪獸房間底下
        ResourceObjDic.Add(ResourceID, CurResourceObj);//加入資源物件字典清單
        RoomResourceDic.Add(ResourceID, curRoomResource);
        ItemSpriteDic.Add(ResourceID, curItemSprite);
        ResourceSpriteDic.Add(ResourceID, curResourceSprite);
        curItemSprite.sortingOrder = _room.Floor * 50 + 30;//設定貼圖層級
        curResourceSprite.sortingOrder = _room.Floor * 50 + 31;//設定貼圖層級
        _room.StartAddRoomOwnResource(targetMonster, ResourceID, EventType, ResourceNum, pos, scale);//將該房間加入資源物件
        curRoomResource.SetResource(targetMonster, ResourceID, ResourceType, ResourceNum, EventType, _room.RoomID, pos, scale);//設定資源
    }
    /// <summary>
    /// 遊戲離開一段時間回來時生成資源物件，傳入要生產資源物件次數
    /// </summary>
    public static void AllMonsterSpanw(int _spawnTimes)//遊戲離開一段時間回來時生成資源物件，傳入要生產資源物件次數
    {
        List<int> RoomDicBuffer = new List<int>(Player.RoomDic.Keys);
        foreach (int RoomKey in RoomDicBuffer)
        {
            List<int> OwnMonsterDicBuffer = new List<int>(Player.RoomDic[RoomKey].OwnMonsterDic.Keys);
            foreach (int MonsterKey in OwnMonsterDicBuffer)
            {
                for (int i = 0; i < _spawnTimes; i++)
                {
                    Player.RoomDic[RoomKey].OwnMonsterDic[MonsterKey].RndDailyEventTrigger();//產生日常事件
                }
            }
        }
    }
}
