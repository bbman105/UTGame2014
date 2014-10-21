using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NailStatus : MonoBehaviour
{

    bool KnockNail;
    float KnockNailTimer;
    Status myStatus;
    public Dictionary<int, bool> NailEffectDic;

    public void NailStatusStartSet(Status _status)
    {
        myStatus = _status;
        KnockNail = false;
        enabled = false;
        NailEffectDic = new Dictionary<int, bool>();
        NailEffectDic.Add(0, false);
    }
    void Update()
    {
        NailTimerFunction();
    }
    public void SetKnockNail(float _nailTime)
    {
        KnockNail = true;
        myStatus.IsNail = true;
        enabled = true;
        KnockNailTimer = _nailTime;
    }
    public void SetEffectNail(int _nailID, bool _isNail)
    {
        if (_isNail)
        {
            if (!NailEffectDic.ContainsKey(_nailID))
                NailEffectDic.Add(_nailID, _isNail);
        }
        else
        {
            if (NailEffectDic.ContainsKey(_nailID))
                NailEffectDic.Remove(_nailID);
        }
        NailCheck();
    }


    private void NailTimerFunction()
    {
        if (KnockNailTimer > 0)
        {
            KnockNailTimer -= Time.deltaTime;
        }
        else
        {
            KnockNail = false;
            KnockNailTimer = 0;
            NailCheck();
            enabled = false;
        }
    }
    private void NailCheck()
    {
        if (!NailEffectDic.ContainsValue(true) && !KnockNail)
        {
            enabled = false;
            myStatus.IsNail = false;
        }
        else
        {
            enabled = true;
            myStatus.IsNail = true;
        }
    }
}
