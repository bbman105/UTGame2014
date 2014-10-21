using UnityEngine;
using System.Collections;

public class ItemTake : MonoBehaviour
{
    /// <summary>
    /// 此物件ID
    /// </summary>
    public int ItemObjID { get; set; }//此物件ID
    /// <summary>
    /// 取得的物品ID
    /// </summary>
    public int ItemID { get; set; }
    /// <summary>
    /// 是否正在場上使用中
    /// </summary>
    public bool InUsing { get; set; }//是否正在場上使用中
    /// <summary>
    /// 是否已被拿取
    /// </summary>
    public bool IsTaked { get; set; }//是否已被拿取

    Rigidbody2D rigid;
    Transform myTransform;
    Animator goldAni;
    SpriteRenderer itemSprite;//掉落物貼圖
    float ForceX;//水平力道
    float ForceY;//垂直力道
    sbyte FlyDir;//物件飛行方向1=右邊,-1=左邊
    Vector2 Force;//給予的最終力道
    int Gold;//金錢
    BoxCollider2D boxCollider;//此物件的觸發拿取金錢範圍
    CircleCollider2D circleCollider;//此物件與牆壁跟地板的碰撞範圍
    Status targetStatus;//要掉落的怪獸
    byte RewardType { get; set; }//掉落種類0=金幣,1=物品


    void Awake()
    {
        myTransform = transform;
        rigid = myTransform.parent.rigidbody2D;
        FlyDir = 1;
        goldAni = myTransform.parent.FindChild("sprite").GetComponent<Animator>();
        itemSprite = goldAni.GetComponent<SpriteRenderer>();
        boxCollider = myTransform.GetComponent<BoxCollider2D>();
        circleCollider = myTransform.parent.GetComponent<CircleCollider2D>();
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
        ForceX = Random.Range(0, 300 * FlyDir);
        ForceY = Random.Range(400, 600);
        Force = new Vector2(ForceX, ForceY);//設定給予的力道
        rigid.AddForce(Force);//施力
    }
    void ResetReward()//重設物品價值
    {
        Gold = 0;
        ItemID = 0;
    }
    public void SetReward(Status _status, byte _rewardType)
    {
        targetStatus = _status;
        RewardType = _rewardType;
        switch (_rewardType)
        {
            case 0://金幣
                Gold = targetStatus.Gold;
                itemSprite.sprite = Resources.Load<Sprite>(string.Format("Sprite/Item/item{0}", 0));
                break;
            case 1://物品
                ItemID = targetStatus.LootID;
                itemSprite.sprite = Resources.Load<Sprite>(string.Format("Sprite/Item/item{0}", targetStatus.LootID));
                break;
            default:
                Debug.LogError("無法識別掉落種類");
                break;
        }
        SetActive();//設定可被剪取
    }
    public void SetActive()//設定可被剪取
    {
        boxCollider.enabled = true;//設定物件可以被觸發
        circleCollider.enabled = true;//設定物件可以被碰牆壁碰撞
        InUsing = true;//設為金幣正在場上
        IsTaked = false;
        AddForce();
    }
    void AddTakeForce()
    {
        Force = new Vector2(0, 350);//設定給予的力道
        rigid.AddForce(Force);//施力
    }
    /// <summary>
    /// 拿取獎勵
    /// </summary>
    void TakeReward()//拿取獎勵
    {
        if (!IsTaked && InUsing)
        {
            switch (RewardType)
            {
                case 0://金幣
                    ForceCondition.HitGold += Gold;
                    break;
                case 1://物品
                    if (ForceCondition.KillLootDic.ContainsKey(targetStatus.LootID))
                        ForceCondition.KillLootDic[targetStatus.LootID]++;
                    else
                        ForceCondition.KillLootDic.Add(targetStatus.LootID, 1);
                    break;
                default:
                    Debug.LogError("無法識別掉落種類");
                    break;
            }
            boxCollider.enabled = false;//設定物件不可以被觸發
            circleCollider.enabled = false;//設定物件不可以被牆壁碰撞
            IsTaked = true;
            AddTakeForce();
            goldAni.SetTrigger("GetGold");//播放動畫
        }
    }
    
    void OnTriggerEnter2D(Collider2D _targetCollider)
    {
        GameObject targetObj = _targetCollider.gameObject;
        if (targetObj.tag == "PlayerMonster")
        {
            TakeReward();//拿取獎勵
        }
    }
    
    /*
    void OnMousePress()
    {
        if (!IsTaked && InUsing)
        {
            for (int i = 0; i < circleColliders.Length;i++ )
            {
                circleColliders[i].enabled = false;
            }
            IsTaked = true;
            ForceCondition.KillGold += Gold;
            AddTakeForce();
            goldAni.SetTrigger("GetGold");//播放動畫
        }
    }
    
    public void TakeGold()//拿取金幣
    {
        if (!IsTaked && InUsing)
        {
            for (int i = 0; i < circleColliders.Length; i++)
            {
                circleColliders[i].enabled = false;
            }
            IsTaked = true;
            ForceCondition.KillGold += Gold;
            AddTakeForce();
            goldAni.SetTrigger("GetGold");//播放動畫
        }
    }
    */
}
