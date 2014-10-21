using UnityEngine;
using System.Collections;

public class GoFormation : MonoBehaviour
{

    
    private int changeMonsterID;
    public int ChangeMonsterID
    {
        get { return changeMonsterID; }
        set { changeMonsterID = value; }
    }
    

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            
            if (RoomCtrl.curPartyUILevel == 2 && ChangeMonsterID != 0)//更換隊員介面點取怪物更換隊員
            {
                Player.ChangeTeamMonster(ChangeMonsterID);//更換隊員
            }
            
            RoomCtrl.curPartyUILevel = 5;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
            //讀取隊伍資料
            Formation.LoadMemberData();
        }
    }
}
