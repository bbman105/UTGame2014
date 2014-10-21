using UnityEngine;
using System.Collections;

public class TweenTwinkleEffect : MonoBehaviour
{
    private byte twinkleTimes;
    private float twinkleSpd;
    private float strength;
    private float colorValue;
    private float addColorNum;
    private float extraColor;
    private Color myColor;
    // Use this for initialization
    void Start()
    {
        myColor = renderer.material.GetColor("_TintColor");
        twinkleTimes = 3;
        twinkleSpd = 10;
        strength = 3f;
    }

    private void twinkleEffectFunction()
    {
        if (twinkleTimes > 0)
        {
            colorValue = Time.deltaTime * twinkleSpd;
            addColorNum += colorValue;
            extraColor += colorValue;
            myColor.r += colorValue;
            myColor.g += colorValue;
            myColor.b += colorValue;
            renderer.material.SetColor("_TintColor", myColor);
            if (addColorNum >= strength)
            {
                twinkleSpd *= -1;
                addColorNum = strength - Time.deltaTime;
            }
            else if (addColorNum < 0)
            {
                twinkleSpd *= -1;
                addColorNum = 0;
                twinkleTimes--;
                if (twinkleTimes <= 0)
                {
                    myColor.r -= extraColor;
                    myColor.g -= extraColor;
                    myColor.b -= extraColor;
                    renderer.material.SetColor("_TintColor", myColor);
                    myColor = renderer.material.GetColor("_TintColor");
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        twinkleEffectFunction();
    }
}
