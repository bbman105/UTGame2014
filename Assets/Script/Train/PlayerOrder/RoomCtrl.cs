using UnityEngine;
using System.Collections;

public class RoomCtrl : TrainSceneUICtrl
{

    public static bool editRoom;
    public static int curGoUILevel;
    public static int lastGoUILevel;
    public static int curPartyUILevel;
    public static int lastPartyUILevel;
    public static Room curRoom;

    void Update()
    {

    }
    public static void StartSetting()
    {
        curGoUILevel = 0;
        lastGoUILevel = 0;
        curPartyUILevel = 0;
        lastPartyUILevel = 0;
        LoadingScene.ChangeSceneFunction += ChangeScene;
    }
    //RoomCtrl.curGoUILevel

    public static new void ChangeScene()//切換場景
    {
        if (Application.loadedLevelName == "TrainScene")
        {
            RoomCtrl.InActiveRoom();//InActive所有怪物房間
            curPartyUILevel = 0;
            curGoUILevel = 2;//戰鬥完回到任務選擇介面
            QuestUI.LoadQuestData();//讀取任務資訊
            GoUICtrl();
        }
    }

    public static void PartyUICtrl()
    {
        switch (curPartyUILevel)
        {
            case 0:
                ClosePartyUI();
                break;
            case 1://編輯介面Menu
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(true);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 2://更換成員清單介面changeMemberList
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(true);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 3://編輯怪獸介面editMemberData
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(true);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 4://選擇要編輯的怪獸清單介面
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(true);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 5://編輯隊伍介面formation
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(true);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 6://編輯技能清單介面skillList
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(true);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 7://編輯怪獸介面? viewMemberData
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
            case 8://強化怪獸介面
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(false);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(true);
                break;
            case 9://選擇強化怪獸清單介面
                CloseGoUI();
                partyUI.SetActive(true);
                menuUI.SetActive(false);
                changeMemberListUI.SetActive(false);
                editMemberDataUI.SetActive(false);
                editMemberListUI.SetActive(true);
                formationUI.SetActive(false);
                skillListUI.SetActive(false);
                EnhanceMonsterUI.SetActive(false);
                break;
        }
    }
    public static void OnlyActiveSeeRoom()//只顯示看到的房間
    {
        Player.TrainSceneBG.SetActive(true);
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            if (i > PlayerCtrl.CurFloor + 2 || i < PlayerCtrl.CurFloor)
            {
                Player.RoomObjList[i].SetActive(false);
            }
            else
            {
                Player.RoomObjList[i].SetActive(true);
            }
        }
    }
    public static void DisplayActiveRoomHappyBar()//只顯示可看到房間的快樂度
    {
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            if (Player.RoomObjList[i].activeInHierarchy)
            {
                curRoom = Player.RoomObjList[i].GetComponent<Room>();
                curRoom.myHappyBar.DragDisplayHappy(curRoom.RoomHappy, curRoom.RoomMaxHappy,curRoom.OwnMonsterDic.Count);
            }
        }
    }
    public static void HideActiveRoomHappyBar()//隱藏房間快樂度
    {
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            if (Player.RoomObjList[i].activeInHierarchy)
            {
                curRoom = Player.RoomObjList[i].GetComponent<Room>();
                curRoom.myHappyBar.DragEndHideHappy();
            }
        }
    }
    public static void OnlyActiveDragSeeRoom(bool _up)//只顯示拖曳方向會看到的房間
    {
        Player.TrainSceneBG.SetActive(true);
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            if (!_up)
            {
                if (i > PlayerCtrl.CurFloor + 4 || i < PlayerCtrl.CurFloor)
                {
                    Player.RoomObjList[i].SetActive(false);
                }
                else
                {
                    Player.RoomObjList[i].SetActive(true);
                }
            }
            else
            {
                if (i < PlayerCtrl.CurFloor - 4 || i > PlayerCtrl.CurFloor + 2)
                {
                    Player.RoomObjList[i].SetActive(false);
                }
                else
                {
                    Player.RoomObjList[i].SetActive(true);
                    curRoom = Player.RoomObjList[i].GetComponent<Room>();
                    curRoom.myHappyBar.DragDisplayHappy(curRoom.RoomHappy, curRoom.RoomMaxHappy, curRoom.OwnMonsterDic.Count);//拖曳時顯示快樂度
                }
            }
        }
    }
    public static void ActiveRoom()
    {
        Player.TrainSceneBG.SetActive(true);
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            Player.RoomObjList[i].SetActive(true);
        }
    }
    public static void InActiveRoom()
    {
        Player.TrainSceneBG.SetActive(false);
        for (int i = 0; i < Player.RoomObjList.Count; i++)
        {
            Player.RoomObjList[i].SetActive(false);
        }
    }

    public static void GoUICtrl()
    {
        switch (curGoUILevel)
        {
            case 0:
                CloseGoUI();
                //editObj.GetComponent<UISprite>().spriteName = "editbuttom-edit";
                //editObj.GetComponent<UISprite>().MakePixelPerfect();
                break;
            case 1:
                ClosePartyUI();
                goUI.SetActive(true);
                mapChoiceUI.SetActive(true);
                questChoiceUI.SetActive(false);
                questDataUI.SetActive(false);
                readyUI.SetActive(false);
                //editObj.SetActive(false);
                break;
            case 2:
                ClosePartyUI();
                goUI.SetActive(true);
                mapChoiceUI.SetActive(false);
                questChoiceUI.SetActive(true);
                questDataUI.SetActive(false);
                readyUI.SetActive(false);
                //editObj.SetActive(false);
                break;
            case 3:
                ClosePartyUI();
                goUI.SetActive(true);
                mapChoiceUI.SetActive(false);
                questChoiceUI.SetActive(false);
                questDataUI.SetActive(true);
                readyUI.SetActive(false);
                //editObj.SetActive(false);
                break;
            case 4:
                ClosePartyUI();
                goUI.SetActive(true);
                mapChoiceUI.SetActive(false);
                questChoiceUI.SetActive(false);
                questDataUI.SetActive(false);
                readyUI.SetActive(true);
                //editObj.SetActive(false);
                break;
            case 5:
                ClosePartyUI();
                goUI.SetActive(false);
                mapChoiceUI.SetActive(false);
                questChoiceUI.SetActive(false);
                questDataUI.SetActive(false);
                readyUI.SetActive(false);
                //editObj.SetActive(true);
                //editObj.GetComponent<UISprite>().spriteName = "editbuttom-exit";
                //editObj.GetComponent<UISprite>().MakePixelPerfect();
                break;
        }
    }
    public static void CloseEventUI()//關閉事件
    {
        EventChoiceButton.CloseUI();
    }
    public static void CloseGoUI()
    {
        //lastGoUILevel = curGoUILevel;        
        curGoUILevel = 0;
        goUI.SetActive(false);
        mapChoiceUI.SetActive(false);
        questChoiceUI.SetActive(false);
        questDataUI.SetActive(false);
        readyUI.SetActive(false);
        //editObj.SetActive(true);
    }
    public static void ClosePartyUI()
    {
        //lastPartyUILevel = curPartyUILevel;
        curPartyUILevel = 0;
        partyUI.SetActive(false);
        menuUI.SetActive(false);
        changeMemberListUI.SetActive(false);
        editMemberDataUI.SetActive(false);
        editMemberListUI.SetActive(false);
        formationUI.SetActive(false);
        skillListUI.SetActive(false);
        //viewMemberDataObj.SetActive(false);
    }
}
