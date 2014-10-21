using UnityEngine;
using System.Collections;

public class Feature  {

    public Talent OwnTalent;
    public bool IsDominant;//是否為優勢基因
    public Feature(int _talentID,bool _isDominant)
    {
        OwnTalent = GameDictionary.TalentDic[_talentID];
        IsDominant = _isDominant;
    }
    public string GetDominantString()
    {
        string dominantString="";
        if (IsDominant)
            dominantString = "●";
        else
            dominantString = "○";
        return dominantString;
    }
}
