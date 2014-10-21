using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public partial class PlayerCtrl
{
    static Vector2 TouchWorldPoint;//觸碰點對應到世界座標
    static public GameObject EventUI;
    static public UIButton[] eventUIButton;
    static public GameObject eventBubbleObj;
    static EventLoad el;
    private RoomCharaStatus charaStatus;
    static private bool MonsterActionSwitch;
    static public bool MonsterActionSwitchOn;
    static void EventUISetting()
    {
        SetCacheObj();
        el.StartSetting();
        for (int i = 0; i < eventUIButton.Length; i++)
        {
            eventUIButton[i].enabled = false;
        }
        EventUI.SetActive(false);
    }
    static void SetCacheObj()
    {
        EventUI = GameObject.FindGameObjectWithTag("EventUI");
        el = (EventLoad)EventUI.GetComponent<EventLoad>();
        eventUIButton = EventUI.GetComponentsInChildren<UIButton>();
    }

    IEnumerator DelaySecondsToCtrl()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < eventUIButton.Length; i++)
        {
            eventUIButton[i].enabled = true;
        }
        CtrlPower.StartCtrl(1);//開始控制EventUI
    }

    void EventTouch()
    {
        if (Input.touchCount == 1)
        {
            TouchWorldPoint = trainSceneCamera.ScreenToWorldPoint(Input.GetTouch(curTounchCount).position);
            if (Physics2D.OverlapPoint(TouchWorldPoint) != null)
            {
                CurTouchTargets = Physics2D.OverlapPointAll(TouchWorldPoint);
                for (int i = 0; i < CurTouchTargets.Length; i++)
                {
                    switch (CurTouchTargets[i].tag)
                    {
                        case "EventBubble":
                            CurCtrlTarget = CurTouchTargets[i].gameObject;
                            ShowEventUI();
                            break;
                        case "RoomChara":
                            CurCtrlTarget = CurTouchTargets[i].gameObject;
                            if (!CurCtrlTarget.GetComponent<PlayerMonster>().OnEvent)
                            {
                                ShowTouchText(CurCtrlTarget);
                            }
                            else
                            {
                                CurCtrlTarget = CurCtrlTarget.transform.FindChild("eventBubble").gameObject;//將物件改為選取EventBubble
                                ShowEventUI();
                            }
                            break;
                        case "RoomResource":
                            CurTouchTargets[i].transform.GetComponent<RoomResource>().TakeResource();
                            break;
                    }
                }
            }
        }
    }
    void ShowTouchText(GameObject target)
    {
        target.GetComponent<PlayerMonster>().ShowTouchText();
    }

    void ShowEventUI()
    {
        CtrlPower.StartCtrl(-1);//-1為關閉所有控制權
        SetCurMonsterID(CurCtrlTarget);
        StopMonsterEventAction(CurCtrlTarget);
        TrainScene.CurEventID = CurCtrlTarget.transform.parent.GetComponent<PlayerMonster>().EventID;
        EventUI.SetActive(true);//預設是inActive所以先打開
        EventLoad.OnEvent = true;//設定為正執行事件介面
        EventLoad.LoadValues();
        StartCoroutine("DelaySecondsToCtrl");
        SwitchAllMonsterAction(false);//暫停怪獸動作
        MonsterActionSwitchOn = false;
    }
    public static void SwitchAllMonsterAction(bool _on)//怪獸動作開關
    {
        if (MonsterActionSwitchOn)
        {
            MonsterActionSwitch = _on;
            List<int> keys = new List<int>(Player.MonsterDic.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                Player.MonsterDic[keys[i]].SwitchMonsterAction(MonsterActionSwitch);
            }
        }
    }
    void SetCurMonsterID(GameObject target)
    {
        charaStatus = (RoomCharaStatus)target.transform.parent.GetComponent<RoomCharaStatus>();
        Player.curMonsterID = charaStatus.monsterID;
        //eventBubbleObj = target;
        //eventBubbleObj.SetActive(false);
    }

    void StopMonsterEventAction(GameObject target)
    {
        target.transform.parent.GetComponent<RoomCharaStatus>().Action(0, 0);
    }
}
