using UnityEngine;
using System.Collections;

public class GoMenu : MonoBehaviour
{

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            CtrlPower.StartCtrl(3);
            RoomCtrl.curPartyUILevel = 1;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
        }
    }
}
