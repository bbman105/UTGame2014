using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;
public partial class Player : MonoBehaviour
{

    public static int RoomFloor { get; set; }//房間樓層 
    public static GameObject roomScene;
    public static GameObject TrainSceneBG;//培養背景
    public static GameObject roomResourceObj;//房間物件資源位置
    public static Dictionary<int, Room> RoomDic { get; set; }//房間字典
    public static List<GameObject> RoomObjList { get; set; }//房間物件清單
    public static int RoomNum { get; set; }//房間數量
    public static int MonsterCapacity;//怪獸上限容量，可擁有怪獸上限為房間數的3倍
    //讀取玩家擁有的房間
    public static void StartRoomSet(List<Dictionary<string, string>> _roomItemList)
    {
        Room.StartSetOwnResource();//起始設定房間就擁有的資源物件
        CreateRooms(_roomItemList);
    }

    /// <summary>
    /// 依造傳入的擁有房間字典，建立房間,_roomItemDic[propertyName]取得value
    /// </summary>
    private static void CreateRooms(List<Dictionary<string, string>> _roomItemList)
    {
        //尋找要建立房間的父物件
        roomScene = GameObject.FindGameObjectWithTag("RoomScene");
        TrainSceneBG = roomScene.transform.FindChild("BG").gameObject;
        //設定讀取怪物物件
        roomResourceObj = Resources.Load<GameObject>("GameObject/Room/Room");
        //設定房間建立基礎位置
        Vector3 roomBasePos = new Vector3(0, -2.6f, 0);
        //設定新增一個房間的位移距離
        Vector3 addPos = new Vector3(0, 5.14f, 0);
        //設定房間建立角度
        Quaternion roomRot = Quaternion.Euler(0f, 0f, 0f);
        //建立房間字典
        RoomDic = new Dictionary<int, Room>();
        //建立房間GameObject列表
        RoomObjList = new List<GameObject>();
        for (int i = 0; i < _roomItemList.Count; i++)
        {
            //建立房間GameObject
            RoomObjList.Add(Instantiate(roomResourceObj, roomBasePos + (addPos * RoomFloor), roomRot) as GameObject);
            //把房間放進父物件裡
            RoomObjList[RoomFloor].transform.parent = roomScene.transform;
            //在房間GameObject裡新增Component
            RoomObjList[RoomFloor].AddComponent<Room>();
            //取得此房間的component
            Room roomClass = RoomObjList[RoomFloor].GetComponent<Room>();
            //傳入propertyDic設定房間資料
            roomClass.StartSetRoomAttribute(_roomItemList[i], RoomFloor);
            //加到房間字典中
            AddRoomDictionary(roomClass);
            //房間數++
            RoomFloor++;
        }
    }
    public static void StartSetRoomResource()//加入房間擁有的資源物件
    {
        List<int> RoomDicBuffer = new List<int>(RoomDic.Keys);
        foreach (int RoomKey in RoomDicBuffer)
        {
            RoomDic[RoomKey].SetOwnResource();//從xml檔案中抓取屬於此房間的資源物件
        }
        Room.SetLeaveGameResource();//設定離開遊戲時經過時間所創造的資源物件
    }
    public static void AddRoomDictionary(Room _room)//增加房間字典
    {
        try
        {
            RoomDic.Add(_room.RoomID, _room);//加到怪物字典中
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

        RoomNum = RoomDic.Count;
        MonsterCapacity = 0;
        List<int> DicBuffer = new List<int>(RoomDic.Keys);
        foreach (int key in DicBuffer)
        {
            MonsterCapacity += RoomDic[key].RoomCapacity;
        }
    }
}
