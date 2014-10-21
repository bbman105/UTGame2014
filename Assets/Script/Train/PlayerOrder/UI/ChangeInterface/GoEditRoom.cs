using UnityEngine;
using System.Collections;

public class GoEditRoom : MonoBehaviour
{

    void OnClick()
    {
        if (!RoomCtrl.editRoom)
        {
            RoomCtrl.editRoom = true;
            CtrlPower.StartCtrl(7);
            RoomCtrl.curGoUILevel = 5;
        }
        else
        {
            RoomCtrl.editRoom = false;
            CtrlPower.StartCtrl(0);
            RoomCtrl.curGoUILevel = 0;
        }
        //OpenGoUI();
        RoomCtrl.GoUICtrl();
    }
}
