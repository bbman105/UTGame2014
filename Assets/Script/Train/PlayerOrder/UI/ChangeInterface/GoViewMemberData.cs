using UnityEngine;
using System.Collections;

public class GoViewMemberData : MonoBehaviour
{


    void OnPress()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            RoomCtrl.curPartyUILevel = 7;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
        }
    }
}
