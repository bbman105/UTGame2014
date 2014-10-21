using UnityEngine;
using System.Collections;

public class SwingEffect : MonoBehaviour
{

    private float SwingSpd { get; set; }
    private float curAngle;
    private float maxAngle;
    private bool negativeAngle;

    private bool jump;
    private float jumpForce;
    private float curJumpForce;
    private float gravity;
    private float extraJump;
    private Transform myTransform;
    private Status status;
    private Animator runDustAni;//跑步的煙霧特效
    bool Reverse { get; set; }//是否反向動作，撤退或恐懼時用

    public void SetReverseMovAnimation(bool _reverse)//設置怪獸跑步動畫方向
    {
        Reverse = _reverse;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!status.IsStun && !status.IsNail)//如果沒被暈眩
        {
            if (!Reverse)
                SwingFunction();//往前跑
            else
                ReverseSwingFunction();//往後跑
        }
        else
        {
            SwingSpd *= -1;
            curAngle = maxAngle;
            myTransform.eulerAngles = new Vector3(0, 0, curAngle);
            //ground
            jump = false;
            curJumpForce = jumpForce;
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y - extraJump, myTransform.position.z);
            extraJump = 0;
        }
    }
    private void ReverseSwingFunction()//往後跑
    {
        if (negativeAngle)
        {
            if (curAngle <= maxAngle && curAngle >= -maxAngle)
            {
                curAngle += Time.deltaTime * SwingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
            }
            else if (curAngle > maxAngle)
            {
                SwingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                jump = true;
            }
            else if (curAngle < -maxAngle)
            {
                SwingSpd *= -1;
                curAngle = -maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                jump = false;
                curJumpForce = jumpForce;
                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y - extraJump, myTransform.position.z);
                extraJump = 0;

            }
        }
        else
        {
            if (curAngle <= maxAngle && curAngle >= 0)
            {
                curAngle += Time.deltaTime * SwingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
            }
            else if (curAngle > maxAngle)
            {
                SwingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                jump = false;
                curJumpForce = jumpForce;
                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y - extraJump, myTransform.position.z);
                extraJump = 0;
                //if ground stop move
                //Status status=(Status)myTransform.parent.GetComponent("Status");
                //status.onGroundStopMov=true;
            }
            else if (curAngle < 0)//-maxAngle
            {
                SwingSpd *= -1;
                curAngle = 0;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                curJumpForce = jumpForce;
                jump = true;
                //if jump start move
                //Status status=(Status)myTransform.parent.GetComponent("Status");
                //status.onGroundStopMov=false;
            }
        }
        swingJumpFunction();
    }

    private void SwingFunction()//往前跑
    {
        if (negativeAngle)
        {
            if (curAngle <= maxAngle && curAngle >= -maxAngle)
            {
                curAngle += Time.deltaTime * SwingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
            }
            else if (curAngle > maxAngle)
            {
                SwingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                runDustAni.SetTrigger("smoke");
                jump = false;
                curJumpForce = jumpForce;
                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y - extraJump, myTransform.position.z);
                extraJump = 0;
            }
            else if (curAngle < -maxAngle)
            {
                SwingSpd *= -1;
                curAngle = -maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                jump = true;
            }
        }
        else
        {
            if (curAngle <= maxAngle && curAngle >= 0)
            {
                curAngle += Time.deltaTime * SwingSpd;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
            }
            else if (curAngle > maxAngle)
            {
                SwingSpd *= -1;
                curAngle = maxAngle;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //ground
                jump = false;
                curJumpForce = jumpForce;
                myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y - extraJump, myTransform.position.z);
                extraJump = 0;
                //if ground stop move
                //Status status=(Status)myTransform.parent.GetComponent("Status");
                //status.onGroundStopMov=true;
            }
            else if (curAngle < 0)//-maxAngle
            {
                SwingSpd *= -1;
                curAngle = 0;
                myTransform.eulerAngles = new Vector3(0, 0, curAngle);
                //jump
                curJumpForce = jumpForce;
                jump = true;
                //if jump start move
                //Status status=(Status)myTransform.parent.GetComponent("Status");
                //status.onGroundStopMov=false;
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
            extraJump += curJumpForce;
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + curJumpForce, myTransform.position.z);
        }
    }

    // Use this for initialization
    void Awake()
    {
        Reverse = false;
        myTransform = transform;
        negativeAngle = true;
        curAngle = 0;
        maxAngle = 15;
        jump = false;
        jumpForce = 0.06f;
        status = (Status)transform.parent.GetComponent("Status");
        runDustAni = myTransform.parent.FindChild("RunDust").FindChild("dust").GetComponent<Animator>();
        SetSwingSpd(15, 35);
    }
    public void SetSwingSpd(float _speed, float _maxSpdDenominator)
    {
        SwingSpd = 250 + (250 * (_speed / _maxSpdDenominator));
        if (negativeAngle)
        {
            gravity = jumpForce / ((maxAngle) / SwingSpd);
        }
        else
        {
            gravity = jumpForce / ((maxAngle / 2) / SwingSpd);
        }
    }
}
