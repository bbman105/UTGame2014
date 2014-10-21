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

    int[] questID;
    public int[] QuestID
    {
        get { return questID; }
        set { questID = value; }
    }



    //地圖建構式
    public Map(int _planetID, int _mapID, string _planetName, string _mapName, int[] _QuestID)
    {
        this.PlanetID = _planetID;
        this.MapID = _mapID;
        this.PlanetName = _planetName;
        this.MapName = _mapName;
        this.QuestID = _QuestID;
    }


}
