using UnityEngine;
using System.Collections;


public class BaseSkillName  {

    int baseSkillID;
    public int BaseSkillID
    {
        get { return baseSkillID; }
        set { baseSkillID = value; }
    }
    string skillName;
    public string SkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }


    public BaseSkillName(int _baseSkillID,string _baseSkillName)
    {
        this.BaseSkillID = _baseSkillID;
        this.SkillName = _baseSkillName;
    }

}
