using UnityEngine;
using System.Collections;

public class HuttingGettor : AchievementGettor
{

    public override void GetAchievement()
    {

        for (int i = 0; i < ForceCondition.CharaStatusDic["NPCMonster"].Length; i++)//判斷死亡怪物中有無隱藏BOSS，若有且死亡則達成懸賞成就
        {
            if (ForceCondition.CharaStatusDic["NPCMonster"][i].NpcType == "HideBoss" && !ForceCondition.CharaStatusDic["NPCMonster"][i].Alive)
            {
                if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))
                    Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[3] = true;//設定此任務的懸賞成就達成
                else
                    Player.QuestAchievementDic.Add(ForceCondition.QuestID, new PlayerOwnQuestAchievement(4, new bool[4]));//設定此任務的懸賞成就達成
            }

        }
    }
}
