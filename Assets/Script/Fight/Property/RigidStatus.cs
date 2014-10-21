using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RigidStatus : MonoBehaviour
{

    GameObject targetTriggerObject;
    GameObject myGameObject;
    bool EnterRigid;
    //float EnterRigidTimer;
    Status myStatus;
    public Dictionary<int, bool> RigidEffectDic;

    public void RigidStatusStartSet(Status _status)
    {
        myStatus = _status;
        myGameObject = myStatus.gameObject;
        EnterRigid = false;
        enabled = false;
        RigidEffectDic = new Dictionary<int, bool>();
        RigidEffectDic.Add(0, false);
    }
    void Update()
    {
        //RigidTimerFunction();
    }
    void OnTriggerExit2D(Collider2D _targetTrigger)
    {
        targetTriggerObject = _targetTrigger.gameObject;
        if (targetTriggerObject.tag == "LWall" && myStatus.Alive && EnterRigid)//如果離開的碰撞物件目標為Wall，正處於進場剛體狀態，且自己又還活著，就設定為非進場中的腳色
        {
            if (LayerMask.LayerToName(myGameObject.layer) == "P1CharaEnter")
            {
                myGameObject.layer = LayerMask.NameToLayer("P1Chara");//要在這裡宣告myLayer，因為layer可能是值不是參考
                RemoveEnterRigid();
            }
        }
        else if (targetTriggerObject.tag == "RWall" && myStatus.Alive && EnterRigid)//如果離開的碰撞物件目標為Wall，正處於進場剛體狀態，且自己又還活著，就設定為非進場中的腳色
        {
            if (LayerMask.LayerToName(myGameObject.layer) == "P2CharaEnter")
            {
                myGameObject.layer = LayerMask.NameToLayer("P2Chara");//要在這裡宣告myLayer，因為layer可能是值不是參考
                RemoveEnterRigid();
            }
        }
    }
    public void RemoveEnterRigid()//移除進場鋼體
    {
        EnterRigid = false;
        RigidCheck();
        enabled = false;
    }
    public void SetEnterRigid()
    {
        EnterRigid = true;
        myStatus.IsRigid = true;
        enabled = true;

        //EnterRigidTimer = _isEnter;
    }
    public void SetEffectRigid(int _rigidD, bool _isRigid)
    {
        if (_isRigid)
        {
            if (!RigidEffectDic.ContainsKey(_rigidD))
                RigidEffectDic.Add(_rigidD, _isRigid);
        }
        else
        {
            if (RigidEffectDic.ContainsKey(_rigidD))
                RigidEffectDic.Remove(_rigidD);
        }
        RigidCheck();
    }


    private void RigidTimerFunction()
    {
        /*
        if (EnterRigidTimer > 0)
        {
            EnterRigidTimer -= Time.deltaTime;
        }
        else
        {
            EnterRigid = false;
            EnterRigidTimer = 0;
            RigidCheck();
            enabled = false;
        }
        */
    }
    private void RigidCheck()
    {
        if (!RigidEffectDic.ContainsValue(true) && !EnterRigid)
        {
            enabled = false;
            myStatus.IsRigid = false;
        }
        else
        {
            enabled = true;
            myStatus.IsRigid = true;
        }
    }
}
