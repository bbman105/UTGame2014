using UnityEngine;
using System.Collections;

public class CharaTrainNervous : MonoBehaviour, ICharaAction
{

    private float nervousTime;
    private float nervousTimer;
    private bool nervous;
    private Transform chara;
    private RoomCharaStatus status;
    private bool rndSwing;
    private float rndMaxTime;

    // Use this for initialization
    void Awake()
    {
        nervousTime = 0.3f;
        nervousTimer = nervousTime;
        nervous = false;
        rndSwing = true;
        rndMaxTime = 10;
        chara = transform.parent;
        status = chara.GetComponent<RoomCharaStatus>();
    }
    void SwingtimeFunction()
    {
        if (nervousTimer > 0)
        {
            nervousTimer -= Time.deltaTime;
            if (nervousTimer < 0)
            {
                if (rndSwing)
                {
                    float rndSwingTime = Random.Range(0, rndMaxTime / 10);
                    nervousTimer = nervousTime + rndSwingTime;
                }
                else
                {
                    nervousTimer = nervousTime;
                }
                status.FaceChange();
            }
        }
    }


    public void SetAction(int actionCase)//緊張控制開關
    {
        switch (actionCase)
        {
            case 0:
                nervous = false;
                break;
            case 1:
                nervous = true;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(nervous) SwingtimeFunction();
    }
}
