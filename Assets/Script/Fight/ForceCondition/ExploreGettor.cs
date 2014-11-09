using UnityEngine;
using System.Collections;

public class ExploreGettor : AchievementGettor
{
    public override void GetAchievement()
    {

        for (int i = 0; i < ForceCondition.CharaStatusDic["NPCMonster"].Length; i++)//判斷死亡怪物中有無隱藏BOSS，若有則達成探索成就
        {
            Debug.Log(ForceCondition.CharaStatusDic["NPCMonster"][i].NpcType);
            if (ForceCondition.CharaStatusDic["NPCMonster"][i].NpcType == "HideBoss")
            {
                if (Player.QuestAchievementDic.ContainsKey(ForceCondition.QuestID))//如果任務成就字典已有此任務的成就
                {
                    if (!Player.QuestAchievementDic[ForceCondition.QuestID].Achieve[2])//如果任務成就字典中此任務的探索成就尚未解鎖
                    {
                        Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 2);//設定此任務的探索成就達成
                    }
                }
                else//如果任務成就字典尚未有此任務的成就
                {
                    Player.UnlockNewQuestAchievement(ForceCondition.QuestID, 2);//設定此任務的探索成就達成
                }
            }
        }
    }
}
