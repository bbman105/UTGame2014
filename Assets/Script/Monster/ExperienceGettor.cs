using UnityEngine;
using System.Collections;

public class ExperienceGettor
{

    PlayerMonster pm;

    public ExperienceGettor(PlayerMonster _pm)
    {
        this.pm = _pm;
    }


    public void GetExperience(int _value)
    {
        pm.CurExp += _value;
        if (pm.CurExp > pm.NeedExp)
        {
            LevelUp();
        }
        else
        {
            IODataFromArcalet.SetPlayerMonster("EXP", pm.CurExp.ToString(), pm.MonsterID);//向Server寫入玩者怪獸
        }
    }

    private void LevelUp()
    {
        pm.CurExp = 0;
        pm.Lv++;
        LevelUpSetProperty(pm.Lv);
        string[] attrNameArray = new string[2] { "LV", "EXP" };
        string[] attrValueArray = new string[2] { pm.Lv.ToString(), pm.CurExp.ToString() };
        IODataFromArcalet.SetPlayerMonster(attrNameArray, attrValueArray, pm.MonsterID);//向Server寫入玩者怪獸
    }

    private void LevelUpSetProperty(int _lv)
    {
        pm.RollLvProperty();
        pm.NeedExp = GameDictionary.MonsterExperienceDic[pm.Lv][pm.RareToString(pm.Rare)];
    }
}
