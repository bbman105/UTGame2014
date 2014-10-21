using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BlessingStatus : MonoBehaviour
{

    //bool KnockBlessing;
    //float KnockBlessingTimer;
    Status myStatus;
    public Dictionary<int, bool> BlessingEffectDic;

    public void BlessingStatusStartSet(Status _status)
    {
        myStatus = _status;
        //KnockBlessing = false;
        enabled = false;
        BlessingEffectDic = new Dictionary<int, bool>();
        BlessingEffectDic.Add(0, false);
    }
    void Update()
    {
        //BlessingTimerFunction();
    }
    public void SetKnockBlessing(float _blessingTime)
    {
        //KnockBlessing = true;
        myStatus.IsBlessing = true;
        enabled = true;
        //KnockBlessingTimer = _blessingTime;
    }
    public void SetEffectBlessing(int _blessingID, bool _isBlessing)
    {
        if (_isBlessing)
        {
            if (!BlessingEffectDic.ContainsKey(_blessingID))
                BlessingEffectDic.Add(_blessingID, _isBlessing);
        }
        else
        {
            if (BlessingEffectDic.ContainsKey(_blessingID))
                BlessingEffectDic.Remove(_blessingID);
        }
        BlessingCheck();
    }

    /*
    private void BlessingTimerFunction()
    {
        if (KnockBlessingTimer > 0)
        {
            KnockBlessingTimer -= Time.deltaTime;
        }
        else
        {
            KnockBlessing = false;
            KnockBlessingTimer = 0;
            BlessingCheck();
            enabled = false;
        }
    }
    */
    private void BlessingCheck()
    {
        if (!BlessingEffectDic.ContainsValue(true))
        {
            myStatus.IsBlessing = false;
            enabled = false;
        }
        else
        {
            enabled = true;
            myStatus.IsBlessing = true;
        }
    }
}
