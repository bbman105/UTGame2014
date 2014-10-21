using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FearStatus : MonoBehaviour
{

    //bool KnockFear;
    //float KnockFearTimer;
    Status myStatus;
    public Dictionary<int, bool> FearEffectDic;

    public void FearStatusStartSet(Status _status)
    {
        myStatus = _status;
        //KnockFear = false;
        enabled = false;
        FearEffectDic = new Dictionary<int, bool>();
        FearEffectDic.Add(0, false);
    }
    void Update()
    {
        //FearTimerFunction();
    }
    public void SetKnockFear(float _fearTime)
    {
        //KnockFear = true;
        myStatus.IsFear = true;
        enabled = true;
        //KnockFearTimer = _fearTime;
    }
    public void SetEffectFear(int _fearID, bool _isFear)
    {
        if (_isFear)
        {
            if (!FearEffectDic.ContainsKey(_fearID))
                FearEffectDic.Add(_fearID, _isFear);
        }
        else
        {
            if (FearEffectDic.ContainsKey(_fearID))
                FearEffectDic.Remove(_fearID);
        }
        FearCheck();
    }

    /*
    private void FearTimerFunction()
    {
        if (KnockFearTimer > 0)
        {
            KnockFearTimer -= Time.deltaTime;
        }
        else
        {
            KnockFear = false;
            KnockFearTimer = 0;
            FearCheck();
            enabled = false;
        }
    }
    */
    private void FearCheck()
    {
        if (!FearEffectDic.ContainsValue(true))// && !KnockFear
        {
            myStatus.IsFear = false;
            enabled = false;
        }
        else
        {
            enabled = true;
            myStatus.IsFear = true;
        }
    }
}
