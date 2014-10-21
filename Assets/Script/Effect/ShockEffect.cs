using UnityEngine;
using System.Collections;

public class ShockEffect : MonoBehaviour
{
    private bool shock;
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
    private byte shockRound;
    private float perTime;
    private float perTimer;
    private bool exempt;
    private Vector3 extraShock;
    private Transform myTransform;
    private float rndV;
    private float rndH;


    // Use this for initialization
    void Awake()
    {
        myTransform = transform;
        shock = true;
        perTime = 0.03f;
        perTimer = 0f;
        maxHStr = 0.1f;
        minHStr = 0.1f;
        maxVStr = 0.0f;
        minVStr = 0.0f;
        baseDecayPercent = 10;
        curDecayPercent = hundredPercent;
        decayPercent = 1;
        dir = 1;
        exempt = false;
        decay = true;
        this.enabled = false;
    }
    public void Shock(byte _shockRound)
    {
        if (shock)
        {
            this.enabled = true;
            if (!exempt)
            {
                shockRound = (byte)(_shockRound * 2);
                exempt = true;
            }
            else
            {
                if (shockRound % 2 == 0)
                {
                    shockRound = (byte)(_shockRound * 2);
                }
                else
                {
                    shockRound = (byte)((_shockRound * 2) - 1);
                }
            }
        }
    }

    private void ShockFunction()
    {
        if (shockRound > 0)
        {
            if (perTimer < perTime)
            {
                perTimer += Time.deltaTime;
            }
            else
            {
                if (decay) decayPercent = (curDecayPercent / 100);
                rndV = Random.Range(minVStr * decayPercent, maxVStr * decayPercent);
                rndH = Random.Range(minHStr * decayPercent, maxHStr * decayPercent);
                curDecayPercent -= baseDecayPercent;
                if (curDecayPercent <= 0) curDecayPercent = 0;


                extraShock += new Vector3((rndH * dir), (rndV * dir), 0);
                myTransform.position = new Vector3(myTransform.position.x + (rndH * dir), myTransform.position.y + (rndV * dir), myTransform.position.z);
                perTimer = 0f;
                dir *= -1;
                shockRound--;
                if (shockRound == 0)
                {
                    myTransform.position -= extraShock;
                    extraShock = Vector3.zero;
                    curDecayPercent = hundredPercent;
                    exempt = false;
                    this.enabled = false;
                }
            }

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ShockFunction();
    }
}
