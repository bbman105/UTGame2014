using UnityEngine;
using System.Collections;

public class NpcSpellCDCounter : CoolDownCounter,IViewDebugLog
{

    SpellAI spellAI;
    public NpcSpellCDCounter(SpellAI _spellAI)
    {
        this.spellAI = _spellAI;
        this.CdTime = this.spellAI.Cd;
        this.CdTimer = this.spellAI.Cd;
    }

    public override void StartCD()
    {
        if (this.spellAI.Cd!=0)
        {
            this.CdTimer = this.spellAI.Cd;
            this.spellAI.OnCD = true;
        }
    }

    public override void OnCDFunction()
    {
        if (this.spellAI.OnCD)
        {
            CdTimer -= Time.deltaTime;
            if (CdTimer <= 0)
                CDDone();
        }
    }
    protected override void CDDone()
    {
        this.CdTimer = this.CdTime;
        this.spellAI.OnCD = false;
        ViewDebugLog();        
    }

    public void ViewDebugLog()
    {
        Debug.Log(string.Format("SpellID:{0}'CD is Done!", this.spellAI.SpellID));
    }
}
