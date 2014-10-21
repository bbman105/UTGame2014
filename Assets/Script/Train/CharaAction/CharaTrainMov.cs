using UnityEngine;
using System.Collections;

public partial class RoomCharaStatus : MonoBehaviour
{
    private float curMovSpd;
    public float CurMovSpd
    {
        get { return curMovSpd; }
        set { curMovSpd = value; }
    }
    private float maxMovSpd;
    public float MaxMovSpd
    {
        get { return maxMovSpd; }
        set { maxMovSpd = value; }
    }
    private float acceleration;
    public float Acceleration
    {
        get { return acceleration; }
        set { acceleration = value; }
    }
    private sbyte faceDir;
    public sbyte FaceDir
    {
        get { return faceDir; }
        set { faceDir = value; }
    }
    private float moveTime;
    public float MoveTime
    {
        get { return moveTime; }
        set { moveTime = value; }
    }
    private float moveTimer;
    private float stayTime;
    public float StayTime
    {
        get { return stayTime; }
        set { stayTime = value; }
    }
    private float stayTimer;
    private bool touchWall;
    private float touchWallTime;
    private float touchWallTimer;
    private bool startMove;
    public bool StartMove
    {
        get { return startMove; }
        set { startMove = value; }
    }
    public bool OnGroundStopMov { get; set; }
    public bool Jump { get; set; }
    public bool MoveJump { get; set; }


    private void MovSetup()
    {
        MoveJump = false;
        Jump = false;
        stayTimer = 1;
        StartMove = false;
        FaceDir = 1;
        MaxMovSpd = 30f;
        touchWall = false;
        touchWallTime = 1f;
        touchWallTimer = 0;
    }

    private void StayTimeFunction()
    {
        if (!StartMove)
        {
            stayTimer -= Time.deltaTime;
            if (stayTimer <= 0)
            {
                MoveTime = Random.Range(8, 30) / 10;
                moveTimer = MoveTime;
                StartMove = true;
                //turn around
                int rnd = Random.Range(1, 3);
                if (rnd == 1)
                {
                    FaceChange();
                }
            }
        }
    }

    public void FaceChange()
    {
        FaceDir *= -1;
        myTransform.localScale = new Vector3(myTransform.localScale.x * -1, myTransform.localScale.y, myTransform.localScale.z);
        //執行自身物件底下的所有IShowText 的UpdateTextDirection，校正文字方向
        for (int i = 0; i < myStatus.ShowTextList.Count; i++)
        {
            myStatus.ShowTextList[i].UpdateTextDirection();
        }
        myStatus.AdjustEventBubblePosition(FaceDir);
    }

    private void MoveTimeFunction()
    {
        if (StartMove)
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0)
            {
                StayTime = Random.Range(10, 45) / 10;
                stayTimer = StayTime;
                StartMove = false;
            }
            MoveFunction();
        }

    }

    private void MoveFunction()
    {
        if (StartMove && !OnGroundStopMov)
        {
            rigidbody2D.velocity = new Vector2(speed * FaceDir, rigidbody2D.velocity.y);
            CurMovSpd = rigidbody2D.velocity.x * FaceDir;
        }
    }



    void OnCollisionStay2D(Collision2D col)
    {
        if (!touchWall)
        {
            if (col.transform.tag == "Wall")
            {
                touchWallTimer = touchWallTime;
                touchWall = true;
                FaceChange();

            }
        }
    }
    void TouchTimerFunction()
    {
        if (touchWall)
        {
            if (touchWallTimer > 0)
            {
                touchWallTimer -= Time.deltaTime;
            }
            else
            {
                touchWallTimer = 0;
                touchWall = false;
            }
        }

    }



}

