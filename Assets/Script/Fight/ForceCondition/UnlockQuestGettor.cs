using UnityEngine;
using System.Collections;

public class UnlockQuestGettor
{

    public void UnlockQuest()
    {
        if (ForceCondition.Result == 1)//如果勝利
        {
            if (ForceCondition.QuestLevel == GameDictionary.UnlockQuestDic[ForceCondition.PlanetID].MapQuestLevel[ForceCondition.MapID])//此關卡是此玩家在這個地圖中最新的關卡
                if (GameDictionary.UnlockQuestDic[ForceCondition.PlanetID].MapQuestLevel[ForceCondition.MapID] <
                    GameDictionary.MapDic[int.Parse(ForceCondition.PlanetID.ToString() + ForceCondition.MapID.ToString())].QuestID.Length)//在這地圖中，還有下一關卡可以解鎖
                {
                    if (GameDictionary.MapDic[int.Parse(ForceCondition.PlanetID.ToString() + ForceCondition.MapID.ToString())].QuestID[ForceCondition.QuestLevel]!=0)//如果下一關卡的任務ID不是0
                    {
                        GameDictionary.UnlockQuestDic[ForceCondition.PlanetID].MapQuestLevel[ForceCondition.MapID]++; 
                        Debug.Log("解鎖新任務");
                    }                   
                }
        }
    }
}
