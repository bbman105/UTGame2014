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
                if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))//如果任務成就字典尚未有此任務的成就
                {
                    if (!Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[4])//如果任務成就字典中此任務的懸賞成就尚未解鎖
                    {
                        Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 4);//設定此任務的懸賞成就達成
                    }
                }
                else
                {
                    Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 4);//設定此任務的懸賞成就達成
                }
            }

        }
    }
}
