using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GoEditMemberList : MonoBehaviour
{
    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            if (RoomCtrl.curPartyUILevel == 1 || RoomCtrl.curPartyUILevel == 3)//如果正在主畫面
            {
                RoomCtrl.curPartyUILevel = 4;
                RoomCtrl.PartyUICtrl();
                //讀取擁有的怪物圖像
                EditMemberList.LoadMonsterSprite();
            }
            else if (RoomCtrl.curPartyUILevel == 8)
            {
                RoomCtrl.curPartyUILevel = 9;
                RoomCtrl.PartyUICtrl();
                //讀取擁有的怪物圖像
                EditMemberList.LoadMonsterSprite();
            }
        }
    }
}
