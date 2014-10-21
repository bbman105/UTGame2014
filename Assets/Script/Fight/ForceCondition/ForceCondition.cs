using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public delegate void UpdateForceStatusDelegate();
public delegate void DeathDelegate();


public partial class ForceCondition : MonoBehaviour
{



    void Update()
    {
        DetailUIShow();
        ExperienceUIShow();
    }



    public static void OnForceStatusChanged()
    {
        if (UpdateForceStatus != null)
        {
            UpdateForceStatus();
        }
    }
    public static void OnSomebodyDeath()
    {
        if (UpdateSombodyDeath != null)
        {
            UpdateSombodyDeath();
        }
    }
    public static void OnPlayerDeath()
    {
        if (UpdatePlayerDeath != null)
        {
            UpdatePlayerDeath();
        }
    }
    public static void OnNPCDeath()
    {
        if (UpdateNPCDeath != null)
        {
            UpdateNPCDeath();
        }
    }

    public static void UpdateForceCondition()
    {
        Status[] charaStatus;
        Dictionary<string, List<GameObject>>.KeyCollection CharaDicKeyCol = CharaDic.Keys;
        foreach (string key in CharaDicKeyCol)
        {
            ForceStatus fs = ForceStatusDic[key];
            fs.MaxCharaNum = CharaDic[key].Count;
            fs.CurCharaNum = CharaDic[key].Count;
            fs.DeathCharaNum = fs.MaxCharaNum;
            fs.CurHealth = 0;
            fs.MaxHealth = 0;
            //fs.Hit = 0;
            //fs.ElementHit = 0;
            charaStatus = new Status[CharaDic[key].Count];
            for (int i = 0; i < CharaDic[key].Count; i++)
            {
                charaStatus[i] = CharaDic[key][i].GetComponent<Status>();
                fs.CurHealth += charaStatus[i].CurHealth;
                fs.MaxHealth += charaStatus[i].MaxHealth;
                //fs.Hit += charaStatus[i].Hit;
                //fs.ElementHit += charaStatus[i].ElementHit;
                if (charaStatus[i].Alive) fs.DeathCharaNum--;
            }
            fs.CurHealthPercent = 100 * (fs.CurHealth / fs.MaxHealth);
            if (fs.Hit != 0) fs.ElementHitRate = 100 * ((float)fs.ElementHit / (float)fs.Hit);
        }
        //設定戰鬥結果
        if (ForceStatusDic["PlayerMonster"].CurHealthPercent <= 0 || ForceStatusDic["NPCMonster"].CurHealthPercent <= 0)
        {
            if (!CallEnd)
            {
                MemberArrow.RestrictCondition = -1;//設定隊伍操作面板停止按鈕功能
                CallEnd = true;
                staticCoroutine.StartCoroutine("CallRewardDetailUI");
            }
        }
    }






}
