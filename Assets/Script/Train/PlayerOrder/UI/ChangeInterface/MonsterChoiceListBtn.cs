using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterChoiceListBtn : MonoBehaviour
{

    //此怪物的按鈕的怪物ID(此ID為怪物的UID)
    private int monsterID;
    public int MonsterID
    {
        get { return monsterID; }
        set { monsterID = value; }
    }

    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            if (RoomCtrl.curPartyUILevel == 4)//在選擇要編輯的怪物中
            {
                EditMemberData.curMonster = Player.MonsterDic[MonsterID];
                //讀取被選取的怪物資料
                Player.curMonsterID = MonsterID;
                EditMemberData.LoadMonsterData();
                RoomCtrl.curPartyUILevel = 3;
                RoomCtrl.PartyUICtrl();//跳至介面
            }
            else if (RoomCtrl.curPartyUILevel == 6)
            {
                EditMemberData.curMonster = Player.MonsterDic[Player.curMonsterID];
                //讀取被選取的怪物資料
                EditMemberData.LoadMonsterData();
                RoomCtrl.curPartyUILevel = 3;
                RoomCtrl.PartyUICtrl();//跳至介面
            }
            else if (RoomCtrl.curPartyUILevel == 9)//在選擇要強化的怪物中
            {
                EnhanceUI.LoadEnhanceMonsterData(MonsterID);//傳入選取到的怪獸資料給強化介面
                RoomCtrl.curPartyUILevel = 8;
                RoomCtrl.PartyUICtrl();//跳至介面
            }
        }
    }





}
