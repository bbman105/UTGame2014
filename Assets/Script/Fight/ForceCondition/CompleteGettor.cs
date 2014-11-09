using UnityEngine;
using System.Collections;

public class CompleteGettor : AchievementGettor
{
    public override void GetAchievement()
    {
        if (ForceCondition.Result == 1)//如果勝利
        {
            if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))//如果任務成就字典已有此任務的成就
            {
                if (!Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[1])//如果任務成就字典中此任務的完成成就尚未解鎖
                {
                    Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 1);//設定此任務的完成成就達成
                }
            }
            else//如果任務成就字典尚未有此任務的成就
            {
                Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 1);//設定此任務的完成成就達成
            }
        }
    }

}
