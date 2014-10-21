using UnityEngine;
using System.Collections;

public class CharaTrainFear : MonoBehaviour, ICharaAction
{
    private bool fear;
    private bool inFear;
    private bool startFear;
    private float maxVStr;
    private float maxHStr;
    private float minVStr;
    private float minHStr;
    private sbyte dir;
    private sbyte baseDecayPercent;
    private float curDecayPercent;
    private float decayPercent;
    private const sbyte hundredPercent = 100;
    private bool decay;
    private float perTime;
    private float perTimer;
    private RoomCharaStatus status;
   



    // Use this for initialization
    void Awake()
    {
        fear = false;
        status = transform.parent.GetComponent<RoomCharaStatus>();
        startFear = true;
        inFear = false;
        perTime = 0.04f;
        perTimer = 0f;
        maxHStr = 0.03f;
        minHStr = 0.03f;
        maxVStr = 0.0f;
        minVStr = 0.0f;
        baseDecayPercent = 5;
        curDecayPercent = hundredPercent;
        decayPercent = 1;
        dir = 1;
        decay = false;
    }
    private void ShockFunction()
    {
        if (!status.MoveJump && !status.StartMove)
        {
            if (startFear)
            {
                inFear = true;
                startFear = false;
                curDecayPercent = hundredPercent;
            }


            if (inFear)
            {

                if (perTimer < perTime)
                {
                    perTimer += Time.deltaTime;
                }
                else
                {
                    if (decay) decayPercent = (curDecayPercent / 100);
                    float rndV = Random.Range(minVStr * decayPercent, maxVStr * decayPercent);
                    float rndH = Random.Range(minHStr * decayPercent, maxHStr * decayPercent);
                    curDecayPercent -= baseDecayPercent;
                    if (curDecayPercent <= 0) curDecayPercent = 0;

                    transform.position = new Vector3(transform.position.x + (rndH * dir), transform.position.y + (rndV * dir), transform.position.z);
                    perTimer = 0f;
                    dir *= -1;
                }
            }
        }
        else
        {
            if (!startFear)
            {
                startFear = true;
                transform.localPosition = Vector3.zero;
            }
        }
    }

    public void SetAction(int actionCase)//顫抖控制開關
    {
        switch(actionCase)
        {
            case 0:
                transform.localPosition = Vector3.zero;
                fear = false;
                break;
            case 1:
                fear = true;
                startFear = true;
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (fear) ShockFunction();
    }
}
