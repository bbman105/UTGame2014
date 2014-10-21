using UnityEngine;
using System.Collections;

public class EventChoiceButton : MonoBehaviour
{
    static private float closeTimer;
    static private float closeTime;
    static private bool onCloseUI;
    static PlayerMonster playerMonster;
    static IShowText showFeedBackText;
    static IShowText showRewardText;


    // Use this for initialization
    void Awake()
    {
        onCloseUI = false;
        closeTime = 0.2f;
        closeTimer = 0;
        //SetCacheObj();
    }
    void Update()
    {
        OnChoiceFunction();
    }
    void OnChoiceFunction()
    {
        if (onCloseUI)
        {
            if (closeTimer > 0)
            {
                closeTimer -= Time.deltaTime;
            }
            else
            {
                EventLoad.OnEvent = false;
                PlayerCtrl.EventUI.SetActive(false);
                closeTimer = 0;
                onCloseUI = false;
                CtrlPower.StartCtrl(0);//開始控制培養介面
            }
        }
    }



    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(1))
        {
            SetTargetMonster();
            switch (transform.tag)
            {
                case "CloseEventUI":
                    CloseEvent();
                    break;
                case "NaturalChoice":
                    CloseEvent();
                    ChoseOption(0);
                    break;
                case "MutationChoice":
                    CloseEvent();
                    ChoseOption(1);
                    break;
                case "WildChoice":
                    CloseEvent();
                    ChoseOption(2);
                    break;
            }
        }
    }
    static void SetTargetMonster()
    {
        playerMonster = (PlayerMonster)Player.monster[Player.curMonsterID].GetComponent<PlayerMonster>();
    }


    public static void CloseUI()//突發介面而關閉事件視窗
    {
        SetTargetMonster();
        EventLoad.OnEvent = false;
        playerMonster.RemoveEvent();//移除事件
        CtrlPower.StopCtrl(1);//停止控制EVENTUI
        PlayerCtrl.EventUI.SetActive(false);
        PlayerCtrl.MonsterActionSwitchOn = true;
        PlayerCtrl.SwitchAllMonsterAction(true);//恢復怪獸動作
    }

    public static void CloseEvent()//透過點取事件關閉程序
    {
        playerMonster.RemoveEvent();//移除事件
        if (!onCloseUI) closeTimer = closeTime;
        onCloseUI = true;
        CtrlPower.StopCtrl(1);//停止控制EVENTUI
        PlayerCtrl.MonsterActionSwitchOn = true;
        PlayerCtrl.SwitchAllMonsterAction(true);//恢復怪獸動
    }

    void ChoseOption(int _choiceCase)
    {
        playerMonster.ShowFeedBackText(_choiceCase);
    }

}
