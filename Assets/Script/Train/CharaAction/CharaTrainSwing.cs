using UnityEngine;
using System.Collections;

public class CharaTrainSwing : MonoBehaviour
{

    public float swingSpd;
    private float curAngle;
    public float maxAngle;
    public bool negativeAngle;

    private bool jump;
    public float jumpForce;
    private float curJumpForce;
    private float gravity;
    private bool stopSwing;
    private bool swing;
    public bool groundMove;
    private RoomCharaStatus status;
    Transform myTransform;

    // Update is called once per frame
    void Update()
    {


        if (!status.StartMove)
        {
            StopSwingFunction();
        }
        else if (!status.Jump)
        {
            if (!groundMove) status.OnGroundStopMov = false;
            swing = true;
            status.MoveJump = true;
        }

        if (swing) SwingFunction();
    }


    private void StopSwingFunction()
    {

        if (stopSwing)
        {
            if (curAngle < 0)
            {
                //stop swing
                swing = false;
                status.MoveJump = false;
                //reset swing
                curAngle = 0;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                jump = false;
                curJumpForce = jumpForce;
                myTransform.localPosition = Vector3.zero;
                stopSwing = false;
            }
        }
    }

    private void SwingFunction()
    {
        if (negativeAngle)
        {
            if (curAngle <= maxAngle && curAngle >= -maxAngle)
            {
                curAngle += Time.deltaTime * swingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                if (!status.StartMove && !stopSwing)
                {
                    if (curAngle > 0)
                    {
                        stopSwing = true;
                    }
                }
            }
            else if (curAngle > maxAngle)
            {
                swingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                jump = false;
                curJumpForce = jumpForce;
                myTransform.localPosition = Vector3.zero;
                if (groundMove) status.OnGroundStopMov = true;

            }
            else if (curAngle < -maxAngle)
            {
                swingSpd *= -1;
                curAngle = -maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                jump = true;
                if (groundMove) status.OnGroundStopMov = false;
            }
        }
        else
        {
            if (curAngle <= maxAngle && curAngle >= 0)
            {
                curAngle += Time.deltaTime * swingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                if (!status.StartMove && !stopSwing)
                {
                    if (curAngle > 0)
                    {
                        stopSwing = true;
                    }
                }
            }
            else if (curAngle > maxAngle)
            {
                swingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground                
                jump = false;
                curJumpForce = jumpForce;
                myTransform.localPosition = Vector3.zero;
                //if ground stop move
                if (groundMove) status.OnGroundStopMov = true;
            }
            else if (curAngle < 0)//-maxAngle
            {
                swingSpd *= -1;
                curAngle = 0;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                curJumpForce = jumpForce;
                jump = true;
                //if ground stop move
                if (groundMove) status.OnGroundStopMov = false;
            }
        }
        swingJumpFunction();
    }


    private void swingJumpFunction()
    {
        if (jump)
        {
            curJumpForce -= Time.deltaTime * gravity;
            if (curJumpForce > jumpForce)
            {
                curJumpForce = jumpForce;
            }
            else if (curJumpForce < -jumpForce)
            {
                curJumpForce = -jumpForce;
            }
            if (myTransform.localPosition.y + curJumpForce < 0) curJumpForce -= myTransform.localPosition.y + curJumpForce;
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + curJumpForce, myTransform.position.z);
        }
    }

    // Use this for initialization
    void Awake()
    {
        myTransform = transform;//將transform暫存自快取中
        stopSwing = false;
        swing = false;
        curAngle = 0;
        jump = false;
        if (swingSpd == 0) swingSpd = 100;
        if (maxAngle == 0) maxAngle = 10;
        if (jumpForce == 0) jumpForce = 0.07f;
        groundMove = true;

        if (negativeAngle)
        {
            gravity = jumpForce / ((maxAngle) / swingSpd);
        }
        else
        {
            gravity = jumpForce / ((maxAngle / 2) / swingSpd);
        }
        status = myTransform.parent.GetComponent<RoomCharaStatus>();
    }

}
