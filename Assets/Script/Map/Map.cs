using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{

    int planetID;
    public int PlanetID
    {
        get { return planetID; }
        set { planetID = value; }
    }

    int mapID;
    public int MapID
    {
        get { return mapID; }
        set { mapID = value; }
    }

    string planetName;
    public string PlanetName
    {
        get { return planetName; }
        set { planetName = value; }
    }

    string mapName;
    public string MapName
    {
        get { return mapName; }
        set { mapName = value; }
    }
    public List<int> QuestIDList;
    //星球數量
    int planetNum;
    public int PlanetNum
    {
        get { return planetNum; }
        set { planetNum = value; }
    }
    //此星球的地圖數量
    int mapNum;
    public int MapNum
    {
        get { return mapNum; }
        set { mapNum = value; }
    }
    //此地圖的任務數量
    int questNum;
    public int QuestNum
    {
        get { return questNum; }
        set { questNum = value; }
    }

    //地圖建構式
    public Map(int _planetID, int _mapID, string _planetName, string _mapName, List<int> _questIDList)
    {
        QuestIDList = new List<int>();
        this.PlanetID = _planetID;
        this.MapID = _mapID;
        this.PlanetName = _planetName;
        this.MapName = _mapName;
        QuestIDList = _questIDList;
        QuestNum = QuestIDList.Count;
        MapNum = 5;
        PlanetNum = 1;
    }


}
