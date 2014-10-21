using UnityEngine;
using System.Collections;

public class TeamSelect : MonoBehaviour {

    public int actionCode;
    void OnClick()
    {
        switch(actionCode)
        {
            case 1:
                Formation.CurTmpTeamID++;                
                break;
            case 2:
                Formation.CurTmpTeamID--;
                break;
        }
        Formation.LoadMemberData();
    }
}
