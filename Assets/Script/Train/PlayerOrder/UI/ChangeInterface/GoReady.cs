using UnityEngine;
using System.Collections;

public class GoReady : MonoBehaviour
{

    private int questID;
    public int QuestID
    {
        get { return questID; }
        set { questID = value; }
    }
    public int QuestLevel { get; set; }


    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(2))
        {
            RoomCtrl.curGoUILevel = 4;
            //OpenGoUI();
            RoomCtrl.GoUICtrl();
            ReadyUI.LoadTeamData();

            //將目前選取的任務ID給予ReadyUI介面
            GoFightScene.QuestID = this.QuestID;
            GoFightScene.QuestLevel = this.QuestLevel;
        }
    }
}
