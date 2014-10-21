using UnityEngine;
using System.Collections;

public class GoQuest : MonoBehaviour
{

    private int mapID;
    public int MapID
    {
        get { return mapID; }
        set { mapID = value; }
    }
    public int PlanetID { get; set; }

    void OnClick()
    {

        if (CtrlPower.GetCanCtrl(2))
        {
            if (RoomCtrl.curGoUILevel == 1)//從地圖接到任務
            {
                if (MapID != 0)//代表為解鎖
                {
                    ForceCondition.MapID = MapID;//設置戰鬥場景
                    ForceCondition.PlanetID = this.PlanetID;//設置戰鬥場景的星球ID
                    QuestUI.CurMapID = MapID;
                    QuestUI.CurMapPage = MapUI.CurPlanetRank;
                    //讀取被選取的地圖任務
                    QuestUI.LoadQuestData();
                    RoomCtrl.curGoUILevel = 2;
                    //OpenGoUI();
                    RoomCtrl.GoUICtrl();
                }
            }
            else//從隊伍選擇或任務細節退回任務
            {
                //讀取被選取的地圖任務
                QuestUI.LoadQuestData();
                RoomCtrl.curGoUILevel = 2;
                //OpenGoUI();
                RoomCtrl.GoUICtrl();
            }
        }
    }

}
