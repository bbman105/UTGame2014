using UnityEngine;
using System.Collections;

public class GO : MonoBehaviour
{
    public static UISprite GoIcon;
    public static UILabel GoText;
    public void StartSet()
    {
        GoIcon = transform.FindChild("icon").GetComponent<UISprite>();
        GoText = transform.FindChild("label").GetComponent<UILabel>();
    }
    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(20))//底下UI介面控制權開啟時才可點取
        {
            //cancelOnEdit();
            if (RoomCtrl.curGoUILevel == 0)
            {
                ChangeToHome();
                if (EventLoad.OnEvent)//如果正在執行Event介面則關閉
                    RoomCtrl.CloseEventUI();//隱藏事件UI

                if (RoomCtrl.lastGoUILevel == 0)
                {
                    RoomCtrl.curGoUILevel++;
                }
                else
                {
                    RoomCtrl.curGoUILevel = RoomCtrl.lastGoUILevel;
                }
                //setCtrl
                CtrlPower.StartCtrl(2);
                Party.ChangeToMonster();
                MapUI.LoadMapData();
                RoomCtrl.InActiveRoom();//InActive所有怪物房間
            }
            else
            {
                ChangeToVenture();
                RoomCtrl.curGoUILevel = 0;
                CtrlPower.StartCtrl(0);
                RoomCtrl.OnlyActiveSeeRoom();//只Active看到的怪物房間
            }
            RoomCtrl.GoUICtrl();
        }
    }

    public static void ChangeToHome()
    {
        GoIcon.spriteName = "main-bottom-icon-home";
        GoIcon.MakePixelPerfect();
        GoText.text = "回小屋";
    }
    public static void ChangeToVenture()
    {
        GoIcon.spriteName = "main-bottom-gobuttom-icon";
        GoIcon.MakePixelPerfect();
        GoText.text = "冒險";
    }

    void cancelOnEdit()
    {
        RoomCtrl.curGoUILevel = 0;
        RoomCtrl.editRoom = false;
    }
}
