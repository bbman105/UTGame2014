using UnityEngine;
using System.Collections;

public class RankSGettor : AchievementGettor
{
    public override void GetAchievement()
    {
        if (ForceCondition.Rank == 1)//如果取得S級評價
        {
            if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))//如果任務成就字典已有此任務的成就
            {
                if (!Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[3])//如果任務成就字典中此任務的評價成就尚未解鎖
                {
                    Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 3);//設定此任務的評價成就達成
                }
            }
            else//如果任務成就字典尚未有此任務的成就
            {
                Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 3);//設定此任務的評價成就達成
            }
        }
    }

}
