using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FetterStatus : MonoBehaviour
{
    /*
    bool KnockFetter;
    float KnockFetterTimer;
    */
    Status myStatus;
    public Dictionary<int, bool> FetterEffectDic;

    public void FetterStatusStartSet(Status _status)
    {
        myStatus = _status;
        //KnockFetter = false;
        enabled = false;
        FetterEffectDic = new Dictionary<int, bool>();
        FetterEffectDic.Add(0, false);
    }
    
    void Update()
    {
        //FetterTimerFunction();
    }
    
    public void SetKnockFetter(float _fetterTime)
    {
        //KnockFetter = true;
        myStatus.IsFetter = true;
        enabled = true;
        //KnockFetterTimer = _fetterTime;
    }
    public void SetEffectFetter(int _fetterID, bool _isFetter)
    {
        if (_isFetter)
        {
            if (!FetterEffectDic.ContainsKey(_fetterID))
                FetterEffectDic.Add(_fetterID, _isFetter);
        }
        else
        {
            if (FetterEffectDic.ContainsKey(_fetterID))
                FetterEffectDic.Remove(_fetterID);
        }
        FetterCheck();
    }

    /*
    private void FetterTimerFunction()
    {
        if (KnockFetterTimer > 0)
        {
            KnockFetterTimer -= Time.deltaTime;
        }
        else
        {
            KnockFetter = false;
            KnockFetterTimer = 0;
            FetterCheck();
            enabled = false;
        }
    }
    */
    private void FetterCheck()
    {
        if (!FetterEffectDic.ContainsValue(true))
        {
            enabled = false;
            myStatus.IsFetter = false;
        }
        else
        {
            enabled = true;
            myStatus.IsFetter = true;
        }
    }
}
