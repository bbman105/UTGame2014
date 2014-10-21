using UnityEngine;
using System.Collections;

public class GoQuestData : MonoBehaviour
{

    public int QuestID { get; set; }
    public int QuestLevel { get; set; }

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(2))
        {
            RoomCtrl.curGoUILevel = 3;
            //OpenGoUI();
            RoomCtrl.GoUICtrl();
            QuestData.QuestID = this.QuestID;
            QuestData.QuestLevel = this.QuestLevel;
            QuestData.LoadMonsterSprite();
        }

    }
}
