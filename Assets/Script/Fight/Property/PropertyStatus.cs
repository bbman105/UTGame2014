using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour
{
    public void SetBlessingProperty(bool _isBlessing, int _effectClassID)
    {
        blessingStatus.SetEffectBlessing(_effectClassID, _isBlessing);
    }
    public void SetCurseProperty(bool _isCurse, int _effectClassID)
    {
        curseStatus.SetEffectCurse(_effectClassID, _isCurse);
    }
    public void SetStunProperty(bool _isStun, int _effectClassID)
    {
        stunStatus.SetEffectStun(_effectClassID, _isStun);
    }
    public void SetFearProperty(bool _isFear, int _effectClassID)
    {
        fearStatus.SetEffectFear(_effectClassID, _isFear);
    }
    public void SetSilenceProperty(bool _isSilence, int _effectClassID)
    {
        stunStatus.SetEffectStun(_effectClassID, _isSilence);
    }
    public void SetFetterProperty(bool _isFetter, int _effectClassID)
    {
        fetterStatus.SetEffectFetter(_effectClassID, _isFetter);
    }
    public void SetNailProperty(bool _isNail, int _effectClassID)
    {
        nailStatus.SetEffectNail(_effectClassID, _isNail);
    }
    public void SetRigidProperty(bool _isRigid, int _effectClassID)
    {
        rigidStatus.SetEffectRigid(_effectClassID, _isRigid);
    }
    public void SetInvincibleProperty(bool _isInvincible, int _effectClassID)
    {
        stunStatus.SetEffectStun(_effectClassID, _isInvincible);
    }
    //要改變腳色基本屬性需透過此function
    public void SetBaseProperty(int _damagePlus, float _damageTimes,
        int _defensePlus, float _defenseTimes,int _reduceDamagePlus,float _reduceDamageTimes,
        int _brutalPlus, float _brutalTimes,
        int _aggressivePlus, float _aggressiveTimes,
        int _resistancePlus, float _resistanceTimes,
        int _speedPlus, float _speedTimes)
    {
        DamagePlus += _damagePlus;
        DamageTimes += _damageTimes;
        DefensePlus += _defensePlus;
        DefenseTimes += _defenseTimes;
        BrutalPlus += _brutalPlus;
        BrutalTimes += _brutalTimes;
        AggressivePlus += _aggressivePlus;
        AggressiveTimes += _aggressiveTimes;
        ResistancePlus += _resistancePlus;
        ResistanceTimes += _resistanceTimes;
        ReduceDamagePlus += _reduceDamagePlus;
        ReduceDamageTimes += _reduceDamageTimes;
        SpeedPlus += _speedPlus;
        SpeedTimes += _speedTimes;
    }
    //要改變腳色撞擊屬性需透過此function
    public void SetKnockProperty(int _knockStrengthPlus, int _beKnockStrengthPlus,
        float _knockNailTimePlus,float _beKnockNailTimePlus,
        int _knockWallDMGPlus,float _knockWallDMGTimes,int _beKnockWallDMGPlus,float _beKnockWallDMGTimes,
        float _knockWallStunPlus,float _beKnockWallStunPlus
        )
    {
        KnockStrengthPlus += _knockStrengthPlus;
        BeKnockStrengthPlus += _beKnockStrengthPlus;
        KnockNailTimePlus += _knockNailTimePlus;
        BeKnockNailTimePlus += _beKnockNailTimePlus;
        KnockWallDMGPlus += _knockWallDMGPlus;
        KnockWallDMGTimes += _knockWallDMGTimes;
        BeKnockWallDMGPlus += _beKnockWallDMGPlus;
        BeKnockWallDMGTimes += _beKnockWallDMGTimes;
        KnockWallStunPlus += _knockWallStunPlus;
        BeKnockWallStunPlus += _beKnockWallStunPlus;
    }
    //要改變腳色元素爆擊屬性需透過此function
    public void SetElementHitProperty(int _criticalDMGPlus, float _criticalDMGTimes, int _beCriticalDMGPlus, float _beCriticalDMGTimes)
    {
        CriticalDMGPlus += _criticalDMGPlus;
        CriticalDMGTimes += _criticalDMGTimes;
        BeCriticalDMGPlus += _beCriticalDMGPlus;
        BeCriticalDMGTimes += _beCriticalDMGTimes;
    }
    //要清除腳色Effect需透過此function
}
