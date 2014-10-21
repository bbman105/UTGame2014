using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{
    bool CanTriggerDailyEvent;//是否可以觸發日常事件
    float DailyEventTime;//觸發的時間間隔
    float DailyEventTimer;//觸發計時器
    byte EventType { get; set; }//觸發的日常事件種類
    public byte ShitCurNum;//目前的大便量
    public byte ShitMaxNum;//大便上限量
    public bool OnVirus;//是否正在生病
    public bool OnGhost;//是否正在鬧鬼
    bool IsSleeping;//是否在睡覺
    List<EventWeight> EventList;//事件類型清單
    EventWeight ShitEvent;//大便事件
    EventWeight VirusEvent;//生病事件
    EventWeight GhostEvent;//鬧鬼事件
    byte AllWeight;//可觸發的事件清單中事件總權重
    struct EventWeight
    {
        public byte EventType;//事件類型
        public byte Weight;//權重
    }
    private void StartSetDailyEvent()
    {
        CanTriggerDailyEvent = true;
        IsSleeping = false;
        ShitMaxNum = 5;
        DailyEventTime = 20f;//每4分鐘檢定一次
        DailyEventTimer = DailyEventTime;
        SetEventWeight();//設定各事件類型權重
    }
    private void SetEventWeight()//設定各事件類型權重
    {
        EventList = new List<EventWeight>();
        //大便事件設定
        ShitEvent = new EventWeight();
        ShitEvent.EventType = 0;
        ShitEvent.Weight = 3;
        //生病事件設定
        VirusEvent = new EventWeight();
        VirusEvent.EventType = 1;
        VirusEvent.Weight = 1;
        //鬧鬼事件設定
        GhostEvent = new EventWeight();
        GhostEvent.EventType = 2;
        GhostEvent.Weight = 1;
    }
    private void SetAvailable()//將清單加入可被觸發的事件，並設定總重
    {
        EventList.Clear();
        AllWeight = 0;
        if (!IsSleeping)//沒在睡覺時才會觸發大便跟生病
        {
            if (ShitCurNum < ShitMaxNum)//大便數量還沒到上限
            {
                EventList.Add(ShitEvent);
            }
            if (!OnVirus)//怪物未生病
            {
                EventList.Add(VirusEvent);
            }
        }
        if (!Player.RoomDic[InRoomID].OnGhost)//怪物所在的房間還沒鬧鬼
            EventList.Add(GhostEvent);
        //計算總權重
        for (int i = 0; i < EventList.Count; i++)
        {
            AllWeight += EventList[i].Weight;
        }
    }
    private void DailyEventTimerFunction()//日常事件的計時觸發
    {
        if (CanTriggerDailyEvent)
        {
            if (DailyEventTimer > 0)
            {
                DailyEventTimer -= Time.deltaTime;
            }
            else
            {
                RndDailyEventTrigger();//產生日常事件
                DailyEventTimer = DailyEventTime;
            }
        }
    }
    public void RndDailyEventTrigger()//產生日常事件
    {
        byte triggerRate = 0;//觸發機率 4=40% ,6=60%,小於0不出,大於10必出
        if (IsSleeping)//不在睡覺時觸發機率40%,睡覺時觸發機率20%
            triggerRate = 4;
        else
            triggerRate = 2;
        int rndTrigger = UnityEngine.Random.Range(1, 11);
        if (rndTrigger <= triggerRate)//觸發
        {
            SetAvailable();//將清單加入可被觸發的事件
            if (AllWeight != 0)
            {
                int rndWeight = UnityEngine.Random.Range(1, AllWeight + 1);
                for (int i = 0; i < EventList.Count; i++)
                {
                    if (rndWeight <= EventList[i].Weight)
                    {
                        EventType = EventList[i].EventType;
                        break;
                    }
                    else
                        rndWeight -= EventList[i].Weight;
                }
                SpawnResource();//產生資源
            }
        }
    }
    private void SpawnResource()//產生資源
    {
        RoomResourceSpawner.SpawnResource(this, EventType, ResourceQuality);//產生資源物件
    }
}
