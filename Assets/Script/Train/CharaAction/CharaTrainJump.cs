using UnityEngine;
using System.Collections;

public class CharaTrainJump : MonoBehaviour, ICharaAction
{

    public float swingSpd;
    private float curAngle;
    public float maxAngle;
    public bool negativeAngle;


    private bool jump;
    private bool jumping;
    public float jumpForce;
    private float curJumpForce;
    private float gravity;
    private bool stopSwing;
    private bool startSwing;
    private RoomCharaStatus status;


    // Update is called once per frame
    void Update()
    {

        if (jump)
        {
            if (status.StartMove)
            {
                StopSwingFunction();
            }
            else if (!status.MoveJump)
            {

                startSwing = true;
                status.Jump = true;

            }
            if (startSwing) SwingFunction();
        }
    }


    private void StopSwingFunction()
    {

        if (stopSwing)
        {
            if (curAngle < 0)
            {
                //stop swing
                startSwing = false;
                status.Jump = false;
                //reset swing
                curAngle = 0;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                jumping = false;
                curJumpForce = jumpForce;
                transform.localPosition = Vector3.zero;
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
                transform.eulerAngles = new Vector3(0, 0, curAngle);
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
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                jumping = false;
                curJumpForce = jumpForce;
                transform.localPosition = Vector3.zero;

            }
            else if (curAngle < -maxAngle)
            {
                swingSpd *= -1;
                curAngle = -maxAngle;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                jumping = true;
            }
        }
        else
        {
            if (curAngle <= maxAngle && curAngle >= 0)
            {
                curAngle += Time.deltaTime * swingSpd;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
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
                curAngle = 0;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground                
                jumping = false;
                curJumpForce = jumpForce;
                transform.localPosition = Vector3.zero;
            }
            else if (curAngle < 0)//-maxAngle
            {
                swingSpd *= -1;
                curAngle = 0;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                curJumpForce = jumpForce;
                jumping = true;


            }
        }
        swingJumpFunction();
    }


    private void swingJumpFunction()
    {
        if (jumping)
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
            if (transform.localPosition.y + curJumpForce < 0) curJumpForce -= transform.localPosition.y + curJumpForce;
            transform.position = new Vector3(transform.position.x, transform.position.y + curJumpForce, transform.position.z);
        }
    }


    public void SetAction(int actionCase)//跳躍控制開關
    {
        switch (actionCase)
        {
            case 0:
                jump = false;
                //stop swing
                startSwing = false;
                status.Jump = false;
                //reset swing
                curAngle = 0;
                transform.eulerAngles = new Vector3(0, 0, curAngle);
                jumping = false;
                curJumpForce = jumpForce;
                transform.localPosition = Vector3.zero;
                stopSwing = false;
                break;
            case 1:
                jump = true;
                break;
        }

    }



    // Use this for initialization
    void Awake()
    {
        jump = false;
        stopSwing = false;
        startSwing = false;
        curAngle = 0;
        jumping = false;
        if (swingSpd == 0) swingSpd = 25;
        if (maxAngle == 0) maxAngle = 15;
        if (jumpForce == 0) jumpForce = 0.1f;

        if (negativeAngle)
        {
            gravity = jumpForce / ((maxAngle) / swingSpd);
        }
        else
        {
            gravity = jumpForce / ((maxAngle / 2) / swingSpd);
        }
        status = transform.parent.GetComponent<RoomCharaStatus>();
    }

}
