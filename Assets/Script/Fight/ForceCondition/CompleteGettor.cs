using UnityEngine;
using System.Collections;

public class CompleteGettor : AchievementGettor
{
    public override void GetAchievement()
    {
        if (ForceCondition.Result == 1)//如果勝利
            if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))
                Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[0] = true;//設定此任務的完成成就達成
            else
                Player.QuestAchievementDic.Add(ForceCondition.QuestID, new PlayerOwnQuestAchievement(1, new bool[4]));//設定此任務的懸賞成就達成
    }

}
