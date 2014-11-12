using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{
    //紀錄未改變前的房間快樂度(在房間的所有怪獸快樂度)，動畫顯示用
    int LastRoomHappy;
    //怪物快樂度
    private int happy;
    public int Happy
    {
        get { return happy; }
        protected set
        {
            if (value > MaxHappy)
                happy = MaxHappy;
            else if (value < MinHappy)
                happy = MinHappy;
            else
                happy = value;
        }
    }
    //最大快樂度
    private const int MaxHappy = 100;
    //最小快樂度
    private const int MinHappy = 0;
    //室友數量(包含自身)
    int RoomMateNum;
    public Room MyRoom;//處在的房間
    //快樂度介面
    HappyBar myHappyBar;
    /// <summary>
    /// 傳入房間，取得房間的快樂度需求資訊
    /// </summary>
    public void SetMonsterHappyInRoom(Room _room)//取得房間的快樂度需求資訊
    {
        MyRoom = _room;
        myHappyBar = MyRoom.transform.FindChild("happyBar").GetComponent<HappyBar>();
    }
    /// <summary>
    /// 取得所在房間的室友數量(包含自身)
    /// </summary>
    public int GetRoomMateNum()//取得室友數量
    {
        RoomMateNum = MyRoom.OwnMonsterDic.Count;
        return RoomMateNum;
    }
    /// <summary>
    /// 起始設至快樂度，傳入要設定的值
    /// </summary>
    public void StartSetHappy(int _setHappy)
    {
        Happy = _setHappy;
        //MyRoom.UpdateRoomHappy();//更新房間的快樂度
    }
    /// <summary>
    /// 增加快樂度，傳入要增加的快樂度
    /// </summary>
    public void AddHappy(int _addHappy)
    {
        LastRoomHappy = GetRoomHappy();//取得增加快樂度前此房間的快樂度總值
        Happy += _addHappy;
        IODataFromArcalet.SetPlayerMonster("Happy", Happy.ToString(), MonsterID);//向Server寫入玩者怪獸
        MonsterHappyChangeEvent();//快樂度改變時呼叫此方法
    }
    /// <summary>
    /// 設定快樂度到多少，傳入要設定的值
    /// </summary>
    public void SetHaapy(int _setHappy)
    {
        LastRoomHappy = GetRoomHappy();//取得增加快樂度前此房間的快樂度總值
        Happy = _setHappy;
        MonsterHappyChangeEvent();//快樂度改變時呼叫此方法
    }
    /// <summary>
    /// 取得此房間的快樂度總值
    /// </summary>
    public int GetRoomHappy()//取得所在房間的快樂度總值
    {
        int myRoomHappy = MyRoom.RoomHappy;
        return myRoomHappy;
    }
    /// <summary>
    /// 取得此房間的最大快樂度
    /// </summary>
    public int GetRoomMaxHappy()//取得此房間的最大快樂度
    {
        int myRoomMaxHappy = MyRoom.RoomMaxHappy;
        return myRoomMaxHappy;
    }
    /// <summary>
    /// 快樂度改變時呼叫此方法
    /// </summary>
    void MonsterHappyChangeEvent()
    {
        MyRoom.UpdateRoomHappy();//更新房間的快樂度
        myHappyBar.DisplayAddHappy(LastRoomHappy, MyRoom.RoomHappy, MyRoom.RoomMaxHappy,MyRoom.OwnMonsterDic.Count);
        UpdateResourceQuality();//更新生產資源價值，快樂度改變或進化會呼叫此方法
    }
}

