using UnityEngine;
using System.Collections;

public class RankSGettor : AchievementGettor
{
    public override void GetAchievement()
    {
        if (ForceCondition.Rank == 1)//如果取得S級評價
            if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))
                Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[2] = true;//設定此任務的SRank成就達成
            else
                Player.QuestAchievementDic.Add(ForceCondition.QuestID, new PlayerOwnQuestAchievement(3, new bool[4]));//設定此任務的懸賞成就達成
    }

}
