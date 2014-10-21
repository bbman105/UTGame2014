using UnityEngine;
using System.Collections;

public class TwinkleEffect : MonoBehaviour
{

    private bool twinkle;
    private byte twinkleRound;
    private float perTime;
    private float perTimer;
    private float strength;
    private bool exempt;
    Renderer thisRenderer;
    Color myColor;

    // Use this for initialization
    void Awake()
    {
        thisRenderer = transform.GetComponentInChildren<Renderer>();
        myColor = thisRenderer.material.GetColor("_TintColor");
        twinkle = true;
        perTime = 0.03f;
        perTimer = 0f;
        strength = 0.25f;
        exempt = false;
        this.enabled = false;
    }
    public void Twinkle(byte _twinkleRound)
    {
        if (twinkle)
        {
            this.enabled = true;
            if (!exempt)
            {
                twinkleRound = (byte)(_twinkleRound * 2);
                exempt = true;
            }
            else
            {
                if (twinkleRound % 2 == 0)
                {
                    twinkleRound = (byte)(_twinkleRound * 2);
                }
                else
                {
                    twinkleRound = (byte)((_twinkleRound * 2) - 1);
                }
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TwinkleFunction();
    }
    private void TwinkleFunction()
    {
        if (twinkleRound > 0)
        {
            if (perTimer < perTime)
            {
                perTimer += Time.deltaTime;
            }
            else
            {
                //Color col=renderer.material.GetColor("_TintColor");
                myColor.r += strength;
                myColor.g += strength;
                myColor.b += strength;
                thisRenderer.material.SetColor("_TintColor", myColor);
                perTimer = 0f;
                strength *= -1;
                twinkleRound--;
                if (twinkleRound == 0)//閃爍結束時Enabled此Component
                {
                    exempt = false;
                    this.enabled = false;
                }
            }

        }
    }


}
