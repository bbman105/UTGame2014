using UnityEngine;
using System.Collections;

public class PlayerTeam{

    //擁有這些隊員的隊伍UID
    private int teamID;
    public int TeamID
    {
      get { return teamID; }
      set { teamID = value; }
    }

    //擁有此隊伍的玩家UID
    private int ownPlayerID;
    public int OwnPlayerID
    {
        get { return ownPlayerID; }
        set { ownPlayerID = value; }
    }
    //隊員陣列UID
    private int[] member;
    public int[] Member
    {
        get { return member; }
        set { member = value; }
    }
    string teamName;
    public string TeamName
    {
        get { return teamName; }
        set { teamName = value; }
    }
    private int memberNum;
    public int MemberNum
    {
        get { return memberNum; }
        set { memberNum = value; }
    }

    //隊伍結構建構式
    public PlayerTeam(int _teamID, int _ownPlayerID,string _teamName ,int[] _member)
    {
        TeamID = _teamID;
        OwnPlayerID = _ownPlayerID;
        TeamName = _teamName;
        Member = _member;
        for(int i=0;i<3;i++)
        {
            if (Member[i] != 0) MemberNum++;
        }        
    }


}
