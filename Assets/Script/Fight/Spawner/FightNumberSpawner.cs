using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FightNumberSpawner
{
    Status status;
    Transform myTransform;
    Quaternion rot;
    Vector3 locScale;
    static Vector3 pos1;//連擊數10位數時位置
    static Vector3 pos2;//連擊數100位數時位置
    static Vector3 pos3;//連擊數1000位數時位置
    int aniID;
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
    static GameObject numberSourceObj;
    static GameObject numberListObj;
    static List<Animator> animator;
    static List<BloodText> bloodText;
    static Sprite[] numberSprite;
    static int rank;//紀錄現在正使用哪個物件播跳寫動畫
    GameObject numberDamageObj;
    GameObject numberObj;
    int power = 1;//從3次方，千位數開始除
    int column = 0;//第幾個sprite
    int remainder;//該位數的數字如1962的十位數字就是6
    SpriteRenderer[] NumberSprites;


    public static void StartSet()//讀取prefab
    {
        //讀取數字sprite
        numberSprite = new Sprite[10];
        animator = new List<Animator>();
        bloodText = new List<BloodText>();
        for (int i = 0; i < numberSprite.Length; i++)
        {
            numberSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/Font/Number/{0}", i));
        }
        //讀特效物物件
        numberSourceObj = Resources.Load<GameObject>("Effect/DamageShow/KnockDamage");
        numberListObj = GameObject.FindGameObjectWithTag("HitDamageList");
        Animator[] tmpAniArray = numberListObj.GetComponentsInChildren<Animator>();
        BloodText[] tmpComArray = numberListObj.GetComponentsInChildren<BloodText>();
        for (int i = 0; i < tmpAniArray.Length; i++)
        {
            animator.Add(tmpAniArray[i]);
            bloodText.Add(tmpComArray[i]);
        }
        pos1 = new Vector3(-0.3f, 0, 0);
        pos2 = new Vector3(-0.15f, 0, 0);
        pos3 = Vector3.zero;
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
    void SetSprite()//設定數字顯示圖像
    {
        NumberSprites = numberObj.GetComponentsInChildren<SpriteRenderer>();
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
            animator[rank].SetTrigger(GetFileName(aniID));
            if (status.gameObject.tag == "PlayerMonster")
                bloodText[rank].Reverse = -1;
            else
                bloodText[rank].Reverse = 1;
            bloodText[rank].ShowSprite(column, aniID);
            AniEndSet();
        }
    }

    public void SpawnNumber(Status _targetStatus, int _aniID, int _number)
    {
        status = _targetStatus;
        myTransform = status.transform;
        aniID = _aniID;
        Number = _number;
        rot = Quaternion.Euler(0f, 0f, 0f);
        //設定動畫
        for (int i = 0; i < animator.Count; i++)
        {
            if (i == animator.Count - 1 && bloodText[i].IsUsing)
            {

                rank = i + 1;
                numberDamageObj = GameObject.Instantiate(numberSourceObj, pos1, rot) as GameObject;
                numberObj = numberDamageObj.transform.FindChild("Number").gameObject;
                //設定動畫
                numberDamageObj.transform.parent = numberListObj.transform;
                animator.Add(numberDamageObj.GetComponent<Animator>());//因為有多出component，將component加入清單中
                bloodText.Add(numberDamageObj.GetComponent<BloodText>());//因為有多出component，將component加入清單中
                bloodText[rank].IsUsing = true;
                SetSprite();//設定數字顯示圖像
                break;
            }
            else
            {
                if (!bloodText[i].IsUsing)
                {
                    rank = i;
                    bloodText[rank].IsUsing = true;
                    numberDamageObj = animator[i].gameObject;
                    numberObj = numberDamageObj.transform.FindChild("Number").gameObject;
                    SetSprite();//設定數字顯示圖像
                    break;
                }
            }
        }
    }
    void AniEndSet()
    {
        numberDamageObj.transform.position = myTransform.position;
        switch (column)
        {
            case 1:
                numberObj.transform.localPosition = pos1;
                break;
            case 2:
                numberObj.transform.localPosition = pos2;
                break;
            case 3:
                numberObj.transform.localPosition = pos3;
                break;
        }
    }
}
