using UnityEngine;
using System.Collections;

public class UnlockQuestGettor
{

    public void UnlockQuest()
    {
        if (ForceCondition.Result == 1)//如果勝利
        {
            if (ForceCondition.QuestLevel == Player.UnlockQuestDic[ForceCondition.PlanetID].MapQuestLevel[ForceCondition.MapID])//此關卡是此玩家在這個地圖中最新的關卡
                if (ForceCondition.QuestLevel <
                    GameDictionary.MapDic[int.Parse(ForceCondition.PlanetID.ToString() + ForceCondition.MapID.ToString())].QuestNum)//在這地圖中，還有下一關卡可以解鎖
                {
                    Player.UnlockNewQuest(ForceCondition.PlanetID, ForceCondition.MapID, ForceCondition.QuestLevel);//解鎖新任務
                }
                else if (ForceCondition.MapID < GameDictionary.MapDic[int.Parse(ForceCondition.PlanetID.ToString() + ForceCondition.MapID.ToString())].MapNum)//如果此地圖不是此星球最後一張地圖
                {
                    Player.UnlockNewMap(ForceCondition.PlanetID, ForceCondition.MapID);//解鎖新地圖
                }
                else if (ForceCondition.PlanetID < GameDictionary.MapDic[int.Parse(ForceCondition.PlanetID.ToString() + ForceCondition.MapID.ToString())].PlanetNum)//如果此星球不是最後一顆星球
                {
                    //UnlockNewPlanet();//解鎖新星球
                }
        }
    }
}
