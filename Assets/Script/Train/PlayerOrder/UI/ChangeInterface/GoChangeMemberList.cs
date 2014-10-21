using UnityEngine;
using System.Collections;

public class GoChangeMemberList : MonoBehaviour
{
    //編輯中的隊伍ID
    public int ChangeTeamID { get; set; }
    //要被替換的怪物在隊伍中的位置
    public int ChangeMemberRank { get; set; }
    void OnClick()
    {
        if (CtrlPower.GetCanCtrl(3))
        {
            CtrlPower.StartCtrl(3);
            RoomCtrl.curPartyUILevel = 2;
            //設定目前要改變的隊伍及位置
            Player.ChangeMemberRank = ChangeMemberRank;
            Player.ChangeTeamID = ChangeTeamID;
            //OpenGoUI();
            RoomCtrl.PartyUICtrl();
            //讀取擁有的怪物圖像
            ChangeMemberList.LoadMonsterSprite();
        }
    }
}
