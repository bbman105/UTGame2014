using UnityEngine;
using System.Collections;

public class GoFightScene : MonoBehaviour
{

    public static int QuestID { get; set; }
    private int fightTeamID;
    public int FightTeamID
    {
        get { return fightTeamID; }
        set { fightTeamID = value; }
    }
    public static int QuestLevel { get; set; }

    void OnClick()
    {
        Application.LoadLevel("FightScene");
        ForceCondition.PlayerTeamID = this.FightTeamID;
        ForceCondition.QuestID = QuestID;
        ForceCondition.QuestLevel = QuestLevel;        
    }
}
