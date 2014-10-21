using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StunStatus : MonoBehaviour
{

    bool KnockWallStun;
    float KnockWallStunTimer;

    Status myStatus;
    public Dictionary<int, bool> StunEffectDic;

    public void StunStatusStartSet(Status _status)
    {
        myStatus = _status;
        KnockWallStun = false;
        enabled = false;
        StunEffectDic = new Dictionary<int, bool>();
        StunEffectDic.Add(0, false);
    }

    void Update()
    {
        StunTimerFunction();
    }

    public void SetKnockStun(float _stunTime)
    {
        KnockWallStun = true;
        myStatus.IsStun = true;
        enabled = true;
        KnockWallStunTimer = _stunTime;
    }
    public void SetEffectStun(int _stunID, bool _isStun)
    {
        if (_isStun)
        {
            if (!StunEffectDic.ContainsKey(_stunID))
                StunEffectDic.Add(_stunID, _isStun);
        }
        else
        {
            if (StunEffectDic.ContainsKey(_stunID))
                StunEffectDic.Remove(_stunID);
        }
        StunCheck();
    }


    private void StunTimerFunction()
    {
        if (KnockWallStunTimer > 0)
        {
            KnockWallStunTimer -= Time.deltaTime;
        }
        else
        {
            KnockWallStun = false;
            KnockWallStunTimer = 0;
            StunCheck();
            enabled = false;
        }
    }

    private void StunCheck()
    {
        if (!StunEffectDic.ContainsValue(true) && !KnockWallStun)
        {
            enabled = false;
            myStatus.IsStun = false;
        }
        else
        {
            enabled = true;
            myStatus.IsStun = true;
        }

    }
}
