using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RetreatNumberSpawner
{
    static Vector3 pos1;//連擊數10位數時位置
    static Vector3 pos2;//連擊數100位數時位置
    static Vector3 pos3;//連擊數1000位數時位置
    int number;
    public int Number
    {
        get { return number; }
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            else
            {
                number = value;
            }
        }
    }//顯示的傷害
    int MemberArrowRank { get; set; }
    static GameObject[] retreatRecoverObj;
    static Animator[] numberAnimator;
    static GameObject[] numberObj;
    static MemberArrowRecoverText[] memberArrowRecoverText;
    static Sprite[] numberSprite;
    int rank;//紀錄現在正使用哪個物件播跳寫動畫
    int power = 1;//從3次方，千位數開始除
    int column = 0;//第幾個sprite
    int remainder;//該位數的數字如1962的十位數字就是6
    SpriteRenderer[] NumberSprites;



    public static void StartSet()
    {
        //讀取數字sprite
        numberSprite = new Sprite[10];
        for (int i = 0; i < numberSprite.Length; i++)
        {
            numberSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/Font/Number/{0}", i));
        }
        retreatRecoverObj = new GameObject[ForceCondition.MemberArrowBtn.Length];
        memberArrowRecoverText = new MemberArrowRecoverText[retreatRecoverObj.Length];
        numberAnimator = new Animator[retreatRecoverObj.Length];
        numberObj = new GameObject[retreatRecoverObj.Length];
        //讀特效物物件
        for (int i = 0; i < ForceCondition.MemberArrowBtn.Length; i++)
        {
            retreatRecoverObj[i] = ForceCondition.MemberArrowBtn[i].transform.FindChild("RetreatRecover").gameObject;
            memberArrowRecoverText[i] = retreatRecoverObj[i].GetComponent<MemberArrowRecoverText>();
            numberAnimator[i] = retreatRecoverObj[i].GetComponent<Animator>();
            numberObj[i] = retreatRecoverObj[i].transform.FindChild("Number").gameObject;
        }
        pos1 = new Vector3(-0.55f, 0, 0);
        pos2 = new Vector3(-0.28f, 0, 0);
        pos3 = Vector3.zero;
    }
    void SetSprite()//設定數字顯示圖像
    {
        NumberSprites = numberObj[MemberArrowRank].GetComponentsInChildren<SpriteRenderer>();
        if (Number < 1000)//傷害數字顯示不可超過999
        {
            int tmpNumber = Number;
            power = 1;//從3次方，千位數開始除
            column = 0;//第幾個sprite
            remainder = 0;//該位數的數字如1962的十位數字就是6
            while (tmpNumber / (int)Math.Pow(10, power - 1) > 0)
            {
                remainder = tmpNumber % ((int)Math.Pow(10, power)) / ((int)Math.Pow(10, power - 1));
                //Debug.Log("remainder=" + remainder);
                NumberSprites[column].sprite = numberSprite[remainder];
                tmpNumber -= remainder;
                power++;
                column++;
            }
            memberArrowRecoverText[MemberArrowRank].ShowText(column);
            //設定動畫
            numberAnimator[MemberArrowRank].SetTrigger("RetreatRecover");
            AniEndSet();
        }
    }
    public void SpawnNumber(int _memberArrowRank, int _number)
    {
        if (_number>0)
        {
            MemberArrowRank = _memberArrowRank;
            Number = _number;
            SetSprite();//設定數字顯示圖像
        }
    }
    void AniEndSet()
    {
        switch (column)
        {
            case 1:
                numberObj[MemberArrowRank].transform.localPosition = pos1;
                break;
            case 2:
                numberObj[MemberArrowRank].transform.localPosition = pos2;
                break;
            case 3:
                numberObj[MemberArrowRank].transform.localPosition = pos3;
                break;
        }
    }
}
