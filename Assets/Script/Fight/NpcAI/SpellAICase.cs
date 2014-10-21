using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class SpellAICase
{
    protected SpellAI spellAI;
    protected List<string> CaseValueList;
    protected int caseRank;

    public SpellAICase(SpellAI _spellAI, List<string> _caseValueList,int _caseRank)
    {
        spellAI = _spellAI;
        CaseValueList = _caseValueList;
        caseRank = _caseRank;
    }
    public abstract void CaseCheck(Status _self, Status _target);

}
