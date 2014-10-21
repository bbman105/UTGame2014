using UnityEngine;
using System.Collections;

public class RoomResourceData
{
    public int ResourceID;//資源ID
    public byte EventType;//事件種類
    public int Value;//資源價值
    public Vector3 ObjPosition;//資源母物件位置
    public Vector3 ObjScale;//資源母物件大小
    public PlayerMonster OwnMonster;//擁有此資源的怪物
    public int MonsterID;
    public RoomResourceData(PlayerMonster _ownMonster, int _resourceID, byte _eventType, int _value, Vector3 _pos, Vector3 _scale)
    {
        OwnMonster = _ownMonster;
        MonsterID = OwnMonster.MonsterID;
        ResourceID = _resourceID;
        EventType = _eventType;
        Value = _value;
        ObjPosition = _pos;
        ObjScale = _scale;
    }
}
