using UnityEngine;
using System.Collections;

public class GoMap : MonoBehaviour
{

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(2))
        {
            // PlayerCtrl.startCtrl(2);
            RoomCtrl.curGoUILevel = 1;
            //OpenGoUI();
            RoomCtrl.GoUICtrl();
            MapUI.LoadMapData();
        }

    }
}
