using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseSkill
{

    //對應到的技能編號
    private int spellID;
    public int SpellID
    {
        get { return spellID; }
        set { spellID = value; }
    }

    //技能名稱
    private string skillName;
    public string SkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }
    //技能敘述
    private string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    //傷害倍率
    private float damage;
    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private int targetAni;
    public int TargetAni
    {
        get { return targetAni; }
        set { targetAni = value; }
    }
    private int targetAreaAni;
    public int TargetAreaAni
    {
        get { return targetAreaAni; }
        set { targetAreaAni = value; }
    }
    private int selfAni;
    public int SelfAni
    {
        get { return selfAni; }
        set { selfAni = value; }
    }
    private int selfAreaAni;
    public int SelfAreaAni
    {
        get { return selfAreaAni; }
        set { selfAreaAni = value; }
    }

}
