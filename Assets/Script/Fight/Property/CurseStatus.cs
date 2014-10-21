using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CurseStatus : MonoBehaviour
{


    //bool KnockCurse;
    //float KnockCurseTimer;
    Status myStatus;
    public Dictionary<int, bool> CurseEffectDic;

    public void CurseStatusStartSet(Status _status)
    {
        myStatus = _status;
        //KnockCurse = false;
        enabled = false;
        CurseEffectDic = new Dictionary<int, bool>();
        CurseEffectDic.Add(0, false);
    }
    void Update()
    {
        //CurseTimerFunction();
    }
    public void SetKnockCurse(float _curseTime)
    {
        //KnockCurse = true;
        myStatus.IsCurse = true;
        enabled = true;
        //KnockCurseTimer = _curseTime;
    }
    public void SetEffectCurse(int _curseID, bool _isCurse)
    {
        if (_isCurse)
        {
            if (!CurseEffectDic.ContainsKey(_curseID))
                CurseEffectDic.Add(_curseID, _isCurse);
        }
        else
        {
            if (CurseEffectDic.ContainsKey(_curseID))
                CurseEffectDic.Remove(_curseID);
        }
        CurseCheck();
    }

    /*
    private void CurseTimerFunction()
    {
        if (KnockCurseTimer > 0)
        {
            KnockCurseTimer -= Time.deltaTime;
        }
        else
        {
            KnockCurse = false;
            KnockCurseTimer = 0;
            CurseCheck();
            enabled = false;
        }
    }
    */
    private void CurseCheck()
    {
        if (!CurseEffectDic.ContainsValue(true))
        {
            myStatus.IsCurse = false;
            enabled = false;
        }
        else
        {
            enabled = true;
            myStatus.IsCurse = true;
        }
    }
}
