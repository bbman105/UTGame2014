using UnityEngine;
using System.Collections;

public class BloodText : MonoBehaviour
{
    public bool IsUsing;
    SpriteRenderer[] damageNumSprites;
    public int ShowSpace;
    Transform myTransform;
    int aniType;
    //動畫用欄位
    public int Reverse { get; set; }
    float gravity;
    float flyTime;
    Vector3 curVelocity;
    void DOTAni()
    {
        flyTime = 5;
        gravity = 1f;
        float rndForceX = (float)Random.Range(-40, 40) / 1000f;
        float rndForceY = (float)Random.Range(17, 20) / 100f;
        curVelocity = new Vector3(rndForceX * Reverse, rndForceY, 0);
    }
    void KnockAni()
    {
        flyTime = 5;
        gravity = 1f;
        float rndForceX = (float)Random.Range(5, 40) / 1000f;
        float rndForceY = (float)Random.Range(17, 20) / 100f;
        curVelocity = new Vector3(rndForceX * Reverse, rndForceY, 0);
    }
    void ElementKnockAni()
    {
        flyTime = 0.1f;
        gravity = 1f;
        float rndForceX = (float)Random.Range(15, 30) / 1000f;
        float rndForceY = (float)Random.Range(18, 18) / 100f;
        curVelocity = new Vector3(rndForceX * Reverse, rndForceY, 0);
    }
    void CureAni()
    {
        flyTime = 0.3f;
        gravity = -0.12f;
        //float ForceX = 0.01f;
        curVelocity = Vector3.zero;
    }
    void Awake()
    {
        flyTime = 0;
        damageNumSprites = transform.FindChild("Number").GetComponentsInChildren<SpriteRenderer>();
        myTransform = transform;
        curVelocity = Vector3.zero;
        Reverse = 1;
    }

    void FixedUpdate()
    {
        ForceFunction();
    }
    void ForceFunction()//給予運動
    {
        if (flyTime > 0)
        {
            flyTime -= Time.deltaTime;
            curVelocity.y -= gravity * Time.deltaTime;
            myTransform.position += curVelocity;
        }
    }
    void HideSprites()
    {
        for (int i = 0; i < damageNumSprites.Length; i++)
        {
            damageNumSprites[i].enabled = false;
        }
    }
    public void ShowSprite(int _showSpace, int _type)
    {
        ShowSpace = _showSpace;
        aniType = _type;
        for (int i = 0; i < 3; i++)
        {
            if (i < ShowSpace)
            {
                damageNumSprites[i].enabled = true;
            }
            else
                damageNumSprites[i].enabled = false;
        }
        switch (aniType)//對應EffectFile表的動畫編號
        {
            case 18:
                KnockAni();
                break;
            case 19:
                ElementKnockAni();
                break;
            case 20:
                DOTAni();
                break;
            case 21:
                CureAni();
                break;
            default:
                KnockAni();
                Debug.Log("BloodText無此動態");
                break;
        }
    }
    public void NotUsing()
    {
        HideSprites();
        IsUsing = false;
    }
}
