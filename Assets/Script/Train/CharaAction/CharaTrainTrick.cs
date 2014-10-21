using UnityEngine;
using System.Collections;

public class CharaTrainTrick : MonoBehaviour
{

    //lie action
    private bool trick;
    private float lieAngle;
    private float lieVelocity;
    private float lieAcceleration;
    private float lieSwingAngle;
    private float lieSwingSpd;
    //jum action
    private bool canJump;
    private float curJumpForce;
    private float jumpForce;
    private float gravity;


    private RoomCharaStatus status;


    // Use this for initialization
    void Start()
    {
        lieAngle = 90;
        trick = true;
        lieVelocity = 0;
        lieAcceleration = 20f;

        canJump = true;
        jumpForce = 0.11f;
        curJumpForce = jumpForce;
        gravity = 1f;
        status = transform.parent.GetComponent<RoomCharaStatus>();
    }

    void LieFunction()
    {
        if (trick)
        {
            if (!status.MoveJump)
            {
                if (transform.eulerAngles.z < lieAngle)
                {
                    lieVelocity += lieAcceleration * Time.deltaTime;
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + lieVelocity);
                }
            }
            else
            {
                lieVelocity = 0;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
            }
        }
        else
        {
            lieVelocity = 0;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
    }
    void LieJump()
    {

        if (trick)
        {
            if (!status.MoveJump)
            {
                if (canJump)
                {
                    curJumpForce -= Time.deltaTime * gravity;
                    if (transform.localPosition.y + curJumpForce < -0.3f)
                    {
                        canJump = false;
                        curJumpForce -= transform.localPosition.y + curJumpForce;
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + curJumpForce, transform.localPosition.z);
                        curJumpForce = jumpForce;
                    }
                    else
                    {
                        LieFunction();
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + curJumpForce, transform.localPosition.z);
                    }
                }
            }
            else
            {
                curJumpForce = jumpForce;
                canJump = true;
            }
        }
        else
        {
            curJumpForce = jumpForce;
            canJump = true;
        }



    }

    // Update is called once per frame
    void Update()
    {
        LieJump();
    }
}
