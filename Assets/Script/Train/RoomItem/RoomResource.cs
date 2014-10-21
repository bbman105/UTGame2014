using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomResource : MonoBehaviour
{

    public int ResourceID;//此資源物件ID
    public byte ResourceType { get; set; }//資源種類
    public byte EventType { get; set; }//事件種類
    public int ResourceNum { get; set; }//資源數
    public Vector3 Pos;
    public Vector3 Scale;
    float ForceX;//水平力道
    float ForceY;//垂直力道
    sbyte FlyDir;//物件飛行方向1=右邊,-1=左邊
    Vector2 Force;//給予的最終力道
    public bool InUsing;//物件正在場上使用中，不可當作新的物件使用
    public bool IsTaked;//已被拿取，不可在觸發拿取
    CircleCollider2D circleCollider;//此物件觸發器
    BoxCollider2D boxCollider;//此物件與牆壁跟地板的碰撞器
    Rigidbody2D rigid;
    Transform myTransform;//此資源的Transform
    Transform roomTransform;//此資源的母物件Transform;
    Animator itemAni;//撿取時的物件動畫
    Animator resourceAni;//撿取時取得的資源動畫
    ResourceAniCtrl resourceCtrl;//動畫控制器
    string ItemAniString;//物件動畫trigger文字
    string TakeIteamAniString;//拿取物件動畫trigger文字
    string ResourceAniString;//資源動畫trigger文字
    public int InRoomID { get; set; }//所在哪個房間ID

    void Awake()
    {
        myTransform = transform;
        roomTransform = myTransform.parent.transform;
        rigid = myTransform.parent.rigidbody2D;
        FlyDir = 1;
        itemAni = myTransform.parent.FindChild("item").GetComponent<Animator>();
        resourceAni = myTransform.parent.FindChild("resource").GetComponent<Animator>();
        resourceCtrl = myTransform.parent.FindChild("item").GetComponent<ResourceAniCtrl>();
        circleCollider = myTransform.GetComponent<CircleCollider2D>();
        boxCollider = myTransform.parent.GetComponent<BoxCollider2D>();
    }
    void AddForce()//加入力道
    {
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                FlyDir = 1;
                break;
            case 1:
                FlyDir = -1;
                break;
        }
        ForceX = Random.Range(0, 150 * FlyDir);
        ForceY = Random.Range(250, 250);
        Force = new Vector2(ForceX, ForceY);//設定給予的力道
        rigid.AddForce(Force);//施力
    }

    public void SetResource(Monster _monster, int _resourceID, byte _resourceType, int _resourceNum, byte _eventType, int _roomID,
        Vector3 _pos, Vector3 _scale)//設定資源
    {
        ResourceID = _resourceID;
        ResourceType = _resourceType;
        ResourceNum = _resourceNum;
        EventType = _eventType;
        Pos = _pos;
        Scale = _scale;
        roomTransform.localPosition = Pos;
        roomTransform.localScale = _scale;
        InRoomID = _roomID;
        SetAni();//設定物件動畫
        boxCollider.enabled = true;//設定物件可以被觸發
        circleCollider.enabled = true;//設定物件可以被碰牆壁碰撞

        InUsing = true;//設為金幣正在場上
        IsTaked = false;
        AddForce();
    }
    void SetAni()//設定物件動畫
    {
        switch (EventType)
        {
            case 0://大便
                ItemAniString = "ShitDefault";
                resourceCtrl.AniStr = "ShitDefault";
                TakeIteamAniString = "TakeShit";
                rigid.isKinematic = false;
                break;
            case 1://生病
                ItemAniString = "VirusDefault";
                resourceCtrl.AniStr = "VirusDefault";
                TakeIteamAniString = "TakeVirus";
                rigid.isKinematic = true;
                break;
            case 2://鬧鬼
                ItemAniString = "GhostDefault";
                resourceCtrl.AniStr = "GhostDefault";
                TakeIteamAniString = "TakeGhost";
                rigid.isKinematic = true;
                break;
            default:
                Debug.Log("事件種類無法辨識");
                break;
        }
        itemAni.SetTrigger(ItemAniString);//播放動畫
    }
    void AddTakeForce()
    {
        //Force = new Vector2(0, 350);//設定給予的力道
        //rigid.AddForce(Force);//施力
    }

    public void TakeResource()//拿取資源
    {
        if (!IsTaked && InUsing)
        {
            rigid.isKinematic = true;
            boxCollider.enabled = false;//設定物件不可以被觸發
            circleCollider.enabled = false;//設定物件不可以被碰牆壁碰撞
            IsTaked = true;
            Player.OwnResource.AddResource(ResourceType, ResourceNum);//增加資源
            Player.RoomDic[InRoomID].RemoveRoomOwnResource(ResourceID);//移除該房間的資源物件
            AddTakeForce();
            itemAni.SetTrigger(TakeIteamAniString);//播放動畫
            resourceAni.SetTrigger("GetResource");//播放動畫
        }

    }
}
