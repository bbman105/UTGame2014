using UnityEngine;
using System.Collections;

public class PartyGoRoom : MonoBehaviour
{

    void OnClick()
    {
        Party.ChangeToMonster();
        RoomCtrl.curPartyUILevel = 0;
        //setCtrl
        CtrlPower.StartCtrl(0);
        RoomCtrl.PartyUICtrl();
        RoomCtrl.ActiveRoom();//Active所有怪物房間
    }
}
