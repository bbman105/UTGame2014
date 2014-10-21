using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stage : MonoBehaviour {

    private int stageID;
    public int StageID
    {
        get { return stageID; }
        set { stageID = value; }
    }

    private string mapName;
    public string MapName
    {
        get { return mapName; }
        set { mapName = value; }
    }

    private string questName;
    public string QuestName
    {
        get { return questName; }
        set { questName = value; }
    }

    //private Dictionary<int, List<NpcMonster>> RoundMonster;

    private int roundNum;
    public int RoundNum
    {
        get { return roundNum; }
        set { roundNum = value; }
    }



}
