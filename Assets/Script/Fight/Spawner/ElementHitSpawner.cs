using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ElementHitSpawner
{
    int AniID;
    GameObject ComboHitObj;
    GameObject ComboNumberObj;
    Transform ComboNumberTransform;
    int rank;//紀錄現在正使用哪個物件播跳寫動畫
    Sprite[] numberSprite;
    Animator animator;
    SpriteRenderer[] NumberSprites;
    Vector3 pos1;//連擊數小於10位置
    Vector3 pos2;//連擊數大於10位置
    int comboHitNum;
    public ElementHitSpawner()
    {
        if (ComboHitObj == null)
            ComboHitObj = GameObject.FindGameObjectWithTag("ComboHit");
        ComboNumberObj = ComboHitObj.transform.FindChild("comboNumber").gameObject;
        ComboNumberTransform = ComboNumberObj.transform;
        animator = ComboHitObj.GetComponent<Animator>();
        NumberSprites = new SpriteRenderer[2];
        NumberSprites[0] = ComboNumberObj.transform.FindChild("combo0").GetComponent<SpriteRenderer>();
        NumberSprites[1] = ComboNumberObj.transform.FindChild("combo1").GetComponent<SpriteRenderer>();

        //讀取數字sprite
        numberSprite = new Sprite[10];
        for (int i = 0; i < numberSprite.Length; i++)
        {
            numberSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/Font/Number/combo{0}", i));
        }
        pos1 = new Vector3(0.7f, 0.08f, 0);
        pos2 = new Vector3(1.06f, 0.08f, 0);
    }
    string GetFileName(int _effectFileID)
    {
        string fileName = "";
        if (GameDictionary.EffectDic.ContainsKey(_effectFileID))
        {
            fileName = GameDictionary.EffectDic[_effectFileID];
        }
        else
        {
            fileName = "Unknow EffectID";
            Debug.Log("嘗試讀取不存在特效字典的特效ID");
        }
        return fileName;
    }
    void HideNumberSprite()//隱藏數字Sprite
    {
        for (int i = 0; i < NumberSprites.Length; i++)
        {
            NumberSprites[i].enabled = false;
        }
    }
    void SetSprite()//設定數字顯示圖像
    {
        HideNumberSprite();
        if (comboHitNum < 100)//報及次數顯示不可超過99
        {
            int tmpNumber = comboHitNum;
            int power = 1;//從3次方，千位數開始除
            int column = 0;//第幾個sprite
            int remainder;//該位數的數字如1962的十位數字就是6
            while (tmpNumber / (int)Math.Pow(10, power - 1) > 0)
            {
                remainder = tmpNumber % ((int)Math.Pow(10, power)) / ((int)Math.Pow(10, power - 1));
                //Debug.Log("remainder=" + remainder);
                NumberSprites[column].sprite = numberSprite[remainder];
                NumberSprites[column].enabled = true;
                tmpNumber -= remainder;
                power++;
                column++;
            }
            animator.SetTrigger(GetFileName(AniID));
            if (comboHitNum < 10)
                ComboNumberTransform.localPosition = pos1;
            else
                ComboNumberTransform.localPosition = pos2;
        }
    }
    private void StartCombo()//開始連擊
    {
        AniID = 22;
        SetSprite();//設定數字顯示圖像
    }
    private void Combo()//連擊
    {
        AniID = 23;
        SetSprite();//設定數字顯示圖像
    }
    public void ComboHit()
    {
        comboHitNum = ForceCondition.ForceStatusDic["PlayerMonster"].ComboHit;
        if (comboHitNum != 1)
            Combo();//連擊
        else
            StartCombo();//開始連擊
    }
    public void InterruptCombo()//中斷連擊
    {
        HideNumberSprite();
        AniID = 24;
        animator.SetTrigger(GetFileName(AniID));
    }
}
