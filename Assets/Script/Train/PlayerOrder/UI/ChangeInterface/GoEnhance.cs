using UnityEngine;
using System.Collections;

public class GoEnhance : MonoBehaviour
{
    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            EnhanceUI.LoadEnhanceMonsterData(0);//傳入選取到的怪獸資料給強化介面
            RoomCtrl.curPartyUILevel = 8;
            RoomCtrl.PartyUICtrl();//跳至介面
        }
    }
}
