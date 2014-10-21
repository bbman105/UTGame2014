using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public partial class ForceCondition : MonoBehaviour
{
    static List<Status> charaStatuList;
    static List<TwinkleEffect> charaTwinkleEffectList;
    static List<SwingEffect> charaSwingEffectList;
    static List<ShockEffect> charaShockEffectList;
    static List<KnockFX> charaKnockFXList;

    private static void SetCharaStatus()
    {
        Dictionary<string, List<GameObject>>.KeyCollection CharaDicKeyCol = CharaDic.Keys;
        charaStatuList = new List<Status>();
        charaTwinkleEffectList = new List<TwinkleEffect>();
        charaSwingEffectList = new List<SwingEffect>();
        charaShockEffectList = new List<ShockEffect>();
        charaKnockFXList = new List<KnockFX>();
        foreach (string key in CharaDicKeyCol)
        {
            for (int i = 0; i < CharaDic[key].Count; i++)
            {
                charaStatuList.Add(CharaDic[key][i].GetComponent<Status>());
                charaTwinkleEffectList.Add(CharaDic[key][i].GetComponentInChildren<TwinkleEffect>());
                charaSwingEffectList.Add(CharaDic[key][i].GetComponentInChildren<SwingEffect>());
                charaShockEffectList.Add(CharaDic[key][i].GetComponentInChildren<ShockEffect>());
                charaKnockFXList.Add(CharaDic[key][i].GetComponentInChildren<KnockFX>());
            }
        }
        //CharaStartFigth();
    }

    public static void CharaStartFigth()
    {
        MemberArrow.RestrictCondition = 2;//戰鬥開始，指揮面板可控制怪物撤退
        for (int i = 0; i < charaStatuList.Count; i++)
        {
            if (charaStatuList[i].RetreatCondition == 0)
            {
                charaStatuList[i].enabled = true;
                charaStatuList[i].canMov = true;
                charaSwingEffectList[i].enabled = true;
            }
        }
    }
    static void CharaKeepFigth()
    {
        for (int i = 0; i < charaStatuList.Count; i++)
        {
            charaStatuList[i].enabled = true;
            charaStatuList[i].canMov = true;
            charaTwinkleEffectList[i].enabled = true;
            charaSwingEffectList[i].enabled = true;
            charaShockEffectList[i].enabled = true;
            charaKnockFXList[i].enabled = true;
        }
    }
    static void CharaStopFigth()
    {
        for (int i = 0; i < charaStatuList.Count; i++)
        {
            charaStatuList[i].enabled = false;
            charaTwinkleEffectList[i].enabled = false;
            charaSwingEffectList[i].enabled = false;
            charaShockEffectList[i].enabled = false;
            charaKnockFXList[i].enabled = false;
        }
    }

    static void FightKo()
    {
        for (int i = 0; i < charaStatuList.Count; i++)
        {
            charaStatuList[i].SpeciesModeler.SetSpriteSortingLayer("KO");
        }
        //KoAni.StartCoroutine(Play());
    }

    IEnumerator CallRewardDetailUI()
    {
        Time.timeScale = 0.005f;
        yield return new WaitForSeconds(0.01f);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(1f);
        CharaStopFigth();
        ResultCaculate();
        BattleEndAnim();
        RewardShow = true;
    }
}
