using UnityEngine;
using System.Collections;

public class GoSkillList : MonoBehaviour
{
    private int selectedMonsterID;
    public int SelectedMonsterID
    {
        get { return selectedMonsterID; }
        set { selectedMonsterID = value; }
    }

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            //讀取被選取的怪物資料
            SkillList.LoadSkillData(SelectedMonsterID);
            CtrlPower.StartCtrl(3);
            RoomCtrl.curPartyUILevel = 6;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
        }
    }
}
