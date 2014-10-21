using UnityEngine;
using System.Collections;

public class Party : MonoBehaviour
{

    public static UISprite PartyIcon;
    public static UILabel PartyText;
    public void StartSet()
    {
        PartyIcon = transform.FindChild("party").GetComponent<UISprite>();
        PartyText = transform.FindChild("label").GetComponent<UILabel>();
    }

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(20))//底下UI介面控制權開啟時才可點取
        {
            //cancelOnEdit();
            if (RoomCtrl.curPartyUILevel == 0)
            {
                ChangeToHome();
                if (RoomCtrl.lastPartyUILevel == 0)
                {
                    RoomCtrl.curPartyUILevel++;
                }
                else
                {
                    RoomCtrl.curPartyUILevel = RoomCtrl.lastPartyUILevel;
                }
                if (EventLoad.OnEvent)//如果正在執行Event介面則關閉
                    RoomCtrl.CloseEventUI();//隱藏事件UI
                //setCtrl
                CtrlPower.StartCtrl(3);
                GO.ChangeToVenture();
                RoomCtrl.InActiveRoom();//InActive所有怪物房間
            }
            else
            {
                ChangeToMonster();
                RoomCtrl.curPartyUILevel = 0;
                //setCtrl
                CtrlPower.StartCtrl(0);
                RoomCtrl.OnlyActiveSeeRoom();//只Active看到的怪物房間
            }
            RoomCtrl.PartyUICtrl();
        }
    }

    public static void ChangeToHome()
    {
        PartyIcon.spriteName = "main-bottom-icon-home_small";
        PartyIcon.MakePixelPerfect();
        PartyText.text = "回小屋";
    }
    public static void ChangeToMonster()
    {
        PartyIcon.spriteName = "main-bottom-icon-monster";
        PartyIcon.MakePixelPerfect();
        PartyText.text = "怪獸";
    }

    void cancelOnEdit()
    {
        RoomCtrl.curPartyUILevel = 0;
        RoomCtrl.editRoom = false;
    }
}
