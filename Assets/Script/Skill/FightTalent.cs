using UnityEngine;
using System.Collections;

public class FightTalent
{
    public int TalentID { get; set; }
    public StatusEffect PermanentEffect;//取得法術狀態
    ExcuteEffect EffectExcuter;//撤退治癒器

    public FightTalent(int _talentID)
    {
        TalentID = _talentID;
        PermanentEffect = new StatusEffect(GameDictionary.TalentDic[TalentID].PermanentEffect, true);
    }
    public void GiveEffect(Status _status)
    {
        EffectExcuter = _status.gameObject.AddComponent<ExcuteEffect>();
        EffectExcuter.SetEffect(_status, _status, PermanentEffect);
    }
}
