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
    }

    private void LevelUp()
    {
        pm.CurExp = 0;
        pm.Lv++;
        LevelUpSetProperty(pm.Lv);

    }

    private void LevelUpSetProperty(int _lv)
    {
        pm.RollLvProperty();
        pm.NeedExp = GameDictionary.MonsterExperienceDic[pm.Lv][pm.RareToString(pm.Rare)];
    }
}
