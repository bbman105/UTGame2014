using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour
{
    //死亡碰撞牆壁
    private bool dyingKnockWall;
    private float dyingKnockWallCD;
    private float dyingKnockWallTimer;
    //Die Property
    private bool dying;

    private float rotDir;
    private float rotForce;
    private float rotSpeed;
    private float flyUpForce;
    private float flyRightForce;
    private float flyUpDir;
    private float flyRightDir;
    private int collideTimes;
    private int fallLayer;
    private float fallScale;
    private float decayVelocity;
    private Transform charaTransform;
    public void DieSet()
    {
        //死亡參數欄位設定
        dying = false;
        dyingKnockWall = false;
        dyingKnockWallCD = 0.1f;
        dyingKnockWallTimer = 0f;
        flyUpDir = 1;
        if (myGameObject.tag == "NPCMonster")
        {
            flyRightDir = 1;
        }
        else { flyRightDir = -1; }
        flyUpForce = 0;
        flyRightForce = 600;
        rotDir = 1;
        rotForce = 1000;
        rotSpeed = 0;
        collideTimes = 0;
        fallScale = 0;
        decayVelocity = 0;
        charaTransform = myTransform.FindChild("Chara");
    }
    void DisplayTypeIcon()//取消顯示怪物種類ICON
    {
        myTransform.FindChild("NpcType").gameObject.SetActive(false);//取消顯示怪物種類ICON
    }

    void OnDyingClliding()//死亡撞牆方法
    {
        if (!dyingKnockWall)
        {
            flyRightDir *= -1;
            rigidbody2D.AddForce(Vector2.right * flyRightForce * flyRightDir);
            collideTimes++;
            DisplayTypeIcon();//取消顯示怪物種類ICON
            //設定自身不可撞人
            dyingKnockWall = true;
            //設定自身不可撞人的時間
            dyingKnockWallTimer = dyingKnockWallCD;
        }
    }
    private void DyingKnockWallTimer()//撞擊牆壁CD倒數
    {
        if (dyingKnockWallTimer > 0)
        {
            dyingKnockWallTimer -= Time.deltaTime;
            if (dyingKnockWallTimer <= 0)
            {
                dyingKnockWall = false;
            }
        }
    }

    void SetCharaToFightSceneBG()
    {
        myTransform.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "FightSceneBG";
        int rnd = Random.Range(1, 3);
        if (rnd == 1)
        {
            fallScale = 0.4f;
            fallLayer = 59;
        }
        else
        {
            fallScale = 0.2f;
            fallLayer = 54;
        }
        myTransform.GetComponentInChildren<SpriteRenderer>().sortingOrder = fallLayer;
        Invoke("SetCharaInActive", 1f);
    }
    void SetCharaInActive()
    {
        myGameObject.SetActive(false);
    }
    void SetDecayVelocity()
    {
        int rnd = Random.Range(2, 5);
        decayVelocity = rnd * 8;
    }
    void SetFlyUpForce()
    {
        int rnd = Random.Range(1, 4);
        flyUpForce = 550 + rnd * 80;
    }
    void DieFunction()
    {
        if (!dying)
        {
            myGameObject.layer = LayerMask.NameToLayer("DyingChara");
            CanBeKnock = false;
            canMov = false;
            canForceMov = false;
            twinkleEffect.enabled = false;
            swingEffect.enabled = false;
            shockEffect.enabled = false;

            SetFlyUpForce();
            SetDecayVelocity();
            rigidbody2D.AddForce(Vector2.right * flyRightForce * flyRightDir);
            rigidbody2D.AddForce(Vector2.up * flyUpForce * flyUpDir);
            Invoke("SetCharaToFightSceneBG", 0.3f);
            dying = true;
        }
        InDieAction();
    }
    void InDieAction()
    {
        if (myTransform.FindChild("Chara").localScale.x > fallScale) myTransform.FindChild("Chara").localScale -= new Vector3(Time.deltaTime, Time.deltaTime, 0);
        rotSpeed = rotForce * Time.deltaTime * rotDir;
        charaTransform.eulerAngles = new Vector3(0, 0, charaTransform.eulerAngles.z + rotSpeed);
        if (collideTimes > 1)
        {
            if (flyRightDir > 0)
            {
                if (rigidbody2D.velocity.x > 0) rigidbody2D.velocity -= new Vector2(decayVelocity * Time.deltaTime, 0);
            }
            else
            {
                if (rigidbody2D.velocity.x < 0) rigidbody2D.velocity += new Vector2(decayVelocity * Time.deltaTime, 0);
            }
        }
    }
}
