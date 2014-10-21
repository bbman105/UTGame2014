using UnityEngine;
using System.Collections;

public class ExploreGettor : AchievementGettor
{
    public override void GetAchievement()
    {

        for (int i = 0; i < ForceCondition.CharaStatusDic["NPCMonster"].Length; i++)//判斷死亡怪物中有無隱藏BOSS，若有則達成探索成就
        {
            if (ForceCondition.CharaStatusDic["NPCMonster"][i].NpcType == "HideBoss")
                if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))
                    Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[1] = true;//設定此任務的探索成就達成
                else
                    Player.QuestAchievementDic.Add(ForceCondition.QuestID, new PlayerOwnQuestAchievement(2, new bool[4]));//設定此任務的懸賞成就達成
        }
    }
}
