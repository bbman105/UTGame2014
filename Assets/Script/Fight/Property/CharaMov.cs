using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour
{
    //chara's max move speed
    private float maxMovSpd;
    public float MaxMovSpd
    {
        get { return maxMovSpd; }
        set
        {
            if (value > 3.5f)
                maxMovSpd = 3.5f;
            else
                maxMovSpd = value;
        }
    }
    public const float MaxSpdBuff = 3;
    public const float MaxSpdDenominator = 35;
    public const float BaseMaxSpd = 0f;
    //chara's current move speed
    public float curMovSpd;
    //chara's force strength,the heigh acceleraction will make chara quick to get max move speed 
    private float acceleration;
    public float Acceleration
    {
        get { return acceleration; }
        set
        {
            if (value < 13)
            {
                acceleration = 13;
            }
            else if (value > 35)
                acceleration = 35;
            else
            {
                acceleration = value;
            }
        }
    }

    public bool canMov;
    public bool canForceMov;
    public bool displacement;
    sbyte MovDir { get; set; }//腳色行徑方向
    public sbyte ForceDir { get; set; }//腳色勢力方向，影響撞到敵方會往哪個方向飛
    public bool onGroundStopMov;
    private Rigidbody2D myRigidbody2D;

    public sbyte GetMovDirection()//取得腳色移動方向
    {
        return MovDir;
    }
    public void SetReverseMovDirection(bool _reverse)//設定怪物移動方向
    {
        if (myGameObject.tag == "PlayerMonster")
        {
            if (_reverse)
                MovDir = -1;
            else
                MovDir = 1;
        }
        else
        {
            if (_reverse)
                MovDir = 1;
            else
                MovDir = -1;
        }
    }

    public void MovSetup()
    {
        myRigidbody2D = rigidbody2D;
        canMov = false;
        canForceMov = true;
        displacement = true;
        onGroundStopMov = false;
        if (myGameObject.tag == "PlayerMonster")
        {
            MovDir = 1;
            ForceDir = 1;
        }
        else
        {
            MovDir = -1;
            ForceDir = -1;
        }
        MaxMovSpd = BaseMaxSpd + MaxSpdBuff * (Speed / MaxSpdDenominator);
        Acceleration = Speed;
    }


    private void MoveFunction()
    {
        if (canMov && !onGroundStopMov)
        {
            if (!IsStun && !IsFetter && !IsNail)//如果沒被暈眩、束縛、定身
            {
                curMovSpd = myRigidbody2D.velocity.x * MovDir;
                if (curMovSpd < MaxMovSpd)
                {
                    myRigidbody2D.AddForce(Vector2.right * Acceleration * MovDir);
                }
            }
        }
    }


}
