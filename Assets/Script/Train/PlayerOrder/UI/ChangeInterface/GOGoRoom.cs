using UnityEngine;
using System.Collections;

public class GOGoRoom : MonoBehaviour
{

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(2))
        {
            RoomCtrl.curGoUILevel = 0;
            GO.ChangeToVenture();
            for (int i = 0; i < PlayerCtrl.eventUIButton.Length; i++)
            {
                PlayerCtrl.eventUIButton[i].enabled = false;
            }
            //setCtrl
            CtrlPower.StartCtrl(0);
            RoomCtrl.GoUICtrl();
            RoomCtrl.OnlyActiveSeeRoom();//只Active看到的怪物房間
        }
    }
}
