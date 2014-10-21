using UnityEngine;
using System.Collections;

public class CharaTrainLie : MonoBehaviour, ICharaAction
{

    public bool lie;//會不會躺
    private bool onLie;//正在躺
    private bool startLie;//開始躺
    private float lieDist;
    private Vector3 liePosition;
    private Vector3 preLieAngle;
    private Vector3 lieAngle;
    private RoomCharaStatus status;

    private float curJumpForce;
    private float jumpForce;
    private float gravity;
    // Use this for initialization
    void Awake()
    {
        jumpForce = 0.12f;
        curJumpForce = jumpForce;
        gravity = 0.8f;
        lie = false;
        startLie = true;
        onLie = false;
        lieDist = -0.2f;
        liePosition = new Vector3(0, lieDist, 0);
        status = transform.parent.GetComponent<RoomCharaStatus>();
        preLieAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 35);
        lieAngle = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 70);
    }

    void LieFunction()
    {
        if (!status.MoveJump && !status.StartMove)
        {
            if (startLie)
            {
                onLie = true;
                startLie = false;
                curJumpForce = jumpForce;
                transform.eulerAngles = preLieAngle;
            }
            if (onLie)
            {
                //transform.localPosition = liePosition;
                //transform.eulerAngles = lieAngle;
                curJumpForce -= Time.deltaTime * gravity;
                if (transform.localPosition.y + curJumpForce < lieDist)
                {
                    onLie = false;
                    transform.localPosition = liePosition;
                    transform.eulerAngles = lieAngle;
                    curJumpForce = jumpForce;
                }
                transform.localPosition = new Vector3(0, transform.localPosition.y + curJumpForce, 0);
            }
        }
        else
        {
            if (!startLie)
            {
                startLie = true;
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = Vector3.zero;
            }
        }
    }

    public void SetAction(int actionCase)//躺的控制開關
    {
        switch (actionCase)
        {
            case 0:
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = Vector3.zero;
                lie = false;
                break;
            case 1:
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = Vector3.zero;
                lie = true;
                startLie = true;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (lie) LieFunction();
    }
}
