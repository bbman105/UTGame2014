using UnityEngine;
using System.Collections;

public class ChangeSkill : MonoBehaviour
{
    private int changeMonsterID;
    public int ChangeMonsterID
    {
        get { return changeMonsterID; }
        set { changeMonsterID = value; }
    }
    private int changeSpellID;
    public int ChangeSpellID
    {
        get { return changeSpellID; }
        set { changeSpellID = value; }
    }



    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            if (RoomCtrl.curPartyUILevel == 6)
            {
                Player.ChangeMonsterSkill(ChangeMonsterID, ChangeSpellID);
            }
            //讀取被選取的怪物資料
            EditMemberData.LoadMonsterData();
            RoomCtrl.curPartyUILevel = 3;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
        }

    }
}
