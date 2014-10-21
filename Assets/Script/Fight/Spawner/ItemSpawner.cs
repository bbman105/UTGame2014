using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    static int ItemObjID;
    static GameObject ItemListObj;
    static Quaternion rot;
    static Vector3 pos;
    static GameObject GoldRescourceObj;
    static GameObject CurItemObj;
    static ItemTake curItemTake;
    public static Dictionary<int, GameObject> ItemObjDic;
    public static Dictionary<int, ItemTake> ItemTakeDic;
    //金幣數量限制
    static int SpawnNum;//產生物品數量
    static int ComboHitNum;//連擊數
    static int maxGoldNum;//最高金幣數
    static Status targetStatus;
    void Awake()
    {
        maxGoldNum = 7;//最高金幣數設定
        ItemObjDic = new Dictionary<int, GameObject>();
        ItemTakeDic = new Dictionary<int, ItemTake>();
        ItemListObj = gameObject;
        rot = Quaternion.Euler(0f, 0f, 0f);
        pos = Vector3.zero;
        GoldRescourceObj = Resources.Load<GameObject>("GameObject/FightScene/FightCom/Gold");
        for (int i = 0; i < 10; i++)//起始創造10個物件
        {
            SpawnNewItem();//產生新的金幣物件
            CurItemObj.gameObject.SetActive(false);
        }
    }
    static void SetGoldNum()//設定金幣數量
    {
        ComboHitNum = ForceCondition.ForceStatusDic["PlayerMonster"].ComboHit;
        if (ComboHitNum > 2)
        {
            SpawnNum = ComboHitNum - 2;
            if (SpawnNum > maxGoldNum)//金幣數量超過最大金幣數量，則設為最大金幣數量
                SpawnNum = maxGoldNum;
        }
        else
        {
            SpawnNum = 0;
        }
    }
    static void SetItemNum(byte _itemType)//設定要創造幾個物品
    {
        //掉落種類
        switch (_itemType)
        {
            case 0://金幣
                SetGoldNum();//設定金幣數量
                break;
            case 1://物品
                int ProbabilityRnd = UnityEngine.Random.Range(1, 101);
                if (ProbabilityRnd <= targetStatus.DropPercent * 100)//如果通過機率檢定，則觸發此狀態效果
                {
                    SpawnNum = 1;//設定掉落物品量
                }
                break;
        }
    }
    public static void SpawnItem(Status _target, byte _itemType)//目標物件地點
    {
        targetStatus = _target;
        List<int> TransformBuffer = new List<int>(ItemTakeDic.Keys);
        pos = _target.transform.position;
        SpawnNum = 0;
        SetItemNum(_itemType);//設定要創造幾個物品
        for (int i = 0; i < SpawnNum; i++)
        {
            bool areadySpawn = false;
            foreach (int key in TransformBuffer)
            {
                if (!ItemTakeDic[key].InUsing)
                {
                    CurItemObj = ItemObjDic[key];
                    CurItemObj.transform.position = pos;
                    CurItemObj.gameObject.SetActive(true);//開啟物件功能
                    curItemTake = ItemTakeDic[key];
                    curItemTake.SetReward(_target, _itemType);
                    areadySpawn = true;
                    break;
                }
            }
            if (!areadySpawn)
            {
                SpawnNewItem();//產生新的物件
                curItemTake.SetReward(_target, _itemType);
            }
        }
    }
    static void SpawnNewItem()//產生新的金幣物件
    {
        CurItemObj = GameObject.Instantiate(GoldRescourceObj, pos, rot) as GameObject;
        curItemTake = CurItemObj.transform.FindChild("trigger").GetComponent<ItemTake>();
        curItemTake.ItemObjID = ItemObjID;
        CurItemObj.transform.parent = ItemListObj.transform;//設定為子物件
        CurItemObj.transform.position = pos;//設定座標為起始座標
        ItemObjDic.Add(ItemObjID, CurItemObj);//加入黃金物件字典清單
        ItemTakeDic.Add(ItemObjID, curItemTake);
        ItemObjID++;//字典中的黃金物件+1
    }
    /*
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Vector3 wp = myCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            if (Physics2D.OverlapPoint(wp) != null)
            {
                GameObject CurCtrlTarget = Physics2D.OverlapPoint(wp).gameObject;
                if (CurCtrlTarget.tag == "FighSceneGold")
                {
                    GoldForce goldForce = CurCtrlTarget.GetComponent<GoldForce>();
                    goldForce.TakeGold();
                }
            }
        }
    }
    */
}
